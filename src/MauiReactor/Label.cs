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
    public partial interface ILabel : IView
    {
        PropertyValue<Microsoft.Maui.TextAlignment>? HorizontalTextAlignment { get; set; }
        PropertyValue<Microsoft.Maui.TextAlignment>? VerticalTextAlignment { get; set; }
        PropertyValue<Microsoft.Maui.Graphics.Color>? TextColor { get; set; }
        PropertyValue<double>? CharacterSpacing { get; set; }
        PropertyValue<string>? Text { get; set; }
        PropertyValue<string>? FontFamily { get; set; }
        PropertyValue<double>? FontSize { get; set; }
        PropertyValue<Microsoft.Maui.Controls.FontAttributes>? FontAttributes { get; set; }
        PropertyValue<bool>? FontAutoScalingEnabled { get; set; }
        PropertyValue<Microsoft.Maui.TextTransform>? TextTransform { get; set; }
        PropertyValue<Microsoft.Maui.TextDecorations>? TextDecorations { get; set; }
        PropertyValue<Microsoft.Maui.Controls.FormattedString>? FormattedText { get; set; }
        PropertyValue<Microsoft.Maui.LineBreakMode>? LineBreakMode { get; set; }
        PropertyValue<double>? LineHeight { get; set; }
        PropertyValue<int>? MaxLines { get; set; }
        PropertyValue<Microsoft.Maui.Thickness>? Padding { get; set; }
        PropertyValue<Microsoft.Maui.TextType>? TextType { get; set; }


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

        PropertyValue<Microsoft.Maui.TextAlignment>? ILabel.HorizontalTextAlignment { get; set; }
        PropertyValue<Microsoft.Maui.TextAlignment>? ILabel.VerticalTextAlignment { get; set; }
        PropertyValue<Microsoft.Maui.Graphics.Color>? ILabel.TextColor { get; set; }
        PropertyValue<double>? ILabel.CharacterSpacing { get; set; }
        PropertyValue<string>? ILabel.Text { get; set; }
        PropertyValue<string>? ILabel.FontFamily { get; set; }
        PropertyValue<double>? ILabel.FontSize { get; set; }
        PropertyValue<Microsoft.Maui.Controls.FontAttributes>? ILabel.FontAttributes { get; set; }
        PropertyValue<bool>? ILabel.FontAutoScalingEnabled { get; set; }
        PropertyValue<Microsoft.Maui.TextTransform>? ILabel.TextTransform { get; set; }
        PropertyValue<Microsoft.Maui.TextDecorations>? ILabel.TextDecorations { get; set; }
        PropertyValue<Microsoft.Maui.Controls.FormattedString>? ILabel.FormattedText { get; set; }
        PropertyValue<Microsoft.Maui.LineBreakMode>? ILabel.LineBreakMode { get; set; }
        PropertyValue<double>? ILabel.LineHeight { get; set; }
        PropertyValue<int>? ILabel.MaxLines { get; set; }
        PropertyValue<Microsoft.Maui.Thickness>? ILabel.Padding { get; set; }
        PropertyValue<Microsoft.Maui.TextType>? ILabel.TextType { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsILabel = (ILabel)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.HorizontalTextAlignmentProperty, thisAsILabel.HorizontalTextAlignment);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.VerticalTextAlignmentProperty, thisAsILabel.VerticalTextAlignment);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.TextColorProperty, thisAsILabel.TextColor);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.CharacterSpacingProperty, thisAsILabel.CharacterSpacing);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.TextProperty, thisAsILabel.Text);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.FontFamilyProperty, thisAsILabel.FontFamily);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.FontSizeProperty, thisAsILabel.FontSize);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.FontAttributesProperty, thisAsILabel.FontAttributes);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.FontAutoScalingEnabledProperty, thisAsILabel.FontAutoScalingEnabled);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.TextTransformProperty, thisAsILabel.TextTransform);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.TextDecorationsProperty, thisAsILabel.TextDecorations);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.FormattedTextProperty, thisAsILabel.FormattedText);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.LineBreakModeProperty, thisAsILabel.LineBreakMode);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.LineHeightProperty, thisAsILabel.LineHeight);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.MaxLinesProperty, thisAsILabel.MaxLines);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.PaddingProperty, thisAsILabel.Padding);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Label.TextTypeProperty, thisAsILabel.TextType);


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
            label.HorizontalTextAlignment = new PropertyValue<Microsoft.Maui.TextAlignment>(horizontalTextAlignment);
            return label;
        }

        public static T HorizontalTextAlignment<T>(this T label, Func<Microsoft.Maui.TextAlignment> horizontalTextAlignmentFunc) where T : ILabel
        {
            label.HorizontalTextAlignment = new PropertyValue<Microsoft.Maui.TextAlignment>(horizontalTextAlignmentFunc);
            return label;
        }



        public static T VerticalTextAlignment<T>(this T label, Microsoft.Maui.TextAlignment verticalTextAlignment) where T : ILabel
        {
            label.VerticalTextAlignment = new PropertyValue<Microsoft.Maui.TextAlignment>(verticalTextAlignment);
            return label;
        }

        public static T VerticalTextAlignment<T>(this T label, Func<Microsoft.Maui.TextAlignment> verticalTextAlignmentFunc) where T : ILabel
        {
            label.VerticalTextAlignment = new PropertyValue<Microsoft.Maui.TextAlignment>(verticalTextAlignmentFunc);
            return label;
        }



        public static T TextColor<T>(this T label, Microsoft.Maui.Graphics.Color textColor) where T : ILabel
        {
            label.TextColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(textColor);
            return label;
        }

        public static T TextColor<T>(this T label, Func<Microsoft.Maui.Graphics.Color> textColorFunc) where T : ILabel
        {
            label.TextColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(textColorFunc);
            return label;
        }



        public static T CharacterSpacing<T>(this T label, double characterSpacing, RxDoubleAnimation? customAnimation = null) where T : ILabel
        {
            label.CharacterSpacing = new PropertyValue<double>(characterSpacing);
            label.AppendAnimatable(Microsoft.Maui.Controls.Label.CharacterSpacingProperty, customAnimation ?? new RxDoubleAnimation(characterSpacing), v => label.CharacterSpacing = new PropertyValue<double>(v.CurrentValue()));
            return label;
        }

        public static T CharacterSpacing<T>(this T label, Func<double> characterSpacingFunc) where T : ILabel
        {
            label.CharacterSpacing = new PropertyValue<double>(characterSpacingFunc);
            return label;
        }



        public static T Text<T>(this T label, string text) where T : ILabel
        {
            label.Text = new PropertyValue<string>(text);
            return label;
        }

        public static T Text<T>(this T label, Func<string> textFunc) where T : ILabel
        {
            label.Text = new PropertyValue<string>(textFunc);
            return label;
        }



        public static T FontFamily<T>(this T label, string fontFamily) where T : ILabel
        {
            label.FontFamily = new PropertyValue<string>(fontFamily);
            return label;
        }

        public static T FontFamily<T>(this T label, Func<string> fontFamilyFunc) where T : ILabel
        {
            label.FontFamily = new PropertyValue<string>(fontFamilyFunc);
            return label;
        }



        public static T FontSize<T>(this T label, double fontSize, RxDoubleAnimation? customAnimation = null) where T : ILabel
        {
            label.FontSize = new PropertyValue<double>(fontSize);
            label.AppendAnimatable(Microsoft.Maui.Controls.Label.FontSizeProperty, customAnimation ?? new RxDoubleAnimation(fontSize), v => label.FontSize = new PropertyValue<double>(v.CurrentValue()));
            return label;
        }

        public static T FontSize<T>(this T label, Func<double> fontSizeFunc) where T : ILabel
        {
            label.FontSize = new PropertyValue<double>(fontSizeFunc);
            return label;
        }


        public static T FontSize<T>(this T label, NamedSize size) where T : ILabel
        {
            label.FontSize = new PropertyValue<double>(Device.GetNamedSize(size, typeof(Label)));
            return label;
        }

        public static T FontAttributes<T>(this T label, Microsoft.Maui.Controls.FontAttributes fontAttributes) where T : ILabel
        {
            label.FontAttributes = new PropertyValue<Microsoft.Maui.Controls.FontAttributes>(fontAttributes);
            return label;
        }

        public static T FontAttributes<T>(this T label, Func<Microsoft.Maui.Controls.FontAttributes> fontAttributesFunc) where T : ILabel
        {
            label.FontAttributes = new PropertyValue<Microsoft.Maui.Controls.FontAttributes>(fontAttributesFunc);
            return label;
        }



        public static T FontAutoScalingEnabled<T>(this T label, bool fontAutoScalingEnabled) where T : ILabel
        {
            label.FontAutoScalingEnabled = new PropertyValue<bool>(fontAutoScalingEnabled);
            return label;
        }

        public static T FontAutoScalingEnabled<T>(this T label, Func<bool> fontAutoScalingEnabledFunc) where T : ILabel
        {
            label.FontAutoScalingEnabled = new PropertyValue<bool>(fontAutoScalingEnabledFunc);
            return label;
        }



        public static T TextTransform<T>(this T label, Microsoft.Maui.TextTransform textTransform) where T : ILabel
        {
            label.TextTransform = new PropertyValue<Microsoft.Maui.TextTransform>(textTransform);
            return label;
        }

        public static T TextTransform<T>(this T label, Func<Microsoft.Maui.TextTransform> textTransformFunc) where T : ILabel
        {
            label.TextTransform = new PropertyValue<Microsoft.Maui.TextTransform>(textTransformFunc);
            return label;
        }



        public static T TextDecorations<T>(this T label, Microsoft.Maui.TextDecorations textDecorations) where T : ILabel
        {
            label.TextDecorations = new PropertyValue<Microsoft.Maui.TextDecorations>(textDecorations);
            return label;
        }

        public static T TextDecorations<T>(this T label, Func<Microsoft.Maui.TextDecorations> textDecorationsFunc) where T : ILabel
        {
            label.TextDecorations = new PropertyValue<Microsoft.Maui.TextDecorations>(textDecorationsFunc);
            return label;
        }



        public static T FormattedText<T>(this T label, Microsoft.Maui.Controls.FormattedString formattedText) where T : ILabel
        {
            label.FormattedText = new PropertyValue<Microsoft.Maui.Controls.FormattedString>(formattedText);
            return label;
        }

        public static T FormattedText<T>(this T label, Func<Microsoft.Maui.Controls.FormattedString> formattedTextFunc) where T : ILabel
        {
            label.FormattedText = new PropertyValue<Microsoft.Maui.Controls.FormattedString>(formattedTextFunc);
            return label;
        }



        public static T LineBreakMode<T>(this T label, Microsoft.Maui.LineBreakMode lineBreakMode) where T : ILabel
        {
            label.LineBreakMode = new PropertyValue<Microsoft.Maui.LineBreakMode>(lineBreakMode);
            return label;
        }

        public static T LineBreakMode<T>(this T label, Func<Microsoft.Maui.LineBreakMode> lineBreakModeFunc) where T : ILabel
        {
            label.LineBreakMode = new PropertyValue<Microsoft.Maui.LineBreakMode>(lineBreakModeFunc);
            return label;
        }



        public static T LineHeight<T>(this T label, double lineHeight, RxDoubleAnimation? customAnimation = null) where T : ILabel
        {
            label.LineHeight = new PropertyValue<double>(lineHeight);
            label.AppendAnimatable(Microsoft.Maui.Controls.Label.LineHeightProperty, customAnimation ?? new RxDoubleAnimation(lineHeight), v => label.LineHeight = new PropertyValue<double>(v.CurrentValue()));
            return label;
        }

        public static T LineHeight<T>(this T label, Func<double> lineHeightFunc) where T : ILabel
        {
            label.LineHeight = new PropertyValue<double>(lineHeightFunc);
            return label;
        }



        public static T MaxLines<T>(this T label, int maxLines) where T : ILabel
        {
            label.MaxLines = new PropertyValue<int>(maxLines);
            return label;
        }

        public static T MaxLines<T>(this T label, Func<int> maxLinesFunc) where T : ILabel
        {
            label.MaxLines = new PropertyValue<int>(maxLinesFunc);
            return label;
        }



        public static T Padding<T>(this T label, Microsoft.Maui.Thickness padding) where T : ILabel
        {
            label.Padding = new PropertyValue<Microsoft.Maui.Thickness>(padding);
            return label;
        }

        public static T Padding<T>(this T label, Func<Microsoft.Maui.Thickness> paddingFunc) where T : ILabel
        {
            label.Padding = new PropertyValue<Microsoft.Maui.Thickness>(paddingFunc);
            return label;
        }
        public static T Padding<T>(this T label, double leftRight, double topBottom) where T : ILabel
        {
            label.Padding = new PropertyValue<Microsoft.Maui.Thickness>(new Thickness(leftRight, topBottom));
            return label;
        }
        public static T Padding<T>(this T label, double uniformSize) where T : ILabel
        {
            label.Padding = new PropertyValue<Microsoft.Maui.Thickness>(new Thickness(uniformSize));
            return label;
        }



        public static T TextType<T>(this T label, Microsoft.Maui.TextType textType) where T : ILabel
        {
            label.TextType = new PropertyValue<Microsoft.Maui.TextType>(textType);
            return label;
        }

        public static T TextType<T>(this T label, Func<Microsoft.Maui.TextType> textTypeFunc) where T : ILabel
        {
            label.TextType = new PropertyValue<Microsoft.Maui.TextType>(textTypeFunc);
            return label;
        }




    }
}
