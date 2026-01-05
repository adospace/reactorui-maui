using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace MauiReactor.ScaffoldGenerator;

[Generator]
public class ComponentPartialClassSourceGenerator : IIncrementalGenerator
{

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource("ComponentAttributes.g.cs", @"using System;

#nullable enable
namespace MauiReactor
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class ComponentAttribute : Attribute
    {
        public ComponentAttribute(string? generatingClassName = null) 
        {
        }
    }

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
        public PropAttribute(string? methodName = null) 
        {
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    internal class ParamAttribute : Attribute
    {
    }
}
"));

        // Create providers for different field types
        var injectFields = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsFieldWithAttribute(s, "Inject", "InjectAttribute"),
                transform: static (ctx, _) => ctx.Node as FieldDeclarationSyntax)
            .Where(static m => m is not null);

        var propFields = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsFieldWithAttribute(s, "Prop", "PropAttribute"),
                transform: static (ctx, _) => ctx.Node as FieldDeclarationSyntax)
            .Where(static m => m is not null);

        var paramFields = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsFieldWithAttribute(s, "Param", "ParamAttribute"),
                transform: static (ctx, _) => ctx.Node as FieldDeclarationSyntax)
            .Where(static m => m is not null);

        // Combine all field types
        var allFields = injectFields.Collect()
            .Combine(propFields.Collect())
            .Combine(paramFields.Collect())
            .Combine(context.CompilationProvider);

        context.RegisterSourceOutput(allFields,
            static (spc, source) => Execute(spc, source.Left.Left.Left, source.Left.Left.Right, source.Left.Right, source.Right));
    }

    static bool IsFieldWithAttribute(SyntaxNode node, string attrName, string fullAttrName)
    {
        if (node is not FieldDeclarationSyntax fds) return false;

        return fds.AttributeLists
            .SelectMany(al => al.Attributes)
            .Any(attr => attr.Name is IdentifierNameSyntax nameSyntax &&
                (nameSyntax.Identifier.Text == attrName || nameSyntax.Identifier.Text == fullAttrName));
    }

    static void Execute(SourceProductionContext context,
        ImmutableArray<FieldDeclarationSyntax?> injectFields,
        ImmutableArray<FieldDeclarationSyntax?> propFields,
        ImmutableArray<FieldDeclarationSyntax?> paramFields,
        Compilation compilation)
    {
        // Format it to get the fully qualified name (namespace + type name).
        SymbolDisplayFormat symbolDisplayFormat = new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
        );

        Dictionary<string, GeneratorClassItem> generatingClassItems = new();

        void generateClassItem(FieldDeclarationSyntax fieldDeclaration, FieldAttributeType attributeType)
        {
            // Get the semantic model for the syntax tree that has your field.
            var semanticModel = compilation.GetSemanticModel(fieldDeclaration.SyntaxTree);

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

            string fullyQualifiedTypeName = classTypeSymbol.ToDisplayString(symbolDisplayFormat);
            string namespaceName = classTypeSymbol.ContainingNamespace.ToDisplayString();
            string generatingClassName = classTypeSymbol.Name;

            // Find the ComponentAttribute by its name
            AttributeData? componentAttribute = classTypeSymbol
                .GetAttributes()
                    .FirstOrDefault(ad =>
                    ad.AttributeClass != null &&
                    ad.AttributeClass.Name == "ComponentAttribute");

            // If the attribute was found, try to get the constructor argument
            if (componentAttribute != null)
            {
                foreach (var arg in componentAttribute.ConstructorArguments)
                {
                    // Check if the argument corresponds to the "generatingClassName" parameter
                    // In case of string, you can compare by the type, or the parameter name if available
                    if (arg.Type != null && arg.Type.SpecialType == SpecialType.System_String)
                    {
                        if (arg.Value != null)
                        {
                            generatingClassName = (string)arg.Value;
                            break; // Exit loop once the argument is found
                        }
                    }
                }
            }

            if (!generatingClassItems.TryGetValue(fullyQualifiedTypeName, out var generatingClassItem))
            {
                generatingClassItems[fullyQualifiedTypeName] = generatingClassItem = 
                    new GeneratorClassItem(namespaceName, classTypeSymbol, generatingClassName);
            }

            foreach (var variableFieldSyntax in fieldDeclaration.Declaration.Variables)
            {
                var variableFieldName = variableFieldSyntax.Identifier.ValueText;

                if (generatingClassItem.FieldItems.ContainsKey(variableFieldName))
                {
                    return;
                }

                string? methodName = null;
                if (attributeType == FieldAttributeType.Prop)
                {
                    if (semanticModel.GetDeclaredSymbol(variableFieldSyntax) 
                        is IFieldSymbol variableDeclaratorFieldSymbol)
                    {
                        var propAttributeData = variableDeclaratorFieldSymbol.GetAttributes()
                            .FirstOrDefault(_ => _.AttributeClass?.Name == "PropAttribute" || _.AttributeClass?.Name == "Prop");

                        if (propAttributeData?.ConstructorArguments.Length > 0)
                        {
                            methodName = propAttributeData.ConstructorArguments[0].Value?.ToString();
                        }
                    }
                }
                else if (attributeType == FieldAttributeType.Parameter)
                {
                    if (semanticModel.GetDeclaredSymbol(variableFieldSyntax)
                        is IFieldSymbol variableDeclaratorFieldSymbol)
                    {
                        // Now get the type symbol of the field's type, which should be a named type symbol
                        if (variableDeclaratorFieldSymbol.Type is INamedTypeSymbol variableDeclaratorFieldTypeSymbol)
                        {
                            // Ensure the field's type is generic (which it should be if it's an IParameter<T>)
                            if (variableDeclaratorFieldTypeSymbol.IsGenericType)
                            {
                                // Get the type argument(s) for the generic type
                                ITypeSymbol genericTypeArgument = variableDeclaratorFieldTypeSymbol.TypeArguments[0];

                                // To get the fully qualified name, use ToDisplayString with the appropriate format
                                fieldTypeFullyQualifiedName = genericTypeArgument.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                            }
                        }

                    }
                }

                generatingClassItem.FieldItems[variableFieldName]
                    = new GeneratorFieldItem(variableFieldName, fieldTypeFullyQualifiedName, attributeType, methodName);
            }
        }

        foreach (var injectFieldToGenerate in injectFields.Where(f => f is not null))
        {
            if (injectFieldToGenerate is not null)
                generateClassItem(injectFieldToGenerate, FieldAttributeType.Inject);
        }

        foreach (var propFieldToGenerate in propFields.Where(f => f is not null))
        {
            if (propFieldToGenerate is not null)
                generateClassItem(propFieldToGenerate, FieldAttributeType.Prop);
        }

        foreach (var parameterFieldToGenerate in paramFields.Where(f => f is not null))
        {
            if (parameterFieldToGenerate is not null)
                generateClassItem(parameterFieldToGenerate, FieldAttributeType.Parameter);
        }

        foreach (var generatingClassItem in generatingClassItems.OrderBy(_=>_.Key)) 
        {
            try
            {
                var textGenerator = new ComponentPartialClassGenerator(generatingClassItem.Value);
                var source = textGenerator.TransformAndPrettify();
                context.AddSource($"{generatingClassItem.Value.Namespace}.{generatingClassItem.Value.GeneratingClassName}.g.cs", source);
            }
            catch (Exception ex)
            {
                // Report diagnostic instead of throwing
                context.ReportDiagnostic(Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "CG0001",
                        "Component generation failed",
                        "Failed to generate component for {0}: {1}",
                        "ComponentGenerator",
                        DiagnosticSeverity.Error,
                        isEnabledByDefault: true),
                    Location.None,
                    generatingClassItem.Value.GeneratingClassName,
                    ex.Message));
            }
        }   
    }
}

