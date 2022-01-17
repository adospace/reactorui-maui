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
    public partial interface IBaseShellItem : INavigableElement
    {
        PropertyValue<Microsoft.Maui.Controls.ImageSource>? FlyoutIcon { get; set; }
        PropertyValue<Microsoft.Maui.Controls.ImageSource>? Icon { get; set; }
        PropertyValue<bool>? IsEnabled { get; set; }
        PropertyValue<string>? Title { get; set; }
        PropertyValue<bool>? IsVisible { get; set; }

        Action? AppearingAction { get; set; }
        Action<EventArgs>? AppearingActionWithArgs { get; set; }
        Action? DisappearingAction { get; set; }
        Action<EventArgs>? DisappearingActionWithArgs { get; set; }

    }
    public partial class BaseShellItem<T> : NavigableElement<T>, IBaseShellItem where T : Microsoft.Maui.Controls.BaseShellItem, new()
    {
        public BaseShellItem()
        {

        }

        public BaseShellItem(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Controls.ImageSource>? IBaseShellItem.FlyoutIcon { get; set; }
        PropertyValue<Microsoft.Maui.Controls.ImageSource>? IBaseShellItem.Icon { get; set; }
        PropertyValue<bool>? IBaseShellItem.IsEnabled { get; set; }
        PropertyValue<string>? IBaseShellItem.Title { get; set; }
        PropertyValue<bool>? IBaseShellItem.IsVisible { get; set; }

        Action? IBaseShellItem.AppearingAction { get; set; }
        Action<EventArgs>? IBaseShellItem.AppearingActionWithArgs { get; set; }
        Action? IBaseShellItem.DisappearingAction { get; set; }
        Action<EventArgs>? IBaseShellItem.DisappearingActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIBaseShellItem = (IBaseShellItem)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.BaseShellItem.FlyoutIconProperty, thisAsIBaseShellItem.FlyoutIcon);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.BaseShellItem.IconProperty, thisAsIBaseShellItem.Icon);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.BaseShellItem.IsEnabledProperty, thisAsIBaseShellItem.IsEnabled);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.BaseShellItem.TitleProperty, thisAsIBaseShellItem.Title);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.BaseShellItem.IsVisibleProperty, thisAsIBaseShellItem.IsVisible);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIBaseShellItem = (IBaseShellItem)this;
            if (thisAsIBaseShellItem.AppearingAction != null || thisAsIBaseShellItem.AppearingActionWithArgs != null)
            {
                NativeControl.Appearing += NativeControl_Appearing;
            }
            if (thisAsIBaseShellItem.DisappearingAction != null || thisAsIBaseShellItem.DisappearingActionWithArgs != null)
            {
                NativeControl.Disappearing += NativeControl_Disappearing;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Appearing(object? sender, EventArgs e)
        {
            var thisAsIBaseShellItem = (IBaseShellItem)this;
            thisAsIBaseShellItem.AppearingAction?.Invoke();
            thisAsIBaseShellItem.AppearingActionWithArgs?.Invoke(e);
        }
        private void NativeControl_Disappearing(object? sender, EventArgs e)
        {
            var thisAsIBaseShellItem = (IBaseShellItem)this;
            thisAsIBaseShellItem.DisappearingAction?.Invoke();
            thisAsIBaseShellItem.DisappearingActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.Appearing -= NativeControl_Appearing;
                NativeControl.Disappearing -= NativeControl_Disappearing;
            }

            base.OnDetachNativeEvents();
        }

    }

    public partial class BaseShellItem : BaseShellItem<Microsoft.Maui.Controls.BaseShellItem>
    {
        public BaseShellItem()
        {

        }

        public BaseShellItem(Action<Microsoft.Maui.Controls.BaseShellItem?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class BaseShellItemExtensions
    {
        public static T FlyoutIcon<T>(this T baseshellitem, Microsoft.Maui.Controls.ImageSource flyoutIcon) where T : IBaseShellItem
        {
            baseshellitem.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(flyoutIcon);
            return baseshellitem;
        }
        public static T FlyoutIcon<T>(this T baseshellitem, Func<Microsoft.Maui.Controls.ImageSource> flyoutIconFunc) where T : IBaseShellItem
        {
            baseshellitem.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(flyoutIconFunc);
            return baseshellitem;
        }


        public static T Flyo<T>(this T baseshellitem, string file) where T : IBaseShellItem
        {
            baseshellitem.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromFile(file));
            return baseshellitem;
        }
        public static T Flyo<T>(this T baseshellitem, string fileAndroid, string fileiOS) where T : IBaseShellItem
        {
            baseshellitem.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Device.RuntimePlatform == Device.Android ? Microsoft.Maui.Controls.ImageSource.FromFile(fileAndroid) : Microsoft.Maui.Controls.ImageSource.FromFile(fileiOS));
            return baseshellitem;
        }
        public static T Flyo<T>(this T baseshellitem, string resourceName, Assembly sourceAssembly) where T : IBaseShellItem
        {
            baseshellitem.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly));
            return baseshellitem;
        }
        public static T Flyo<T>(this T baseshellitem, Uri imageUri) where T : IBaseShellItem
        {
            baseshellitem.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromUri(imageUri));
            return baseshellitem;
        }
        public static T Flyo<T>(this T baseshellitem, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IBaseShellItem
        {
            baseshellitem.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            });
            return baseshellitem;
        }
        public static T Flyo<T>(this T baseshellitem, Func<Stream> imageStream) where T : IBaseShellItem
        {
            baseshellitem.FlyoutIcon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromStream(imageStream));
            return baseshellitem;
        }

        public static T Icon<T>(this T baseshellitem, Microsoft.Maui.Controls.ImageSource icon) where T : IBaseShellItem
        {
            baseshellitem.Icon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(icon);
            return baseshellitem;
        }
        public static T Icon<T>(this T baseshellitem, Func<Microsoft.Maui.Controls.ImageSource> iconFunc) where T : IBaseShellItem
        {
            baseshellitem.Icon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(iconFunc);
            return baseshellitem;
        }


        public static T Icon<T>(this T baseshellitem, string file) where T : IBaseShellItem
        {
            baseshellitem.Icon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromFile(file));
            return baseshellitem;
        }
        public static T Icon<T>(this T baseshellitem, string fileAndroid, string fileiOS) where T : IBaseShellItem
        {
            baseshellitem.Icon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Device.RuntimePlatform == Device.Android ? Microsoft.Maui.Controls.ImageSource.FromFile(fileAndroid) : Microsoft.Maui.Controls.ImageSource.FromFile(fileiOS));
            return baseshellitem;
        }
        public static T Icon<T>(this T baseshellitem, string resourceName, Assembly sourceAssembly) where T : IBaseShellItem
        {
            baseshellitem.Icon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly));
            return baseshellitem;
        }
        public static T Icon<T>(this T baseshellitem, Uri imageUri) where T : IBaseShellItem
        {
            baseshellitem.Icon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromUri(imageUri));
            return baseshellitem;
        }
        public static T Icon<T>(this T baseshellitem, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity) where T : IBaseShellItem
        {
            baseshellitem.Icon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(new UriImageSource
            {
                Uri = imageUri,
                CachingEnabled = cachingEnabled,
                CacheValidity = cacheValidity
            });
            return baseshellitem;
        }
        public static T Icon<T>(this T baseshellitem, Func<Stream> imageStream) where T : IBaseShellItem
        {
            baseshellitem.Icon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(Microsoft.Maui.Controls.ImageSource.FromStream(imageStream));
            return baseshellitem;
        }

        public static T IsEnabled<T>(this T baseshellitem, bool isEnabled) where T : IBaseShellItem
        {
            baseshellitem.IsEnabled = new PropertyValue<bool>(isEnabled);
            return baseshellitem;
        }
        public static T IsEnabled<T>(this T baseshellitem, Func<bool> isEnabledFunc) where T : IBaseShellItem
        {
            baseshellitem.IsEnabled = new PropertyValue<bool>(isEnabledFunc);
            return baseshellitem;
        }



        public static T Title<T>(this T baseshellitem, string title) where T : IBaseShellItem
        {
            baseshellitem.Title = new PropertyValue<string>(title);
            return baseshellitem;
        }
        public static T Title<T>(this T baseshellitem, Func<string> titleFunc) where T : IBaseShellItem
        {
            baseshellitem.Title = new PropertyValue<string>(titleFunc);
            return baseshellitem;
        }



        public static T IsVisible<T>(this T baseshellitem, bool isVisible) where T : IBaseShellItem
        {
            baseshellitem.IsVisible = new PropertyValue<bool>(isVisible);
            return baseshellitem;
        }
        public static T IsVisible<T>(this T baseshellitem, Func<bool> isVisibleFunc) where T : IBaseShellItem
        {
            baseshellitem.IsVisible = new PropertyValue<bool>(isVisibleFunc);
            return baseshellitem;
        }




        public static T OnAppearing<T>(this T baseshellitem, Action appearingAction) where T : IBaseShellItem
        {
            baseshellitem.AppearingAction = appearingAction;
            return baseshellitem;
        }

        public static T OnAppearing<T>(this T baseshellitem, Action<EventArgs> appearingActionWithArgs) where T : IBaseShellItem
        {
            baseshellitem.AppearingActionWithArgs = appearingActionWithArgs;
            return baseshellitem;
        }
        public static T OnDisappearing<T>(this T baseshellitem, Action disappearingAction) where T : IBaseShellItem
        {
            baseshellitem.DisappearingAction = disappearingAction;
            return baseshellitem;
        }

        public static T OnDisappearing<T>(this T baseshellitem, Action<EventArgs> disappearingActionWithArgs) where T : IBaseShellItem
        {
            baseshellitem.DisappearingActionWithArgs = disappearingActionWithArgs;
            return baseshellitem;
        }
    }
}
