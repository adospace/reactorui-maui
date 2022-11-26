using FigmaSharp.Models;
using MauiReactor.Compatibility;
using MauiReactor.FigmaPlugin.Resources.Styles;
using MauiReactor.FigmaPlugin.Services.UI;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.FigmaPlugin.Pages.Components;


class TreeViewState
{
    public FigmaDocument? Document { get; set; }
    public TreeViewNode[] Roots { get; set; } = Array.Empty<TreeViewNode>();
    public TreeViewNode[] Nodes { get; set; } = Array.Empty<TreeViewNode>();
    public double ContainerWidth { get; set; }
}

class TreeView : Component<TreeViewState>
{
    private FigmaDocument? _document;

    public TreeView Document(FigmaDocument? document)
    {
        _document = document;
        return this;
    }

    protected override void OnMounted()
    {
        State.Document = _document;
        if (_document != null)
        {
            State.Roots = _document.children
                .Select(_ => new TreeViewNode(_, Invalidate))
                .ToArray();

            State.Nodes = State.Roots
                .SelectMany(_ => _.GetDescendants())
                .ToArray();
        }
        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        if (State.Document != _document)
        {
            State.Document = _document;

            if (_document != null)
            {
                State.Roots = _document.children
                    .Select(_ => new TreeViewNode(_, Invalidate))
                    .ToArray();

                State.Nodes = State.Roots
                    .SelectMany(_ => _.GetDescendants())
                    .ToArray();
            }
            else
            {
                State.Roots = Array.Empty<TreeViewNode>();
                State.Nodes = Array.Empty<TreeViewNode>();
            }
        }

        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        return new ScrollView
        {
            new CanvasView()
            {
                new Box
                {
                    RenderBody()
                }
            }
            .VStart()
            .HeightRequest(State.Nodes.Length * 24)
            .WidthRequest(Math.Max(State.ContainerWidth, State.Nodes.Length == 0 ? 0.0 : State.Nodes.Max(_=>_.Width)))
            .BackgroundColor(Colors.Transparent)
        }
        .Padding(0)
        .Orientation(ScrollOrientation.Both)
        .OnSizeChanged(size => SetState(s => s.ContainerWidth = size.Width))
        .BackgroundColor(ThemeColors.Gray600);
    }

    private VisualNode RenderBody()
        => (VisualNode)
            new ItemsRepeater<TreeViewNode>()
                .Items(State.Nodes)
                .ItemHeight(24)
                .Orientation(ItemsLayoutOrientation.Vertical)
                .ItemTemplate(RenderChildItem);

    private VisualNode RenderChildItem(TreeViewNode node)
        => new TreeViewItem()
            .Node(node)
            .OnExpand(() => SetState(s => s.Nodes = State.Roots.SelectMany(_ => _.GetDescendants()).ToArray()));
}

class TreeViewItem : Component
{
    private TreeViewNode? _node;
    private Action? _expandedAction;

    public TreeViewItem Node(TreeViewNode node)
    {
        _node = node;
        return this;
    }

    public TreeViewItem OnExpand(Action expandedAction)
    {
        _expandedAction = expandedAction;
        return this;
    }

    public override VisualNode Render()
    {
        if (_node == null)
        {
            return null!;
        }

        if (_node.Children.Length > 0)
        {
            return new Box
            {
                new PointInterationHandler
                {
                    new Row("16, *")
                    {
                        new Box()
                        {
                            new Text(_node.IsExpanded ? "˅" : "˃")
                                .FontColor(Colors.White)
                                .FontSize(14)
                                .FontWeight(600)
                                .VerticalAlignment(VerticalAlignment.Center)
                                .HorizontalAlignment(HorizontalAlignment.Center)
                        }
                        //.BorderColor(Colors.White)
                        //.BorderSize(1)
                        ,

                        new Text(_node.Node.name)
                            .FontColor(Colors.White)
                            .VerticalAlignment(VerticalAlignment.Center)
                            .When(_node.Width == 0, text => text.OnMeasure((sender, args) => _node.Width = args.Size.Width + _node.Indent + 24)),
                    }
                }
                .OnTap(() =>
                {
                    _node.IsExpanded = !_node.IsExpanded;
                    _expandedAction?.Invoke();
                })
            }
            .Margin(_node.Indent, 0, 0, 0);
        }

        return new Text(_node.Node.name)
            .VerticalAlignment(VerticalAlignment.Center)
            .FontColor(Colors.White)
            .Margin(_node.Indent + 16, 0, 0, 0)
            .When(_node.Width == 0, text => text.OnMeasure((sender, args) => _node.Width = args.Size.Width + _node.Indent + 24));
    }
}
