using System;
using System.Collections.Immutable;
using System.Linq;
using MauiReactor;
using MauiReactor.ScaffoldGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Shouldly;

namespace UnitTests;

public class TestHookRulesAnalyzer
{
    private static string[] Analyze(string body)
    {
        var source = $$"""
            using System;
            using MauiReactor;

            namespace Probe;

            class ProbePage : HookComponent
            {
            {{body}}
            }
            """;

        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
            .Select(a => (MetadataReference)MetadataReference.CreateFromFile(a.Location));

        var compilation = CSharpCompilation.Create(
            "analyzerprobe",
            [CSharpSyntaxTree.ParseText(source)],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // Guard against the probe silently not resolving HookComponent, which would make
        // every test vacuously pass.
        compilation.GetTypeByMetadataName("MauiReactor.HookComponent").ShouldNotBeNull();

        return compilation
            .WithAnalyzers(ImmutableArray.Create<DiagnosticAnalyzer>(new HookRulesAnalyzer()))
            .GetAnalyzerDiagnosticsAsync()
            .GetAwaiter()
            .GetResult()
            .Select(d => d.Id)
            .OrderBy(id => id)
            .ToArray();
    }

    [Test]
    public void HooksDirectlyInRender_AreClean()
    {
        Analyze("""
                public override VisualNode Render()
                {
                    var (count, setCount) = UseState(0);
                    var doubled = UseMemo(() => count * 2, count);
                    return null!;
                }
            """).ShouldBeEmpty();
    }

    [Test]
    public void HookInsideAnIfBody_IsFlagged()
    {
        Analyze("""
                public bool Flag;

                public override VisualNode Render()
                {
                    if (Flag)
                    {
                        UseState(0);
                    }
                    return null!;
                }
            """).ShouldBe(["MRH002"]);
    }

    [Test]
    public void HookInsideALoop_IsFlagged()
    {
        Analyze("""
                public override VisualNode Render()
                {
                    foreach (var i in new[] { 1, 2, 3 })
                    {
                        UseState(i);
                    }
                    return null!;
                }
            """).ShouldBe(["MRH003"]);
    }

    [Test]
    public void HookInsideALambda_IsFlagged()
    {
        Analyze("""
                public override VisualNode Render()
                {
                    Action a = () => UseState(0);
                    a();
                    return null!;
                }
            """).ShouldBe(["MRH004"]);
    }

    [Test]
    public void HookInAnEventHandler_IsFlagged()
    {
        Analyze("""
                void OnSomethingHappened()
                {
                    UseState(0);
                }

                public override VisualNode Render() => null!;
            """).ShouldBe(["MRH001"]);
    }

    [Test]
    public void HookInsideACustomHook_IsClean()
    {
        Analyze("""
                (int, Action<int>) UseCounter()
                {
                    return UseState(0);
                }

                public override VisualNode Render()
                {
                    UseCounter();
                    return null!;
                }
            """).ShouldBeEmpty();
    }

    [Test]
    public void HookInAnIfCondition_IsNotFlagged()
    {
        // The condition always evaluates, so this is legal - flagging it would be a
        // false positive that teaches people to ignore the analyzer.
        Analyze("""
                public override VisualNode Render()
                {
                    if (UseState(0).Value > 0)
                    {
                        return null!;
                    }
                    return null!;
                }
            """).ShouldBeEmpty();
    }

    [Test]
    public void HookOnTheRightOfShortCircuit_IsFlagged()
    {
        Analyze("""
                public bool Flag;

                public override VisualNode Render()
                {
                    var ok = Flag && UseState(0).Value > 0;
                    return null!;
                }
            """).ShouldBe(["MRH002"]);
    }

    [Test]
    public void EffectWithALambdaBody_IsNotFlagged()
    {
        // The shape the samples use: UseEffect is called at the top level and its lambda
        // contains no hooks. This must stay clean or the analyzer is unusable.
        Analyze("""
                public bool Running;

                public override VisualNode Render()
                {
                    UseEffect(() =>
                    {
                        var timer = new System.Threading.CancellationTokenSource();
                        return () => timer.Cancel();
                    }, Running);
                    return null!;
                }
            """).ShouldBeEmpty();
    }
}
