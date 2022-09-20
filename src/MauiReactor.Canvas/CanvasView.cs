using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.ComponentModel.DataAnnotations;
using Microsoft.Maui.Graphics;
using System.Net;
using Microsoft.Maui.Primitives;
using System.Threading;
using System.Collections;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.IO;

namespace MauiReactor.Canvas
{

    public partial interface ICanvasView : IView
    {


    }


    public partial class CanvasView<T> : View<T>, ICanvasView, IEnumerable where T : Internals.CanvasView, new()
    {
        public CanvasView()
        {

        }

        public CanvasView(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        protected override bool SupportChildIndexing => false;

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.Children.Insert(widget.ChildIndex, node);
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.Children.Remove(node);
            }

            base.OnRemoveChild(widget, childControl);
        }


        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);

            NativeControl.Invalidate();

            base.OnUpdate();
        }

        protected override void OnAttachNativeEvents()
        {
            //Validate.EnsureNotNull(NativeControl);

            //var thisAsIGraphicsView = (IGraphicsView)this;

            //if (thisAsIGraphicsView.StartHoverInteractionAction != null || thisAsIGraphicsView.StartHoverInteractionActionWithArgs != null)
            //{
            //    NativeControl.StartHoverInteraction += NativeControl_StartHoverInteraction;
            //}

            //if (thisAsIGraphicsView.MoveHoverInteractionAction != null || thisAsIGraphicsView.MoveHoverInteractionActionWithArgs != null)
            //{
            //    NativeControl.MoveHoverInteraction += NativeControl_MoveHoverInteraction;
            //}

            //if (thisAsIGraphicsView.EndHoverInteractionAction != null || thisAsIGraphicsView.EndHoverInteractionActionWithArgs != null)
            //{
            //    NativeControl.EndHoverInteraction += NativeControl_EndHoverInteraction;
            //}

            //if (thisAsIGraphicsView.StartInteractionAction != null || thisAsIGraphicsView.StartInteractionActionWithArgs != null)
            //{
            //    NativeControl.StartInteraction += NativeControl_StartInteraction;
            //}

            //if (thisAsIGraphicsView.DragInteractionAction != null || thisAsIGraphicsView.DragInteractionActionWithArgs != null)
            //{
            //    NativeControl.DragInteraction += NativeControl_DragInteraction;
            //}

            //if (thisAsIGraphicsView.EndInteractionAction != null || thisAsIGraphicsView.EndInteractionActionWithArgs != null)
            //{
            //    NativeControl.EndInteraction += NativeControl_EndInteraction;
            //}

            //if (thisAsIGraphicsView.CancelInteractionAction != null || thisAsIGraphicsView.CancelInteractionActionWithArgs != null)
            //{
            //    NativeControl.CancelInteraction += NativeControl_CancelInteraction;
            //}


            //OnAttachingNativeEvents();

            base.OnAttachNativeEvents();
        }

        protected override void OnDetachNativeEvents()
        {

            base.OnDetachNativeEvents();
        }


    }


    public partial class CanvasView : CanvasView<Internals.CanvasView>
    {
        public CanvasView()
        {

        }

        public CanvasView(Action<Internals.CanvasView?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }


    public static partial class GraphicsViewExtensions
    {


        //public static T Drawable<T>(this T graphicsView, Microsoft.Maui.Graphics.IDrawable drawable) where T : IGraphicsView
        //{
        //    graphicsView.Drawable = new PropertyValue<Microsoft.Maui.Graphics.IDrawable>(drawable);
        //    return graphicsView;
        //}


        //public static T Drawable<T>(this T graphicsView, Func<Microsoft.Maui.Graphics.IDrawable> drawableFunc) where T : IGraphicsView
        //{
        //    graphicsView.Drawable = new PropertyValue<Microsoft.Maui.Graphics.IDrawable>(drawableFunc);
        //    return graphicsView;
        //}










        //public static T OnStartHoverInteraction<T>(this T graphicsView, Action? startHoverInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.StartHoverInteractionAction = startHoverInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnStartHoverInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? startHoverInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.StartHoverInteractionActionWithArgs = startHoverInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnMoveHoverInteraction<T>(this T graphicsView, Action? moveHoverInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.MoveHoverInteractionAction = moveHoverInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnMoveHoverInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? moveHoverInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.MoveHoverInteractionActionWithArgs = moveHoverInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnEndHoverInteraction<T>(this T graphicsView, Action? endHoverInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.EndHoverInteractionAction = endHoverInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnEndHoverInteraction<T>(this T graphicsView, Action<object?, EventArgs>? endHoverInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.EndHoverInteractionActionWithArgs = endHoverInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnStartInteraction<T>(this T graphicsView, Action? startInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.StartInteractionAction = startInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnStartInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? startInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.StartInteractionActionWithArgs = startInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnDragInteraction<T>(this T graphicsView, Action? dragInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.DragInteractionAction = dragInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnDragInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? dragInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.DragInteractionActionWithArgs = dragInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnEndInteraction<T>(this T graphicsView, Action? endInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.EndInteractionAction = endInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnEndInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? endInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.EndInteractionActionWithArgs = endInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnCancelInteraction<T>(this T graphicsView, Action? cancelInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.CancelInteractionAction = cancelInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnCancelInteraction<T>(this T graphicsView, Action<object?, EventArgs>? cancelInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.CancelInteractionActionWithArgs = cancelInteractionActionWithArgs;
        //    return graphicsView;
        //}

    }
}

