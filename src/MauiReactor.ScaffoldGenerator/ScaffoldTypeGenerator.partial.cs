using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MauiReactor.ScaffoldGenerator;

public partial class ScaffoldTypeGenerator
{
    private readonly (INamedTypeSymbol TypeSymbol, string ChildPropertyName)[] _childrenTypes;

    public ScaffoldTypeGenerator(
        Compilation compilation, 
        INamedTypeSymbol generatorType, 
        INamedTypeSymbol typeToScaffold,
        (INamedTypeSymbol TypeSymbol, string ChildPropertyName)[] childrenTypes,
        bool implementItemTemplateFlag,
        string? baseTypeNamespace)
    {
        var declaringTypeFullName = typeToScaffold.GetFullyQualifiedName();
        var bindablePropertyType = compilation.FindNamedType("Microsoft.Maui.Controls.BindableProperty");
        var editorBrowsableAttribute = compilation.FindNamedType("System.ComponentModel.EditorBrowsableAttribute");
        _childrenTypes = childrenTypes;

        var propertiesMap = typeToScaffold.GetMembers()
            .Where(_ => _.Kind == SymbolKind.Property)
            .Cast<IPropertySymbol>()
            .Where(_ => !_.IsReadOnly && !_.IsWriteOnly)
            .Where(_ => (_.ContainingType is INamedTypeSymbol namedTypeSymbol) && namedTypeSymbol.GetFullyQualifiedName() == typeToScaffold.GetFullyQualifiedName())
            //.Where(_ => !((INamedTypeSymbol)_.Type).IsGenericType)
            .GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);


        Properties = typeToScaffold.GetMembers()
            .Where(_ => _.Kind == SymbolKind.Field)
            .Cast<IFieldSymbol>()
            .Where(_ => _.Type.Equals(bindablePropertyType, SymbolEqualityComparer.Default))
            .Select(_ => _.Name.Substring(0, _.Name.Length - "Property".Length))
            .Where(_ => propertiesMap.ContainsKey(_))
            .Select(_ => propertiesMap[_])

            .Where(_ => _.Type.GetFullyQualifiedName() != "Microsoft.Maui.Controls.View")
            .Where(_ => _.Type.GetFullyQualifiedName() != "Microsoft.Maui.Controls.ColumnDefinitionCollection")
            .Where(_ => _.Type.GetFullyQualifiedName() != "Microsoft.Maui.Controls.RowDefinitionCollection")
            .Where(_ => !(declaringTypeFullName == "Microsoft.Maui.Controls.Shell" && _.Name == "CurrentItem"))
            .Where(_ => !(declaringTypeFullName == "Microsoft.Maui.Controls.ShellItem" && _.Name == "CurrentItem"))
            .Where(_ => !(declaringTypeFullName == "Microsoft.Maui.Controls.ShellSection" && _.Name == "CurrentItem"))
            .Where(_ => _.Type.GetFullyQualifiedName() != "Microsoft.Maui.Controls.Shapes.Geometry")
            .Where(_ => _.Type.GetFullyQualifiedName() != "Microsoft.Maui.Controls.Shadow")
            .Where(_ => _.Type.GetFullyQualifiedName() != "Microsoft.Maui.Graphics.IShape")
            .Where(_ => _.Name != "Content")
            .Where(_ => !_.Name.Contains("Command"))
            .Where(_ => _.Type.GetFullyQualifiedName() != "Microsoft.Maui.Controls.DataTemplate")
            .Where(_ => _.Type.GetFullyQualifiedName() != "Microsoft.Maui.Controls.DataTemplate?")
            .Where(_ => _.Name != "ItemsSource")
            .Where(_ => _.Type.GetFullyQualifiedName() != "Microsoft.Maui.Controls.IItemsLayout")
            .Where(_ => _.Type.GetFullyQualifiedName() != "Microsoft.Maui.Controls.LinearItemsLayout")
            .Where(_ => !(declaringTypeFullName == "Microsoft.Maui.Controls.StructuredItemsView" && (_.Name == "Header" || _.Name == "Footer")))
            .Where(_ => !(declaringTypeFullName == "Microsoft.Maui.Controls.Shell" && (_.Name == "FlyoutHeader" || _.Name == "FlyoutFooter" || _.Name == "FlyoutContent")))
            .Where(_ => !(declaringTypeFullName == "Microsoft.Maui.Controls.Picker" && _.Name == "SelectedItem"))
            .Where(_ => !(declaringTypeFullName == "Microsoft.Maui.Controls.MenuItem" && _.Name == "IsEnabled"))
            .Where(_ => !(declaringTypeFullName == "Microsoft.Maui.Controls.ListView" && (_.Name == "Header" || _.Name == "Footer")))

