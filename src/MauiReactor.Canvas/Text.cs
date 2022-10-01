using MauiReactor.Internals;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            node.Font = new PropertyValue<IFont?>(() =>
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
