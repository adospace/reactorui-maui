using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MauiReactor.XamlConverterTool;


public class MauiReactorCodeBuilder(XDocument xamlDocument)
{
    private readonly XDocument _xamlDocument = xamlDocument;

    public string GenerateCode()
    {
        var documentNode = new MauiCodeComponentNode(_xamlDocument);

        var generatedCode = documentNode.GenerateCode();

        return Prettify(generatedCode);
    }

    public string Prettify(string sourceCode)
    {
        var allLines = sourceCode.Split(Environment.NewLine).ToArray();

        StringBuilder buffer = new();
        var margin = 0;

        for (int i = 0; i < allLines.Length; i++)
        {
            var line = allLines[i].Trim();

            if (line.StartsWith('{'))
            {
                buffer.AppendLine(new string(' ', margin) + line);
                margin += 4;
            }
            else if (line.StartsWith("//"))
            {
                buffer.AppendLine(new string(' ', margin + 4) + line);
            }
            else if (line.EndsWith('}'))
            {
                margin -= 4;
                buffer.AppendLine(new string(' ', margin) + line);
            }
            else
            {
                buffer.AppendLine(new string(' ', margin) + line);
            }
        }

        return buffer.ToString();
    }
}

public abstract class MauiCodeNode
{
    private List<MauiCodeVisualNode>? _children;

    public IReadOnlyList<MauiCodeVisualNode> Children
    {
        get
        {
            _children ??= GetChildren().ToList();

            return _children;
        }
    }

    public abstract IEnumerable<MauiCodeVisualNode> GetChildren();

    public abstract string GenerateCode();
}

public class MauiCodeComponentNode : MauiCodeNode
{
    private readonly XDocument _xamlDocument;
    private readonly MauiCodeVisualNode _root;

    public MauiCodeComponentNode(XDocument xamlDocument)
    {
        _xamlDocument = xamlDocument;
        _root = MauiCodeVisualNode.Create(_xamlDocument.Root ?? throw new InvalidOperationException());
    }

    public override IEnumerable<MauiCodeVisualNode> GetChildren()
    {
        return new[] { _root };
    }

    public override string GenerateCode()
    {
        if (Children.Count == 0 || _xamlDocument.Root == null) 
        {
            return "error-no-component-found";
        }

        return $$"""
            class {{(_root.Class ?? _root.Name ?? _xamlDocument.Root.Name.LocalName).Replace('.', '_')}}Component : Component
            {
                public override VisualNode Render()
                {
                    return {{_root.GenerateCode()}};
                }
            }
            """;
    }
}

public abstract class MauiCodeVisualNode(XElement xamlElement) : MauiCodeNode
{
    protected readonly XElement _xamlElement = xamlElement;
    private readonly Dictionary<string, (string FullTypeName, bool IsEnum)> _properties = new();

    internal static MauiCodeVisualNode Create(XElement element)
    {
        //http://schemas.microsoft.com/dotnet/2021/maui
        var typeName = $"MauiReactor.XamlConverterTool.MauiCode{element.Name.LocalName}";
        return (MauiCodeVisualNode?)Assembly.GetExecutingAssembly().CreateInstance(typeName, false, BindingFlags.Default, null, new[] { element }, null, null) ?? 
            new MauiNotFoundNode(element);
    }

    public string? Name => _xamlElement.Attributes().FirstOrDefault(_ => _.Name.LocalName == "Name")?.Value;
    public string? Class => _xamlElement.Attributes().FirstOrDefault(_ => _.Name.LocalName == "Class")?.Value;
    public string? Key => _xamlElement.Attributes().FirstOrDefault(_ => _.Name.LocalName == "Key")?.Value;

    protected void RegisterProperty(string propertyName, string propertyType, bool isEnum = false)
    {
        _properties.Add(propertyName, (propertyType, isEnum));
    }

    protected virtual string? GenerateCodeForProperty(string propertyName, string propertyValue)
    {
        bool bindingMarkup = propertyValue.Trim().StartsWith("{");
        if (bindingMarkup)
        {
            return $"//{propertyName}={propertyValue}";
        }


        if (_properties.TryGetValue(propertyName, out (string FullTypeName, bool IsEnum) property))
        {
            if (property.FullTypeName == "string" ||
                property.FullTypeName == "Microsoft.Maui.Controls.ImageSource")
            {
                return $"//.{propertyName}(\"{propertyValue.Replace('\r', ' ').Replace('\n', ' ')}\")";
            }
            else if (property.FullTypeName.StartsWith("Microsoft.Maui.Controls."))
            {
                return $"//.{propertyName}({property.FullTypeName.Replace("Microsoft.Maui.Controls.", string.Empty)}.{propertyValue})";
            }
            else if (property.FullTypeName.StartsWith("Microsoft.Maui.") && property.IsEnum)
            {
                return $"//.{propertyName}({property.FullTypeName}.{propertyValue})";
            }

            return $"//.{propertyName}({propertyValue})";
        }

        bool dependencyProperty = propertyName.Contains('.');

        if (dependencyProperty)
        {
            return $"//.{propertyName.Replace(".", string.Empty)}({propertyValue})";
        }

        return $"//{propertyName}={propertyValue}";
    }

    protected virtual string GeneratePropertiesCode()
    {
        return string.Join(Environment.NewLine, _xamlElement
            .Attributes()
            .Select(_ => GenerateCodeForProperty(_.Name.LocalName, _.Value))
            .Where(_ => _ != null));
    }

    public override string GenerateCode()
    {
        if (Children.Count == 0)
        {
            return $$"""
            new {{_xamlElement.Name.LocalName}}()
            {{GeneratePropertiesCode()}}
            """;
        }
        
        return $$"""
            new {{_xamlElement.Name.LocalName}}
            {
                {{string.Join($",{Environment.NewLine}", Children.Select(_=>_.GenerateCode()))}}
            }
            {{GeneratePropertiesCode()}}
            """;
    }

    public override IEnumerable<MauiCodeVisualNode> GetChildren()
    {
        return _xamlElement.Elements().Select(Create) ?? [];
    }

}


public class MauiNotFoundNode : MauiCodeVisualNode
{
    public MauiNotFoundNode(XElement xamlElement) : base(xamlElement)
    {
    }

    public override string GenerateCode()
    {
        if (Children.Count == 0)
        {
            return $$"""
            //new {{_xamlElement.Name}}()
            {{GeneratePropertiesCode()}}
            """;
        }

        return $$"""
            //new {{_xamlElement.Name}}()
            {
                {{string.Join($",{Environment.NewLine}", Children.Select(_ => _.GenerateCode()))}}
            }
            {{GeneratePropertiesCode()}}
            """;
    }
}