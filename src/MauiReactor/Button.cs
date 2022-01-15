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
    public partial interface IButton
    {
        System.Windows.Input.ICommand Command { get; set; }
        object CommandParameter { get; set; }
        Microsoft.Maui.Controls.Button.ButtonContentLayout ContentLayout { get; set; }
        string Text { get; set; }
        Microsoft.Maui.Graphics.Color TextColor { get; set; }
        double CharacterSpacing { get; set; }
        string FontFamily { get; set; }
        double FontSize { get; set; }
        Microsoft.Maui.TextTransform TextTransform { get; set; }
        Microsoft.Maui.Controls.FontAttributes FontAttributes { get; set; }
        bool FontAutoScalingEnabled { get; set; }
        double BorderWidth { get; set; }
        Microsoft.Maui.Graphics.Color BorderColor { get; set; }
        int CornerRadius { get; set; }
        Microsoft.Maui.Controls.ImageSource ImageSource { get; set; }
        Microsoft.Maui.Thickness Padding { get; set; }
        Microsoft.Maui.LineBreakMode LineBreakMode { get; set; }

        Action? ClickedAction { get; set; }
        Action<EventArgs>? ClickedActionWithArgs { get; set; }
        Action? PressedAction { get; set; }
        Action<EventArgs>? PressedActionWithArgs { get; set; }
        Action? ReleasedAction { get; set; }
        Action<EventArgs>? ReleasedActionWithArgs { get; set; }

    }
    public partial class Button<T> : View<T>, IButton where T : Microsoft.Maui.Controls.Button, new()
    {
        public Button()
        {

        }

        public Button(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        System.Windows.Input.ICommand IButton.Command { get; set; } = (System.Windows.Input.ICommand)Microsoft.Maui.Controls.Button.CommandProperty.DefaultValue;
        object IButton.CommandParameter { get; set; } = (object)Microsoft.Maui.Controls.Button.CommandParameterProperty.DefaultValue;
        Microsoft.Maui.Controls.Button.ButtonContentLayout IButton.ContentLayout { get; set; } = (Microsoft.Maui.Controls.Button.ButtonContentLayout)Microsoft.Maui.Controls.Button.ContentLayoutProperty.DefaultValue;
        string IButton.Text { get; set; } = (string)Microsoft.Maui.Controls.Button.TextProperty.DefaultValue;
        Microsoft.Maui.Graphics.Color IButton.TextColor { get; set; } = (Microsoft.Maui.Graphics.Color)Microsoft.Maui.Controls.Button.TextColorProperty.DefaultValue;
        double IButton.CharacterSpacing { get; set; } = (double)Microsoft.Maui.Controls.Button.CharacterSpacingProperty.DefaultValue;
        string IButton.FontFamily { get; set; } = (string)Microsoft.Maui.Controls.Button.FontFamilyProperty.DefaultValue;
        double IButton.FontSize { get; set; } = (double)Microsoft.Maui.Controls.Button.FontSizeProperty.DefaultValue;
        Microsoft.Maui.TextTransform IButton.TextTransform { get; set; } = (Microsoft.Maui.TextTransform)Microsoft.Maui.Controls.Button.TextTransformProperty.DefaultValue;
        Microsoft.Maui.Controls.FontAttributes IButton.FontAttributes { get; set; } = (Microsoft.Maui.Controls.FontAttributes)Microsoft.Maui.Controls.Button.FontAttributesProperty.DefaultValue;
        bool IButton.FontAutoScalingEnabled { get; set; } = (bool)Microsoft.Maui.Controls.Button.FontAutoScalingEnabledProperty.DefaultValue;
        double IButton.BorderWidth { get; set; } = (double)Microsoft.Maui.Controls.Button.BorderWidthProperty.DefaultValue;
        Microsoft.Maui.Graphics.Color IButton.BorderColor { get; set; } = (Microsoft.Maui.Graphics.Color)Microsoft.Maui.Controls.Button.BorderColorProperty.DefaultValue;
        int IButton.CornerRadius { get; set; } = (int)Microsoft.Maui.Controls.Button.CornerRadiusProperty.DefaultValue;
        Microsoft.Maui.Controls.ImageSource IButton.ImageSource { get; set; } = (Microsoft.Maui.Controls.ImageSource)Microsoft.Maui.Controls.Button.ImageSourceProperty.DefaultValue;
        Microsoft.Maui.Thickness IButton.Padding { get; set; } = (Microsoft.Maui.Thickness)Microsoft.Maui.Controls.Button.PaddingProperty.DefaultValue;
        Microsoft.Maui.LineBreakMode IButton.LineBreakMode { get; set; } = (Microsoft.Maui.LineBreakMode)Microsoft.Maui.Controls.Button.LineBreakModeProperty.DefaultValue;

        Action? IButton.ClickedAction { get; set; }
        Action<EventArgs>? IButton.ClickedActionWithArgs { get; set; }
        Action? IButton.PressedAction { get; set; }
        Action<EventArgs>? IButton.PressedActionWithArgs { get; set; }
        Action? IButton.ReleasedAction { get; set; }
        Action<EventArgs>? IButton.ReleasedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIButton = (IButton)this;
            if (NativeControl.Command != thisAsIButton.Command) NativeControl.Command = thisAsIButton.Command;
            if (NativeControl.CommandParameter != thisAsIButton.CommandParameter) NativeControl.CommandParameter = thisAsIButton.CommandParameter;
            if (NativeControl.ContentLayout != thisAsIButton.ContentLayout) NativeControl.ContentLayout = thisAsIButton.ContentLayout;
            if (NativeControl.Text != thisAsIButton.Text) NativeControl.Text = thisAsIButton.Text;
            if (NativeControl.TextColor != thisAsIButton.TextColor) NativeControl.TextColor = thisAsIButton.TextColor;
            if (NativeControl.CharacterSpacing != thisAsIButton.CharacterSpacing) NativeControl.CharacterSpacing = thisAsIButton.CharacterSpacing;
            if (NativeControl.FontFamily != thisAsIButton.FontFamily) NativeControl.FontFamily = thisAsIButton.FontFamily;
            if (NativeControl.FontSize != thisAsIButton.FontSize) NativeControl.FontSize = thisAsIButton.FontSize;
            if (NativeControl.TextTransform != thisAsIButton.TextTransform) NativeControl.TextTransform = thisAsIButton.TextTransform;
            if (NativeControl.FontAttributes != thisAsIButton.FontAttributes) NativeControl.FontAttributes = thisAsIButton.FontAttributes;
            if (NativeControl.FontAutoScalingEnabled != thisAsIButton.FontAutoScalingEnabled) NativeControl.FontAutoScalingEnabled = thisAsIButton.FontAutoScalingEnabled;
            if (NativeControl.BorderWidth != thisAsIButton.BorderWidth) NativeControl.BorderWidth = thisAsIButton.BorderWidth;
            if (NativeControl.BorderColor != thisAsIButton.BorderColor) NativeControl.BorderColor = thisAsIButton.BorderColor;
            if (NativeControl.CornerRadius != thisAsIButton.CornerRadius) NativeControl.CornerRadius = thisAsIButton.CornerRadius;
            if (NativeControl.ImageSource != thisAsIButton.ImageSource) NativeControl.ImageSource = thisAsIButton.ImageSource;
            if (NativeControl.Padding != thisAsIButton.Padding) NativeControl.Padding = thisAsIButton.Padding;
            if (NativeControl.LineBreakMode != thisAsIButton.LineBreakMode) NativeControl.LineBreakMode = thisAsIButton.LineBreakMode;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIButton = (IButton)this;
            if (thisAsIButton.ClickedAction != null || thisAsIButton.ClickedActionWithArgs != null)
            {
                NativeControl.Clicked += NativeControl_Clicked;
            }
            if (thisAsIButton.PressedAction != null || thisAsIButton.PressedActionWithArgs != null)
            {
                NativeControl.Pressed += NativeControl_Pressed;
            }
            if (thisAsIButton.ReleasedAction != null || thisAsIButton.ReleasedActionWithArgs != null)
            {
                NativeControl.Released += NativeControl_Released;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Clicked(object? sender, EventArgs e)
        {
            var thisAsIButton = (IButton)this;
            thisAsIButton.ClickedAction?.Invoke();
            thisAsIButton.ClickedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_Pressed(object? sender, EventArgs e)
        {
            var thisAsIButton = (IButton)this;
            thisAsIButton.PressedAction?.Invoke();
            thisAsIButton.PressedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_Released(object? sender, EventArgs e)
        {
            var thisAsIButton = (IButton)this;
            thisAsIButton.ReleasedAction?.Invoke();
            thisAsIButton.ReleasedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.Clicked -= NativeControl_Clicked;
                NativeControl.Pressed -= NativeControl_Pressed;
                NativeControl.Released -= NativeControl_Released;
            }

            base.OnDetachNativeEvents();
        }

    }

    public partial class Button : Button<Microsoft.Maui.Controls.Button>
    {
        public Button()
        {

        }

        public Button(Action<Microsoft.Maui.Controls.Button?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ButtonExtensions
    {
        public static T Command<T>(this T button, System.Windows.Input.ICommand command) where T : IButton
        {
            button.Command = command;
            return button;
        }

        public static T CommandParameter<T>(this T button, object commandParameter) where T : IButton
        {
            button.CommandParameter = commandParameter;
            return button;
        }

        public static T ContentLayout<T>(this T button, Microsoft.Maui.Controls.Button.ButtonContentLayout contentLayout) where T : IButton
        {
            button.ContentLayout = contentLayout;
            return button;
        }

        public static T Text<T>(this T button, string text) where T : IButton
        {
            button.Text = text;
            return button;
        }

        public static T TextColor<T>(this T button, Microsoft.Maui.Graphics.Color textColor) where T : IButton
        {
            button.TextColor = textColor;
            return button;
        }

        public static T CharacterSpacing<T>(this T button, double characterSpacing) where T : IButton
        {
            button.CharacterSpacing = characterSpacing;
            return button;
        }

        public static T FontFamily<T>(this T button, string fontFamily) where T : IButton
        {
            button.FontFamily = fontFamily;
            return button;
        }

        public static T FontSize<T>(this T button, double fontSize) where T : IButton
        {
            button.FontSize = fontSize;
            return button;
        }
        public static T FontSize<T>(this T button, NamedSize size) where T : IButton
        {
            button.FontSize = Device.GetNamedSize(size, typeof(Button));
            return button;
        }

        public static T TextTransform<T>(this T button, Microsoft.Maui.TextTransform textTransform) where T : IButton
        {
            button.TextTransform = textTransform;
            return button;
        }

        public static T FontAttributes<T>(this T button, Microsoft.Maui.Controls.FontAttributes fontAttributes) where T : IButton
        {
            button.FontAttributes = fontAttributes;
            return button;
        }

        public static T FontAutoScalingEnabled<T>(this T button, bool fontAutoScalingEnabled) where T : IButton
        {
            button.FontAutoScalingEnabled = fontAutoScalingEnabled;
            return button;
        }

        public static T BorderWidth<T>(this T button, double borderWidth) where T : IButton
        {
            button.BorderWidth = borderWidth;
            return button;
        }

        public static T BorderColor<T>(this T button, Microsoft.Maui.Graphics.Color borderColor) where T : IButton
        {
            button.BorderColor = borderColor;
            return button;
        }

        public static T CornerRadius<T>(this T button, int cornerRadius) where T : IButton
        {
            button.CornerRadius = cornerRadius;
            return button;
        }

        public static T ImageSource<T>(this T button, Microsoft.Maui.Controls.ImageSource imageSource) where T : IButton
        {
            button.ImageSource = imageSource;
            return button;
        }
        public static T Image<T>(this T button, string file) where T : IButton
        {
            button.ImageSource = Microsoft.Maui.Controls.ImageSource.FromFile(file);
            return button;
        }
        public static T Image<T>(this T button, string fileAndroid, string fileiOS) where T : IButton
        {
            button.ImageSource = Device.RuntimePlatform == Device.Android ? Microsoft.Maui.Controls.ImageSource.FromFile(fileAndroid) : Microsoft.Maui.Controls.ImageSource.FromFile(fileiOS);
            return button;
        }
        public static T Image<T>(this T button, string resourceName, Assembly sourceAssembly) where T : IButton
        {
            button.ImageSource = Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly);
            return button;
        }
        public static T Image<T>(this T button, Uri imageUri) where T : IButton
        {
            button.ImageSource = Microsoft.Maui.Controls.ImageSource.FromUri(imageUri);
            return button;
        }
        public static T Image<T>(this T button, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IButton
        {
            button.ImageSource = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return button;
        }
        public static T Image<T>(this T button, Func<Stream> imageStream) where T : IButton
        {
            button.ImageSource = Microsoft.Maui.Controls.ImageSource.FromStream(imageStream);
            return button;
        }

        public static T Padding<T>(this T button, Microsoft.Maui.Thickness padding) where T : IButton
        {
            button.Padding = padding;
            return button;
        }
        public static T Padding<T>(this T button, double leftRight, double topBottom) where T : IButton
        {
            button.Padding = new Thickness(leftRight, topBottom);
            return button;
        }
        public static T Padding<T>(this T button, double uniformSize) where T : IButton
        {
            button.Padding = new Thickness(uniformSize);
            return button;
        }

        public static T LineBreakMode<T>(this T button, Microsoft.Maui.LineBreakMode lineBreakMode) where T : IButton
        {
            button.LineBreakMode = lineBreakMode;
            return button;
        }


        public static T OnClicked<T>(this T button, Action clickedAction) where T : IButton
        {
            button.ClickedAction = clickedAction;
            return button;
        }

        public static T OnClicked<T>(this T button, Action<EventArgs> clickedActionWithArgs) where T : IButton
        {
            button.ClickedActionWithArgs = clickedActionWithArgs;
            return button;
        }
        public static T OnPressed<T>(this T button, Action pressedAction) where T : IButton
        {
            button.PressedAction = pressedAction;
            return button;
        }

        public static T OnPressed<T>(this T button, Action<EventArgs> pressedActionWithArgs) where T : IButton
        {
            button.PressedActionWithArgs = pressedActionWithArgs;
            return button;
        }
        public static T OnReleased<T>(this T button, Action releasedAction) where T : IButton
        {
            button.ReleasedAction = releasedAction;
            return button;
        }

        public static T OnReleased<T>(this T button, Action<EventArgs> releasedActionWithArgs) where T : IButton
        {
            button.ReleasedActionWithArgs = releasedActionWithArgs;
            return button;
        }
    }
}