namespace MauiReactor.Canvas
{
    public partial interface ICanvasNode : IVisualNode
    {
        PropertyValue<Thickness>? Margin { get; set; }
    }

    public partial class CanvasNode<T> : VisualNode<T>, ICanvasNode where T : Internals.CanvasNode, new()
    {
        public CanvasNode()
        {

        }

        public CanvasNode(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Thickness>? ICanvasNode.Margin { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIGraphicsView = (ICanvasNode)this;

            SetPropertyValue(NativeControl, Internals.CanvasNode.MarginProperty, thisAsIGraphicsView.Margin);

            base.OnUpdate();
        }
    }

    public partial class CanvasNode : CanvasNode<Internals.CanvasNode>
    {
        public CanvasNode()
        {

        }

        public CanvasNode(Action<Internals.CanvasNode?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class CanvasNodeExtensions
    {
        public static T Margin<T>(this T node, Thickness value) where T : IBox
        {
            node.Margin = new PropertyValue<Thickness>(value);
            return node;
        }

        public static T Margin<T>(this T node, Func<Thickness> valueFunc) where T : IBox
        {
            node.Margin = new PropertyValue<Thickness>(valueFunc);
            return node;
        }
    }
}

namespace MauiReactor.Canvas
{
    public partial interface IBox : ICanvasNode
    {
        PropertyValue<Color?>? BackgroundColor { get; set; }
        PropertyValue<Color?>? BorderColor { get; set; }
        PropertyValue<float>? CornerRadius { get; set; }
        PropertyValue<float>? BorderSize { get; set; }
    }

    public partial class Box<T> : CanvasNode<T>, IBox, IEnumerable where T : Internals.Box, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public Box()
        {

        }

        public Box(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Color?>? IBox.BackgroundColor { get; set; }
        PropertyValue<Color?>? IBox.BorderColor { get; set; }
        PropertyValue<float>? IBox.CornerRadius { get; set; }
        PropertyValue<float>? IBox.BorderSize { get; set; }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }


        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(params VisualNode?[]? animations)
        {
            if (animations is null)
            {
                return;
            }

            foreach (var node in animations)
            {
                if (node != null)
                {
                    _internalChildren.Add(node);
                }
            }
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.Child = node;
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node &&
                node == NativeControl.Child)
            {
                NativeControl.Child = null;
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBorder = (IBox)this;

            SetPropertyValue(NativeControl, Internals.Box.BackgroundColorProperty, thisAsIBorder.BackgroundColor);
            SetPropertyValue(NativeControl, Internals.Box.BorderColorProperty, thisAsIBorder.BorderColor);
            SetPropertyValue(NativeControl, Internals.Box.CornerRadiusProperty, thisAsIBorder.CornerRadius);
            SetPropertyValue(NativeControl, Internals.Box.BorderSizeProperty, thisAsIBorder.BorderSize);

            base.OnUpdate();
        }
    }

    public partial class Box : Box<Internals.Box>
    {
        public Box()
        {

        }

        public Box(Action<Internals.Box?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class BoxExtensions
    {
        public static T BackgroundColor<T>(this T ndoe, Color? value) where T : IBox
        {
            ndoe.BackgroundColor = new PropertyValue<Color?>(value);
            return ndoe;
        }

        public static T BackgroundColor<T>(this T node, Func<Color?> valueFunc) where T : IBox
        {
            node.BackgroundColor = new PropertyValue<Color?>(valueFunc);
            return node;
        }
        public static T BorderColor<T>(this T ndoe, Color? value) where T : IBox
        {
            ndoe.BorderColor = new PropertyValue<Color?>(value);
            return ndoe;
        }

        public static T BorderColor<T>(this T node, Func<Color?> valueFunc) where T : IBox
        {
            node.BorderColor = new PropertyValue<Color?>(valueFunc);
            return node;
        }
        public static T BorderSize<T>(this T node, float value) where T : IBox
        {
            node.BorderSize = new PropertyValue<float>(value);
            return node;
        }

        public static T BorderSize<T>(this T node, Func<float> valueFunc) where T : IBox
        {
            node.BorderSize = new PropertyValue<float>(valueFunc);
            return node;
        }
        public static T CornerRadius<T>(this T ndoe, float value) where T : IBox
        {
            ndoe.CornerRadius = new PropertyValue<float>(value);
            return ndoe;
        }

        public static T CornerRadius<T>(this T node, Func<float> valueFunc) where T : IBox
        {
            node.CornerRadius = new PropertyValue<float>(valueFunc);
            return node;
        }
    }
}

namespace MauiReactor.Canvas
{
    public partial interface IAlign : ICanvasNode
    {
        PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? HorizontalAlignment { get; set; }
        PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? VerticalAlignment { get; set; }
        PropertyValue<float>? Width { get; set; }
        PropertyValue<float>? Height { get; set; }
    }

