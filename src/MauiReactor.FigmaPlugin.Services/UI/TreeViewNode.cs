using FigmaNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.FigmaPlugin.Services.UI;

public class TreeViewNode
{
    private readonly Action _widthUpdated;

    public TreeViewNode(Node node, Action widthUpdated, float indent = 0)
    {
        Node = node;
        _widthUpdated = widthUpdated;
        if (node is INodeContainer nodeAsContainer)
        {
            Children = nodeAsContainer.Children
                .Select(_ => new TreeViewNode(_, this, widthUpdated, indent + 24)).ToArray();
        }

        Indent = indent;
    }

    private TreeViewNode(Node node, TreeViewNode parent, Action widthUpdated, float indent = 0)
        : this(node, widthUpdated, indent)
    {
        Parent = parent;
    }

    public Node Node { get; }

    public bool IsExpanded
    {
        get;
        set;
    }

    public bool IsSelected
    {
        get;
        set;
    }

    public bool IsParentSelected()
    {
        var parent = Parent;
        while (parent != null)
        {
            if (parent.IsSelected)
            {
                return true;
            }

            parent = parent.Parent;
        }

        return false;
    }

    private float _width;
    public float Width
    {
        get => _width;
        set
        {
            if (_width != value)
            {
                _width = value;
                _widthUpdated();
            }
        }
    }

    public TreeViewNode[] Children { get; } = Array.Empty<TreeViewNode>();
    public float Indent { get; }
    public TreeViewNode? Parent { get; }

    public IEnumerable<TreeViewNode> GetDescendants()
    {
        yield return this;
        if (IsExpanded)
        {
            foreach (var childNode in Children)
            {
                foreach (var descendantNode in childNode.GetDescendants())
                {
                    yield return descendantNode;
                }
            }
        }
    }
}

