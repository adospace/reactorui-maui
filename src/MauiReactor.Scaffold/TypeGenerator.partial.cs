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
                //.Where(_ => _.GetCustomAttribute<ObsoleteAttribute>() == null)
                .Select(_ => _.Name.Substring(0, _.Name.Length - "Property".Length))
                .Where(_ => propertiesMap.ContainsKey(_))
                .Select(_ => propertiesMap[_])
                //.Where(_ => _.GetCustomAttribute<ObsoleteAttribute>() == null)
                .Where(_ => !_.PropertyType.IsGenericType)
                .Where(_ => (_.GetSetMethod()?.IsPublic).GetValueOrDefault())
                .ToArray();

            Events = _typeToScaffold.GetEvents(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(_ => _.GetCustomAttribute<EditorBrowsableAttribute>() == null)
                .Distinct(new EventInfoEqualityComparer())
                .ToArray();
        }

        public string TypeName() => _typeToScaffold.Name;

        public string FullTypeName() => _typeToScaffold.FullName ?? throw new InvalidOperationException();

        public string BaseTypeName() => (_typeToScaffold.BaseType ?? throw new InvalidOperationException()).Name == "BindableObject" ? "VisualNode" : $"{_typeToScaffold.BaseType.Name}";

        public bool IsTypeNotAbstractWithEmptyConstructur() => !_typeToScaffold.IsAbstract && _typeToScaffold.GetConstructor(Array.Empty<Type>()) != null;

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