    public partial class Align<T> : CanvasNode<T>, IAlign, IEnumerable where T : Internals.Align, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public Align()
        {

        }

        public Align(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? IAlign.HorizontalAlignment { get; set; }
        PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? IAlign.VerticalAlignment { get; set; }
        PropertyValue<float>? IAlign.Width { get; set; }
        PropertyValue<float>? IAlign.Height { get; set; }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }


        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(params VisualNode?[]? animations)
        {
            if (animations is null)
            {
                return;
            }

            foreach (var node in animations)
            {
                if (node != null)
                {
                    _internalChildren.Add(node);
                }
            }
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.Child = node;
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node &&
                node == NativeControl.Child)
            {
                NativeControl.Child = null;
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIAlign = (IAlign)this;

            SetPropertyValue(NativeControl, Internals.Align.HorizontalAlignmentProperty, thisAsIAlign.HorizontalAlignment);
            SetPropertyValue(NativeControl, Internals.Align.VerticalAlignmentProperty, thisAsIAlign.VerticalAlignment);
            SetPropertyValue(NativeControl, Internals.Align.WidthProperty, thisAsIAlign.Width);
            SetPropertyValue(NativeControl, Internals.Align.HeightProperty, thisAsIAlign.Height);

            base.OnUpdate();
        }
    }

    public partial class Align : Align<Internals.Align>
    {
        public Align()
        {

        }

        public Align(Action<Internals.Align?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class AlignExtensions
    {
        public static T HorizontalAlignment<T>(this T ndoe, Microsoft.Maui.Primitives.LayoutAlignment value) where T : IAlign
        {
            ndoe.HorizontalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(value);
            return ndoe;
        }

        public static T HorizontalAlignment<T>(this T node, Func<Microsoft.Maui.Primitives.LayoutAlignment> valueFunc) where T : IAlign
        {
            node.HorizontalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(valueFunc);
            return node;
        }
        public static T VerticalAlignment<T>(this T ndoe, Microsoft.Maui.Primitives.LayoutAlignment value) where T : IAlign
        {
            ndoe.VerticalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(value);
            return ndoe;
        }

        public static T VerticalAlignment<T>(this T node, Func<Microsoft.Maui.Primitives.LayoutAlignment> valueFunc) where T : IAlign
        {
            node.VerticalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(valueFunc);
            return node;
        }

        public static T Width<T>(this T node, float value) where T : IAlign
        {
            node.Width = new PropertyValue<float>(value);
            return node;
        }

        public static T Width<T>(this T node, Func<float> valueFunc) where T : IAlign
        {
            node.Width = new PropertyValue<float>(valueFunc);
            return node;
        }

        public static T Height<T>(this T node, float value) where T : IAlign
        {
            node.Height = new PropertyValue<float>(value);
            return node;
        }

        public static T Height<T>(this T node, Func<float> valueFunc) where T : IAlign
        {
            node.Height = new PropertyValue<float>(valueFunc);
            return node;
        }
    }
}

namespace MauiReactor.Canvas
{
    public partial interface INodeContainer : ICanvasNode
    {
    }

    public abstract partial class NodeContainer<T> : CanvasNode<T>, INodeContainer, IEnumerable where T : Internals.NodeContainer, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public NodeContainer()
        {

        }

        public NodeContainer(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }


        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(params VisualNode?[]? animations)
        {
            if (animations is null)
            {
                return;
            }

            foreach (var node in animations)
            {
                if (node != null)
                {
                    _internalChildren.Add(node);
                }
            }
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.InsertChild(widget.ChildIndex, node);
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.RemoveChild(node);
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnUpdate()
        {
            //Validate.EnsureNotNull(NativeControl);
            //var thisAsIBorder = (IGroup)this;


            base.OnUpdate();
        }
    }

    public static partial class NodeContainerExtensions
    {
    }
}

namespace MauiReactor.Canvas
{
    public partial interface IGroup : INodeContainer
    {
    }

    public partial class Group<T> : NodeContainer<T>, IGroup where T : Internals.Group, new()
    {
        public Group()
        {

        }

        public Group(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }


        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIGroup = (IGroup)this;

            base.OnUpdate();
        }
    }

    public partial class Group : Group<Internals.Group>
    {
        public Group()
        {

        }

        public Group(Action<Internals.Group?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class GroupExtensions
    {
    }
}

namespace MauiReactor.Canvas
{
    public partial interface IRow : INodeContainer
    {
        PropertyValue<string>? Columns { get; set; }
    }

    public partial class Row<T> : NodeContainer<T>, IRow where T : Internals.Row, new()
    {
        public Row()
        {

        }

        public Row(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<string>? IRow.Columns { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIRow = (IRow)this;
            SetPropertyValue(NativeControl, Internals.Row.ColumnsProperty, thisAsIRow.Columns);


            base.OnUpdate();
        }
    }

    public partial class Row : Row<Internals.Row>
    {
        public Row()
        {

        }

