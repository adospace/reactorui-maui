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
public class ComponentPartialClassSourceGenerator : ISourceGenerator
{

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForPostInitialization((i) => i.AddSource("ServiceInjection.g.cs", @"using System;

namespace MauiReactor
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class InjectAttribute : Attribute
    {
        public InjectAttribute() 
        {
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    internal class PropAttribute : Attribute
    {
        public PropAttribute() 
        {
        }
    }
}
"));

        context.RegisterForSyntaxNotifications(() => new ComponentPartialClassSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // Format it to get the fully qualified name (namespace + type name).
        SymbolDisplayFormat qualifiedFormat = new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces
        );
        SymbolDisplayFormat symbolDisplayFormat = new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
        );

        Dictionary<string, GeneratorClassItem> generatingClassItems = new();

        void generateClassItem(FieldDeclarationSyntax fieldDeclaration, FieldAttributeType attributeType)
        {
            // Get the semantic model for the syntax tree that has your field.
            var semanticModel = context.Compilation.GetSemanticModel(fieldDeclaration.SyntaxTree);

            // Get the TypeSyntax from the FieldDeclarationSyntax.
            TypeSyntax typeSyntax = fieldDeclaration.Declaration.Type;

            // Get the type symbol using the semantic model.
            ITypeSymbol? fieldTypeSymbol = semanticModel.GetTypeInfo(typeSyntax).Type;

            if (fieldTypeSymbol == null)
            {
                return;
            }

            string fieldTypeFullyQualifiedName = fieldTypeSymbol.ToDisplayString(symbolDisplayFormat) +
                  (fieldTypeSymbol.NullableAnnotation == NullableAnnotation.Annotated ? "?" : string.Empty);

            if (fieldDeclaration.Declaration.Variables.Count != 1)
            {
                return;
            }

            string variableFieldName = fieldDeclaration.Declaration.Variables[0].Identifier.ValueText;

            var typeDeclarationSyntax = fieldDeclaration.Ancestors()
                .OfType<TypeDeclarationSyntax>()
                .FirstOrDefault();

            if (typeDeclarationSyntax == null)
            {
                return;
            }

            // Get the type symbol for the containing type.
            var classTypeSymbol = semanticModel.GetDeclaredSymbol(typeDeclarationSyntax) as INamedTypeSymbol;

            if (classTypeSymbol == null)
            {
                return;
            }

            string fullyQualifiedTypeName = classTypeSymbol.ToDisplayString(qualifiedFormat);
            string namespaceName = classTypeSymbol.ContainingNamespace.ToDisplayString();
            string className = classTypeSymbol.Name;

            if (!generatingClassItems.TryGetValue(fullyQualifiedTypeName, out var generatingClassItem))
            {
                generatingClassItems[fullyQualifiedTypeName] = generatingClassItem = new GeneratorClassItem(namespaceName, className);
            }

            if (generatingClassItem.FieldItems.ContainsKey(variableFieldName))
            {
                return;
            }

            generatingClassItem.FieldItems[variableFieldName]
                = new GeneratorFieldItem(variableFieldName, fieldTypeFullyQualifiedName, attributeType);            
        }

        foreach (var injectFieldToGenerate in ((ComponentPartialClassSyntaxReceiver)context.SyntaxReceiver.EnsureNotNull()).InjectFieldsToGenerate)
        {
            generateClassItem(injectFieldToGenerate, FieldAttributeType.Inject);
        }

        foreach (var propFieldToGenerate in ((ComponentPartialClassSyntaxReceiver)context.SyntaxReceiver.EnsureNotNull()).PropFieldsToGenerate)
        {
            generateClassItem(propFieldToGenerate, FieldAttributeType.Prop);
        }

        foreach (var generatingClassItem in generatingClassItems.OrderBy(_=>_.Key)) 
        {
            var textGenerator = new ComponentPartialClassGenerator(generatingClassItem.Value);

            var source = textGenerator.TransformAndPrettify();

            context.AddSource($"{generatingClassItem.Value.ClassName}.g.cs", source);
        }   
    }
}

class ComponentPartialClassSyntaxReceiver : ISyntaxReceiver
{
    public List<FieldDeclarationSyntax> InjectFieldsToGenerate = new();
    public List<FieldDeclarationSyntax> PropFieldsToGenerate = new();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is FieldDeclarationSyntax fds)
        {
            var injectAttribute = fds.AttributeLists
                .Where(_ => _.Attributes.Any(attr => attr.Name is IdentifierNameSyntax nameSyntax && (nameSyntax.Identifier.Text == "Inject" ||
                    nameSyntax.Identifier.Text == "InjectAttribute")))
                .Select(_ => _.Attributes.First())
                .FirstOrDefault();

            if (injectAttribute != null)
            {
                InjectFieldsToGenerate.Add(fds);
            }

            var propAttribute = fds.AttributeLists
                .Where(_ => _.Attributes.Any(attr => attr.Name is IdentifierNameSyntax nameSyntax && (nameSyntax.Identifier.Text == "Prop" ||
                    nameSyntax.Identifier.Text == "PropAttribute")))
                .Select(_ => _.Attributes.First())
                .FirstOrDefault();

            if (propAttribute != null)
            {
                PropFieldsToGenerate.Add(fds);
            }
        }
    }
}

public class GeneratorClassItem
{
    public GeneratorClassItem(string @namespace, string className)
    {
        Namespace = @namespace;
        ClassName = className;
    }

    public string Namespace { get; }
    public string ClassName { get; }

    public Dictionary<string, GeneratorFieldItem> FieldItems { get; } = new();
}

public class GeneratorFieldItem
{
    public GeneratorFieldItem(string fieldName, string fieldTypeFullyQualifiedName, FieldAttributeType type)
    {
        FieldName = fieldName;
        FieldTypeFullyQualifiedName = fieldTypeFullyQualifiedName;
        Type = type;
    }

    public string FieldName { get; }
    public string FieldTypeFullyQualifiedName { get; }

    public FieldAttributeType Type { get; }
}

public enum FieldAttributeType
{
    Inject,

    Prop
}

