using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Immutable;
using System.Linq;


namespace MauiReactor.ScaffoldGenerator;

[Generator]
public class ScaffoldSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register the ScaffoldAttribute source
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource("ScaffoldAttribute.g.cs", @"using System;

namespace MauiReactor
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class ScaffoldAttribute : Attribute
    {
        public ScaffoldAttribute(Type nativeControlType, bool implementItemTemplate = false, string baseTypeNamespace = null) 
        {
            NativeControlType = nativeControlType;
            ImplementItemTemplate = implementItemTemplate;
            BaseTypeNamespace = baseTypeNamespace;
        }

        public ScaffoldAttribute(string nativeControlTypeName, bool implementItemTemplate = false, string baseTypeNamespace = null) 
        {
            NativeControlTypeName = nativeControlTypeName;
            ImplementItemTemplate = implementItemTemplate;
            BaseTypeNamespace = baseTypeNamespace;
        }

        public Type NativeControlType { get; }
        public bool ImplementItemTemplate { get; }
        public string NativeControlTypeName { get; }
        public string BaseTypeNamespace { get; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal class ScaffoldChildrenAttribute : Attribute
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

        // Create a provider for classes with ScaffoldAttribute
        var classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
            .Where(static m => m is not null);

        // Combine with compilation
        var compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

        // Register source output
        context.RegisterSourceOutput(compilationAndClasses,
            static (spc, source) => Execute(source.Left, source.Right, spc));
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node)
    {
        return node is ClassDeclarationSyntax classDeclaration
            && classDeclaration.AttributeLists.Count > 0;
    }

    static string? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        var classDeclaration = (ClassDeclarationSyntax)context.Node;
        
        var hasScaffoldAttribute = classDeclaration.AttributeLists
            .SelectMany(attrList => attrList.Attributes)
            .Any(attr => attr.Name is IdentifierNameSyntax nameSyntax && 
                (nameSyntax.Identifier.Text == "Scaffold" || nameSyntax.Identifier.Text == "ScaffoldAttribute"));

        if (hasScaffoldAttribute)
        {
            return classDeclaration.GetFullyQualifiedName();
        }

        return null;
    }

    static void Execute(Compilation compilation, ImmutableArray<string?> classesToScaffold, SourceProductionContext context)
    {
        foreach (var classToScaffold in classesToScaffold.Where(x => x is not null))
        {
            if (classToScaffold is null) continue;

            var generatorTypeSymbol = compilation.GetTypeByMetadataName(classToScaffold);
            if (generatorTypeSymbol is null) continue;

            var scaffoldAttribute = generatorTypeSymbol.GetAttributes()
                .FirstOrDefault(attr => attr.AttributeClass?.Name == "ScaffoldAttribute" || 
                                       attr.AttributeClass?.Name == "Scaffold");

            if (scaffoldAttribute?.ConstructorArguments.Length == 0)
            {
                continue; // Skip instead of throwing
            }

            if (scaffoldAttribute is null) continue;

            var firstArgument = scaffoldAttribute.ConstructorArguments[0].Value;
            if (firstArgument is null) continue;

            var typeMetadataName = firstArgument is ISymbol symbol ? 
                symbol.GetFullyQualifiedName() : firstArgument.ToString();

            if (string.IsNullOrEmpty(typeMetadataName)) continue;

            var implementItemTemplateFlag = scaffoldAttribute.ConstructorArguments.Length > 1 ? 
                (bool)(scaffoldAttribute.ConstructorArguments[1].Value ?? false) : false;

            var baseTypeNamespace = scaffoldAttribute.ConstructorArguments.Length > 2 ? 
                (string?)scaffoldAttribute.ConstructorArguments[2].Value : null;

            var typeToScaffold = compilation.FindNamedType(typeMetadataName);
            if (typeToScaffold is null) continue;

            var listOfChildTypes = new System.Collections.Generic.List<(INamedTypeSymbol TypeSymbol, string ChildPropertyName)>();

            var scaffoldChildAttributes = generatorTypeSymbol.GetAttributes()
                .Where(attr => attr.AttributeClass?.Name == "ScaffoldChildrenAttribute" || 
                              attr.AttributeClass?.Name == "ScaffoldChildren");

            foreach (var scaffoldChildAttribute in scaffoldChildAttributes)
            {
                if (scaffoldChildAttribute.ConstructorArguments.Length < 2) continue;

                var childTypeArg = scaffoldChildAttribute.ConstructorArguments[0].Value;
                if (childTypeArg is not ISymbol childSymbol) continue;

                var childTypeMetadataName = childSymbol.GetFullyQualifiedName();
                var childrenPropertyName = scaffoldChildAttribute.ConstructorArguments[1].Value?.ToString();

                if (string.IsNullOrEmpty(childrenPropertyName)) continue;

                var childTypeToScaffold = compilation.FindNamedType(childTypeMetadataName);
                if (childTypeToScaffold is not null && childrenPropertyName is not null)
                {
                    listOfChildTypes.Add((childTypeToScaffold, childrenPropertyName));
                }
            }

            try
            {
                var scaffoldedType = new ScaffoldTypeGenerator(compilation, generatorTypeSymbol, typeToScaffold, 
                    listOfChildTypes.ToArray(), implementItemTemplateFlag, baseTypeNamespace);

                var source = scaffoldedType.TransformAndPrettify();

                context.AddSource($"{classToScaffold}.g.cs", source);
            }
            catch (Exception ex)
            {
                // Report diagnostic instead of throwing
                context.ReportDiagnostic(Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "SG0001",
                        "Scaffold generation failed",
                        "Failed to generate scaffold for {0}: {1}",
                        "ScaffoldGenerator",
                        DiagnosticSeverity.Error,
                        isEnabledByDefault: true),
                    Location.None,
                    classToScaffold,
                    ex.Message));
            }
        }
    }
}
