using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface ILabel
    {
        Microsoft.Maui.TextAlignment HorizontalTextAlignment { get; set; }
        Microsoft.Maui.TextAlignment VerticalTextAlignment { get; set; }
        Microsoft.Maui.Graphics.Color TextColor { get; set; }
        double CharacterSpacing { get; set; }
        string Text { get; set; }
        string FontFamily { get; set; }
        double FontSize { get; set; }
        Microsoft.Maui.Controls.FontAttributes FontAttributes { get; set; }
        bool FontAutoScalingEnabled { get; set; }
        Microsoft.Maui.TextTransform TextTransform { get; set; }
        Microsoft.Maui.TextDecorations TextDecorations { get; set; }
        Microsoft.Maui.Controls.FormattedString FormattedText { get; set; }
        Microsoft.Maui.LineBreakMode LineBreakMode { get; set; }
        double LineHeight { get; set; }
        int MaxLines { get; set; }
        Microsoft.Maui.Thickness Padding { get; set; }
        Microsoft.Maui.TextType TextType { get; set; }


    }
    public partial class Label<T> : View<T>, ILabel where T : Microsoft.Maui.Controls.Label, new()
    {
        public Label()
        {

        }

        public Label(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.TextAlignment ILabel.HorizontalTextAlignment { get; set; } = (Microsoft.Maui.TextAlignment)Microsoft.Maui.Controls.Label.HorizontalTextAlignmentProperty.DefaultValue;
        Microsoft.Maui.TextAlignment ILabel.VerticalTextAlignment { get; set; } = (Microsoft.Maui.TextAlignment)Microsoft.Maui.Controls.Label.VerticalTextAlignmentProperty.DefaultValue;
        Microsoft.Maui.Graphics.Color ILabel.TextColor { get; set; } = (Microsoft.Maui.Graphics.Color)Microsoft.Maui.Controls.Label.TextColorProperty.DefaultValue;
        double ILabel.CharacterSpacing { get; set; } = (double)Microsoft.Maui.Controls.Label.CharacterSpacingProperty.DefaultValue;
        string ILabel.Text { get; set; } = (string)Microsoft.Maui.Controls.Label.TextProperty.DefaultValue;
        string ILabel.FontFamily { get; set; } = (string)Microsoft.Maui.Controls.Label.FontFamilyProperty.DefaultValue;
        double ILabel.FontSize { get; set; } = (double)Microsoft.Maui.Controls.Label.FontSizeProperty.DefaultValue;
        Microsoft.Maui.Controls.FontAttributes ILabel.FontAttributes { get; set; } = (Microsoft.Maui.Controls.FontAttributes)Microsoft.Maui.Controls.Label.FontAttributesProperty.DefaultValue;
        bool ILabel.FontAutoScalingEnabled { get; set; } = (bool)Microsoft.Maui.Controls.Label.FontAutoScalingEnabledProperty.DefaultValue;
        Microsoft.Maui.TextTransform ILabel.TextTransform { get; set; } = (Microsoft.Maui.TextTransform)Microsoft.Maui.Controls.Label.TextTransformProperty.DefaultValue;
        Microsoft.Maui.TextDecorations ILabel.TextDecorations { get; set; } = (Microsoft.Maui.TextDecorations)Microsoft.Maui.Controls.Label.TextDecorationsProperty.DefaultValue;
        Microsoft.Maui.Controls.FormattedString ILabel.FormattedText { get; set; } = (Microsoft.Maui.Controls.FormattedString)Microsoft.Maui.Controls.Label.FormattedTextProperty.DefaultValue;
        Microsoft.Maui.LineBreakMode ILabel.LineBreakMode { get; set; } = (Microsoft.Maui.LineBreakMode)Microsoft.Maui.Controls.Label.LineBreakModeProperty.DefaultValue;
        double ILabel.LineHeight { get; set; } = (double)Microsoft.Maui.Controls.Label.LineHeightProperty.DefaultValue;
        int ILabel.MaxLines { get; set; } = (int)Microsoft.Maui.Controls.Label.MaxLinesProperty.DefaultValue;
        Microsoft.Maui.Thickness ILabel.Padding { get; set; } = (Microsoft.Maui.Thickness)Microsoft.Maui.Controls.Label.PaddingProperty.DefaultValue;
        Microsoft.Maui.TextType ILabel.TextType { get; set; } = (Microsoft.Maui.TextType)Microsoft.Maui.Controls.Label.TextTypeProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsILabel = (ILabel)this;
            if (NativeControl.HorizontalTextAlignment != thisAsILabel.HorizontalTextAlignment) NativeControl.HorizontalTextAlignment = thisAsILabel.HorizontalTextAlignment;
            if (NativeControl.VerticalTextAlignment != thisAsILabel.VerticalTextAlignment) NativeControl.VerticalTextAlignment = thisAsILabel.VerticalTextAlignment;
            if (NativeControl.TextColor != thisAsILabel.TextColor) NativeControl.TextColor = thisAsILabel.TextColor;
            if (NativeControl.CharacterSpacing != thisAsILabel.CharacterSpacing) NativeControl.CharacterSpacing = thisAsILabel.CharacterSpacing;
            if (NativeControl.Text != thisAsILabel.Text) NativeControl.Text = thisAsILabel.Text;
            if (NativeControl.FontFamily != thisAsILabel.FontFamily) NativeControl.FontFamily = thisAsILabel.FontFamily;
            if (NativeControl.FontSize != thisAsILabel.FontSize) NativeControl.FontSize = thisAsILabel.FontSize;
            if (NativeControl.FontAttributes != thisAsILabel.FontAttributes) NativeControl.FontAttributes = thisAsILabel.FontAttributes;
            if (NativeControl.FontAutoScalingEnabled != thisAsILabel.FontAutoScalingEnabled) NativeControl.FontAutoScalingEnabled = thisAsILabel.FontAutoScalingEnabled;
            if (NativeControl.TextTransform != thisAsILabel.TextTransform) NativeControl.TextTransform = thisAsILabel.TextTransform;
            if (NativeControl.TextDecorations != thisAsILabel.TextDecorations) NativeControl.TextDecorations = thisAsILabel.TextDecorations;
            if (NativeControl.FormattedText != thisAsILabel.FormattedText) NativeControl.FormattedText = thisAsILabel.FormattedText;
            if (NativeControl.LineBreakMode != thisAsILabel.LineBreakMode) NativeControl.LineBreakMode = thisAsILabel.LineBreakMode;
            if (NativeControl.LineHeight != thisAsILabel.LineHeight) NativeControl.LineHeight = thisAsILabel.LineHeight;
            if (NativeControl.MaxLines != thisAsILabel.MaxLines) NativeControl.MaxLines = thisAsILabel.MaxLines;
            if (NativeControl.Padding != thisAsILabel.Padding) NativeControl.Padding = thisAsILabel.Padding;
            if (NativeControl.TextType != thisAsILabel.TextType) NativeControl.TextType = thisAsILabel.TextType;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class Label : Label<Microsoft.Maui.Controls.Label>
    {
        public Label()
        {

        }

        public Label(Action<Microsoft.Maui.Controls.Label?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class LabelExtensions
    {
        public static T HorizontalTextAlignment<T>(this T label, Microsoft.Maui.TextAlignment horizontalTextAlignment) where T : ILabel
        {
            label.HorizontalTextAlignment = horizontalTextAlignment;
            return label;
        }

        public static T VerticalTextAlignment<T>(this T label, Microsoft.Maui.TextAlignment verticalTextAlignment) where T : ILabel
        {
            label.VerticalTextAlignment = verticalTextAlignment;
            return label;
        }

        public static T TextColor<T>(this T label, Microsoft.Maui.Graphics.Color textColor) where T : ILabel
        {
            label.TextColor = textColor;
            return label;
        }

        public static T CharacterSpacing<T>(this T label, double characterSpacing) where T : ILabel
        {
            label.CharacterSpacing = characterSpacing;
            return label;
        }

        public static T Text<T>(this T label, string text) where T : ILabel
        {
            label.Text = text;
            return label;
        }

        public static T FontFamily<T>(this T label, string fontFamily) where T : ILabel
        {
            label.FontFamily = fontFamily;
            return label;
        }

        public static T FontSize<T>(this T label, double fontSize) where T : ILabel
        {
            label.FontSize = fontSize;
            return label;
        }
        public static T FontSize<T>(this T label, NamedSize size) where T : ILabel
        {
            label.FontSize = Device.GetNamedSize(size, typeof(Label));
            return label;
        }

        public static T FontAttributes<T>(this T label, Microsoft.Maui.Controls.FontAttributes fontAttributes) where T : ILabel
        {
            label.FontAttributes = fontAttributes;
            return label;
        }

        public static T FontAutoScalingEnabled<T>(this T label, bool fontAutoScalingEnabled) where T : ILabel
        {
            label.FontAutoScalingEnabled = fontAutoScalingEnabled;
            return label;
        }

        public static T TextTransform<T>(this T label, Microsoft.Maui.TextTransform textTransform) where T : ILabel
        {
            label.TextTransform = textTransform;
            return label;
        }

        public static T TextDecorations<T>(this T label, Microsoft.Maui.TextDecorations textDecorations) where T : ILabel
        {
            label.TextDecorations = textDecorations;
            return label;
        }

        public static T FormattedText<T>(this T label, Microsoft.Maui.Controls.FormattedString formattedText) where T : ILabel
        {
            label.FormattedText = formattedText;
            return label;
        }

        public static T LineBreakMode<T>(this T label, Microsoft.Maui.LineBreakMode lineBreakMode) where T : ILabel
        {
            label.LineBreakMode = lineBreakMode;
            return label;
        }

        public static T LineHeight<T>(this T label, double lineHeight) where T : ILabel
        {
            label.LineHeight = lineHeight;
            return label;
        }

        public static T MaxLines<T>(this T label, int maxLines) where T : ILabel
        {
            label.MaxLines = maxLines;
            return label;
        }

        public static T Padding<T>(this T label, Microsoft.Maui.Thickness padding) where T : ILabel
        {
            label.Padding = padding;
            return label;
        }
        public static T Padding<T>(this T label, double leftRight, double topBottom) where T : ILabel
        {
            label.Padding = new Thickness(leftRight, topBottom);
            return label;
        }
        public static T Padding<T>(this T label, double uniformSize) where T : ILabel
        {
            label.Padding = new Thickness(uniformSize);
            return label;
        }

        public static T TextType<T>(this T label, Microsoft.Maui.TextType textType) where T : ILabel
        {
            label.TextType = textType;
            return label;
        }


    }
}
