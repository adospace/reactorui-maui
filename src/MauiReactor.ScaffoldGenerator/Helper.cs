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
        //var symbolDisplayFormat = new SymbolDisplayFormat(
        //    typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

        //return typeSymbol.ToDisplayString(symbolDisplayFormat);
        return typeSymbol.ToString();
    }

    public static string? GetFullyQualifiedName(this ClassDeclarationSyntax classDeclarationSyntax)
    {
        if (!TryGetNamespace(classDeclarationSyntax, out string? namespaceName))
        {
            return null; // or whatever you want to do in this scenario
        }

        //var namespaceName = namespaceDeclarationSyntax!.Name.ToString();
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

        // Check if the class is generic
        if (namedTypeSymbol.IsGenericType)
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

}