        public Row(Action<Internals.Row?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class RowExtensions
    {
        public static T Columns<T>(this T node, string value) where T : IRow
        {
            node.Columns = new PropertyValue<string>(value);
            return node;
        }

        public static T Columns<T>(this T node, Func<string> valueFunc) where T : IRow
        {
            node.Columns = new PropertyValue<string>(valueFunc);
            return node;
        }
    }
}

namespace MauiReactor.Canvas
{
    public partial interface IColumn : INodeContainer
    {
        PropertyValue<string>? Rows { get; set; }
    }

    public partial class Column<T> : NodeContainer<T>, IColumn where T : Internals.Column, new()
    {
        public Column()
        {

        }

        public Column(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<string>? IColumn.Rows { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIColumn = (IColumn)this;
            SetPropertyValue(NativeControl, Internals.Column.RowsProperty, thisAsIColumn.Rows);


            base.OnUpdate();
        }
    }

    public partial class Column : Column<Internals.Column>
    {
        public Column()
        {

        }

        public Column(Action<Internals.Column?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ColumnExtensions
    {
        public static T Rows<T>(this T node, string value) where T : IColumn
        {
            node.Rows = new PropertyValue<string>(value);
            return node;
        }

        public static T Rows<T>(this T node, Func<string> valueFunc) where T : IColumn
        {
            node.Rows = new PropertyValue<string>(valueFunc);
            return node;
        }
    }
}

namespace MauiReactor.Canvas
{
    public partial interface IText : ICanvasNode
    {
        PropertyValue<VerticalAlignment>? VerticalAlignment { get; set; }
        PropertyValue<HorizontalAlignment>? HorizontalAlignment { get; set; }
        PropertyValue<string>? Value { get; set; }
        PropertyValue<float>? FontSize { get; set; }
        PropertyValue<Color?>? FontColor { get; set; }
        PropertyValue<IFont?>? Font { get; set; }
    }

    public partial class Text<T> : CanvasNode<T>, IText where T : Internals.Text, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public Text()
        {

        }

        public Text(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<HorizontalAlignment>? IText.HorizontalAlignment { get; set; }
        PropertyValue<VerticalAlignment>? IText.VerticalAlignment { get; set; }
        PropertyValue<string>? IText.Value { get; set; }
        PropertyValue<float>? IText.FontSize { get; set; }
        PropertyValue<Color?>? IText.FontColor { get; set; }
        PropertyValue<IFont?>? IText.Font { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIText = (IText)this;

            SetPropertyValue(NativeControl, Internals.Text.HorizontalAlignmentProperty, thisAsIText.HorizontalAlignment);
            SetPropertyValue(NativeControl, Internals.Text.VerticalAlignmentProperty, thisAsIText.VerticalAlignment);
            SetPropertyValue(NativeControl, Internals.Text.ValueProperty, thisAsIText.Value);
            SetPropertyValue(NativeControl, Internals.Text.FontSizeProperty, thisAsIText.FontSize);
            SetPropertyValue(NativeControl, Internals.Text.FontColorProperty, thisAsIText.FontColor);
            SetPropertyValue(NativeControl, Internals.Text.FontProperty, thisAsIText.Font);

            base.OnUpdate();
        }
    }

    public partial class Text : Text<Internals.Text>
    {
        public Text()
        {

        }

        public Text(string value)
        {
            this.Value(value);
        }

        public Text(Action<Internals.Text?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class TextExtensions
    {
        public static T HorizontalAlignment<T>(this T node, HorizontalAlignment value) where T : IText
        {
            node.HorizontalAlignment = new PropertyValue<HorizontalAlignment>(value);
            return node;
        }

        public static T HorizontalAlignment<T>(this T node, Func<HorizontalAlignment> valueFunc) where T : IText
        {
            node.HorizontalAlignment = new PropertyValue<HorizontalAlignment>(valueFunc);
            return node;
        }
        public static T VerticalAlignment<T>(this T node, VerticalAlignment value) where T : IText
        {
            node.VerticalAlignment = new PropertyValue<VerticalAlignment>(value);
            return node;
        }

        public static T VerticalAlignment<T>(this T node, Func<VerticalAlignment> valueFunc) where T : IText
        {
            node.VerticalAlignment = new PropertyValue<VerticalAlignment>(valueFunc);
            return node;
        }

        public static T Value<T>(this T node, string value) where T : IText
        {
            node.Value = new PropertyValue<string>(value);
            return node;
        }

        public static T Value<T>(this T node, Func<string> valueFunc) where T : IText
        {
            node.Value = new PropertyValue<string>(valueFunc);
            return node;
        }

        public static T FontSize<T>(this T node, float value) where T : IText
        {
            node.FontSize = new PropertyValue<float>(value);
            return node;
        }

        public static T FontSize<T>(this T node, Func<float> valueFunc) where T : IText
        {
            node.FontSize = new PropertyValue<float>(valueFunc);
            return node;
        }

        public static T FontColor<T>(this T node, Color? value) where T : IText
        {
            node.FontColor = new PropertyValue<Color?>(value);
            return node;
        }

        public static T FontColor<T>(this T node, Func<Color?> valueFunc) where T : IText
        {
            node.FontColor = new PropertyValue<Color?>(valueFunc);
            return node;
        }

        public static T Font<T>(this T node, IFont? value) where T : IText
        {
            node.Font = new PropertyValue<IFont?>(value);
            return node;
        }

        public static T Font<T>(this T node, string? fontName) where T : IText
        {
            node.Font = new PropertyValue<IFont?>(fontName == null ? null : new Microsoft.Maui.Graphics.Font(fontName));
            return node;
        }

        public static T Font<T>(this T node, Func<IFont?> valueFunc) where T : IText
        {
            node.Font = new PropertyValue<IFont?>(valueFunc);
            return node;
        }
        public static T Font<T>(this T node, Func<string?> valueFunc) where T : IText
        {
            node.Font = new PropertyValue<IFont?>(()=>
            {
                var fontName = valueFunc.Invoke();
                if (fontName != null)
                {
                    return new Microsoft.Maui.Graphics.Font(fontName);
                }
                return null;
            });
            return node;
        }
    }
}

namespace MauiReactor.Canvas
{
    public partial interface IPicture : ICanvasNode
    {
        PropertyValue<Microsoft.Maui.Graphics.IImage?>? Source { get; set; }
    }

