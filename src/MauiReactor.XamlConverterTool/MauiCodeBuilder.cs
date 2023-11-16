using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.XamlConverterTool;

//public class MauiCodeBuilder
//{
//    readonly MauiCodeNode _root;

//    internal MauiCodeBuilder()
//    {
//        _root = new MauiCodeNode
//    }


//}

public abstract class MauiCodeNode
{
    readonly List<MauiCodeNode> _children = [];

    public IReadOnlyList<MauiCodeNode> Children => _children;

    public void AppendChild(MauiCodeNode node)
    {
        _children.Add(node);
    }

    public abstract string GenerateCode();
}

public class MauiCodeComponentNode : MauiCodeNode
{
    public MauiCodeComponentNode(string componentName)
    {
        ComponentName = componentName;
    }

    public string ComponentName { get; }

    public override string GenerateCode()
    {
        if (Children.Count == 0) 
        {
            return "error-no-component-found";
        }

        return $$"""
            class {{ComponentName}} : Component
            {
                public override VisualNode Render()
                {
                    return {{Children[0]}};
                }
            }
            """;
    }
}

public class MauiCodeComponentNodeWidget : MauiCodeNode
{
    public MauiCodeComponentNodeWidget(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public override string GenerateCode()
    {
        if (Children.Count == 0)
        {
            return $$"""
            new {{Name}}()
            {{GeneratePropertiesCode()}}
            """;
        }
        
        return $$"""
            new {{Name}}
            {
                {{string.Join(Environment.NewLine, Children.Select(_=>_.GenerateCode()))}}
            }
            {{GeneratePropertiesCode()}}
            """;
    }

    private string GeneratePropertiesCode()
    {
        throw new NotImplementedException();
    }
}
