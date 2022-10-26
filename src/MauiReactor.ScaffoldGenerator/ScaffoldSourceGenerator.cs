using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;


namespace MauiReactor.ScaffoldGenerator;

[Generator]
public class ScaffoldSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForPostInitialization((i) => i.AddSource("ScaffoldAttribute.g.cs", @"using System;

namespace MauiReactor
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ScaffoldAttribute : Attribute
    {
        public ScaffoldAttribute(Type nativeControlType) 
        {
            NativeControlType = nativeControlType;
        }

        public Type NativeControlType { get; }
    }
}
"));


        context.RegisterForSyntaxNotifications(() => new ScaffoldSyntaxReceiver());
    }


    public void Execute(GeneratorExecutionContext context)
    {
        foreach (var classToScaffold in ((ScaffoldSyntaxReceiver)context.SyntaxReceiver.EnsureNotNull()).ClassesToScaffold)
        {
            INamedTypeSymbol generatorTypeSymbol = context.Compilation.GetTypeByMetadataName(classToScaffold).EnsureNotNull();

            var scaffoldAttribute = generatorTypeSymbol.GetAttributes().First(_ => _.AttributeClass.EnsureNotNull().Name == "ScaffoldAttribute" || _.AttributeClass.EnsureNotNull().Name == "Scaffold");

            var typeMetadataName = ((ISymbol)scaffoldAttribute.ConstructorArguments[0].Value.EnsureNotNull())
                .GetFullyQualifiedName();

            var typeToScaffold = context.Compilation.FindNamedType(typeMetadataName).EnsureNotNull();

            var scaffoldedType = new ScaffoldTypeGenerator(context.Compilation, generatorTypeSymbol, typeToScaffold);

            var source = scaffoldedType.TransformAndPrettify();

            context.AddSource($"{classToScaffold}.g.cs", source);
            ;
        }   
    }
}

class ScaffoldSyntaxReceiver : ISyntaxReceiver
{
    public List<string> ClassesToScaffold { get; } = new();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is ClassDeclarationSyntax cds)
        {
            var scaffoldAttribute = cds.AttributeLists
                .Where(_ => _.Attributes.Any(attr => attr.Name is IdentifierNameSyntax nameSyntax && (nameSyntax.Identifier.Text == "Scaffold" ||
                    nameSyntax.Identifier.Text == "ScaffoldAttribute")))
                .Select(_ => _.Attributes.First())
                .FirstOrDefault();

            if (scaffoldAttribute != null)
            {
                ClassesToScaffold.Add(cds.GetFullyQualifiedName().EnsureNotNull());
            }
        }
    }
}