    public partial class Picture<T> : CanvasNode<T>, IPicture where T : Internals.Picture, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public Picture()
        {

        }

        public Picture(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Graphics.IImage?>? IPicture.Source { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIPicture = (IPicture)this;

            SetPropertyValue(NativeControl, Internals.Picture.SourceProperty, thisAsIPicture.Source);

            base.OnUpdate();
        }
    }

    public partial class Picture : Picture<Internals.Picture>
    {
        public Picture()
        {

        }

        public Picture(string imageSource)
        {
            this.Source(imageSource, true, Assembly.GetCallingAssembly());
        }

        public Picture(Action<Internals.Picture?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class PictureExtensions
    {
        private static readonly Dictionary<string, Microsoft.Maui.Graphics.IImage> _imageCache = new();

        public static T Source<T>(this T node, Microsoft.Maui.Graphics.IImage? value) where T : IPicture
        {
            node.Source = new PropertyValue<Microsoft.Maui.Graphics.IImage?>(value);
            return node;
        }

        public static T Source<T>(this T node, Func<Microsoft.Maui.Graphics.IImage?> valueFunc) where T : IPicture
        {
            node.Source = new PropertyValue<Microsoft.Maui.Graphics.IImage?>(valueFunc);
            return node;
        }

        public static T Source<T>(this T node, string? imageSource, bool cacheImage = true, Assembly? resourceAssembly = null) where T : IPicture
        {
            resourceAssembly ??= Assembly.GetCallingAssembly();
            node.Source = new PropertyValue<Microsoft.Maui.Graphics.IImage?>(LoadImage(imageSource, cacheImage, resourceAssembly));
            return node;
        }

        public static T Source<T>(this T node, Func<string?> valueFunc, bool cacheImage = true, Assembly? resourceAssembly = null) where T : IPicture
        {
            resourceAssembly ??= Assembly.GetCallingAssembly();
            node.Source = new PropertyValue<Microsoft.Maui.Graphics.IImage?>(() => LoadImage(valueFunc.Invoke(), cacheImage, resourceAssembly));
            return node;
        }

        private static Microsoft.Maui.Graphics.IImage? LoadImage(string? imageSource, bool cacheImage, Assembly resourceAssembly)
        {
            if (imageSource == null)
            {
                return null;
            }
            else
            {
                if (_imageCache.TryGetValue(imageSource, out var cachedImage))
                {
                    return cachedImage;
                }
                else
                {
                    Microsoft.Maui.Graphics.IImage? image = null;
                    var imageResourceStream = resourceAssembly.GetManifestResourceStream(imageSource);

                    if (imageResourceStream == null)
                    {
                        throw new InvalidOperationException($"Unable to load resource: '{imageSource}'. Available resources: {string.Join(",", resourceAssembly.GetManifestResourceNames())}");
                    }

                    using (Stream stream = imageResourceStream)
                    {
#if !WINDOWS
                        image = Microsoft.Maui.Graphics.Platform.PlatformImage.FromStream(stream);
#endif
                    }

                    if (cacheImage && image != null)
                    {
                        _imageCache.Add(imageSource, image);
                    }

                    return image;
                }
            }
        }
    }
}

namespace MauiReactor.Canvas.Internals
{ 
    public class CanvasView : Microsoft.Maui.Controls.GraphicsView
    {
        private class CanvasViewDrawable : IDrawable
        {
            private readonly CanvasView _owner;

            public CanvasViewDrawable(CanvasView owner)
            {
                _owner = owner;
            }

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                _owner.OnDraw(canvas, dirtyRect);
            }
        }

        public CanvasView()
        {
            Drawable = new CanvasViewDrawable(this);
        }

        private void OnDraw(ICanvas canvas, RectF dirtyRect)
        {
            var context = new DrawingContext(canvas, dirtyRect);
            foreach (var child in Children)
            {
                child.Draw(context);
            }
        }

        public List<CanvasNode> Children { get; } = new();

    }

    public record DrawingContext(ICanvas Canvas, RectF DirtyRect);

