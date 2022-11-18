using FigmaSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MauiReactor.FigmaPlugin.FigmaSharp.UI;

public class TreeViewNode
{
    public TreeViewNode(FigmaNode node, double indent = 0)
    {
        Node = node;
        if (node is IFigmaNodeContainer nodeAsContainer)
        {
            Children = nodeAsContainer.children.Select(_ => new TreeViewNode(_, indent + 24)).ToArray();
        }

        Indent = indent;
    }

    public FigmaNode Node { get; }

    public bool IsExpanded
    {
        get;
        set;
    }

    public TreeViewNode[] Children { get; } = Array.Empty<TreeViewNode>();
    public double Indent { get; }

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
