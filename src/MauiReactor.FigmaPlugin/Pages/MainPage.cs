using FigmaSharp;
using FigmaSharp.Models;
using MauiReactor;
using MauiReactor.Compatibility;
using MauiReactor.FigmaPlugin.Resources.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.FigmaPlugin.Pages;

class MainPageState : IState
{
    public FigmaDocument Document { get; set; }
}

class MainPage : Component<MainPageState>
{
    protected override void OnMounted()
    {
        InitializeDocument();

        base.OnMounted();
    }

    private async void InitializeDocument()
    {
        const string Token = "figd_J__-s-kUbXv6AH1MmkVg1r8epkOPvobZFpIFouKl";
        const string FileId = "W4yKdm26o6JwB2gzdws7HS";

        var api = new FigmaApi(Token);
        var fileResponse = await api.GetFileAsync(new FigmaFileQuery(FileId));

        SetState(s => s.Document = fileResponse.document);
    }

    public override VisualNode Render()
    {
        return new ContentPage
        {
            new Grid("*", "200, *")
            {
                new TreeView()
                    .Document(State.Document)
            }
        };
    }
}

class TreeViewNode
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

class TreeViewState : IState
{
    public FigmaDocument Document { get; set; }
    public TreeViewNode[] Roots { get; set; } = Array.Empty<TreeViewNode>();
    public TreeViewNode[] Nodes { get; set; } = Array.Empty<TreeViewNode>();
}

class TreeView : Component<TreeViewState>
{
    private FigmaDocument _document;

    public TreeView Document(FigmaDocument document)
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
                .Select(_ => new TreeViewNode(_))
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
                    .Select(_ => new TreeViewNode(_))
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
            new CollectionView()
                .ItemsSource(State.Nodes, RenderChildItem)
        }
        .BackgroundColor(ThemeColors.Gray600)
        .Padding(0,0,10,0)
        .Orientation(ScrollOrientation.Horizontal);
    }

    private VisualNode RenderChildItem(TreeViewNode node)
    {
        return new TreeViewItem()
            .Node(node)
            .OnExpand(()=> SetState(s => s.Nodes = State.Roots.SelectMany(_ => _.GetDescendants()).ToArray()));
    }
}

class TreeViewItem : Component
{
    private TreeViewNode _node;
    private double _indent;
    private Action _expandedAction;

    public TreeViewItem Node(TreeViewNode node)
    {
        _node = node;
        return this;
    }

    public TreeViewItem Indent(double indent)
    {
        _indent = indent;
        return this;
    }

    public TreeViewItem OnExpand(Action expandedAction)
    {
        _expandedAction = expandedAction;
        return this;
    }

    public override VisualNode Render()
    {
        if (_node.Children.Length > 0)
        {
            return new Grid("24", "24, *")
            {

                new Image(_node.IsExpanded ? "down.png" : "right.png")
                    .Aspect(Aspect.AspectFit),

                new Label(_node.Node.name)
                    .TextColor(Colors.White)
                    .GridColumn(1),
            }
            .OnTapped(() =>
            {
                _node.IsExpanded = !_node.IsExpanded;
                _expandedAction();
            })
            .Margin(_node.Indent, 0, 0, 0);
        }

        return new Label(_node.Node.name)
            .TextColor(Colors.White)
            .Margin(_node.Indent, 0, 0, 0);
    }
}