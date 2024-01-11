﻿using MauiReactor.ScaffoldGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

class TestScaffoldGenerator
{
    [Test]
    public void TestPropAttributeGenerators()
    {
        // Create the 'input' compilation that the generator will act on
        Compilation inputCompilation = CreateCompilation("""
            using MauiReactor;

            namespace MyCode;
            
            partial class MyComponent
            {
                [Prop]
                int _myProp;

                [Prop]
                int? _myProp2;
            
                [Prop]
                Action _myProp3;

                [Prop]
                Action? _myProp4;
            
                [Prop]
                VisualNode _myProp5;
            
                [Prop]
                VisualNode? _myProp6;
            
                [Prop]
                Action<int> _myProp7;
            
                [Prop]
                Action<int>? _myProp8;
            
                [Prop]
                Action<int?> _myProp9;
            
                [Prop]
                Action<int?>? _myProp10;

                [Prop("MyTestMethod")]
                int _myProp11;
            }            
            """);

        // directly create an instance of the generator
        // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
        var generator = new ComponentPartialClassSourceGenerator();

        // Create the driver that will control the generation, passing in our generator
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run the generation pass
        // (Note: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls)
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

        // We can now assert things about the resulting compilation:
        Debug.Assert(diagnostics.IsEmpty); // there were no diagnostics created by the generators
        Debug.Assert(outputCompilation.SyntaxTrees.Count() == 3); 
        //Debug.Assert(outputCompilation.GetDiagnostics().IsEmpty); // verify the compilation with the added source has no diagnostics

        // Or we can look at the results directly:
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        // The runResult contains the combined results of all generators passed to the driver
        Debug.Assert(runResult.GeneratedTrees.Length == 2);
        Debug.Assert(runResult.Diagnostics.IsEmpty);

        // Or you can access the individual results on a by-generator basis
        GeneratorRunResult generatorResult = runResult.Results[0];
        Debug.Assert(generatorResult.Generator == generator);
        Debug.Assert(generatorResult.Diagnostics.IsEmpty);
        Debug.Assert(generatorResult.GeneratedSources.Length == 2);
        Debug.Assert(generatorResult.Exception is null);


        generatorResult.GeneratedSources[1].SourceText.ToString().ShouldBe("""
            // <auto-generated />
            #nullable enable
            namespace MyCode
            {
                partial class MyComponent
                {
                    public MyComponent MyProp(int propValue)
                    {
                        _myProp = propValue;
                        return this;
                    }

                    public MyComponent MyProp10(Action<int?>? propValue)
                    {
                        _myProp10 = propValue;
                        return this;
                    }
            
                    public MyComponent MyTestMethod(int propValue)
                    {
                        _myProp11 = propValue;
                        return this;
                    }

                    public MyComponent MyProp2(int? propValue)
                    {
                        _myProp2 = propValue;
                        return this;
                    }

                    public MyComponent MyProp3(Action propValue)
                    {
                        _myProp3 = propValue;
                        return this;
                    }

                    public MyComponent MyProp4(Action? propValue)
                    {
                        _myProp4 = propValue;
                        return this;
                    }

                    public MyComponent MyProp5(VisualNode propValue)
                    {
                        _myProp5 = propValue;
                        return this;
                    }

                    public MyComponent MyProp6(VisualNode? propValue)
                    {
                        _myProp6 = propValue;
                        return this;
                    }

                    public MyComponent MyProp7(Action<int> propValue)
                    {
                        _myProp7 = propValue;
                        return this;
                    }

                    public MyComponent MyProp8(Action<int>? propValue)
                    {
                        _myProp8 = propValue;
                        return this;
                    }

                    public MyComponent MyProp9(Action<int?> propValue)
                    {
                        _myProp9 = propValue;
                        return this;
                    }
                }
            }
            """);

    }

