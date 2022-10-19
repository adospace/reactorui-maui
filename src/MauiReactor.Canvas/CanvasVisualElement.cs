using MauiReactor.Animations;
using MauiReactor.Internals;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MauiReactor.Canvas
{
    public partial interface ICanvasVisualElement : ICanvasNode
    {
        PropertyValue<ThicknessF>? Margin { get; set; }
        PropertyValue<Vector2>? Translation { get; set; }
        PropertyValue<Vector2>? Scale { get; set; }
        PropertyValue<Vector2>? Anchor { get; set; }
        PropertyValue<float>? Rotation { get; set; }
    }

    public partial class CanvasVisualElement<T> : CanvasNode<T>, ICanvasVisualElement where T : Internals.CanvasVisualElement, new()
    {
        public CanvasVisualElement()
        {

        }

        public CanvasVisualElement(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<ThicknessF>? ICanvasVisualElement.Margin { get; set; }
        PropertyValue<Vector2>? ICanvasVisualElement.Translation { get; set; }
        PropertyValue<Vector2>? ICanvasVisualElement.Scale { get; set; }
        PropertyValue<Vector2>? ICanvasVisualElement.Anchor { get; set; }
        PropertyValue<float>? ICanvasVisualElement.Rotation { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsICanvasVisualElement = (ICanvasVisualElement)this;

            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.MarginProperty, thisAsICanvasVisualElement.Margin);
            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.TranslationProperty, thisAsICanvasVisualElement.Translation);
            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.ScaleProperty, thisAsICanvasVisualElement.Scale);
            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.AnchorProperty, thisAsICanvasVisualElement.Anchor);
            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.RotationProperty, thisAsICanvasVisualElement.Rotation);

            base.OnUpdate();
        }

        protected override void OnAnimate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsICanvasVisualElement = (ICanvasVisualElement)this;

            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.MarginProperty, thisAsICanvasVisualElement.Margin);
            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.TranslationProperty, thisAsICanvasVisualElement.Translation);
            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.ScaleProperty, thisAsICanvasVisualElement.Scale);
            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.RotationProperty, thisAsICanvasVisualElement.Rotation);

            base.OnAnimate();
        }
    }

    public partial class CanvasVisualElement : CanvasVisualElement<Internals.CanvasVisualElement>
    {
        public CanvasVisualElement()
        {

        }

        public CanvasVisualElement(Action<Internals.CanvasVisualElement?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class CanvasVisualElementExtensions
    {
        public static T Margin<T>(this T node, ThicknessF value, RxThicknessFAnimation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Margin = new PropertyValue<ThicknessF>(value);
            node.AppendAnimatable(Internals.CanvasVisualElement.MarginProperty, customAnimation ?? new RxSimpleThicknessFAnimation(value), v => node.Margin = new PropertyValue<ThicknessF>(v.CurrentValue()));
            return node;
        }

        public static T Margin<T>(this T node, Func<ThicknessF> valueFunc) where T : ICanvasVisualElement
        {
            node.Margin = new PropertyValue<ThicknessF>(valueFunc);
            return node;
        }

        public static T Margin<T>(this T node, float leftRight, float topBottom, RxThicknessFAnimation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Margin = new PropertyValue<ThicknessF>(new ThicknessF(leftRight, topBottom));
            node.AppendAnimatable(Internals.CanvasVisualElement.MarginProperty, customAnimation ?? new RxSimpleThicknessFAnimation(new ThicknessF(leftRight, topBottom)), v => node.Margin = new PropertyValue<ThicknessF>(v.CurrentValue()));
            return node;
        }
        public static T Margin<T>(this T node, float uniformSize, RxThicknessFAnimation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Margin = new PropertyValue<ThicknessF>(new ThicknessF(uniformSize));
            node.AppendAnimatable(Internals.CanvasVisualElement.MarginProperty, customAnimation ?? new RxSimpleThicknessFAnimation(new ThicknessF(uniformSize)), v => node.Margin = new PropertyValue<ThicknessF>(v.CurrentValue()));
            return node;
        }
        public static T Margin<T>(this T node, float left, float top, float right, float bottom, RxThicknessFAnimation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Margin = new PropertyValue<ThicknessF>(new ThicknessF(left, top, right, bottom));
            node.AppendAnimatable(Internals.CanvasVisualElement.MarginProperty, customAnimation ?? new RxSimpleThicknessFAnimation(new ThicknessF(left, top, right, bottom)), v => node.Margin = new PropertyValue<ThicknessF>(v.CurrentValue()));
            return node;
        }

        public static T Translate<T>(this T node, Vector2 value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(value);
            node.AppendAnimatable(Internals.CanvasVisualElement.TranslationProperty, customAnimation ?? new RxSimpleVector2Animation(value), v => node.Translation = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T Translate<T>(this T node, float x, float y, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(new Vector2(x, y));
            node.AppendAnimatable(Internals.CanvasVisualElement.TranslationProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(x, y)), v => node.Translation = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T Translation<T>(this T node, Func<Vector2> valueFunc) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(valueFunc);
            return node;
        }

        public static T TranslateX<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(new Vector2(value, 0.0f));
            node.AppendAnimatable(Internals.CanvasVisualElement.TranslationProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(value, 0.0f)), v => node.Translation = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T TranslateX<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(() => new Vector2(valueFunc(), 0.0f));
            return node;
        }

        public static T TranslateY<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(new Vector2(0.0f, value));
            node.AppendAnimatable(Internals.CanvasVisualElement.TranslationProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(0.0f, value)), v => node.Translation = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T TranslateY<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(() => new Vector2(0.0f, valueFunc()));
            return node;
        }

        public static T ScaleX<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(new Vector2(value, 0.0f));
            node.AppendAnimatable(Internals.CanvasVisualElement.ScaleProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(value, 0.0f)), v => node.Scale = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T ScaleX<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(() => new Vector2(valueFunc(), 0.0f));
            return node;
        }

        public static T ScaleY<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(new Vector2(0.0f, value));
            node.AppendAnimatable(Internals.CanvasVisualElement.ScaleProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(0.0f, value)), v => node.Scale = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T ScaleY<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(() => new Vector2(0.0f, valueFunc()));
            return node;
        }

        public static T Scale<T>(this T node, Vector2 value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(value);
            node.AppendAnimatable(Internals.CanvasVisualElement.ScaleProperty, customAnimation ?? new RxSimpleVector2Animation(value), v => node.Scale = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T Scale<T>(this T node, float x, float y, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(new Vector2(x, y));
            node.AppendAnimatable(Internals.CanvasVisualElement.ScaleProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(x, y)), v => node.Scale = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T Scale<T>(this T node, Func<Vector2> valueFunc) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(valueFunc);
            return node;
        }

        public static T Rotate<T>(this T node, float value, RxFloatAnimation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Rotation = new PropertyValue<float>(value);
            node.AppendAnimatable(Internals.CanvasVisualElement.RotationProperty, customAnimation ?? new RxFloatAnimation(value), v => node.Rotation = new PropertyValue<float>(v.CurrentValue()));
            return node;
        }

        public static T Rotate<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Rotation = new PropertyValue<float>(valueFunc);
            return node;
        }

        public static T AnchorX<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Anchor = new PropertyValue<Vector2>(new Vector2(value, 0.0f));
            node.AppendAnimatable(Internals.CanvasVisualElement.AnchorProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(value, 0.0f)), v => node.Anchor = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T AnchorX<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Anchor = new PropertyValue<Vector2>(() => new Vector2(valueFunc(), 0.0f));
            return node;
        }

        public static T AnchorY<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Anchor = new PropertyValue<Vector2>(new Vector2(0.0f, value));
            node.AppendAnimatable(Internals.CanvasVisualElement.AnchorProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(0.0f, value)), v => node.Anchor = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T AnchorY<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Anchor = new PropertyValue<Vector2>(() => new Vector2(0.0f, valueFunc()));
            return node;
        }

        public static T Anchor<T>(this T node, Vector2 value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Anchor = new PropertyValue<Vector2>(value);
            node.AppendAnimatable(Internals.CanvasVisualElement.AnchorProperty, customAnimation ?? new RxSimpleVector2Animation(value), v => node.Anchor = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T Anchor<T>(this T node, float x, float y, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
        {
            node.Anchor = new PropertyValue<Vector2>(new Vector2(x, y));
            node.AppendAnimatable(Internals.CanvasVisualElement.AnchorProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(x, y)), v => node.Anchor = new PropertyValue<Vector2>(v.CurrentValue()));
            return node;
        }

        public static T Anchor<T>(this T node, Func<Vector2> valueFunc) where T : ICanvasVisualElement
        {
            node.Anchor = new PropertyValue<Vector2>(valueFunc);
            return node;
        }
    }

}
