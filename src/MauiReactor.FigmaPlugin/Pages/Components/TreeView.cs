using MauiReactor.Canvas;
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
    public FigmaNet.DOCUMENT? Document { get; set; }
    public TreeViewNode[] Roots { get; set; } = Array.Empty<TreeViewNode>();
    public TreeViewNode[] Nodes { get; set; } = Array.Empty<TreeViewNode>();
    public double ContainerWidth { get; set; }
    public TreeViewNode? SelectedNode { get; set; }
    public TreeViewNode? HoverNode { get; set; }
}

class TreeView : Component<TreeViewState>
{
    private FigmaNet.DOCUMENT? _document;

    public TreeView Document(FigmaNet.DOCUMENT? document)
    {
        _document = document;
        return this;
    }

    protected override void OnMounted()
    {
        State.Document = _document;
        if (_document != null)
        {
            State.Roots = _document.Children
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
                State.Roots = _document.Children
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
            .OnExpand(() => SetState(s => s.Nodes = State.Roots.SelectMany(_ => _.GetDescendants()).ToArray()))
            .OnSelected(node => SetState(s =>
            {
                if (s.SelectedNode != null)
                {
                    s.SelectedNode.IsSelected = false;
                }

                s.SelectedNode = node;

                if (s.SelectedNode != null)
                {
                    s.SelectedNode.IsSelected = true;
                }
            }))
            .IsMouseHover(State.HoverNode == node)
            .OnHoverIn(node => SetState(s => s.HoverNode = node))
            .OnHoverOut(node =>
            {
                if (State.HoverNode == node)
                {
                    SetState(s => s.HoverNode = null);
                }
            })
        ;
}

class TreeViewItem : Component
{
    private TreeViewNode? _node;
    private Action? _expandedAction;
    private Action<TreeViewNode>? _selectedAction;
    private Action<TreeViewNode>? _hoverInAction;
    private Action<TreeViewNode>? _hoverOutAction;
    private bool _isMouseHover;

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

    public TreeViewItem OnSelected(Action<TreeViewNode> selectedAction)
    {
        _selectedAction = selectedAction;
        return this;
    }

    public TreeViewItem IsMouseHover(bool isMouseHover)
    {
        _isMouseHover = isMouseHover;
        return this;
    }

    public TreeViewItem OnHoverIn(Action<TreeViewNode> hoverInAction)
    {
        _hoverInAction = hoverInAction;
        return this;
    }

    public TreeViewItem OnHoverOut(Action<TreeViewNode> hoverOutAction)
    {
        _hoverOutAction = hoverOutAction;
        return this;
    }

    private static PathF ArrowDown => new PathF()
                .LineTo(5, 0)
                .LineTo(2.5f, 4)
                .LineTo(0, 0);

    private static PathF ArrowRight => new PathF()
                .MoveTo(0, 5)
                .LineTo(4, 2.5f)
                .LineTo(0, 0);

    public override VisualNode Render()
    {
        if (_node == null)
        {
            return null!;
        }

        return new Box
        {
            RenderInternal()
        }
        .When(_node.IsSelected, box => box.BackgroundColor(ThemeColors.Gray200))
        .When(_node.IsParentSelected(), box => box.BackgroundColor(ThemeColors.Gray300));
        ;
    }

    VisualNode RenderInternal()
    {
        if (_node == null)
        {
            throw new InvalidOperationException();
        }

        return new Box
        {
            new PointInterationHandler
            {
                _node.Children.Length > 0 ?
                RenderWithChildren()
                :
                RenderNoChildren()
            }
            .OnTap(() =>
            {
                _selectedAction?.Invoke(_node);
            })
            .OnHoverIn(()=>
            {
                _hoverInAction?.Invoke(_node);
            })
            .OnHoverOut(()=>
            {
                _hoverOutAction?.Invoke(_node);
            })
        }
        .When(_isMouseHover && !(_node.IsSelected || _node.IsParentSelected()), box => box.BorderColor(ThemeColors.Blue100Accent).BorderSize(1));
    }

    VisualNode RenderWithChildren()
    {
        if (_node == null)
        {
            throw new InvalidOperationException();
        }

        return new Row("24, *")
        {
            new PointInterationHandler
            {
                new Align
                {
                    new Canvas.Path()
                        .Data(_node.IsExpanded ? ArrowDown : ArrowRight)
                        .FillColor(_node.IsSelected || _node.IsParentSelected() ? Colors.Black : Colors.White)
                }
                .Width(5)
                .Height(5)
                .VerticalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.Center)
                .HorizontalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.Center)
            }
            .OnTap(() =>
            {
                _node.IsExpanded = !_node.IsExpanded;
                _expandedAction?.Invoke();
            }),

            new Text(_node.Node.Name)
                .FontColor(_node.IsSelected || _node.IsParentSelected() ? Colors.Black : Colors.White)
                .VerticalAlignment(VerticalAlignment.Center)
                .When(_node.Width == 0, text => text.OnMeasure((sender, args) => 
                {
                    _node.Width = args.Size.Width + _node.Indent + 24;
                    Invalidate();
                })),
        }
        .Margin(_node.Indent, 0, 0, 0);
    }

    VisualNode RenderNoChildren()
    {
        if (_node == null)
        {
            throw new InvalidOperationException();
        }

        return new Text(_node.Node.Name)
            .VerticalAlignment(VerticalAlignment.Center)
            .FontColor(_node.IsSelected || _node.IsParentSelected() ? Colors.Black : Colors.White)
            .Margin(_node.Indent + 16, 0, 0, 0)
            .When(_node.Width == 0, text => text.OnMeasure((sender, args) =>
            {
                _node.Width = args.Size.Width + _node.Indent + 24;
                Invalidate();
            }));
    }
}
