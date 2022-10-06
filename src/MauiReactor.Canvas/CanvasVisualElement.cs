using MauiReactor.Internals;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{
    public partial interface ICanvasVisualElement : IVisualNode
    {
        PropertyValue<ThicknessF>? Margin { get; set; }
        PropertyValue<Vector2>? Translation { get; set; }
        PropertyValue<Vector2>? Scale { get; set; }
        PropertyValue<float>? Rotation { get; set; }
    }

    public partial class CanvasVisualElement<T> : VisualNode<T>, ICanvasVisualElement where T : Internals.CanvasVisualElement, new()
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
        PropertyValue<float>? ICanvasVisualElement.Rotation { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsICanvasVisualElement = (ICanvasVisualElement)this;

            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.MarginProperty, thisAsICanvasVisualElement.Margin);
            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.TranslationProperty, thisAsICanvasVisualElement.Translation);
            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.ScaleProperty, thisAsICanvasVisualElement.Scale);
            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.RotationProperty, thisAsICanvasVisualElement.Rotation);

            base.OnUpdate();
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
        public static T Margin<T>(this T node, ThicknessF value) where T : ICanvasVisualElement
        {
            node.Margin = new PropertyValue<ThicknessF>(value);
            return node;
        }

        public static T Margin<T>(this T node, Func<ThicknessF> valueFunc) where T : ICanvasVisualElement
        {
            node.Margin = new PropertyValue<ThicknessF>(valueFunc);
            return node;
        }

        public static T Translate<T>(this T node, Vector2 value) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(value);
            return node;
        }

        public static T Translate<T>(this T node, float x, float y) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(new Vector2(x, y));
            return node;
        }

        public static T Translation<T>(this T node, Func<Vector2> valueFunc) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(valueFunc);
            return node;
        }

        public static T TranslateX<T>(this T node, float value) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(new Vector2(value, 0.0f));
            return node;
        }

        public static T TranslateX<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(() => new Vector2(valueFunc(), 0.0f));
            return node;
        }

        public static T TranslateY<T>(this T node, float value) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(new Vector2(0.0f, value));
            return node;
        }

        public static T TranslateY<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Translation = new PropertyValue<Vector2>(() => new Vector2(0.0f, valueFunc()));
            return node;
        }

        public static T ScaleX<T>(this T node, float value) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(new Vector2(value, 0.0f));
            return node;
        }

        public static T ScaleX<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(() => new Vector2(valueFunc(), 0.0f));
            return node;
        }

        public static T ScaleY<T>(this T node, float value) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(new Vector2(0.0f, value));
            return node;
        }

        public static T ScaleY<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(() => new Vector2(0.0f, valueFunc()));
            return node;
        }

        public static T Scale<T>(this T node, Vector2 value) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(value);
            return node;
        }

        public static T Scale<T>(this T node, float x, float y) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(new Vector2(x, y));
            return node;
        }

        public static T Scale<T>(this T node, Func<Vector2> valueFunc) where T : ICanvasVisualElement
        {
            node.Scale = new PropertyValue<Vector2>(valueFunc);
            return node;
        }

        public static T Rotate<T>(this T node, float value) where T : ICanvasVisualElement
        {
            node.Rotation = new PropertyValue<float>(value);
            return node;
        }

        public static T Rotate<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
        {
            node.Rotation = new PropertyValue<float>(valueFunc);
            return node;
        }

    }

}
