using MauiReactor.Animations;
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

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBorder = (IEllipse)this;

            SetPropertyValue(NativeControl, Internals.Ellipse.FillColorProperty, thisAsIBorder.FillColor);
            SetPropertyValue(NativeControl, Internals.Ellipse.StrokeColorProperty, thisAsIBorder.StrokeColor);
            SetPropertyValue(NativeControl, Internals.Ellipse.StrokeSizeProperty, thisAsIBorder.StrokeSize);

            base.OnUpdate();
        }

        protected override void OnAnimate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBorder = (IEllipse)this;

            SetPropertyValue(NativeControl, Internals.Ellipse.FillColorProperty, thisAsIBorder.FillColor);
            SetPropertyValue(NativeControl, Internals.Ellipse.StrokeColorProperty, thisAsIBorder.StrokeColor);
            SetPropertyValue(NativeControl, Internals.Ellipse.StrokeSizeProperty, thisAsIBorder.StrokeSize);

            base.OnAnimate();
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
        public static T FillColor<T>(this T node, Color? value, RxColorAnimation? customAnimation = null) where T : IEllipse
        {
            node.FillColor = new PropertyValue<Color?>(value);
            if (value != null)
            {
                node.AppendAnimatable(Internals.Ellipse.FillColorProperty, customAnimation ?? new RxSimpleColorAnimation(value), v => node.FillColor = new PropertyValue<Color?>(v.CurrentValue()));
            }
            return node;
        }

        public static T FillColor<T>(this T node, Func<Color?> valueFunc) where T : IEllipse
        {
            node.FillColor = new PropertyValue<Color?>(valueFunc);
            return node;
        }

        public static T StrokeColor<T>(this T node, Color? value, RxColorAnimation? customAnimation = null) where T : IEllipse
        {
            node.StrokeColor = new PropertyValue<Color?>(value);
            if (value != null)
            {
                node.AppendAnimatable(Internals.Ellipse.StrokeColorProperty, customAnimation ?? new RxSimpleColorAnimation(value), v => node.StrokeColor = new PropertyValue<Color?>(v.CurrentValue()));
            }
            return node;
        }

        public static T StrokeColor<T>(this T node, Func<Color?> valueFunc) where T : IEllipse
        {
            node.StrokeColor = new PropertyValue<Color?>(valueFunc);
            return node;
        }

        public static T StrokeSize<T>(this T node, float value, RxFloatAnimation? customAnimation = null) where T : IEllipse
        {
            node.StrokeSize = new PropertyValue<float>(value);
            node.AppendAnimatable(Internals.Ellipse.StrokeSizeProperty, customAnimation ?? new RxFloatAnimation(value), v => node.StrokeSize = new PropertyValue<float>(v.CurrentValue()));
            return node;
        }

        public static T StrokeSize<T>(this T node, Func<float> valueFunc) where T : IEllipse
        {
            node.StrokeSize = new PropertyValue<float>(valueFunc);
            return node;
        }

    }
}
