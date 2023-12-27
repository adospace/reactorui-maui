using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
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
        context.RegisterForPostInitialization((i) => i.AddSource("ComponentAttributes.g.cs", @"using System;

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
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
        );

        Dictionary<string, GeneratorClassItem> generatingClassItems = new();

        void generateClassItem(FieldDeclarationSyntax fieldDeclaration, FieldAttributeType attributeType)
        {
            // Get the semantic model for the syntax tree that has your field.
            var semanticModel = context.Compilation.GetSemanticModel(fieldDeclaration.SyntaxTree);

            // Get the TypeSyntax from the FieldDeclarationSyntax.
            TypeSyntax typeSyntax = fieldDeclaration.Declaration.Type;

            // Check if it is a nullable value type.
            bool isNullableValueType = typeSyntax is NullableTypeSyntax;

            // Get the type symbol using the semantic model.
            var fieldTypeSymbol = semanticModel.GetTypeInfo(typeSyntax).Type;
            
            if (fieldTypeSymbol == null)
            {
                return;
            }

            // Start with the fully qualified name of the type.
            string fieldTypeFullyQualifiedName = fieldTypeSymbol.ToDisplayString(symbolDisplayFormat);

            // Append "?" if it's a nullable value type or if the nullable annotation is set for reference types.
            if ((isNullableValueType || fieldTypeSymbol.NullableAnnotation == NullableAnnotation.Annotated) &&
                !fieldTypeFullyQualifiedName.EndsWith("?"))
            {
                fieldTypeFullyQualifiedName += "?";
            }

            if (fieldDeclaration.Declaration.Variables.Count != 1)
            {
                return;
            }

            var typeDeclarationSyntax = fieldDeclaration.Ancestors()
                                .OfType<TypeDeclarationSyntax>()
                                .FirstOrDefault();

            if (typeDeclarationSyntax == null)
            {
                return;
            }

            // Get the type symbol for the containing type.
            var classTypeSymbol = semanticModel.GetDeclaredSymbol(typeDeclarationSyntax);

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

            foreach (var variableFieldName in fieldDeclaration.Declaration.Variables.Select(_=>_.Identifier.ValueText))
            {
                if (generatingClassItem.FieldItems.ContainsKey(variableFieldName))
                {
                    return;
                }

                generatingClassItem.FieldItems[variableFieldName]
                    = new GeneratorFieldItem(variableFieldName, fieldTypeFullyQualifiedName, attributeType);
            }
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

