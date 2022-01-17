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
    public partial interface IShell : IPage
    {
        PropertyValue<Microsoft.Maui.Controls.FlyoutBehavior>? FlyoutBehavior { get; set; }
        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? MenuItemTemplate { get; set; }
        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? ItemTemplate { get; set; }
        PropertyValue<Microsoft.Maui.Graphics.Color>? BackgroundColor { get; set; }
        PropertyValue<Microsoft.Maui.Controls.Brush>? FlyoutBackdrop { get; set; }
        PropertyValue<double>? FlyoutWidth { get; set; }
        PropertyValue<double>? FlyoutHeight { get; set; }
        PropertyValue<Microsoft.Maui.Controls.ImageSource>? FlyoutBackgroundImage { get; set; }
        PropertyValue<Microsoft.Maui.Aspect>? FlyoutBackgroundImageAspect { get; set; }
        PropertyValue<Microsoft.Maui.Graphics.Color>? FlyoutBackgroundColor { get; set; }
        PropertyValue<Microsoft.Maui.Controls.Brush>? FlyoutBackground { get; set; }
        PropertyValue<Microsoft.Maui.Controls.FlyoutHeaderBehavior>? FlyoutHeaderBehavior { get; set; }
        PropertyValue<object>? FlyoutHeader { get; set; }
        PropertyValue<object>? FlyoutFooter { get; set; }
        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? FlyoutHeaderTemplate { get; set; }
        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? FlyoutFooterTemplate { get; set; }
        PropertyValue<bool>? FlyoutIsPresented { get; set; }
        PropertyValue<Microsoft.Maui.Controls.ImageSource>? FlyoutIcon { get; set; }
        PropertyValue<Microsoft.Maui.Controls.ScrollMode>? FlyoutVerticalScrollMode { get; set; }
        PropertyValue<object>? FlyoutContent { get; set; }
        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? FlyoutContentTemplate { get; set; }

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

        PropertyValue<Microsoft.Maui.Controls.FlyoutBehavior>? IShell.FlyoutBehavior { get; set; }
        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? IShell.MenuItemTemplate { get; set; }
        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? IShell.ItemTemplate { get; set; }
        PropertyValue<Microsoft.Maui.Graphics.Color>? IShell.BackgroundColor { get; set; }
        PropertyValue<Microsoft.Maui.Controls.Brush>? IShell.FlyoutBackdrop { get; set; }
        PropertyValue<double>? IShell.FlyoutWidth { get; set; }
        PropertyValue<double>? IShell.FlyoutHeight { get; set; }
        PropertyValue<Microsoft.Maui.Controls.ImageSource>? IShell.FlyoutBackgroundImage { get; set; }
        PropertyValue<Microsoft.Maui.Aspect>? IShell.FlyoutBackgroundImageAspect { get; set; }
        PropertyValue<Microsoft.Maui.Graphics.Color>? IShell.FlyoutBackgroundColor { get; set; }
        PropertyValue<Microsoft.Maui.Controls.Brush>? IShell.FlyoutBackground { get; set; }
        PropertyValue<Microsoft.Maui.Controls.FlyoutHeaderBehavior>? IShell.FlyoutHeaderBehavior { get; set; }
        PropertyValue<object>? IShell.FlyoutHeader { get; set; }
        PropertyValue<object>? IShell.FlyoutFooter { get; set; }
        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? IShell.FlyoutHeaderTemplate { get; set; }
        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? IShell.FlyoutFooterTemplate { get; set; }
        PropertyValue<bool>? IShell.FlyoutIsPresented { get; set; }
        PropertyValue<Microsoft.Maui.Controls.ImageSource>? IShell.FlyoutIcon { get; set; }
        PropertyValue<Microsoft.Maui.Controls.ScrollMode>? IShell.FlyoutVerticalScrollMode { get; set; }
        PropertyValue<object>? IShell.FlyoutContent { get; set; }
        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? IShell.FlyoutContentTemplate { get; set; }

        Action? IShell.NavigatedAction { get; set; }
        Action<ShellNavigatedEventArgs>? IShell.NavigatedActionWithArgs { get; set; }
        Action? IShell.NavigatingAction { get; set; }
        Action<ShellNavigatingEventArgs>? IShell.NavigatingActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIShell = (IShell)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutBehaviorProperty, thisAsIShell.FlyoutBehavior);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.MenuItemTemplateProperty, thisAsIShell.MenuItemTemplate);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.ItemTemplateProperty, thisAsIShell.ItemTemplate);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.BackgroundColorProperty, thisAsIShell.BackgroundColor);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutBackdropProperty, thisAsIShell.FlyoutBackdrop);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutWidthProperty, thisAsIShell.FlyoutWidth);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutHeightProperty, thisAsIShell.FlyoutHeight);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutBackgroundImageProperty, thisAsIShell.FlyoutBackgroundImage);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutBackgroundImageAspectProperty, thisAsIShell.FlyoutBackgroundImageAspect);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutBackgroundColorProperty, thisAsIShell.FlyoutBackgroundColor);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutBackgroundProperty, thisAsIShell.FlyoutBackground);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutHeaderBehaviorProperty, thisAsIShell.FlyoutHeaderBehavior);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutHeaderProperty, thisAsIShell.FlyoutHeader);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutFooterProperty, thisAsIShell.FlyoutFooter);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutHeaderTemplateProperty, thisAsIShell.FlyoutHeaderTemplate);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutFooterTemplateProperty, thisAsIShell.FlyoutFooterTemplate);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutIsPresentedProperty, thisAsIShell.FlyoutIsPresented);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutIconProperty, thisAsIShell.FlyoutIcon);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutVerticalScrollModeProperty, thisAsIShell.FlyoutVerticalScrollMode);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutContentProperty, thisAsIShell.FlyoutContent);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutContentTemplateProperty, thisAsIShell.FlyoutContentTemplate);


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
            shell.FlyoutBehavior = new PropertyValue<Microsoft.Maui.Controls.FlyoutBehavior>(flyoutBehavior);
            return shell;
        }
        public static T FlyoutBehavior<T>(this T shell, Func<Microsoft.Maui.Controls.FlyoutBehavior> flyoutBehaviorFunc) where T : IShell
        {
            shell.FlyoutBehavior = new PropertyValue<Microsoft.Maui.Controls.FlyoutBehavior>(flyoutBehaviorFunc);
            return shell;
        }



        public static T MenuItemTemplate<T>(this T shell, Microsoft.Maui.Controls.DataTemplate menuItemTemplate) where T : IShell
        {
            shell.MenuItemTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(menuItemTemplate);
            return shell;
        }
        public static T MenuItemTemplate<T>(this T shell, Func<Microsoft.Maui.Controls.DataTemplate> menuItemTemplateFunc) where T : IShell
        {
            shell.MenuItemTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(menuItemTemplateFunc);
            return shell;
        }



        public static T ItemTemplate<T>(this T shell, Microsoft.Maui.Controls.DataTemplate itemTemplate) where T : IShell
        {
            shell.ItemTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(itemTemplate);
            return shell;
        }
        public static T ItemTemplate<T>(this T shell, Func<Microsoft.Maui.Controls.DataTemplate> itemTemplateFunc) where T : IShell
        {
            shell.ItemTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(itemTemplateFunc);
            return shell;
        }



        public static T BackgroundColor<T>(this T shell, Microsoft.Maui.Graphics.Color backgroundColor) where T : IShell
        {
            shell.BackgroundColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(backgroundColor);
            return shell;
        }
        public static T BackgroundColor<T>(this T shell, Func<Microsoft.Maui.Graphics.Color> backgroundColorFunc) where T : IShell
        {
            shell.BackgroundColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(backgroundColorFunc);
            return shell;
        }



        public static T FlyoutBackdrop<T>(this T shell, Microsoft.Maui.Controls.Brush flyoutBackdrop) where T : IShell
        {
            shell.FlyoutBackdrop = new PropertyValue<Microsoft.Maui.Controls.Brush>(flyoutBackdrop);
            return shell;
        }
        public static T FlyoutBackdrop<T>(this T shell, Func<Microsoft.Maui.Controls.Brush> flyoutBackdropFunc) where T : IShell
        {
            shell.FlyoutBackdrop = new PropertyValue<Microsoft.Maui.Controls.Brush>(flyoutBackdropFunc);
            return shell;
        }



        public static T FlyoutWidth<T>(this T shell, double flyoutWidth) where T : IShell
        {
            shell.FlyoutWidth = new PropertyValue<double>(flyoutWidth);
            return shell;
        }
        public static T FlyoutWidth<T>(this T shell, Func<double> flyoutWidthFunc) where T : IShell
        {
            shell.FlyoutWidth = new PropertyValue<double>(flyoutWidthFunc);
            return shell;
        }



        public static T FlyoutHeight<T>(this T shell, double flyoutHeight) where T : IShell
        {
            shell.FlyoutHeight = new PropertyValue<double>(flyoutHeight);
            return shell;
        }
        public static T FlyoutHeight<T>(this T shell, Func<double> flyoutHeightFunc) where T : IShell
        {
            shell.FlyoutHeight = new PropertyValue<double>(flyoutHeightFunc);
            return shell;
        }



        public static T FlyoutBackgroundImage<T>(this T shell, Microsoft.Maui.Controls.ImageSource flyoutBackgroundImage) where T : IShell
        {
            shell.FlyoutBackgroundImage = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(flyoutBackgroundImage);
            return shell;
        }
        public static T FlyoutBackgroundImage<T>(this T shell, Func<Microsoft.Maui.Controls.ImageSource> flyoutBackgroundImageFunc) where T : IShell
        {
            shell.FlyoutBackgroundImage = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(flyoutBackgroundImageFunc);
            return shell;
        }


        public static T FlyoutBackgroun<T>(this T shell, string file) where T : IShell
        {
            shell.FlyoutBackgroundImage = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromFile(file));
            return shell;
        }
        public static T FlyoutBackgroun<T>(this T shell, string fileAndroid, string fileiOS) where T : IShell
        {
            shell.FlyoutBackgroundImage = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Device.RuntimePlatform == Device.Android ? Microsoft.Maui.Controls.ImageSource.FromFile(fileAndroid) : Microsoft.Maui.Controls.ImageSource.FromFile(fileiOS));
            return shell;
        }
        public static T FlyoutBackgroun<T>(this T shell, string resourceName, Assembly sourceAssembly) where T : IShell
        {
            shell.FlyoutBackgroundImage = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly));
            return shell;
        }
        public static T FlyoutBackgroun<T>(this T shell, Uri imageUri) where T : IShell
        {
            shell.FlyoutBackgroundImage = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromUri(imageUri));
            return shell;
        }
        public static T FlyoutBackgroun<T>(this T shell, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IShell
        {
            shell.FlyoutBackgroundImage = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            });
            return shell;
        }
        public static T FlyoutBackgroun<T>(this T shell, Func<Stream> imageStream) where T : IShell
        {
            shell.FlyoutBackgroundImage = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromStream(imageStream));
            return shell;
        }

        public static T FlyoutBackgroundImageAspect<T>(this T shell, Microsoft.Maui.Aspect flyoutBackgroundImageAspect) where T : IShell
        {
            shell.FlyoutBackgroundImageAspect = new PropertyValue<Microsoft.Maui.Aspect>(flyoutBackgroundImageAspect);
            return shell;
        }
        public static T FlyoutBackgroundImageAspect<T>(this T shell, Func<Microsoft.Maui.Aspect> flyoutBackgroundImageAspectFunc) where T : IShell
        {
            shell.FlyoutBackgroundImageAspect = new PropertyValue<Microsoft.Maui.Aspect>(flyoutBackgroundImageAspectFunc);
            return shell;
        }



        public static T FlyoutBackgroundColor<T>(this T shell, Microsoft.Maui.Graphics.Color flyoutBackgroundColor) where T : IShell
        {
            shell.FlyoutBackgroundColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(flyoutBackgroundColor);
            return shell;
        }
        public static T FlyoutBackgroundColor<T>(this T shell, Func<Microsoft.Maui.Graphics.Color> flyoutBackgroundColorFunc) where T : IShell
        {
            shell.FlyoutBackgroundColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(flyoutBackgroundColorFunc);
            return shell;
        }



        public static T FlyoutBackground<T>(this T shell, Microsoft.Maui.Controls.Brush flyoutBackground) where T : IShell
        {
            shell.FlyoutBackground = new PropertyValue<Microsoft.Maui.Controls.Brush>(flyoutBackground);
            return shell;
        }
        public static T FlyoutBackground<T>(this T shell, Func<Microsoft.Maui.Controls.Brush> flyoutBackgroundFunc) where T : IShell
        {
            shell.FlyoutBackground = new PropertyValue<Microsoft.Maui.Controls.Brush>(flyoutBackgroundFunc);
            return shell;
        }



        public static T FlyoutHeaderBehavior<T>(this T shell, Microsoft.Maui.Controls.FlyoutHeaderBehavior flyoutHeaderBehavior) where T : IShell
        {
            shell.FlyoutHeaderBehavior = new PropertyValue<Microsoft.Maui.Controls.FlyoutHeaderBehavior>(flyoutHeaderBehavior);
            return shell;
        }
        public static T FlyoutHeaderBehavior<T>(this T shell, Func<Microsoft.Maui.Controls.FlyoutHeaderBehavior> flyoutHeaderBehaviorFunc) where T : IShell
        {
            shell.FlyoutHeaderBehavior = new PropertyValue<Microsoft.Maui.Controls.FlyoutHeaderBehavior>(flyoutHeaderBehaviorFunc);
            return shell;
        }



        public static T FlyoutHeader<T>(this T shell, object flyoutHeader) where T : IShell
        {
            shell.FlyoutHeader = new PropertyValue<object>(flyoutHeader);
            return shell;
        }
        public static T FlyoutHeader<T>(this T shell, Func<object> flyoutHeaderFunc) where T : IShell
        {
            shell.FlyoutHeader = new PropertyValue<object>(flyoutHeaderFunc);
            return shell;
        }



        public static T FlyoutFooter<T>(this T shell, object flyoutFooter) where T : IShell
        {
            shell.FlyoutFooter = new PropertyValue<object>(flyoutFooter);
            return shell;
        }
        public static T FlyoutFooter<T>(this T shell, Func<object> flyoutFooterFunc) where T : IShell
        {
            shell.FlyoutFooter = new PropertyValue<object>(flyoutFooterFunc);
            return shell;
        }



        public static T FlyoutHeaderTemplate<T>(this T shell, Microsoft.Maui.Controls.DataTemplate flyoutHeaderTemplate) where T : IShell
        {
            shell.FlyoutHeaderTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(flyoutHeaderTemplate);
            return shell;
        }
        public static T FlyoutHeaderTemplate<T>(this T shell, Func<Microsoft.Maui.Controls.DataTemplate> flyoutHeaderTemplateFunc) where T : IShell
        {
            shell.FlyoutHeaderTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(flyoutHeaderTemplateFunc);
            return shell;
        }



        public static T FlyoutFooterTemplate<T>(this T shell, Microsoft.Maui.Controls.DataTemplate flyoutFooterTemplate) where T : IShell
        {
            shell.FlyoutFooterTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(flyoutFooterTemplate);
            return shell;
        }
        public static T FlyoutFooterTemplate<T>(this T shell, Func<Microsoft.Maui.Controls.DataTemplate> flyoutFooterTemplateFunc) where T : IShell
        {
            shell.FlyoutFooterTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(flyoutFooterTemplateFunc);
            return shell;
        }



        public static T FlyoutIsPresented<T>(this T shell, bool flyoutIsPresented) where T : IShell
        {
            shell.FlyoutIsPresented = new PropertyValue<bool>(flyoutIsPresented);
            return shell;
        }
        public static T FlyoutIsPresented<T>(this T shell, Func<bool> flyoutIsPresentedFunc) where T : IShell
        {
            shell.FlyoutIsPresented = new PropertyValue<bool>(flyoutIsPresentedFunc);
            return shell;
        }



        public static T FlyoutIcon<T>(this T shell, Microsoft.Maui.Controls.ImageSource flyoutIcon) where T : IShell
        {
            shell.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(flyoutIcon);
            return shell;
        }
        public static T FlyoutIcon<T>(this T shell, Func<Microsoft.Maui.Controls.ImageSource> flyoutIconFunc) where T : IShell
        {
            shell.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(flyoutIconFunc);
            return shell;
        }


        public static T Flyo<T>(this T shell, string file) where T : IShell
        {
            shell.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromFile(file));
            return shell;
        }
        public static T Flyo<T>(this T shell, string fileAndroid, string fileiOS) where T : IShell
        {
            shell.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Device.RuntimePlatform == Device.Android ? Microsoft.Maui.Controls.ImageSource.FromFile(fileAndroid) : Microsoft.Maui.Controls.ImageSource.FromFile(fileiOS));
            return shell;
        }
        public static T Flyo<T>(this T shell, string resourceName, Assembly sourceAssembly) where T : IShell
        {
            shell.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly));
            return shell;
        }
        public static T Flyo<T>(this T shell, Uri imageUri) where T : IShell
        {
            shell.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromUri(imageUri));
            return shell;
        }
        public static T Flyo<T>(this T shell, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IShell
        {
            shell.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            });
            return shell;
        }
        public static T Flyo<T>(this T shell, Func<Stream> imageStream) where T : IShell
        {
            shell.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromStream(imageStream));
            return shell;
        }

        public static T FlyoutVerticalScrollMode<T>(this T shell, Microsoft.Maui.Controls.ScrollMode flyoutVerticalScrollMode) where T : IShell
        {
            shell.FlyoutVerticalScrollMode = new PropertyValue<Microsoft.Maui.Controls.ScrollMode>(flyoutVerticalScrollMode);
            return shell;
        }
        public static T FlyoutVerticalScrollMode<T>(this T shell, Func<Microsoft.Maui.Controls.ScrollMode> flyoutVerticalScrollModeFunc) where T : IShell
        {
            shell.FlyoutVerticalScrollMode = new PropertyValue<Microsoft.Maui.Controls.ScrollMode>(flyoutVerticalScrollModeFunc);
            return shell;
        }



        public static T FlyoutContent<T>(this T shell, object flyoutContent) where T : IShell
        {
            shell.FlyoutContent = new PropertyValue<object>(flyoutContent);
            return shell;
        }
        public static T FlyoutContent<T>(this T shell, Func<object> flyoutContentFunc) where T : IShell
        {
            shell.FlyoutContent = new PropertyValue<object>(flyoutContentFunc);
            return shell;
        }



        public static T FlyoutContentTemplate<T>(this T shell, Microsoft.Maui.Controls.DataTemplate flyoutContentTemplate) where T : IShell
        {
            shell.FlyoutContentTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(flyoutContentTemplate);
            return shell;
        }
        public static T FlyoutContentTemplate<T>(this T shell, Func<Microsoft.Maui.Controls.DataTemplate> flyoutContentTemplateFunc) where T : IShell
        {
            shell.FlyoutContentTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(flyoutContentTemplateFunc);
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