    public class CanvasNode : BindableObject
    {
        public static readonly BindableProperty MarginProperty = BindableProperty.Create(nameof(Margin), typeof(Thickness), typeof(CanvasNode), new Thickness());

        public Thickness Margin
        {
            get => (Thickness)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }

        public void Draw(DrawingContext context)
        {
            if (!Margin.IsEmpty)
            {
                OnDraw(context with 
                { 
                    DirtyRect = new RectF(
                        context.DirtyRect.X + (float)Margin.Left,
                        context.DirtyRect.Y + (float)Margin.Top,
                        context.DirtyRect.Width - (float)(Margin.Left + Margin.Right),
                        context.DirtyRect.Height - (float)(Margin.Top + Margin.Bottom)
                    )
                });
            }
            else
            {
                OnDraw(context);
            }
        }

        protected virtual void OnDraw(DrawingContext context)
        {

        }
    }

    public abstract class NodeContainer : CanvasNode
    {
        private readonly List<CanvasNode> _children = new();

        public IReadOnlyList<CanvasNode> Children => _children;

        public void InsertChild(int index, CanvasNode child)
        {
            _children.Insert(index, child);

            OnChildAdded(child);
        }

        protected virtual void OnChildAdded(CanvasNode child)
        {
        }

        public void RemoveChild(CanvasNode child)
        {
            _children.Remove(child);

            OnChildRemoved(child);
        }

        protected virtual void OnChildRemoved(CanvasNode child)
        {
            
        }
    }

    public class Group : NodeContainer
    {
        protected override void OnDraw(DrawingContext context)
        {
            foreach(var child in Children)
            {
                child.Draw(context);
            }

            base.OnDraw(context);
        }

    }

    public class Column : NodeContainer
    {
        private static readonly GridLengthTypeConverter _gridLengthTypeConverter = new GridLengthTypeConverter();

        public static readonly BindableProperty RowsProperty = BindableProperty.Create(nameof(Rows), typeof(string), typeof(Column), "*",
            coerceValue: (bindableObject, value) => string.IsNullOrWhiteSpace((string)value) ? "*" : value);


        public string Rows
        {
            get => (string)GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        private static IEnumerable<RowDefinition> ParseRows(string rows)
        {
            foreach (var rowDefinition in rows.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => (GridLength)Validate.EnsureNotNull(_gridLengthTypeConverter.ConvertFromInvariantString(_)))
                .Select(_ => new RowDefinition() { Height = _ }))
            {
                yield return rowDefinition;
            }
        }

        private IEnumerable<float> GetRowWidths(double width)
        {
            var rows = ParseRows(Rows).ToList();

            while (rows.Count < Children.Count)
            {
                rows.Add(new RowDefinition() { Height = rows[rows.Count - 1].Height });
            }

            var heightForStarRows = Math.Max(0.0, width - rows.Where(_ => _.Height.IsAbsolute).Sum(_ => _.Height.Value));
            var starHeight = heightForStarRows / rows.Where(_ => _.Height.IsStar).Sum(_ => _.Height.Value);

            foreach (var row in rows)
            {
                if (row.Height.IsAbsolute)
                {
                    yield return (float)row.Height.Value;
                }
                else if (row.Height.IsStar)
                {
                    yield return (float)(row.Height.Value * starHeight);
                }
                else
                {
                    throw new NotSupportedException("Auto sizing is not supported");
                }
            }
        }

        protected override void OnDraw(DrawingContext context)
        {
            var rowHeights = GetRowWidths(context.DirtyRect.Width).ToArray();
            var y = context.DirtyRect.Y;
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Draw(context with
                {
                    DirtyRect = new RectF(
                        context.DirtyRect.Left,
                        y,
                        context.DirtyRect.Height,
                        rowHeights[i]
                        )
                });

                y += rowHeights[i];
            }

            base.OnDraw(context);
        }

    }

    public class Row : NodeContainer
    {
        private static readonly GridLengthTypeConverter _gridLengthTypeConverter = new GridLengthTypeConverter();

        public static readonly BindableProperty ColumnsProperty = BindableProperty.Create(nameof(Columns), typeof(string), typeof(Row), "*",
            coerceValue: (bindableObject, value) => string.IsNullOrWhiteSpace((string)value) ? "*" : value);

        public string Columns
        {
            get => (string)GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        private static IEnumerable<ColumnDefinition> ParseColumns(string columns)
        {
            foreach (var columnDefinition in columns.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => (GridLength)Validate.EnsureNotNull(_gridLengthTypeConverter.ConvertFromInvariantString(_)))
                .Select(_ => new ColumnDefinition() { Width = _ }))
            {
                yield return columnDefinition;
            }
        }

