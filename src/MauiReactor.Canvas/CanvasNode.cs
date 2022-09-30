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
    public partial interface ICanvasNode : IVisualNode
    {
        PropertyValue<Thickness>? Margin { get; set; }
        PropertyValue<Vector2>? Translation { get; set; }
        PropertyValue<Vector2>? Scale { get; set; }
        PropertyValue<float>? Rotation { get; set; }
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
        PropertyValue<Vector2>? ICanvasNode.Translation { get; set; }
        PropertyValue<Vector2>? ICanvasNode.Scale { get; set; }
        PropertyValue<float>? ICanvasNode.Rotation { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsICanvasNode = (ICanvasNode)this;

            SetPropertyValue(NativeControl, Internals.CanvasNode.MarginProperty, thisAsICanvasNode.Margin);
            SetPropertyValue(NativeControl, Internals.CanvasNode.TranslationProperty, thisAsICanvasNode.Translation);
            SetPropertyValue(NativeControl, Internals.CanvasNode.ScaleProperty, thisAsICanvasNode.Scale);
            SetPropertyValue(NativeControl, Internals.CanvasNode.RotationProperty, thisAsICanvasNode.Rotation);

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
        public static T Margin<T>(this T node, Thickness value) where T : ICanvasNode
        {
            node.Margin = new PropertyValue<Thickness>(value);
            return node;
        }

        public static T Margin<T>(this T node, Func<Thickness> valueFunc) where T : ICanvasNode
        {
            node.Margin = new PropertyValue<Thickness>(valueFunc);
            return node;
        }

        public static T Translate<T>(this T node, Vector2 value) where T : ICanvasNode
        {
            node.Translation = new PropertyValue<Vector2>(value);
            return node;
        }

        public static T Translate<T>(this T node, float x, float y) where T : ICanvasNode
        {
            node.Translation = new PropertyValue<Vector2>(new Vector2(x, y));
            return node;
        }

        public static T Translation<T>(this T node, Func<Vector2> valueFunc) where T : ICanvasNode
        {
            node.Translation = new PropertyValue<Vector2>(valueFunc);
            return node;
        }

        public static T TranslateX<T>(this T node, float value) where T : ICanvasNode
        {
            node.Translation = new PropertyValue<Vector2>(new Vector2(value, 0.0f));
            return node;
        }

        public static T TranslateX<T>(this T node, Func<float> valueFunc) where T : ICanvasNode
        {
            node.Translation = new PropertyValue<Vector2>(()=> new Vector2(valueFunc(), 0.0f));
            return node;
        }

        public static T TranslateY<T>(this T node, float value) where T : ICanvasNode
        {
            node.Translation = new PropertyValue<Vector2>(new Vector2(0.0f, value));
            return node;
        }

        public static T TranslateY<T>(this T node, Func<float> valueFunc) where T : ICanvasNode
        {
            node.Translation = new PropertyValue<Vector2>(() => new Vector2(0.0f, valueFunc()));
            return node;
        }

        public static T ScaleX<T>(this T node, float value) where T : ICanvasNode
        {
            node.Scale = new PropertyValue<Vector2>(new Vector2(value, 0.0f));
            return node;
        }

        public static T ScaleX<T>(this T node, Func<float> valueFunc) where T : ICanvasNode
        {
            node.Scale = new PropertyValue<Vector2>(() => new Vector2(valueFunc(), 0.0f));
            return node;
        }

        public static T ScaleY<T>(this T node, float value) where T : ICanvasNode
        {
            node.Scale = new PropertyValue<Vector2>(new Vector2(0.0f, value));
            return node;
        }

        public static T ScaleY<T>(this T node, Func<float> valueFunc) where T : ICanvasNode
        {
            node.Scale = new PropertyValue<Vector2>(() => new Vector2(0.0f, valueFunc()));
            return node;
        }

        public static T Scale<T>(this T node, Vector2 value) where T : ICanvasNode
        {
            node.Scale = new PropertyValue<Vector2>(value);
            return node;
        }

        public static T Scale<T>(this T node, float x, float y) where T : ICanvasNode
        {
            node.Scale = new PropertyValue<Vector2>(new Vector2(x, y));
            return node;
        }

        public static T Scale<T>(this T node, Func<Vector2> valueFunc) where T : ICanvasNode
        {
            node.Scale = new PropertyValue<Vector2>(valueFunc);
            return node;
        }

        public static T Rotate<T>(this T node, float value) where T : ICanvasNode
        {
            node.Rotation = new PropertyValue<float>(value);
            return node;
        }

        public static T Rotate<T>(this T node, Func<float> valueFunc) where T : ICanvasNode
        {
            node.Rotation = new PropertyValue<float>(valueFunc);
            return node;
        }

    }
}
