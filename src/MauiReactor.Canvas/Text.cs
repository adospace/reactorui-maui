using MauiReactor.Internals;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{
    public partial interface IText : ICanvasVisualElement
    {
        PropertyValue<VerticalAlignment>? VerticalAlignment { get; set; }
        PropertyValue<HorizontalAlignment>? HorizontalAlignment { get; set; }
        PropertyValue<string>? Value { get; set; }
        PropertyValue<float>? FontSize { get; set; }
        PropertyValue<Color?>? FontColor { get; set; }
        PropertyValue<string?>? FontName { get; set; }
        PropertyValue<int>? FontWeight { get; set; }
        PropertyValue<FontStyleType>? FontStyle { get; set; }
    }

    public partial class Text<T> : CanvasVisualElement<T>, IText where T : Internals.Text, new()
    {
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
        PropertyValue<string?>? IText.FontName { get; set; }
        PropertyValue<int>? IText.FontWeight { get; set; }
        PropertyValue<FontStyleType>? IText.FontStyle { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIText = (IText)this;

            SetPropertyValue(NativeControl, Internals.Text.HorizontalAlignmentProperty, thisAsIText.HorizontalAlignment);
            SetPropertyValue(NativeControl, Internals.Text.VerticalAlignmentProperty, thisAsIText.VerticalAlignment);
            SetPropertyValue(NativeControl, Internals.Text.ValueProperty, thisAsIText.Value);
            SetPropertyValue(NativeControl, Internals.Text.FontSizeProperty, thisAsIText.FontSize);
            SetPropertyValue(NativeControl, Internals.Text.FontColorProperty, thisAsIText.FontColor);
            SetPropertyValue(NativeControl, Internals.Text.FontNameProperty, thisAsIText.FontName);
            SetPropertyValue(NativeControl, Internals.Text.FontWeightProperty, thisAsIText.FontWeight);
            SetPropertyValue(NativeControl, Internals.Text.FontStyleProperty, thisAsIText.FontStyle);

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

        public static T FontName<T>(this T node, string? fontName) where T : IText
        {
            node.FontName = new PropertyValue<string?>(fontName);
            return node;
        }

        public static T FontName<T>(this T node, Func<string?> valueFunc) where T : IText
        {
            node.FontName = new PropertyValue<string?>(valueFunc);
            return node;
        }

        public static T FontWeight<T>(this T node, int value) where T : IText
        {
            node.FontWeight = new PropertyValue<int>(value);
            return node;
        }

        public static T FontWeight<T>(this T node, Func<int> valueFunc) where T : IText
        {
            node.FontWeight = new PropertyValue<int>(valueFunc);
            return node;
        }

        public static T FontStyle<T>(this T node, FontStyleType value) where T : IText
        {
            node.FontStyle = new PropertyValue<FontStyleType>(value);
            return node;
        }

        public static T FontStyle<T>(this T node, Func<FontStyleType> valueFunc) where T : IText
        {
            node.FontStyle = new PropertyValue<FontStyleType>(valueFunc);
            return node;
        }

    }
}
