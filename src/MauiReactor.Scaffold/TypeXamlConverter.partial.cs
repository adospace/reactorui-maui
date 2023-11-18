using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Scaffold;

public partial class TypeXamlConverter
{
    private readonly Type _typeToScaffold;

    public TypeXamlConverter(Type typeToScaffold)
    {
        _typeToScaffold = typeToScaffold;

        var propertiesMap = _typeToScaffold.GetProperties()
            .Where(p => p.DeclaringType == _typeToScaffold)
            .Where(_ => !_.PropertyType.IsGenericType)
            .Distinct(new PropertyInfoEqualityComparer())
            .ToDictionary(_ => _.Name, _ => _);

        Properties = _typeToScaffold.GetFields()
            .Where(_ => _.FieldType == typeof(BindableProperty))
            .Select(_ => _.Name.Substring(0, _.Name.Length - "Property".Length))
            .Where(_ => propertiesMap.ContainsKey(_))
            .Select(_ => propertiesMap[_])
            .Where(_ => !_.PropertyType.IsGenericType)
            .Where(_ => (_.GetSetMethod()?.IsPublic).GetValueOrDefault())
            //Microsoft.Maui.Controls.LayoutOptions doesn't support ==
            //.Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.LayoutOptions")
            //Custom handling
            .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.ColumnDefinitionCollection")
            .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.RowDefinitionCollection")
            .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.ControlTemplate")
            .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.Shell" && _.Name == "CurrentItem"))
            .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.ShellItem" && _.Name == "CurrentItem"))
            .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.ShellSection" && _.Name == "CurrentItem"))
            .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.Shapes.Geometry")
            .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.Shadow")
            .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Graphics.IShape")
            .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.SwipeItems")
            .Where(_ => _.Name != "Content")
            .Where(_ => !_.Name.Contains("Command"))
            .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.DataTemplate")
            .Where(_ => _.Name != "ItemsSource")
            .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.IItemsLayout")
            .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.LinearItemsLayout")
            .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.ItemsView" && _.Name == "EmptyView"))
            .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.StructuredItemsView" && (_.Name == "Header" || _.Name == "Footer")))
            .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.Shell" && (_.Name == "FlyoutHeader" || _.Name == "FlyoutFooter" || _.Name == "FlyoutContent")))
            .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.Picker" && _.Name == "SelectedItem"))
            .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.MenuItem" && _.Name == "IsEnabled"))
            .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.ListView" && (_.Name == "Header" || _.Name == "Footer")))

            .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.Page")
        .ToArray();

        Events = _typeToScaffold.GetEvents(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Where(_ => _.GetCustomAttribute<EditorBrowsableAttribute>() == null)
            .Distinct(new EventInfoEqualityComparer())
            .ToArray();
    }

    public PropertyInfo[] Properties { get; }

    public EventInfo[] Events { get; }

    public string Namespace() => Validate.EnsureNotNull(_typeToScaffold.Namespace).Replace("Microsoft.Maui.Controls", "MauiReactor.XamlConverterTool");

    public string TypeName() => _typeToScaffold.Name.Replace("`1", string.Empty);

    public string BaseTypeName()
    {
        var baseType = Validate.EnsureNotNull(_typeToScaffold.BaseType);
        if (baseType.Name == "BindableObject")
        {
            return "VisualNode";
        }

        if (baseType.IsGenericType)
        {
            return Validate.EnsureNotNull(baseType.Name)
                .Replace("PlatformBehavior", "Behavior")
                .Replace("`1", string.Empty);
        }

        return Validate.EnsureNotNull(baseType.Name)
            .Replace("PlatformBehavior", "Behavior")
            .Replace("Microsoft.Maui.Controls.", string.Empty);
    }

    public string TransformAndPrettify()
    {
        var tree = CSharpSyntaxTree.ParseText(TransformText());
        var root = tree.GetRoot().NormalizeWhitespace();
        var ret = root.ToFullString();
        return $"// <auto-generated />{Environment.NewLine}{ret}";
    }

}