            .OrderBy(_=>_.Name)
            .ToArray();

        Events = typeToScaffold.GetMembers()
            .Where(_ => _.Kind == SymbolKind.Event)
            .Cast<IEventSymbol>()
            .Where(_ => _.Type.Name != "Func")
            .Where(_ => !_.Name.Contains('.'))
            .Where(_ => (_.ContainingType is INamedTypeSymbol namedTypeSymbol) && namedTypeSymbol.GetFullyQualifiedName() == typeToScaffold.GetFullyQualifiedName())
            .Where(_ => !_.GetAttributes().Any(_ => _.AttributeClass.EnsureNotNull().Equals(editorBrowsableAttribute, SymbolEqualityComparer.Default)))
            .GroupBy(_ => _.Name, StringComparer.OrdinalIgnoreCase)
            .Select(_ => _.First())
            .OrderBy(_ => _.Name)
            .ToArray();


        Namespace = generatorType.ContainingNamespace.GetFullyQualifiedName();
        TypeName = typeToScaffold.Name.Replace("`1", string.Empty);
        FullTypeName = typeToScaffold.GetFullyQualifiedName().Replace('+', '.').Replace("`1", string.Empty);
        IsGenericType = typeToScaffold.IsGenericType;
        TypeToScaffold = typeToScaffold;
        GeneratorType = generatorType;
        BaseTypeNamespace = baseTypeNamespace;

        TypeofDouble = compilation.FindNamedType("System.Double").EnsureNotNull();
        TypeofFloat = compilation.FindNamedType("System.Single").EnsureNotNull();
        TypeofRect = compilation.FindNamedType("Microsoft.Maui.Graphics.Rect").EnsureNotNull();
        TypeofThickness = compilation.FindNamedType("Microsoft.Maui.Thickness").EnsureNotNull();
        TypeofThicknessF = compilation.FindNamedType("MauiReactor.ThicknessF").EnsureNotNull();
        TypeofPoint = compilation.FindNamedType("Microsoft.Maui.Graphics.Point").EnsureNotNull();
        TypeofPointF = compilation.FindNamedType("Microsoft.Maui.Graphics.PointF").EnsureNotNull();
        TypeofCornerRadius = compilation.FindNamedType("Microsoft.Maui.CornerRadius").EnsureNotNull();
        TypeofCornerRadiusF = compilation.FindNamedType("MauiReactor.CornerRadiusF").EnsureNotNull();
        TypeofVector2 = compilation.FindNamedType("System.Numerics.Vector2").EnsureNotNull();
        TypeofSizeF = compilation.FindNamedType("Microsoft.Maui.Graphics.SizeF").EnsureNotNull();
        TypeofColor = compilation.FindNamedType("Microsoft.Maui.Graphics.Color").EnsureNotNull();
        
