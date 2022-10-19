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
    public partial interface IDropShadow : ICanvasNode
    {
        PropertyValue<Color>? Color { get; set; }
        PropertyValue<SizeF>? Size { get; set; }
        PropertyValue<float>? Blur { get; set; }
    }

    public partial class DropShadow<T> : CanvasNode<T>, IDropShadow, IEnumerable where T : Internals.DropShadow, new()
    {

        public DropShadow()
        {

        }

        public DropShadow(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Color>? IDropShadow.Color { get; set; }
        PropertyValue<SizeF>? IDropShadow.Size { get; set; }
        PropertyValue<float>? IDropShadow.Blur { get; set; }


        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBorder = (IDropShadow)this;

            SetPropertyValue(NativeControl, Internals.DropShadow.ColorProperty, thisAsIBorder.Color);
            SetPropertyValue(NativeControl, Internals.DropShadow.SizeProperty, thisAsIBorder.Size);
            SetPropertyValue(NativeControl, Internals.DropShadow.BlurProperty, thisAsIBorder.Blur);

            base.OnUpdate();
        }

        protected override void OnAnimate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBorder = (IDropShadow)this;

            SetPropertyValue(NativeControl, Internals.DropShadow.ColorProperty, thisAsIBorder.Color);
            SetPropertyValue(NativeControl, Internals.DropShadow.SizeProperty, thisAsIBorder.Size);
            SetPropertyValue(NativeControl, Internals.DropShadow.BlurProperty, thisAsIBorder.Blur);

            base.OnAnimate();
        }
    }

    public partial class DropShadow : DropShadow<Internals.DropShadow>
    {
        public DropShadow()
        {

        }

        public DropShadow(Action<Internals.DropShadow?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class DropShadowExtensions
    {
        public static T Color<T>(this T node, Color value, RxColorAnimation? customAnimation = null) where T : IDropShadow
        {
            node.Color = new PropertyValue<Color>(value);
            node.AppendAnimatable(Internals.DropShadow.ColorProperty, customAnimation ?? new RxSimpleColorAnimation(value), v => node.Color = new PropertyValue<Color>(v.CurrentValue()));
            return node;
        }

        public static T Color<T>(this T node, Func<Color> valueFunc) where T : IDropShadow
        {
            node.Color = new PropertyValue<Color>(valueFunc);
            return node;
        }

        public static T Size<T>(this T node, SizeF value, RxSizeFAnimation? customAnimation = null) where T : IDropShadow
        {
            node.Size = new PropertyValue<SizeF>(value);
            node.AppendAnimatable(Internals.DropShadow.SizeProperty, customAnimation ?? new RxSimpleSizeFAnimation(value), v => node.Size = new PropertyValue<SizeF>(v.CurrentValue()));
            return node;
        }

        public static T Size<T>(this T node, float x, float y, RxSizeFAnimation? customAnimation = null) where T : IDropShadow
        {
            node.Size = new PropertyValue<SizeF>(new SizeF(x, y));
            node.AppendAnimatable(Internals.DropShadow.SizeProperty, customAnimation ?? new RxSimpleSizeFAnimation(new SizeF(x, y)), v => node.Size = new PropertyValue<SizeF>(v.CurrentValue()));
            return node;
        }

        public static T Size<T>(this T node, Func<SizeF> valueFunc) where T : IDropShadow
        {
            node.Size = new PropertyValue<SizeF>(valueFunc);
            return node;
        }

        public static T Blur<T>(this T node, float value, RxFloatAnimation? customAnimation = null) where T : IDropShadow
        {
            node.Blur = new PropertyValue<float>(value);
            node.AppendAnimatable(Internals.DropShadow.BlurProperty, customAnimation ?? new RxFloatAnimation(value), v => node.Blur = new PropertyValue<float>(v.CurrentValue()));
            return node;
        }

        public static T Blur<T>(this T node, Func<float> valueFunc) where T : IDropShadow
        {
            node.Blur = new PropertyValue<float>(valueFunc);
            return node;
        }
    }
}
