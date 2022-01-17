using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MauiReactor.Scaffold
{
    public partial class TypeGenerator
    {
        private readonly Type _typeToScaffold;

        public TypeGenerator(Type typeToScaffold)
        {
            _typeToScaffold = typeToScaffold;

            var propertiesMap = _typeToScaffold.GetProperties()
                .Where(_ => !_.PropertyType.IsGenericType)
                .Distinct(new PropertyInfoEqualityComparer())
                .ToDictionary(_ => _.Name, _ => _);

            Properties = _typeToScaffold.GetFields()
                .Where(_ => _.FieldType == typeof(BindableProperty))
                .Select(_ => _.Name.Substring(0, _.Name.Length - "Property".Length))
                .Where(_ => propertiesMap.ContainsKey(_))
                .Select(_ => propertiesMap[_])
                .Where(_ => !_.PropertyType.IsGenericType)
                //Microsoft.Maui.Controls.LayoutOptions doesn't support ==
                .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.LayoutOptions")
                //Custom handling
                .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.ColumnDefinitionCollection")
                .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.RowDefinitionCollection")
                .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.Shell" && _.Name == "CurrentItem"))
                .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.ShellItem" && _.Name == "CurrentItem"))
                .Where(_ => !(_typeToScaffold.FullName == "Microsoft.Maui.Controls.ShellSection" && _.Name == "CurrentItem"))
                .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Controls.Shapes.Geometry")
                .Where(_ => _.PropertyType.FullName != "Microsoft.Maui.Graphics.IShape")
                .Where(_ => _.Name != "Content")
                .Where(_ => !_.Name.Contains("Command"))

                .Where(_ => (_.GetSetMethod()?.IsPublic).GetValueOrDefault())
                .ToArray();

            Events = _typeToScaffold.GetEvents(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(_ => _.GetCustomAttribute<EditorBrowsableAttribute>() == null)
                .Distinct(new EventInfoEqualityComparer())
                .ToArray();
        }

        public string Namespace() => Validate.EnsureNotNull(_typeToScaffold.Namespace).Replace("Microsoft.Maui.Controls", "MauiReactor");

        public string TypeName() => _typeToScaffold.Name;

        public string FullTypeName() => Validate.EnsureNotNull(_typeToScaffold.FullName)
            .Replace('+', '.');

        public string BaseTypeName()
        {
            var baseType = Validate.EnsureNotNull(_typeToScaffold.BaseType);
            if (baseType.Name == "BindableObject")
            {
                return "VisualNode";
            }

            return Validate.EnsureNotNull(baseType.FullName)
                .Replace("Microsoft.Maui.Controls.", string.Empty);
        }

        public string BaseInterfaceName()
        {
            var baseTypeName = BaseTypeName();
            return baseTypeName.Insert(baseTypeName.LastIndexOf('.') + 1, "I");
        }

        public bool IsTypeNotAbstractWithEmptyConstructur() 
            => !_typeToScaffold.IsAbstract && _typeToScaffold.GetConstructor(Array.Empty<Type>()) != null;

        public bool IsTypeSealed()
            => _typeToScaffold.IsSealed;

        public PropertyInfo[] Properties { get; }

        public EventInfo[] Events { get; }

        public string TransformAndPrettify()
        {
            var tree = CSharpSyntaxTree.ParseText(TransformText());

            var workspace = new AdhocWorkspace();
            workspace.AddSolution(
                      SolutionInfo.Create(SolutionId.CreateNewId("formatter"),
                      VersionStamp.Default)
            );

            var formatter = Formatter.Format(tree.GetCompilationUnitRoot(), workspace);
            return formatter.ToString();
        }

    }



}
