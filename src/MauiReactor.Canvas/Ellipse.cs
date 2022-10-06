using MauiReactor.Canvas.Internals;
using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{
    public partial interface IEllipse : ICanvasVisualElement
    {
        PropertyValue<Color?>? FillColor { get; set; }
        PropertyValue<Color?>? StrokeColor { get; set; }
        PropertyValue<float>? StrokeSize { get; set; }
    }

    public partial class Ellipse<T> : CanvasVisualElement<T>, IEllipse, IEnumerable where T : Internals.Ellipse, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public Ellipse()
        {

        }

        public Ellipse(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Color?>? IEllipse.FillColor { get; set; }
        PropertyValue<Color?>? IEllipse.StrokeColor { get; set; }
        PropertyValue<float>? IEllipse.StrokeSize { get; set; }

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

            if (childControl is Internals.CanvasVisualElement node)
            {
                NativeControl.Child = node;
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasVisualElement node &&
                node == NativeControl.Child)
            {
                NativeControl.Child = null;
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBorder = (IEllipse)this;

            SetPropertyValue(NativeControl, Internals.Ellipse.FillColorProperty, thisAsIBorder.FillColor);
            SetPropertyValue(NativeControl, Internals.Ellipse.StrokeColorProperty, thisAsIBorder.StrokeColor);
            SetPropertyValue(NativeControl, Internals.Ellipse.StrokeSizeProperty, thisAsIBorder.StrokeSize);

            base.OnUpdate();
        }
    }

    public partial class Ellipse : Ellipse<Internals.Ellipse>
    {
        public Ellipse()
        {

        }

        public Ellipse(Action<Internals.Ellipse?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class EllipseExtensions
    {
        public static T FillColor<T>(this T node, Color? value) where T : IEllipse
        {
            node.FillColor = new PropertyValue<Color?>(value);
            return node;
        }

        public static T FillColor<T>(this T node, Func<Color?> valueFunc) where T : IEllipse
        {
            node.FillColor = new PropertyValue<Color?>(valueFunc);
            return node;
        }

        public static T StrokeColor<T>(this T node, Color? value) where T : IEllipse
        {
            node.StrokeColor = new PropertyValue<Color?>(value);
            return node;
        }

        public static T StrokeColor<T>(this T node, Func<Color?> valueFunc) where T : IEllipse
        {
            node.StrokeColor = new PropertyValue<Color?>(valueFunc);
            return node;
        }

        public static T StrokeSize<T>(this T node, float value) where T : IEllipse
        {
            node.StrokeSize = new PropertyValue<float>(value);
            return node;
        }

        public static T StrokeSize<T>(this T node, Func<float> valueFunc) where T : IEllipse
        {
            node.StrokeSize = new PropertyValue<float>(valueFunc);
            return node;
        }

    }
}