        AnimatableProperties = Properties
            .Where(_ =>
                _.Type.Equals(TypeofDouble, SymbolEqualityComparer.Default) ||
                _.Type.Equals(TypeofFloat, SymbolEqualityComparer.Default) ||
                _.Type.Equals(TypeofRect, SymbolEqualityComparer.Default) ||
                _.Type.Equals(TypeofThickness, SymbolEqualityComparer.Default) ||
                _.Type.Equals(TypeofThicknessF, SymbolEqualityComparer.Default) ||
                _.Type.Equals(TypeofPoint, SymbolEqualityComparer.Default) ||
                _.Type.Equals(TypeofPointF, SymbolEqualityComparer.Default) ||
                _.Type.Equals(TypeofCornerRadius, SymbolEqualityComparer.Default) ||
                _.Type.Equals(TypeofCornerRadiusF, SymbolEqualityComparer.Default) ||
                _.Type.Equals(TypeofVector2, SymbolEqualityComparer.Default) ||
                _.Type.Equals(TypeofSizeF, SymbolEqualityComparer.Default) ||
                _.Type.Equals(TypeofColor, SymbolEqualityComparer.Default))
            .ToArray();

        SupportItemTemplate = implementItemTemplateFlag && typeToScaffold.GetMembers().Count(_ => _.Name == "ItemsSource" || _.Name == "ItemTemplate") == 2;
        ItemSourceIsIList = SupportItemTemplate && typeToScaffold
            .GetMembers("ItemsSource")
            .Cast<IPropertySymbol>()
            .First()
            .Type
            .GetFullyQualifiedName() == "System.Collections.IList";
    }

    private IPropertySymbol[] Properties { get; }
    private IPropertySymbol[] AnimatableProperties { get; }
    public bool SupportItemTemplate { get; }
    public bool ItemSourceIsIList { get; }
    private IEventSymbol[] Events { get; }
    private string Namespace { get; }
    private string TypeName { get; }
    private string FullTypeName { get; }
    private bool IsGenericType { get; }
    private INamedTypeSymbol TypeToScaffold { get; }
    private INamedTypeSymbol GeneratorType { get; }

    public INamedTypeSymbol TypeofDouble { get; }
    public INamedTypeSymbol TypeofFloat { get; }
    public INamedTypeSymbol TypeofRect { get; }
    public INamedTypeSymbol TypeofThickness { get; }
    public INamedTypeSymbol TypeofThicknessF { get; }
    public INamedTypeSymbol TypeofPoint { get; }
    public INamedTypeSymbol TypeofPointF { get; }
    public INamedTypeSymbol TypeofCornerRadius { get; }
    public INamedTypeSymbol TypeofCornerRadiusF { get; }
    public INamedTypeSymbol TypeofVector2 { get; }
    public INamedTypeSymbol TypeofSizeF { get; }    
    public INamedTypeSymbol TypeofColor { get; }

    private string? BaseTypeNamespace { get; }
    public bool IsBaseGenericType =>
        TypeToScaffold.BaseType.EnsureNotNull().IsGenericType;

    private string InterfaceName
        => IsGenericType ? $"IGeneric{TypeName}" : $"I{TypeName}";

    private string GenericArgumentBaseFullTypeName
        => TypeToScaffold
        .TypeArguments[0]
        .BaseType
        .EnsureNotNull()
        .GetFullyQualifiedName()
        .Replace('+', '.');

    private string GenericArgumentBaseFullBaseTypeName
        => TypeToScaffold
        .BaseType
        .EnsureNotNull()
        .TypeArguments[0]
        .GetFullyQualifiedName()
        .Replace('+', '.');

    private string BaseTypeName()
    {
        return BaseTypeNamespace != null ? $"{BaseTypeNamespace}.{InternalBaseTypeName()}" : InternalBaseTypeName();
    }

    private string InternalBaseTypeName()
    {
        var baseType = TypeToScaffold.BaseType
            .EnsureNotNull();
        if (baseType.Name == "BindableObject")
        {
            return "VisualNode";
        }

        if (baseType.IsGenericType)
        {
            return baseType.Name
                .Replace("PlatformBehavior", "Behavior")
                .Replace("BaseBehavior", "Behavior")
                .Replace("`1", string.Empty);
        }

        var baseTypeFullName = baseType.GetFullyQualifiedName();

        if (baseTypeFullName.StartsWith("Microsoft.Maui.Controls."))
            return baseTypeFullName.Replace("Microsoft.Maui.Controls.", "MauiReactor.");

        return baseType.Name;
    }

    private string BaseInterfaceName()
    {
        var baseTypeName = BaseTypeName();
        return baseTypeName.Insert(baseTypeName.LastIndexOf('.') + 1, "I");
    }

    private bool IsTypeNotAbstractWithEmptyConstructor
        => !TypeToScaffold.IsAbstract && TypeToScaffold.Constructors.Any(_ => _.DeclaredAccessibility == Accessibility.Public && _.Parameters.Length == 0);

    private bool IsTypeSealed
        => TypeToScaffold.IsSealed;

    private string GetDelegateParametersDescriptor(IEventSymbol ev)
    {
        /*
    private void NativeControl_<#= ev.Name #>(object? sender, <#= genericArgs.Length > 0 ? genericArgs[0].Name : "EventArgs" #> e)
    {
        var thisAs<#= InterfaceName #> = (<#= InterfaceName #>)this;
        thisAs<#= InterfaceName #>.<#= ev.Name #>Action?.Invoke();
        thisAs<#= InterfaceName #>.<#= ev.Name #>ActionWithArgs?.Invoke(sender, e);
    }             
         */

        var invokeMember = (IMethodSymbol)ev.Type.GetMembers().First(_ => _.Name == "Invoke");
        if (invokeMember.Parameters.Length == 1)
        {
            return $"private void NativeControl_{ev.Name}(global::{invokeMember.Parameters[0].Type.GetFullyQualifiedName()} sender)";
        }

        return $"private void NativeControl_{ev.Name}({invokeMember.Parameters[0].Type.GetFullyQualifiedName()} sender, global::{invokeMember.Parameters[1].Type.GetFullyQualifiedName()} e)";
    }

    private string GetDelegateInvokeDescriptor(IEventSymbol ev)
    {
        /*
    private void NativeControl_<#= ev.Name #>(object? sender, <#= genericArgs.Length > 0 ? genericArgs[0].Name : "EventArgs" #> e)
    {
        var thisAs<#= InterfaceName #> = (<#= InterfaceName #>)this;
        thisAs<#= InterfaceName #>.<#= ev.Name #>Action?.Invoke();
        thisAs<#= InterfaceName #>.<#= ev.Name #>ActionWithArgs?.Invoke(sender, e);
    }             
         */

        var invokeMember = (IMethodSymbol)ev.Type.GetMembers().First(_ => _.Name == "Invoke");
        if (invokeMember.Parameters.Length == 1)
        {
            return $"thisAs{InterfaceName}.{ev.Name}ActionWithArgs?.Invoke(sender);";
        }

        return $"thisAs{InterfaceName}.{ev.Name}ActionWithArgs?.Invoke(sender, e);";
    }

    private string GetActionWithArgsParameters(IEventSymbol ev)
    {
        //object?, <#= genericArgs.Length > 0 ? genericArgs[0].GetFullyQualifiedName().ToResevedWordFullTypeName() : "EventArgs" #>
        var invokeMember = (IMethodSymbol)ev.Type.GetMembers().First(_ => _.Name == "Invoke");
        if (invokeMember.Parameters.Length == 1)
        {
            return $"global::{invokeMember.Parameters[0].Type.GetFullyQualifiedName()}";
        }

        return $"{invokeMember.Parameters[0].Type.GetFullyQualifiedName()}, global::{invokeMember.Parameters[1].Type.GetFullyQualifiedName()}";

    }

    public string TransformAndPrettify()
    {
        var tree = CSharpSyntaxTree.ParseText(TransformText());
        var root = tree.GetRoot().NormalizeWhitespace();
        var ret = root.ToFullString();
        return $"// <auto-generated />{Environment.NewLine}{ret}";
    }
}