public class GeneratorClassItem
{
    public GeneratorClassItem(string @namespace, INamedTypeSymbol symbol, string generatingClassName)
    {
        Namespace = @namespace;
        ClassName = symbol.Name;
        FullyQualifiedClassName = symbol.GetFullyQualifiedTypeName();
        GeneratingClassName = generatingClassName;
    }

    public string Namespace { get; }
    public string ClassName { get; }
    public string FullyQualifiedClassName { get; }
    public Dictionary<string, GeneratorFieldItem> FieldItems { get; } = new();
    public string GeneratingClassName { get; }
}

public class GeneratorFieldItem
{
    private readonly string? _propMethodName;

    public GeneratorFieldItem(string fieldName, string fieldTypeFullyQualifiedName, FieldAttributeType type, string? propMethodName)
    {
        FieldName = fieldName;
        FieldTypeFullyQualifiedName = fieldTypeFullyQualifiedName;
        Type = type;
        _propMethodName = propMethodName;
    }

    public string FieldName { get; }

    public string FieldTypeFullyQualifiedName { get; }

    public FieldAttributeType Type { get; }

    public string GetPropMethodName()
    {
        if (_propMethodName != null)
        {
            return _propMethodName;
        }

        var fieldName = FieldName.TrimStart('_');
        fieldName = char.ToUpper(fieldName[0]) + fieldName.Substring(1);
        return fieldName;
    }
}

public enum FieldAttributeType
{
    Inject,

    Prop,

    Parameter
}