    [Test]
    public void TestInjectAttributeGenerators()
    {
        // Create the 'input' compilation that the generator will act on
        Compilation inputCompilation = CreateCompilation("""
            using MauiReactor;

            namespace MyCode
            {
                partial class MyComponent
                {
                    [Inject]
                    VisualNode _myProp;

                    [Inject]
                    VisualNode? _myProp2;
                }
            }
            """);

        // directly create an instance of the generator
        // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
        var generator = new ComponentPartialClassSourceGenerator();

        // Create the driver that will control the generation, passing in our generator
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run the generation pass
        // (Note: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls)
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

        // We can now assert things about the resulting compilation:
        Debug.Assert(diagnostics.IsEmpty); // there were no diagnostics created by the generators
        Debug.Assert(outputCompilation.SyntaxTrees.Count() == 3);
        //Debug.Assert(outputCompilation.GetDiagnostics().IsEmpty); // verify the compilation with the added source has no diagnostics

        // Or we can look at the results directly:
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        // The runResult contains the combined results of all generators passed to the driver
        Debug.Assert(runResult.GeneratedTrees.Length == 2);
        Debug.Assert(runResult.Diagnostics.IsEmpty);

        // Or you can access the individual results on a by-generator basis
        GeneratorRunResult generatorResult = runResult.Results[0];
        Debug.Assert(generatorResult.Generator == generator);
        Debug.Assert(generatorResult.Diagnostics.IsEmpty);
        Debug.Assert(generatorResult.GeneratedSources.Length == 2);
        Debug.Assert(generatorResult.Exception is null);


        generatorResult.GeneratedSources[1].SourceText.ToString().ShouldBe("""
            // <auto-generated />
            using Microsoft.Extensions.DependencyInjection;

            #nullable enable
            namespace MyCode
            {
                partial class MyComponent
                {
                    public MyComponent()
                    {
                        _myProp = Services.GetRequiredService<VisualNode>();
                        _myProp2 = Services.GetService<VisualNode>();
                    }
                }
            }
            """);

    }

    [Test]
    public void TestParameterAttributeGenerators()
    {
        // Create the 'input' compilation that the generator will act on
        Compilation inputCompilation = CreateCompilation("""
            using MauiReactor;

            namespace MyCode
            {
                partial class MyComponent
                {
                    [Param]
                    IParameter<MyParameterType> _myParameter;
                }
            }
            """);

        // directly create an instance of the generator
        // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
        var generator = new ComponentPartialClassSourceGenerator();

        // Create the driver that will control the generation, passing in our generator
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run the generation pass
        // (Note: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls)
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

        // We can now assert things about the resulting compilation:
        Debug.Assert(diagnostics.IsEmpty); // there were no diagnostics created by the generators
        Debug.Assert(outputCompilation.SyntaxTrees.Count() == 3);
        //Debug.Assert(outputCompilation.GetDiagnostics().IsEmpty); // verify the compilation with the added source has no diagnostics

        // Or we can look at the results directly:
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        // The runResult contains the combined results of all generators passed to the driver
        Debug.Assert(runResult.GeneratedTrees.Length == 2);
        Debug.Assert(runResult.Diagnostics.IsEmpty);

        // Or you can access the individual results on a by-generator basis
        GeneratorRunResult generatorResult = runResult.Results[0];
        Debug.Assert(generatorResult.Generator == generator);
        Debug.Assert(generatorResult.Diagnostics.IsEmpty);
        Debug.Assert(generatorResult.GeneratedSources.Length == 2);
        Debug.Assert(generatorResult.Exception is null);


        generatorResult.GeneratedSources[1].SourceText.ToString().ShouldBe("""
            // <auto-generated />
            #nullable enable
            namespace MyCode
            {
                partial class MyComponent
                {
                    public MyComponent()
                    {
                        _myParameter = GetOrCreateParameter<MyParameterType>();
                    }
                }
            }
            """);

    }


    private static Compilation CreateCompilation(string source)
        => CSharpCompilation.Create("compilation",
            new[] { CSharpSyntaxTree.ParseText(source) },
            new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
            new CSharpCompilationOptions(OutputKind.ConsoleApplication));
}

