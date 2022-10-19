using MauiReactor.Animations;
using MauiReactor.Canvas.Internals;
using MauiReactor.Internals;
using Microsoft.Maui;
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
    public partial interface IClipRectangle : ICanvasNode
    {
        PropertyValue<CornerRadiusF>? CornerRadius { get; set; }
    }

    public partial class ClipRectangle<T> : CanvasNode<T>, IClipRectangle, IEnumerable where T : Internals.ClipRectangle, new()
    {
        public ClipRectangle()
        {

        }

        public ClipRectangle(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<CornerRadiusF>? IClipRectangle.CornerRadius { get; set; }


        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBorder = (IClipRectangle)this;

            SetPropertyValue(NativeControl, Internals.ClipRectangle.CornerRadiusProperty, thisAsIBorder.CornerRadius);

            base.OnUpdate();
        }

        protected override void OnAnimate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBorder = (IClipRectangle)this;

            SetPropertyValue(NativeControl, Internals.ClipRectangle.CornerRadiusProperty, thisAsIBorder.CornerRadius);

            base.OnAnimate();
        }
    }

    public partial class ClipRectangle : ClipRectangle<Internals.ClipRectangle>
    {
        public ClipRectangle()
        {

        }

        public ClipRectangle(Action<Internals.ClipRectangle?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ClipRectangleExtensions
    {
        public static T CornerRadius<T>(this T node, CornerRadiusF value, RxCornerRadiusFAnimation? customAnimation = null) where T : IClipRectangle
        {
            node.CornerRadius = new PropertyValue<CornerRadiusF>(value);
            node.AppendAnimatable(Internals.Box.CornerRadiusProperty, customAnimation ?? new RxSimpleCornerRadiusFAnimation(value), v => node.CornerRadius = new PropertyValue<CornerRadiusF>(v.CurrentValue()));
            return node;
        }

        public static T CornerRadius<T>(this T node, float topLeft, float topRight, float bottomRight, float bottomLeft, RxCornerRadiusFAnimation? customAnimation = null) where T : IClipRectangle
        {
            node.CornerRadius = new PropertyValue<CornerRadiusF>(new CornerRadiusF(topLeft, topRight, bottomRight, bottomLeft));
            node.AppendAnimatable(Internals.Box.CornerRadiusProperty, customAnimation ?? new RxSimpleCornerRadiusFAnimation(new CornerRadiusF(topLeft, topRight, bottomRight, bottomLeft)), v => node.CornerRadius = new PropertyValue<CornerRadiusF>(v.CurrentValue()));
            return node;
        }

        public static T CornerRadius<T>(this T node, float uniformSize, RxCornerRadiusFAnimation? customAnimation = null) where T : IClipRectangle
        {
            node.CornerRadius = new PropertyValue<CornerRadiusF>(new CornerRadiusF(uniformSize));
            node.AppendAnimatable(Internals.Box.CornerRadiusProperty, customAnimation ?? new RxSimpleCornerRadiusFAnimation(new CornerRadiusF(uniformSize)), v => node.CornerRadius = new PropertyValue<CornerRadiusF>(v.CurrentValue()));
            return node;
        }

        public static T CornerRadius<T>(this T node, Func<CornerRadiusF> valueFunc) where T : IClipRectangle
        {
            node.CornerRadius = new PropertyValue<CornerRadiusF>(valueFunc);
            return node;
        }
    }

}
