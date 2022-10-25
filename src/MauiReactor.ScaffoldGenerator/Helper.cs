using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;

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
        var symbolDisplayFormat = new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

        return typeSymbol.ToDisplayString(symbolDisplayFormat);
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

}
