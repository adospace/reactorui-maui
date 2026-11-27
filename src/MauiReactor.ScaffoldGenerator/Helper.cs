using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MauiReactor.ScaffoldGenerator;

static class Helper
{
    public static INamedTypeSymbol? FindNamedType(this Compilation compilation, string typeMetadataName)
    {
        var typeToScaffold = compilation.GetTypeByMetadataName(typeMetadataName);

        typeToScaffold ??= compilation.References
            .Select(compilation.GetAssemblyOrModuleSymbol)
            .OfType<IAssemblySymbol>()
            .Select(assemblySymbol => assemblySymbol.GetTypeByMetadataName(typeMetadataName))
            .FirstOrDefault(_ => _ != null);

        return typeToScaffold;
    }

    public static string GetFullyQualifiedName(this ISymbol typeSymbol)
    {
        return typeSymbol.ToString();
    }

    public static string? GetFullyQualifiedName(this ClassDeclarationSyntax classDeclarationSyntax)
    {
        if (!TryGetNamespace(classDeclarationSyntax, out string? namespaceName))
        {
            return null; // or whatever you want to do in this scenario
        }

        return namespaceName + "." + classDeclarationSyntax.Identifier.ToString();
    }

    public static bool TryGetNamespace(SyntaxNode? syntaxNode, out string? result)
    {
        // set defaults
        result = null;

        if (syntaxNode == null)
        {
            return false;
        }

        try
        {
            syntaxNode = syntaxNode.Parent;

            if (syntaxNode == null)
            {
                return false;
            }

            if (syntaxNode is NamespaceDeclarationSyntax namespaceDeclarationSyntax)
            {
                result = namespaceDeclarationSyntax.Name.ToString();
                return true;
            }

            if (syntaxNode is FileScopedNamespaceDeclarationSyntax fileScopedNamespaceDeclarationSyntax)
            {
                result = fileScopedNamespaceDeclarationSyntax.Name.ToString();
                return true;
            }

            return TryGetNamespace(syntaxNode, out result);
        }
        catch
        {
            return false;
        }
    }

    public static T EnsureNotNull<T>(this T? value)
        => value ?? throw new InvalidOperationException();

    public static string GetFullyQualifiedTypeName(this INamedTypeSymbol namedTypeSymbol)
    {
        // Get the name of the class
        string className = namedTypeSymbol.Name;

        // A type nested inside a generic type reports IsGenericType, but declares no type
        // parameters of its own; only the ones it actually declares belong on its name.
        if (namedTypeSymbol.TypeParameters.Length > 0)
        {
            // Construct the generic type parameter list (e.g., "<T>")
            string typeParameters = string.Join(", ", namedTypeSymbol.TypeParameters.Select(p => p.Name));
            return $"{className}<{typeParameters}>";
        }
        else
        {
            // If it's not a generic type, just return the class name
            return className;
        }
    }

    /// <summary>
    /// Partial declarations that re-open every type containing
    /// <paramref name="namedTypeSymbol"/>, outermost first.
    /// A component declared inside another type must have its generated half declared inside
    /// that same type too: emitted at namespace level it becomes a different, unrelated class,
    /// so the fields the generated code assigns are not the ones the user declared.
    /// </summary>
    public static IReadOnlyList<string> GetContainingTypeDeclarations(this INamedTypeSymbol namedTypeSymbol)
    {
        var declarations = new List<string>();

        for (var containingType = namedTypeSymbol.ContainingType; containingType is not null; containingType = containingType.ContainingType)
        {
            declarations.Insert(0, containingType.GetPartialDeclaration());
        }

        return declarations;
    }

    private static string GetPartialDeclaration(this INamedTypeSymbol namedTypeSymbol)
    {
        // Only the kind has to match: accessibility, static/abstract/sealed and constraints
        // may differ between partial declarations of the same type.
        var kind = namedTypeSymbol.TypeKind switch
        {
            TypeKind.Interface => "interface",
            TypeKind.Struct when namedTypeSymbol.IsRecord => "record struct",
            TypeKind.Struct => "struct",
            _ when namedTypeSymbol.IsRecord => "record",
            _ => "class",
        };

        var typeParameters = namedTypeSymbol.TypeParameters.Length == 0
            ? string.Empty
            : $"<{string.Join(", ", namedTypeSymbol.TypeParameters.Select(_ => _.Name))}>";

        return $"partial {kind} {namedTypeSymbol.Name}{typeParameters}";
    }

    public static bool IsNewModifierUsed(this IFieldSymbol fieldSymbol)
    {
        var containingType = fieldSymbol.ContainingType;
        if (containingType.BaseType == null)
        {
            // No base type, so no member can be hidden
            return false;
        }

        var baseTypeMembers = containingType.BaseType.GetMembers(fieldSymbol.Name);
        foreach (var member in baseTypeMembers)
        {
            if (member.Kind == SymbolKind.Field)
            {
                var baseField = (IFieldSymbol)member;
                if (baseField.Type.Equals(fieldSymbol.Type, SymbolEqualityComparer.Default))
                {
                    // Found a field with the same name and type in the base type, so `new` is used.
                    return true;
                }
            }
        }

        // No matching field found in the base type
        return false;
    }
    public static bool IsNewModifierUsed(this IPropertySymbol propertySymbol)
    {
        var containingType = propertySymbol.ContainingType;
        if (containingType.BaseType == null)
        {
            // No base type, so no member can be hidden
            return false;
        }

        INamedTypeSymbol? currentBaseType = containingType.BaseType;
        while (currentBaseType != null)
        {
            // This loop traverses up the inheritance chain, which might be necessary
            // for correctly assessing hiding across multiple levels of inheritance
            foreach (var member in currentBaseType.GetMembers(propertySymbol.Name))
            {
                if (member.Kind == SymbolKind.Property)
                {
                    var baseProperty = (IPropertySymbol)member;
                    // You could refine this comparison based on your requirements
                    // For a more accurate match, you might want to compare parameters for indexers, etc.
                    if (SymbolEqualityComparer.Default.Equals(baseProperty.Type, propertySymbol.Type))
                    {
                        // Found a property with the same name and type in the base type, so `new` is used.
                        return true;
                    }
                }
            }
            currentBaseType = currentBaseType.BaseType;
        }

        // No matching property found in the base type or any ancestor type
        return false;
    }

    public static bool IsDerivedFrom(this INamedTypeSymbol type, INamedTypeSymbol baseType)
    {
        INamedTypeSymbol? currentBaseType = type.BaseType;
        while (currentBaseType != null)
        {
            if (currentBaseType.Equals(baseType, SymbolEqualityComparer.Default))
            {
                return true;
            }
            currentBaseType = currentBaseType.BaseType;
        }
        return false;
    }

    public static bool IsTypeKeyword(string typeName)
    {
        switch (typeName)
        {
            case "bool":
            case "byte":
            case "sbyte":
            case "char":
            case "decimal":
            case "double":
            case "float":
            case "int":
            case "uint":
            case "long":
            case "ulong":
            case "object":
            case "short":
            case "ushort":
            case "string":
                return true;
            default:
                return false;
        }
    }
}
