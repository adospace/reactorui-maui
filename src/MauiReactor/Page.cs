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
    public partial interface IPage
    {
        Microsoft.Maui.Controls.ImageSource BackgroundImageSource { get; set; }
        bool IsBusy { get; set; }
        Microsoft.Maui.Thickness Padding { get; set; }
        string Title { get; set; }
        Microsoft.Maui.Controls.ImageSource IconImageSource { get; set; }

        Action? NavigatedToAction { get; set; }
        Action<NavigatedToEventArgs>? NavigatedToActionWithArgs { get; set; }
        Action? NavigatingFromAction { get; set; }
        Action<NavigatingFromEventArgs>? NavigatingFromActionWithArgs { get; set; }
        Action? NavigatedFromAction { get; set; }
        Action<NavigatedFromEventArgs>? NavigatedFromActionWithArgs { get; set; }
        Action? LayoutChangedAction { get; set; }
        Action<EventArgs>? LayoutChangedActionWithArgs { get; set; }
        Action? AppearingAction { get; set; }
        Action<EventArgs>? AppearingActionWithArgs { get; set; }
        Action? DisappearingAction { get; set; }
        Action<EventArgs>? DisappearingActionWithArgs { get; set; }

    }

    public partial class Page<T> : VisualElement<T>, IPage where T : Microsoft.Maui.Controls.Page, new()
    {
        public Page()
        {

        }

        public Page(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Controls.ImageSource IPage.BackgroundImageSource { get; set; } = (Microsoft.Maui.Controls.ImageSource)Microsoft.Maui.Controls.Page.BackgroundImageSourceProperty.DefaultValue;
        bool IPage.IsBusy { get; set; } = (bool)Microsoft.Maui.Controls.Page.IsBusyProperty.DefaultValue;
        Microsoft.Maui.Thickness IPage.Padding { get; set; } = (Microsoft.Maui.Thickness)Microsoft.Maui.Controls.Page.PaddingProperty.DefaultValue;
        string IPage.Title { get; set; } = (string)Microsoft.Maui.Controls.Page.TitleProperty.DefaultValue;
        Microsoft.Maui.Controls.ImageSource IPage.IconImageSource { get; set; } = (Microsoft.Maui.Controls.ImageSource)Microsoft.Maui.Controls.Page.IconImageSourceProperty.DefaultValue;

        Action? IPage.NavigatedToAction { get; set; }
        Action<NavigatedToEventArgs>? IPage.NavigatedToActionWithArgs { get; set; }
        Action? IPage.NavigatingFromAction { get; set; }
        Action<NavigatingFromEventArgs>? IPage.NavigatingFromActionWithArgs { get; set; }
        Action? IPage.NavigatedFromAction { get; set; }
        Action<NavigatedFromEventArgs>? IPage.NavigatedFromActionWithArgs { get; set; }
        Action? IPage.LayoutChangedAction { get; set; }
        Action<EventArgs>? IPage.LayoutChangedActionWithArgs { get; set; }
        Action? IPage.AppearingAction { get; set; }
        Action<EventArgs>? IPage.AppearingActionWithArgs { get; set; }
        Action? IPage.DisappearingAction { get; set; }
        Action<EventArgs>? IPage.DisappearingActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIPage = (IPage)this;
            if (NativeControl.BackgroundImageSource != thisAsIPage.BackgroundImageSource) NativeControl.BackgroundImageSource = thisAsIPage.BackgroundImageSource;
            if (NativeControl.IsBusy != thisAsIPage.IsBusy) NativeControl.IsBusy = thisAsIPage.IsBusy;
            if (NativeControl.Padding != thisAsIPage.Padding) NativeControl.Padding = thisAsIPage.Padding;
            if (NativeControl.Title != thisAsIPage.Title) NativeControl.Title = thisAsIPage.Title;
            if (NativeControl.IconImageSource != thisAsIPage.IconImageSource) NativeControl.IconImageSource = thisAsIPage.IconImageSource;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIPage = (IPage)this;
            if (thisAsIPage.NavigatedToAction != null || thisAsIPage.NavigatedToActionWithArgs != null)
            {
                NativeControl.NavigatedTo += NativeControl_NavigatedTo;
            }
            if (thisAsIPage.NavigatingFromAction != null || thisAsIPage.NavigatingFromActionWithArgs != null)
            {
                NativeControl.NavigatingFrom += NativeControl_NavigatingFrom;
            }
            if (thisAsIPage.NavigatedFromAction != null || thisAsIPage.NavigatedFromActionWithArgs != null)
            {
                NativeControl.NavigatedFrom += NativeControl_NavigatedFrom;
            }
            if (thisAsIPage.LayoutChangedAction != null || thisAsIPage.LayoutChangedActionWithArgs != null)
            {
                NativeControl.LayoutChanged += NativeControl_LayoutChanged;
            }
            if (thisAsIPage.AppearingAction != null || thisAsIPage.AppearingActionWithArgs != null)
            {
                NativeControl.Appearing += NativeControl_Appearing;
            }
            if (thisAsIPage.DisappearingAction != null || thisAsIPage.DisappearingActionWithArgs != null)
            {
                NativeControl.Disappearing += NativeControl_Disappearing;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_NavigatedTo(object? sender, NavigatedToEventArgs e)
        {
            var thisAsIPage = (IPage)this;
            thisAsIPage.NavigatedToAction?.Invoke();
            thisAsIPage.NavigatedToActionWithArgs?.Invoke(e);
        }
        private void NativeControl_NavigatingFrom(object? sender, NavigatingFromEventArgs e)
        {
            var thisAsIPage = (IPage)this;
            thisAsIPage.NavigatingFromAction?.Invoke();
            thisAsIPage.NavigatingFromActionWithArgs?.Invoke(e);
        }
        private void NativeControl_NavigatedFrom(object? sender, NavigatedFromEventArgs e)
        {
            var thisAsIPage = (IPage)this;
            thisAsIPage.NavigatedFromAction?.Invoke();
            thisAsIPage.NavigatedFromActionWithArgs?.Invoke(e);
        }
        private void NativeControl_LayoutChanged(object? sender, EventArgs e)
        {
            var thisAsIPage = (IPage)this;
            thisAsIPage.LayoutChangedAction?.Invoke();
            thisAsIPage.LayoutChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_Appearing(object? sender, EventArgs e)
        {
            var thisAsIPage = (IPage)this;
            thisAsIPage.AppearingAction?.Invoke();
            thisAsIPage.AppearingActionWithArgs?.Invoke(e);
        }
        private void NativeControl_Disappearing(object? sender, EventArgs e)
        {
            var thisAsIPage = (IPage)this;
            thisAsIPage.DisappearingAction?.Invoke();
            thisAsIPage.DisappearingActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.NavigatedTo -= NativeControl_NavigatedTo;
                NativeControl.NavigatingFrom -= NativeControl_NavigatingFrom;
                NativeControl.NavigatedFrom -= NativeControl_NavigatedFrom;
                NativeControl.LayoutChanged -= NativeControl_LayoutChanged;
                NativeControl.Appearing -= NativeControl_Appearing;
                NativeControl.Disappearing -= NativeControl_Disappearing;
            }

            base.OnDetachNativeEvents();
        }

    }

    public class Page : Page<Microsoft.Maui.Controls.Page>
    {
        public Page()
        {

        }

        public Page(Action<Microsoft.Maui.Controls.Page?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class PageExtensions
    {
        public static T BackgroundImageSource<T>(this T page, Microsoft.Maui.Controls.ImageSource backgroundImageSource) where T : IPage
        {
            page.BackgroundImageSource = backgroundImageSource;
            return page;
        }
        public static T BackgroundImage<T>(this T page, string file) where T : IPage
        {
            page.BackgroundImageSource = ImageSource.FromFile(file);
            return page;
        }
        public static T BackgroundImage<T>(this T page, string fileAndroid, string fileiOS) where T : IPage
        {
            page.BackgroundImageSource = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return page;
        }
        public static T BackgroundImage<T>(this T page, string resourceName, Assembly sourceAssembly) where T : IPage
        {
            page.BackgroundImageSource = ImageSource.FromResource(resourceName, sourceAssembly);
            return page;
        }
        public static T BackgroundImage<T>(this T page, Uri imageUri) where T : IPage
        {
            page.BackgroundImageSource = ImageSource.FromUri(imageUri);
            return page;
        }
        public static T BackgroundImage<T>(this T page, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IPage
        {
            page.BackgroundImageSource = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return page;
        }
        public static T BackgroundImage<T>(this T page, Func<Stream> imageStream) where T : IPage
        {
            page.BackgroundImageSource = ImageSource.FromStream(imageStream);
            return page;
        }

        public static T IsBusy<T>(this T page, bool isBusy) where T : IPage
        {
            page.IsBusy = isBusy;
            return page;
        }

        public static T Padding<T>(this T page, Microsoft.Maui.Thickness padding) where T : IPage
        {
            page.Padding = padding;
            return page;
        }
        public static T Padding<T>(this T page, double leftRight, double topBottom) where T : IPage
        {
            page.Padding = new Thickness(leftRight, topBottom);
            return page;
        }
        public static T Padding<T>(this T page, double uniformSize) where T : IPage
        {
            page.Padding = new Thickness(uniformSize);
            return page;
        }

        public static T Title<T>(this T page, string title) where T : IPage
        {
            page.Title = title;
            return page;
        }

        public static T IconImageSource<T>(this T page, Microsoft.Maui.Controls.ImageSource iconImageSource) where T : IPage
        {
            page.IconImageSource = iconImageSource;
            return page;
        }
        public static T IconImage<T>(this T page, string file) where T : IPage
        {
            page.IconImageSource = ImageSource.FromFile(file);
            return page;
        }
        public static T IconImage<T>(this T page, string fileAndroid, string fileiOS) where T : IPage
        {
            page.IconImageSource = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile(fileAndroid) : ImageSource.FromFile(fileiOS);
            return page;
        }
        public static T IconImage<T>(this T page, string resourceName, Assembly sourceAssembly) where T : IPage
        {
            page.IconImageSource = ImageSource.FromResource(resourceName, sourceAssembly);
            return page;
        }
        public static T IconImage<T>(this T page, Uri imageUri) where T : IPage
        {
            page.IconImageSource = ImageSource.FromUri(imageUri);
            return page;
        }
        public static T IconImage<T>(this T page, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IPage
        {
            page.IconImageSource = new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            };
            return page;
        }
        public static T IconImage<T>(this T page, Func<Stream> imageStream) where T : IPage
        {
            page.IconImageSource = ImageSource.FromStream(imageStream);
            return page;
        }


        public static T OnNavigatedTo<T>(this T page, Action navigatedtoAction) where T : IPage
        {
            page.NavigatedToAction = navigatedtoAction;
            return page;
        }

        public static T OnNavigatedTo<T>(this T page, Action<NavigatedToEventArgs> navigatedtoActionWithArgs) where T : IPage
        {
            page.NavigatedToActionWithArgs = navigatedtoActionWithArgs;
            return page;
        }
        public static T OnNavigatingFrom<T>(this T page, Action navigatingfromAction) where T : IPage
        {
            page.NavigatingFromAction = navigatingfromAction;
            return page;
        }

        public static T OnNavigatingFrom<T>(this T page, Action<NavigatingFromEventArgs> navigatingfromActionWithArgs) where T : IPage
        {
            page.NavigatingFromActionWithArgs = navigatingfromActionWithArgs;
            return page;
        }
        public static T OnNavigatedFrom<T>(this T page, Action navigatedfromAction) where T : IPage
        {
            page.NavigatedFromAction = navigatedfromAction;
            return page;
        }

        public static T OnNavigatedFrom<T>(this T page, Action<NavigatedFromEventArgs> navigatedfromActionWithArgs) where T : IPage
        {
            page.NavigatedFromActionWithArgs = navigatedfromActionWithArgs;
            return page;
        }
        public static T OnLayoutChanged<T>(this T page, Action layoutchangedAction) where T : IPage
        {
            page.LayoutChangedAction = layoutchangedAction;
            return page;
        }

        public static T OnLayoutChanged<T>(this T page, Action<EventArgs> layoutchangedActionWithArgs) where T : IPage
        {
            page.LayoutChangedActionWithArgs = layoutchangedActionWithArgs;
            return page;
        }
        public static T OnAppearing<T>(this T page, Action appearingAction) where T : IPage
        {
            page.AppearingAction = appearingAction;
            return page;
        }

        public static T OnAppearing<T>(this T page, Action<EventArgs> appearingActionWithArgs) where T : IPage
        {
            page.AppearingActionWithArgs = appearingActionWithArgs;
            return page;
        }
        public static T OnDisappearing<T>(this T page, Action disappearingAction) where T : IPage
        {
            page.DisappearingAction = disappearingAction;
            return page;
        }

        public static T OnDisappearing<T>(this T page, Action<EventArgs> disappearingActionWithArgs) where T : IPage
        {
            page.DisappearingActionWithArgs = disappearingActionWithArgs;
            return page;
        }
    }
}
