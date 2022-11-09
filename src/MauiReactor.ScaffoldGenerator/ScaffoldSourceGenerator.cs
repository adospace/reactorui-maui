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
        public ScaffoldAttribute(Type nativeControlType, bool implementItemTemplate = false) 
        {
            NativeControlType = nativeControlType;
            ImplementItemTemplate = implementItemTemplate;
        }

        public Type NativeControlType { get; }
        public bool ImplementItemTemplate { get; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ScaffoldChildrenAttribute : Attribute
    {
        public ScaffoldChildrenAttribute(Type childNativeControlType, string childrenPropertyName) 
        {
            ChildNativeControlType = childNativeControlType;
            ChildrenPropertyName = childrenPropertyName;
        }

        public Type ChildNativeControlType { get; }
        public string ChildrenPropertyName { get; }
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

            var scaffoldAttribute = generatorTypeSymbol.GetAttributes()
                .First(_ => _.AttributeClass.EnsureNotNull().Name == "ScaffoldAttribute" || _.AttributeClass.EnsureNotNull().Name == "Scaffold");

            if (scaffoldAttribute.ConstructorArguments.Length == 0)
            {
                throw new InvalidOperationException("Invalid ScaffoldAttribute arguments");
            }

            var typeMetadataName = ((ISymbol)scaffoldAttribute.ConstructorArguments[0].Value.EnsureNotNull())
                .GetFullyQualifiedName();

            var implementItemTemplateFlag = (bool)scaffoldAttribute.ConstructorArguments[1].Value.EnsureNotNull();

            var typeToScaffold = context.Compilation.FindNamedType(typeMetadataName).EnsureNotNull();

            var listOfChildTypes = new List<(INamedTypeSymbol TypeSymbol, string ChildPropertyName)>();

            var scaffoldChildAttributes = generatorTypeSymbol.GetAttributes()
                .Where(_ => _.AttributeClass.EnsureNotNull().Name == "ScaffoldChildrenAttribute" || _.AttributeClass.EnsureNotNull().Name == "ScaffoldChildren");

            foreach (var scaffoldChildAttribute in scaffoldChildAttributes)
            {
                if (scaffoldAttribute.ConstructorArguments.Length == 0)
                {
                    throw new InvalidOperationException("Invalid ScaffoldChildrenAttribute arguments");
                }

                var childTypeMetadataName = ((ISymbol)scaffoldChildAttribute.ConstructorArguments[0].Value.EnsureNotNull())
                    .GetFullyQualifiedName();

                var childrenProprtyName = scaffoldChildAttribute.ConstructorArguments[1].Value.EnsureNotNull().ToString();

                var childTypeToScaffold = context.Compilation.FindNamedType(childTypeMetadataName).EnsureNotNull();

                listOfChildTypes.Add((childTypeToScaffold, childrenProprtyName));
            }

            var scaffoldedType = new ScaffoldTypeGenerator(context.Compilation, generatorTypeSymbol, typeToScaffold, listOfChildTypes.ToArray(), implementItemTemplateFlag);

            var source = scaffoldedType.TransformAndPrettify();

            context.AddSource($"{classToScaffold}.g.cs", source);
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