        private IEnumerable<float> GetColumnWidths(double width)
        {
            var columns = ParseColumns(Columns).ToList();

            while (columns.Count < Children.Count)
            {
                columns.Add(new ColumnDefinition() { Width = columns[columns.Count-1].Width });
            }

            var widthForStarColumns = Math.Max(0.0, width - columns.Where(_ => _.Width.IsAbsolute).Sum(_ => _.Width.Value));
            var starWidth = widthForStarColumns / columns.Where(_ => _.Width.IsStar).Sum(_ => _.Width.Value);

            foreach(var column in columns)
            {
                if (column.Width.IsAbsolute)
                {
                    yield return (float)column.Width.Value;
                }
                else if (column.Width.IsStar)
                {
                    yield return (float)(column.Width.Value * starWidth);
                }
                else
                {
                    throw new NotSupportedException("Auto sizing is not supported");
                }
            }
        }

        protected override void OnDraw(DrawingContext context)
        {
            var columnWidths = GetColumnWidths(context.DirtyRect.Width).ToArray();
            var x = context.DirtyRect.X;
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Draw(context with
                {
                    DirtyRect = new RectF(
                        x,
                        context.DirtyRect.Top,
                        columnWidths[i],
                        context.DirtyRect.Height
                        )
                });

                x += columnWidths[i];
            }

            base.OnDraw(context);
        }
    }

    public class Box : CanvasNode
    {
        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(Box), null);

        public Color? BackgroundColor
        {
            get => (Color?)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(Box), null);

        public Color? BorderColor
        {
            get => (Color?)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(Box), 0.0f,
            coerceValue: (BindableObject bindable, object value) => Math.Max((float)value, 0.0f));

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty BorderSizeProperty = BindableProperty.Create(nameof(BorderSize), typeof(float), typeof(Box), 1.0f,
            coerceValue: (BindableObject bindable, object value) => Math.Max((float)value, 0.0f));

        public float BorderSize
        {
            get => (float)GetValue(BorderSizeProperty);
            set => SetValue(BorderSizeProperty, value);
        }

        public CanvasNode? Child { get; set; }

        protected override void OnDraw(DrawingContext context)
        {
            var canvas = context.Canvas;
            var dirtyRect = context.DirtyRect;

            var fillColor = BackgroundColor;
            var strokeColor = BorderColor;
            var corderRadius = CornerRadius;
            var borderSize = BorderSize;

            canvas.SaveState();

            if (corderRadius > 0.0f)
            {
                if (fillColor != null)
                {
                    canvas.FillColor = fillColor;
                    canvas.FillRoundedRectangle(dirtyRect, corderRadius);
                }

                Child?.Draw(context);

                if (strokeColor != null)
                {
                    canvas.StrokeColor = strokeColor;
                    canvas.StrokeSize = borderSize;
                    canvas.DrawRoundedRectangle(dirtyRect, corderRadius);
                }
            }
            else
            {
                if (fillColor != null)
                {
                    canvas.FillColor = fillColor;
                    canvas.FillRectangle(dirtyRect);
                }

                Child?.Draw(context);

                if (strokeColor != null)
                {
                    canvas.StrokeColor = strokeColor;
                    canvas.StrokeSize = borderSize;
                    canvas.DrawRectangle(dirtyRect);
                }
            }

            canvas.RestoreState();

            base.OnDraw(context);
        }
    }

    public class Align : CanvasNode
    {
        public static readonly BindableProperty WidthProperty = BindableProperty.Create(nameof(Width), typeof(float), typeof(Align), float.NaN,
            coerceValue: (BindableObject bindable, object value) => float.IsNaN((float)value) ? float.NaN : Math.Max(((float)value), 0.0f));

