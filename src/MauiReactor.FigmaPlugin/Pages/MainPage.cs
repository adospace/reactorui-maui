using FigmaSharp;
using FigmaSharp.Models;
using MauiReactor;
using MauiReactor.Compatibility;
using MauiReactor.FigmaPlugin.FigmaSharp.UI;
using MauiReactor.FigmaPlugin.Pages.Components;
using MauiReactor.FigmaPlugin.Resources.Styles;
using MauiReactor.Shapes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.FigmaPlugin.Pages;

class MainPageState : IState
{
    public FigmaDocument? Document { get; set; }

    public FigmaNode? SelectedNode { get; set; }
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
            State.Document == null ? new ActivityIndicator().IsRunning(true) :
            new ResizableContainer
            {
                new TreeView()
                    .Document(State.Document)
                    .OnSelectedNode(node=>SetState(s => s.SelectedNode = node)),

                new NodeTextEditor()
                    .Node(State.SelectedNode)
            }
            .Orientation(StackOrientation.Horizontal)
        };
    }
}

class TreeViewState : IState
{
    public FigmaDocument? Document { get; set; }
    public TreeViewNode[] Roots { get; set; } = Array.Empty<TreeViewNode>();
    public TreeViewNode[] Nodes { get; set; } = Array.Empty<TreeViewNode>();
    public TreeViewNode? SelectedNode { get; set; }
}

class TreeView : Component<TreeViewState>
{
    private FigmaDocument? _document;
    private Action<FigmaNode>? _selectedAction;

    public TreeView Document(FigmaDocument document)
    {
        _document = document;
        return this;
    }

    public TreeView OnSelectedNode(Action<FigmaNode> selectedAction)
    {
        _selectedAction = selectedAction;
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
            .IsSelected(State.SelectedNode == node)
            .OnExpand(()=> SetState(s => s.Nodes = State.Roots.SelectMany(_ => _.GetDescendants()).ToArray()))
            .OnSelected(()=> SetState(s =>
            {
                s.Nodes = State.Roots.SelectMany(_ => _.GetDescendants()).ToArray();
                s.SelectedNode = node;

                _selectedAction?.Invoke(node.Node);
            }));
    }
}

class TreeViewItemState : IState
{ 
    public bool IsSelected {  get; set; }
}

class TreeViewItem : Component<TreeViewItemState>
{
    private TreeViewNode? _node;
    private Action? _expandedAction;
    private Action? _selectedAction;
    private bool _isSelected;

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

    public TreeViewItem OnSelected(Action selectedAction)
    {
        _selectedAction = selectedAction;
        return this;
    }

    public TreeViewItem IsSelected(bool selected)
    {
        _isSelected = selected;
        return this;
    }

    protected override void OnMounted()
    {
        State.IsSelected = _isSelected;
        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        State.IsSelected = _isSelected;
        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        if (_node == null)
        {
            return null!;
        }

        if (_node.Children.Length > 0)
        {
            return new Grid("24", "16, *")
            {
                new Image(_node.IsExpanded ? "down.png" : "right.png")
                    .Aspect(Aspect.AspectFit)
                    .OnTapped(() =>
                    {
                        _node.IsExpanded = !_node.IsExpanded;
                        _expandedAction?.Invoke();
                    }),

                new Label(_node.Node.name)
                    .TextColor(State.IsSelected ? Colors.Black : Colors.White)
                    .When(State.IsSelected, _ => _.BackgroundColor(ThemeColors.Gray100))
                    .Padding(4,0,0,0)
                    .FontSize(14)
                    .GridColumn(1)
                    .OnTapped(() =>
                    {
                        _selectedAction?.Invoke();
                        SetState(s => s.IsSelected = true);
                    }),
            }
            .Margin(_node.Indent, 0, 0, 0);
        }

        return new Label(_node.Node.name)
            .TextColor(State.IsSelected ? Colors.Black : Colors.White)
            .When(State.IsSelected, _ => _.BackgroundColor(ThemeColors.Gray100))
            .FontSize(14)
            .OnTapped(() =>
            {
                _selectedAction?.Invoke();
                SetState(s => s.IsSelected = true);
            })
            .Margin(_node.Indent, 0, 0, 0);
    }
}

enum NodeTextEditorView
{
    Code,

    Source
}

class NodeTextEditorState : IState
{
    public NodeTextEditorView View {  get; set; }
}

class NodeTextEditor : Component<NodeTextEditorState>
{
    FigmaNode? _node;

    public NodeTextEditor Node(FigmaNode? node)
    {
        _node = node;
        return this;
    }

