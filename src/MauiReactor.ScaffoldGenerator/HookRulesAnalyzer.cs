using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MauiReactor.ScaffoldGenerator;

/// <summary>
/// Enforces the rules of hooks: a hook cell table is positional, so every render has to
/// call the same hooks in the same order. Without this the failure shows up at runtime as
/// a HookOrderException, reported through the unhandled-exception event, far from the edit
/// that caused it.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class HookRulesAnalyzer : DiagnosticAnalyzer
{
    private const string Category = "MauiReactor.Hooks";

    internal static readonly DiagnosticDescriptor OutsideRender = new(
        "MRH001",
        "Hook called outside Render",
        "Hook '{0}' must be called from Render() or from another Use* method, not from '{1}'",
        Category,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Hooks are matched to their state by call order during a render, so they cannot be called from event handlers, lifecycle methods or constructors.");

    internal static readonly DiagnosticDescriptor InConditional = new(
        "MRH002",
        "Hook called conditionally",
        "Hook '{0}' is called conditionally; hooks must run on every render in the same order",
        Category,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A hook that is skipped on some renders shifts every hook after it onto the wrong cell.");

    internal static readonly DiagnosticDescriptor InLoop = new(
        "MRH003",
        "Hook called in a loop",
        "Hook '{0}' is called inside a loop; hooks must run a fixed number of times per render",
        Category,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A loop makes the number of hook calls depend on data, so cells no longer line up between renders.");

    internal static readonly DiagnosticDescriptor InNestedFunction = new(
        "MRH004",
        "Hook called inside a nested function",
        "Hook '{0}' is called inside a lambda or local function; hooks must be called directly in the render body",
        Category,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A nested function runs at a different time than the render that declared it, so the cursor is no longer where the hook expects.");

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(OutsideRender, InConditional, InLoop, InNestedFunction);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        context.RegisterCompilationStartAction(start =>
        {
            var hookComponent = start.Compilation.GetTypeByMetadataName("MauiReactor.HookComponent");

            if (hookComponent is null)
            {
                return;
            }

            start.RegisterSyntaxNodeAction(
                nodeContext => Analyze(nodeContext, hookComponent),
                SyntaxKind.InvocationExpression);
        });
    }

    private static void Analyze(SyntaxNodeAnalysisContext context, INamedTypeSymbol hookComponent)
    {
        var invocation = (InvocationExpressionSyntax)context.Node;

        if (context.SemanticModel.GetSymbolInfo(invocation, context.CancellationToken).Symbol is not IMethodSymbol method ||
            !IsHook(method, hookComponent))
        {
            return;
        }

        var name = method.Name;
        var span = invocation.Span;
        var location = invocation.GetLocation();

        foreach (var node in invocation.Ancestors())
        {
            switch (node)
            {
                case SimpleLambdaExpressionSyntax:
                case ParenthesizedLambdaExpressionSyntax:
                case AnonymousMethodExpressionSyntax:
                case LocalFunctionStatementSyntax:
                    Report(context, InNestedFunction, location, name);
                    return;

                case ForStatementSyntax:
                case ForEachStatementSyntax:
                case ForEachVariableStatementSyntax:
                case WhileStatementSyntax:
                case DoStatementSyntax:
                    Report(context, InLoop, location, name);
                    return;

                // A hook in the tested expression itself always runs, so only the branches
                // are conditional.
                case IfStatementSyntax ifStatement when !ifStatement.Condition.Span.Contains(span):
                    Report(context, InConditional, location, name);
                    return;

                case ConditionalExpressionSyntax conditional when !conditional.Condition.Span.Contains(span):
                    Report(context, InConditional, location, name);
                    return;

                case SwitchStatementSyntax switchStatement when !switchStatement.Expression.Span.Contains(span):
                    Report(context, InConditional, location, name);
                    return;

                case SwitchExpressionSyntax switchExpression when !switchExpression.GoverningExpression.Span.Contains(span):
                    Report(context, InConditional, location, name);
                    return;

                // The left operand of &&, || and ?? always evaluates; the right one does not.
                case BinaryExpressionSyntax binary when IsShortCircuiting(binary) && !binary.Left.Span.Contains(span):
                    Report(context, InConditional, location, name);
                    return;

                case MethodDeclarationSyntax methodDeclaration:
                    var host = methodDeclaration.Identifier.ValueText;

                    if (host != "Render" && !host.StartsWith("Use", StringComparison.Ordinal))
                    {
                        Report(context, OutsideRender, location, name, host);
                    }

                    return;

                case ConstructorDeclarationSyntax constructor:
                    Report(context, OutsideRender, location, name, constructor.Identifier.ValueText);
                    return;

                case PropertyDeclarationSyntax property:
                    Report(context, OutsideRender, location, name, property.Identifier.ValueText);
                    return;
            }
        }
    }

    private static bool IsShortCircuiting(BinaryExpressionSyntax binary)
        => binary.IsKind(SyntaxKind.LogicalAndExpression)
            || binary.IsKind(SyntaxKind.LogicalOrExpression)
            || binary.IsKind(SyntaxKind.CoalesceExpression);

    private static bool IsHook(IMethodSymbol method, INamedTypeSymbol hookComponent)
    {
        if (!method.Name.StartsWith("Use", StringComparison.Ordinal))
        {
            return false;
        }

        for (var type = method.ContainingType; type is not null; type = type.BaseType)
        {
            if (SymbolEqualityComparer.Default.Equals(type, hookComponent))
            {
                return true;
            }
        }

        return false;
    }

    private static void Report(SyntaxNodeAnalysisContext context, DiagnosticDescriptor descriptor, Location location, params object[] args)
        => context.ReportDiagnostic(Diagnostic.Create(descriptor, location, args));
}
