using FigmaSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.FigmaPlugin.Services.UI;

public class TreeViewNode
{
    private readonly Action _widthUpdated;

    public TreeViewNode(FigmaNode node, Action widthUpdated, float indent = 0)
    {
        Node = node;
        _widthUpdated = widthUpdated;
        if (node is IFigmaNodeContainer nodeAsContainer)
        {
            Children = nodeAsContainer.children.Select(_ => new TreeViewNode(_, widthUpdated, indent + 24)).ToArray();
        }

        Indent = indent;
    }

    public FigmaNode Node { get; }

    public bool IsExpanded
    {
        get;
        set;
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