    public override VisualNode Render()
    {
        return new Grid("48, *", "*")
        {
            new HStack(spacing: 0)
            {
                new Button("Code")
                    .OnClicked(()=>SetState(s => s.View = NodeTextEditorView.Code)),

                new Button("Source")
                    .OnClicked(()=>SetState(s => s.View = NodeTextEditorView.Source))
            },


            new Editor()
                .FontFamily("CascadiaCodeRegular")
                .IsReadOnly(true)
                .Text((State.View == NodeTextEditorView.Source ? _node?.GenerateSource() : _node?.GeneratePrettyCode()) ?? string.Empty)
                .GridRow(1)
        };
    }
}

public static class FigmaNodeGenerator
{
    public static string GenerateSource(this FigmaNode node)
    {
        var jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        };

        return JsonConvert.SerializeObject(node, jsonSerializerSettings);
    }

    public static string GeneratePrettyCode(this FigmaNode node)
    {
        return PrettifyCode(GenerateCode(node));
    }

    public static string GenerateCode(this FigmaNode node, FigmaNode? parent = null)
    {
        if (node is FigmaText figmaText)
        {
            return GenerateCode(figmaText);
        }

        if (node is IAbsoluteBoundingBox boundingBox)
        {
            var nodeWidth = Math.Round(boundingBox.absoluteBoundingBox.Width.GetValueOrDefault(), 1);
            var nodeHeight = Math.Round(boundingBox.absoluteBoundingBox.Height.GetValueOrDefault(), 1);

            if (parent is IAbsoluteBoundingBox parentBoundingBox)
            {
                var parentWidth = Math.Round(parentBoundingBox.absoluteBoundingBox.Width.GetValueOrDefault(), 1);
                var parentHeight = Math.Round(parentBoundingBox.absoluteBoundingBox.Height.GetValueOrDefault(), 1);
                var parentLeft = Math.Round(parentBoundingBox.absoluteBoundingBox.X.GetValueOrDefault(), 1);
                var parentTop = Math.Round(parentBoundingBox.absoluteBoundingBox.Y.GetValueOrDefault(), 1);
                var nodeLeft = Math.Round(boundingBox.absoluteBoundingBox.X.GetValueOrDefault(), 1);
                var nodeTop = Math.Round(boundingBox.absoluteBoundingBox.Y.GetValueOrDefault(), 1);

                var marginLeft = Math.Round(nodeLeft - parentLeft);
                var marginTop = Math.Round(nodeTop - parentTop);

                var alignLeft = marginLeft == 0;
                var alignTop = marginTop == 0;

                var alignHCentered = parentWidth / 2 == nodeWidth / 2 + marginLeft;
                var alignVCentered = parentHeight / 2 == nodeHeight / 2 + marginTop;

                var alignRight = marginLeft == parentWidth - nodeWidth;
                var alighBottom = marginTop == parentHeight - nodeHeight;

                return $$"""
                //{{node.name}}
                new Align()
                    .Margin({{marginLeft}}, {{marginTop}}, 0, 0)
                    .Width({{nodeWidth}})
                    .Height({{nodeHeight}})
                    {
                        {{node.GenerateChildrenCode()}}
                    }
                """;
            }
            else
            {
                return $$"""
                //{{node.name}}
                new Align()
                    .Width({{nodeWidth}})
                    .Height({{nodeHeight}})
                    {
                        {{node.GenerateChildrenCode()}}
                    }
                """;
            }
        };

        return string.Empty;
    }

    public static string GenerateCode(FigmaText text)
    {
        var nodeWidth = Math.Round(text.absoluteBoundingBox.Width.GetValueOrDefault(), 1);
        var nodeHeight = Math.Round(text.absoluteBoundingBox.Height.GetValueOrDefault(), 1);

        return $$"""
                //{{text.name}}
                new Align()
                    .Width({{nodeWidth}})
                    .Height({{nodeHeight}})
                    {
                        new Text()
                            .Value("{{text.name}}")
                            .HorizontalAlignment({{GetCanvasHAlignment(text.style.textAlignHorizontal)}})
                            .VerticalAlignment({{GetCanvasVAlignment(text.style.textAlignHorizontal)}})
                    }
                """;
    }

    private static string GetCanvasHAlignment(string alignText)
    {
        switch (alignText)
        {
            case "CENTER":
                return "HorizontalAlignment.Center";

            default:
                return string.Empty;
        }
    }

    private static string GetCanvasVAlignment(string alignText)
    {
        switch (alignText)
        {
            case "CENTER":
                return "VerticalAlignment.Center";

            default:
                return string.Empty;
        }
    }

    public static string GenerateChildrenCode(this FigmaNode node)
    {
        if (node is IFigmaNodeContainer nodeContainer)
        {
            return string.Join(Environment.NewLine, nodeContainer.children.Select(_ => _.GenerateCode(node)));
        }

        return string.Empty;
    }

    private static string PrettifyCode(string code)
    {
        var tree = CSharpSyntaxTree.ParseText(code);
        var root = tree.GetRoot().NormalizeWhitespace();
        var ret = root.ToFullString();
        return $"// <auto-generated />{Environment.NewLine}{ret}";
    }
}