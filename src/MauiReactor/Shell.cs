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
    public partial interface IShell
    {
        Microsoft.Maui.Controls.FlyoutBehavior FlyoutBehavior { get; set; }
        Microsoft.Maui.Controls.DataTemplate MenuItemTemplate { get; set; }
        Microsoft.Maui.Controls.DataTemplate ItemTemplate { get; set; }
        Microsoft.Maui.Graphics.Color BackgroundColor { get; set; }
        Microsoft.Maui.Controls.Brush FlyoutBackdrop { get; set; }
        double FlyoutWidth { get; set; }
        double FlyoutHeight { get; set; }
        Microsoft.Maui.Controls.ImageSource FlyoutBackgroundImage { get; set; }
        Microsoft.Maui.Aspect FlyoutBackgroundImageAspect { get; set; }
        Microsoft.Maui.Graphics.Color FlyoutBackgroundColor { get; set; }
        Microsoft.Maui.Controls.Brush FlyoutBackground { get; set; }
        Microsoft.Maui.Controls.FlyoutHeaderBehavior FlyoutHeaderBehavior { get; set; }
        object FlyoutHeader { get; set; }
        object FlyoutFooter { get; set; }
        Microsoft.Maui.Controls.DataTemplate FlyoutHeaderTemplate { get; set; }
        Microsoft.Maui.Controls.DataTemplate FlyoutFooterTemplate { get; set; }
        bool FlyoutIsPresented { get; set; }
        Microsoft.Maui.Controls.ImageSource FlyoutIcon { get; set; }
        Microsoft.Maui.Controls.ScrollMode FlyoutVerticalScrollMode { get; set; }
        object FlyoutContent { get; set; }
        Microsoft.Maui.Controls.DataTemplate FlyoutContentTemplate { get; set; }

        Action? NavigatedAction { get; set; }
        Action<ShellNavigatedEventArgs>? NavigatedActionWithArgs { get; set; }
        Action? NavigatingAction { get; set; }
        Action<ShellNavigatingEventArgs>? NavigatingActionWithArgs { get; set; }

    }
    public partial class Shell<T> : Page<T>, IShell where T : Microsoft.Maui.Controls.Shell, new()
    {
        public Shell()
        {

        }

        public Shell(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Controls.FlyoutBehavior IShell.FlyoutBehavior { get; set; } = (Microsoft.Maui.Controls.FlyoutBehavior)Microsoft.Maui.Controls.Shell.FlyoutBehaviorProperty.DefaultValue;
        Microsoft.Maui.Controls.DataTemplate IShell.MenuItemTemplate { get; set; } = (Microsoft.Maui.Controls.DataTemplate)Microsoft.Maui.Controls.Shell.MenuItemTemplateProperty.DefaultValue;
        Microsoft.Maui.Controls.DataTemplate IShell.ItemTemplate { get; set; } = (Microsoft.Maui.Controls.DataTemplate)Microsoft.Maui.Controls.Shell.ItemTemplateProperty.DefaultValue;
        Microsoft.Maui.Graphics.Color IShell.BackgroundColor { get; set; } = (Microsoft.Maui.Graphics.Color)Microsoft.Maui.Controls.Shell.BackgroundColorProperty.DefaultValue;
        Microsoft.Maui.Controls.Brush IShell.FlyoutBackdrop { get; set; } = (Microsoft.Maui.Controls.Brush)Microsoft.Maui.Controls.Shell.FlyoutBackdropProperty.DefaultValue;
        double IShell.FlyoutWidth { get; set; } = (double)Microsoft.Maui.Controls.Shell.FlyoutWidthProperty.DefaultValue;
        double IShell.FlyoutHeight { get; set; } = (double)Microsoft.Maui.Controls.Shell.FlyoutHeightProperty.DefaultValue;
        Microsoft.Maui.Controls.ImageSource IShell.FlyoutBackgroundImage { get; set; } = (Microsoft.Maui.Controls.ImageSource)Microsoft.Maui.Controls.Shell.FlyoutBackgroundImageProperty.DefaultValue;
        Microsoft.Maui.Aspect IShell.FlyoutBackgroundImageAspect { get; set; } = (Microsoft.Maui.Aspect)Microsoft.Maui.Controls.Shell.FlyoutBackgroundImageAspectProperty.DefaultValue;
        Microsoft.Maui.Graphics.Color IShell.FlyoutBackgroundColor { get; set; } = (Microsoft.Maui.Graphics.Color)Microsoft.Maui.Controls.Shell.FlyoutBackgroundColorProperty.DefaultValue;
        Microsoft.Maui.Controls.Brush IShell.FlyoutBackground { get; set; } = (Microsoft.Maui.Controls.Brush)Microsoft.Maui.Controls.Shell.FlyoutBackgroundProperty.DefaultValue;
        Microsoft.Maui.Controls.FlyoutHeaderBehavior IShell.FlyoutHeaderBehavior { get; set; } = (Microsoft.Maui.Controls.FlyoutHeaderBehavior)Microsoft.Maui.Controls.Shell.FlyoutHeaderBehaviorProperty.DefaultValue;
        object IShell.FlyoutHeader { get; set; } = (object)Microsoft.Maui.Controls.Shell.FlyoutHeaderProperty.DefaultValue;
        object IShell.FlyoutFooter { get; set; } = (object)Microsoft.Maui.Controls.Shell.FlyoutFooterProperty.DefaultValue;
        Microsoft.Maui.Controls.DataTemplate IShell.FlyoutHeaderTemplate { get; set; } = (Microsoft.Maui.Controls.DataTemplate)Microsoft.Maui.Controls.Shell.FlyoutHeaderTemplateProperty.DefaultValue;
        Microsoft.Maui.Controls.DataTemplate IShell.FlyoutFooterTemplate { get; set; } = (Microsoft.Maui.Controls.DataTemplate)Microsoft.Maui.Controls.Shell.FlyoutFooterTemplateProperty.DefaultValue;
        bool IShell.FlyoutIsPresented { get; set; } = (bool)Microsoft.Maui.Controls.Shell.FlyoutIsPresentedProperty.DefaultValue;
        Microsoft.Maui.Controls.ImageSource IShell.FlyoutIcon { get; set; } = (Microsoft.Maui.Controls.ImageSource)Microsoft.Maui.Controls.Shell.FlyoutIconProperty.DefaultValue;
        Microsoft.Maui.Controls.ScrollMode IShell.FlyoutVerticalScrollMode { get; set; } = (Microsoft.Maui.Controls.ScrollMode)Microsoft.Maui.Controls.Shell.FlyoutVerticalScrollModeProperty.DefaultValue;
        object IShell.FlyoutContent { get; set; } = (object)Microsoft.Maui.Controls.Shell.FlyoutContentProperty.DefaultValue;
        Microsoft.Maui.Controls.DataTemplate IShell.FlyoutContentTemplate { get; set; } = (Microsoft.Maui.Controls.DataTemplate)Microsoft.Maui.Controls.Shell.FlyoutContentTemplateProperty.DefaultValue;

        Action? IShell.NavigatedAction { get; set; }
        Action<ShellNavigatedEventArgs>? IShell.NavigatedActionWithArgs { get; set; }
        Action? IShell.NavigatingAction { get; set; }
        Action<ShellNavigatingEventArgs>? IShell.NavigatingActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIShell = (IShell)this;
            if (NativeControl.FlyoutBehavior != thisAsIShell.FlyoutBehavior) NativeControl.FlyoutBehavior = thisAsIShell.FlyoutBehavior;
            if (NativeControl.MenuItemTemplate != thisAsIShell.MenuItemTemplate) NativeControl.MenuItemTemplate = thisAsIShell.MenuItemTemplate;
            if (NativeControl.ItemTemplate != thisAsIShell.ItemTemplate) NativeControl.ItemTemplate = thisAsIShell.ItemTemplate;
            if (NativeControl.BackgroundColor != thisAsIShell.BackgroundColor) NativeControl.BackgroundColor = thisAsIShell.BackgroundColor;
            if (NativeControl.FlyoutBackdrop != thisAsIShell.FlyoutBackdrop) NativeControl.FlyoutBackdrop = thisAsIShell.FlyoutBackdrop;
            if (NativeControl.FlyoutWidth != thisAsIShell.FlyoutWidth) NativeControl.FlyoutWidth = thisAsIShell.FlyoutWidth;
            if (NativeControl.FlyoutHeight != thisAsIShell.FlyoutHeight) NativeControl.FlyoutHeight = thisAsIShell.FlyoutHeight;
            if (NativeControl.FlyoutBackgroundImage != thisAsIShell.FlyoutBackgroundImage) NativeControl.FlyoutBackgroundImage = thisAsIShell.FlyoutBackgroundImage;
            if (NativeControl.FlyoutBackgroundImageAspect != thisAsIShell.FlyoutBackgroundImageAspect) NativeControl.FlyoutBackgroundImageAspect = thisAsIShell.FlyoutBackgroundImageAspect;
            if (NativeControl.FlyoutBackgroundColor != thisAsIShell.FlyoutBackgroundColor) NativeControl.FlyoutBackgroundColor = thisAsIShell.FlyoutBackgroundColor;
            if (NativeControl.FlyoutBackground != thisAsIShell.FlyoutBackground) NativeControl.FlyoutBackground = thisAsIShell.FlyoutBackground;
            if (NativeControl.FlyoutHeaderBehavior != thisAsIShell.FlyoutHeaderBehavior) NativeControl.FlyoutHeaderBehavior = thisAsIShell.FlyoutHeaderBehavior;
            if (NativeControl.FlyoutHeader != thisAsIShell.FlyoutHeader) NativeControl.FlyoutHeader = thisAsIShell.FlyoutHeader;
            if (NativeControl.FlyoutFooter != thisAsIShell.FlyoutFooter) NativeControl.FlyoutFooter = thisAsIShell.FlyoutFooter;
            if (NativeControl.FlyoutHeaderTemplate != thisAsIShell.FlyoutHeaderTemplate) NativeControl.FlyoutHeaderTemplate = thisAsIShell.FlyoutHeaderTemplate;
            if (NativeControl.FlyoutFooterTemplate != thisAsIShell.FlyoutFooterTemplate) NativeControl.FlyoutFooterTemplate = thisAsIShell.FlyoutFooterTemplate;
            if (NativeControl.FlyoutIsPresented != thisAsIShell.FlyoutIsPresented) NativeControl.FlyoutIsPresented = thisAsIShell.FlyoutIsPresented;
            if (NativeControl.FlyoutIcon != thisAsIShell.FlyoutIcon) NativeControl.FlyoutIcon = thisAsIShell.FlyoutIcon;
            if (NativeControl.FlyoutVerticalScrollMode != thisAsIShell.FlyoutVerticalScrollMode) NativeControl.FlyoutVerticalScrollMode = thisAsIShell.FlyoutVerticalScrollMode;
            if (NativeControl.FlyoutContent != thisAsIShell.FlyoutContent) NativeControl.FlyoutContent = thisAsIShell.FlyoutContent;
            if (NativeControl.FlyoutContentTemplate != thisAsIShell.FlyoutContentTemplate) NativeControl.FlyoutContentTemplate = thisAsIShell.FlyoutContentTemplate;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIShell = (IShell)this;
            if (thisAsIShell.NavigatedAction != null || thisAsIShell.NavigatedActionWithArgs != null)
            {
                NativeControl.Navigated += NativeControl_Navigated;
            }
            if (thisAsIShell.NavigatingAction != null || thisAsIShell.NavigatingActionWithArgs != null)
            {
                NativeControl.Navigating += NativeControl_Navigating;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Navigated(object? sender, ShellNavigatedEventArgs e)
        {
            var thisAsIShell = (IShell)this;
            thisAsIShell.NavigatedAction?.Invoke();
            thisAsIShell.NavigatedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_Navigating(object? sender, ShellNavigatingEventArgs e)
        {
            var thisAsIShell = (IShell)this;
            thisAsIShell.NavigatingAction?.Invoke();
            thisAsIShell.NavigatingActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.Navigated -= NativeControl_Navigated;
                NativeControl.Navigating -= NativeControl_Navigating;
            }

            base.OnDetachNativeEvents();
        }

    }

    public partial class Shell : Shell<Microsoft.Maui.Controls.Shell>
    {
        public Shell()
        {

        }

        public Shell(Action<Microsoft.Maui.Controls.Shell?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ShellExtensions
    {
        public static T FlyoutBehavior<T>(this T shell, Microsoft.Maui.Controls.FlyoutBehavior flyoutBehavior) where T : IShell
        {
            shell.FlyoutBehavior = flyoutBehavior;
            return shell;
        }

        public static T MenuItemTemplate<T>(this T shell, Microsoft.Maui.Controls.DataTemplate menuItemTemplate) where T : IShell
        {
            shell.MenuItemTemplate = menuItemTemplate;
            return shell;
        }

        public static T ItemTemplate<T>(this T shell, Microsoft.Maui.Controls.DataTemplate itemTemplate) where T : IShell
        {
            shell.ItemTemplate = itemTemplate;
            return shell;
        }

        public static T BackgroundColor<T>(this T shell, Microsoft.Maui.Graphics.Color backgroundColor) where T : IShell
        {
            shell.BackgroundColor = backgroundColor;
            return shell;
        }

        public static T FlyoutBackdrop<T>(this T shell, Microsoft.Maui.Controls.Brush flyoutBackdrop) where T : IShell
        {
            shell.FlyoutBackdrop = flyoutBackdrop;
            return shell;
        }

        public static T FlyoutWidth<T>(this T shell, double flyoutWidth) where T : IShell
        {
            shell.FlyoutWidth = flyoutWidth;
            return shell;
        }

        public static T FlyoutHeight<T>(this T shell, double flyoutHeight) where T : IShell
        {
            shell.FlyoutHeight = flyoutHeight;
            return shell;
        }

        public static T FlyoutBackgroundImage<T>(this T shell, Microsoft.Maui.Controls.ImageSource flyoutBackgroundImage) where T : IShell
        {
            shell.FlyoutBackgroundImage = flyoutBackgroundImage;
            return shell;
        }
        public static T FlyoutBackgroun<T>(this T shell, string file) where T : IShell
        {
            shell.FlyoutBackgroundImage = Microsoft.Maui.Controls.ImageSource.FromFile(file);
            return shell;
        }
        public static T FlyoutBackgroun<T>(this T shell, string fileAndroid, string fileiOS) where T : IShell
        {
            shell.FlyoutBackgroundImage = Device.RuntimePlatform == Device.Android ? Microsoft.Maui.Controls.ImageSource.FromFile(fileAndroid) : Microsoft.Maui.Controls.ImageSource.FromFile(fileiOS);
            return shell;
        }
        public static T FlyoutBackgroun<T>(this T shell, string resourceName, Assembly sourceAssembly) where T : IShell
        {
            shell.FlyoutBackgroundImage = Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly);
            return shell;
        }
        public static T FlyoutBackgroun<T>(this T shell, Uri imageUri) where T : IShell
        {
            shell.FlyoutBackgroundImage = Microsoft.Maui.Controls.ImageSource.FromUri(imageUri);
            return shell;
        }
        public static T FlyoutBackgroun<T>(this T shell, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IShell
        {
            shell.FlyoutBackgroundImage = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return shell;
        }
        public static T FlyoutBackgroun<T>(this T shell, Func<Stream> imageStream) where T : IShell
        {
            shell.FlyoutBackgroundImage = Microsoft.Maui.Controls.ImageSource.FromStream(imageStream);
            return shell;
        }

        public static T FlyoutBackgroundImageAspect<T>(this T shell, Microsoft.Maui.Aspect flyoutBackgroundImageAspect) where T : IShell
        {
            shell.FlyoutBackgroundImageAspect = flyoutBackgroundImageAspect;
            return shell;
        }

        public static T FlyoutBackgroundColor<T>(this T shell, Microsoft.Maui.Graphics.Color flyoutBackgroundColor) where T : IShell
        {
            shell.FlyoutBackgroundColor = flyoutBackgroundColor;
            return shell;
        }

        public static T FlyoutBackground<T>(this T shell, Microsoft.Maui.Controls.Brush flyoutBackground) where T : IShell
        {
            shell.FlyoutBackground = flyoutBackground;
            return shell;
        }

        public static T FlyoutHeaderBehavior<T>(this T shell, Microsoft.Maui.Controls.FlyoutHeaderBehavior flyoutHeaderBehavior) where T : IShell
        {
            shell.FlyoutHeaderBehavior = flyoutHeaderBehavior;
            return shell;
        }

        public static T FlyoutHeader<T>(this T shell, object flyoutHeader) where T : IShell
        {
            shell.FlyoutHeader = flyoutHeader;
            return shell;
        }

        public static T FlyoutFooter<T>(this T shell, object flyoutFooter) where T : IShell
        {
            shell.FlyoutFooter = flyoutFooter;
            return shell;
        }

        public static T FlyoutHeaderTemplate<T>(this T shell, Microsoft.Maui.Controls.DataTemplate flyoutHeaderTemplate) where T : IShell
        {
            shell.FlyoutHeaderTemplate = flyoutHeaderTemplate;
            return shell;
        }

        public static T FlyoutFooterTemplate<T>(this T shell, Microsoft.Maui.Controls.DataTemplate flyoutFooterTemplate) where T : IShell
        {
            shell.FlyoutFooterTemplate = flyoutFooterTemplate;
            return shell;
        }

        public static T FlyoutIsPresented<T>(this T shell, bool flyoutIsPresented) where T : IShell
        {
            shell.FlyoutIsPresented = flyoutIsPresented;
            return shell;
        }

        public static T FlyoutIcon<T>(this T shell, Microsoft.Maui.Controls.ImageSource flyoutIcon) where T : IShell
        {
            shell.FlyoutIcon = flyoutIcon;
            return shell;
        }
        public static T Flyo<T>(this T shell, string file) where T : IShell
        {
            shell.FlyoutIcon = Microsoft.Maui.Controls.ImageSource.FromFile(file);
            return shell;
        }
        public static T Flyo<T>(this T shell, string fileAndroid, string fileiOS) where T : IShell
        {
            shell.FlyoutIcon = Device.RuntimePlatform == Device.Android ? Microsoft.Maui.Controls.ImageSource.FromFile(fileAndroid) : Microsoft.Maui.Controls.ImageSource.FromFile(fileiOS);
            return shell;
        }
        public static T Flyo<T>(this T shell, string resourceName, Assembly sourceAssembly) where T : IShell
        {
            shell.FlyoutIcon = Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly);
            return shell;
        }
        public static T Flyo<T>(this T shell, Uri imageUri) where T : IShell
        {
            shell.FlyoutIcon = Microsoft.Maui.Controls.ImageSource.FromUri(imageUri);
            return shell;
        }
        public static T Flyo<T>(this T shell, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IShell
        {
            shell.FlyoutIcon = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return shell;
        }
        public static T Flyo<T>(this T shell, Func<Stream> imageStream) where T : IShell
        {
            shell.FlyoutIcon = Microsoft.Maui.Controls.ImageSource.FromStream(imageStream);
            return shell;
        }

        public static T FlyoutVerticalScrollMode<T>(this T shell, Microsoft.Maui.Controls.ScrollMode flyoutVerticalScrollMode) where T : IShell
        {
            shell.FlyoutVerticalScrollMode = flyoutVerticalScrollMode;
            return shell;
        }

        public static T FlyoutContent<T>(this T shell, object flyoutContent) where T : IShell
        {
            shell.FlyoutContent = flyoutContent;
            return shell;
        }

        public static T FlyoutContentTemplate<T>(this T shell, Microsoft.Maui.Controls.DataTemplate flyoutContentTemplate) where T : IShell
        {
            shell.FlyoutContentTemplate = flyoutContentTemplate;
            return shell;
        }


        public static T OnNavigated<T>(this T shell, Action navigatedAction) where T : IShell
        {
            shell.NavigatedAction = navigatedAction;
            return shell;
        }

        public static T OnNavigated<T>(this T shell, Action<ShellNavigatedEventArgs> navigatedActionWithArgs) where T : IShell
        {
            shell.NavigatedActionWithArgs = navigatedActionWithArgs;
            return shell;
        }
        public static T OnNavigating<T>(this T shell, Action navigatingAction) where T : IShell
        {
            shell.NavigatingAction = navigatingAction;
            return shell;
        }

        public static T OnNavigating<T>(this T shell, Action<ShellNavigatingEventArgs> navigatingActionWithArgs) where T : IShell
        {
            shell.NavigatingActionWithArgs = navigatingActionWithArgs;
            return shell;
        }
    }
}