        public float Width
        {
            get => (float)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        public static readonly BindableProperty HeightProperty = BindableProperty.Create(nameof(Height), typeof(float), typeof(Align), float.NaN,
            coerceValue: (BindableObject bindable, object value) => float.IsNaN((float)value) ? float.NaN : Math.Max(((float)value), 0.0f));

        public float Height
        {
            get => (float)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        public static readonly BindableProperty HorizontalAlignmentProperty = BindableProperty.Create(nameof(HorizontalAlignment), typeof(Microsoft.Maui.Primitives.LayoutAlignment),  typeof(Align), Microsoft.Maui.Primitives.LayoutAlignment.Fill);

        public Microsoft.Maui.Primitives.LayoutAlignment HorizontalAlignment
        {
            get => (Microsoft.Maui.Primitives.LayoutAlignment)GetValue(HorizontalAlignmentProperty);
            set => SetValue(HorizontalAlignmentProperty, value);
        }

        public static readonly BindableProperty VerticalAlignmentProperty = BindableProperty.Create(nameof(VerticalAlignment), typeof(Microsoft.Maui.Primitives.LayoutAlignment), typeof(Align), Microsoft.Maui.Primitives.LayoutAlignment.Fill);

        public Microsoft.Maui.Primitives.LayoutAlignment VerticalAlignment
        {
            get => (Microsoft.Maui.Primitives.LayoutAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }

        public CanvasNode? Child { get; set; }

        protected override void OnDraw(DrawingContext context)
        {
            if (Child != null)
            {
                if (HorizontalAlignment != Microsoft.Maui.Primitives.LayoutAlignment.Fill &&
                    !float.IsNaN(Width))
                {
                    switch (HorizontalAlignment)
                    {
                        case Microsoft.Maui.Primitives.LayoutAlignment.Start:
                            context = context with
                            {
                                DirtyRect = new RectF(
                                    context.DirtyRect.X,
                                    context.DirtyRect.Y,
                                    Width,
                                    context.DirtyRect.Height)
                            };
                            break;
                        case Microsoft.Maui.Primitives.LayoutAlignment.Center:
                            context = context with
                            {
                                DirtyRect = new RectF(
                                    context.DirtyRect.X + (context.DirtyRect.Width - Width) / 2.0f,
                                    context.DirtyRect.Y,
                                    Width,
                                    context.DirtyRect.Height)
                            };
                            break;
                        case Microsoft.Maui.Primitives.LayoutAlignment.End:
                            context = context with
                            {
                                DirtyRect = new RectF(
                                    context.DirtyRect.X + (context.DirtyRect.Width - Width),
                                    context.DirtyRect.Y,
                                    Width,
                                    context.DirtyRect.Height)
                            };
                            break;
                    }
                }

                if (VerticalAlignment != Microsoft.Maui.Primitives.LayoutAlignment.Fill &&
                    !float.IsNaN(Height))
                {
                    switch (VerticalAlignment)
                    {
                        case Microsoft.Maui.Primitives.LayoutAlignment.Start:
                            context = context with
                            {
                                DirtyRect = new RectF(
                                    context.DirtyRect.X,
                                    context.DirtyRect.Y,
                                    context.DirtyRect.Width,
                                    Height)
                            };
                            break;
                        case Microsoft.Maui.Primitives.LayoutAlignment.Center:
                            context = context with
                            {
                                DirtyRect = new RectF(
                                    context.DirtyRect.X,
                                    context.DirtyRect.Y + (context.DirtyRect.Height - Height) / 2.0f,
                                    context.DirtyRect.Width,
                                    Height)
                            };
                            break;
                        case Microsoft.Maui.Primitives.LayoutAlignment.End:
                            context = context with
                            {
                                DirtyRect = new RectF(
                                    context.DirtyRect.X,
                                    context.DirtyRect.Y + (context.DirtyRect.Height - Height),
                                    context.DirtyRect.Width,
                                    Height)
                            };
                            break;
                    }
                }


                Child.Draw(context);
            }

            base.OnDraw(context);
        }
    }

    public class Text : CanvasNode
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(Text), null);

        public string? Value
        {
            get => (string?)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty VerticalAlignmentProperty = BindableProperty.Create(nameof(VerticalAlignment), typeof(VerticalAlignment), typeof(Text), VerticalAlignment.Top);

        public VerticalAlignment VerticalAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }

        public static readonly BindableProperty HorizontalAlignmentProperty = BindableProperty.Create(nameof(HorizontalAlignment), typeof(HorizontalAlignment), typeof(Text), Microsoft.Maui.Graphics.HorizontalAlignment.Left);

        public HorizontalAlignment HorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalAlignmentProperty);
            set => SetValue(HorizontalAlignmentProperty, value);
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSizeProperty), typeof(float), typeof(Text), 12.0f,
            coerceValue: (bindableObject, value) => ((float)value) <= 0.0f ? 12.0f : (float)value);

        public float FontSize
        {
            get => (float)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static readonly BindableProperty FontColorProperty = BindableProperty.Create(nameof(FontColorProperty), typeof(Color), typeof(Text), null);

        public Color? FontColor
        {
            get => (Color?)GetValue(FontColorProperty);
            set => SetValue(FontColorProperty, value);
        }

        public static readonly BindableProperty FontProperty = BindableProperty.Create(nameof(FontProperty), typeof(IFont), typeof(Text), Microsoft.Maui.Graphics.Font.Default);

        public IFont? Font
        {
            get => (IFont?)GetValue(FontProperty);
            set => SetValue(FontProperty, value);
        }

        protected override void OnDraw(DrawingContext context)
        {
            if (Value != null)
            {
                var canvas = context.Canvas;
                var dirtyRect = context.DirtyRect;

                canvas.SaveState();

                canvas.FontSize = FontSize;
                if (FontColor != null)
                {
                    canvas.FontColor = FontColor;
                }
                if (Font != null)
                {
                    canvas.Font = Font;
                }

                canvas.DrawString(Value, dirtyRect, HorizontalAlignment, VerticalAlignment);

                canvas.RestoreState();
            }

            base.OnDraw(context);
        }
    }

    public class Picture : CanvasNode
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(Microsoft.Maui.Graphics.IImage), typeof(Picture), null);

        public Microsoft.Maui.Graphics.IImage? Source
        {
            get => (Microsoft.Maui.Graphics.IImage?)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        
        public static readonly BindableProperty AspectProperty = BindableProperty.Create(nameof(Source), typeof(Aspect), typeof(Picture), Aspect.AspectFit);

        public Aspect Aspect
        {
            get => (Aspect)GetValue(AspectProperty);
            set => SetValue(AspectProperty, value);
        }

        protected override void OnDraw(DrawingContext context)
        {
            if (Source != null)
            {
                var canvas = context.Canvas;
                var dirtyRect = context.DirtyRect;

                canvas.SaveState();

                canvas.DrawImage(Source, dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height);

                canvas.RestoreState();
            }

            base.OnDraw(context);
        }
    }

}