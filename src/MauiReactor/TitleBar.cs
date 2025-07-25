// <auto-generated />
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;

#nullable enable
namespace MauiReactor;
public partial interface ITitleBar : ITemplatedView
{
}

public partial class TitleBar<T> : TemplatedView<T>, ITitleBar where T : Microsoft.Maui.Controls.TitleBar, new()
{
    public TitleBar(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        TitleBarStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && TitleBarStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class TitleBar : TitleBar<Microsoft.Maui.Controls.TitleBar>
{
    public TitleBar(Action<Microsoft.Maui.Controls.TitleBar?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    public TitleBar(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class TitleBarExtensions
{
    public static T Icon<T>(this T titleBar, Microsoft.Maui.Controls.ImageSource icon)
        where T : ITitleBar
    {
        //titleBar.Icon = icon;
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.IconProperty, icon);
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, Func<Microsoft.Maui.Controls.ImageSource> iconFunc, IComponentWithState? componentWithState = null)
        where T : ITitleBar
    {
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.IconProperty, new PropertyValue<Microsoft.Maui.Controls.ImageSource>(iconFunc, componentWithState));
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, string file)
        where T : ITitleBar
    {
        //titleBar.Icon = Microsoft.Maui.Controls.ImageSource.FromFile(file);
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.IconProperty, Microsoft.Maui.Controls.ImageSource.FromFile(file));
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, Func<string> action)
        where T : ITitleBar
    {
        /*titleBar.Icon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(
            () => Microsoft.Maui.Controls.ImageSource.FromFile(action()));*/
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.IconProperty, new PropertyValue<Microsoft.Maui.Controls.ImageSource>(() => Microsoft.Maui.Controls.ImageSource.FromFile(action())));
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, string resourceName, Assembly sourceAssembly)
        where T : ITitleBar
    {
        //titleBar.Icon = Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly);
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.IconProperty, Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly));
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, Uri imageUri)
        where T : ITitleBar
    {
        //titleBar.Icon = Microsoft.Maui.Controls.ImageSource.FromUri(imageUri);
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.IconProperty, Microsoft.Maui.Controls.ImageSource.FromUri(imageUri));
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity)
        where T : ITitleBar
    {
        //titleBar.Icon = new Microsoft.Maui.Controls.UriImageSource
        //{
        //    Uri = imageUri,
        //    CachingEnabled = cachingEnabled,
        //    CacheValidity = cacheValidity
        //};
        var newValue = new Microsoft.Maui.Controls.UriImageSource
        {
            Uri = imageUri,
            CachingEnabled = cachingEnabled,
            CacheValidity = cacheValidity
        };
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.IconProperty, newValue);
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, Func<Stream> imageStream)
        where T : ITitleBar
    {
        //titleBar.Icon = Microsoft.Maui.Controls.ImageSource.FromStream(imageStream);
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.IconProperty, Microsoft.Maui.Controls.ImageSource.FromStream(imageStream));
        return titleBar;
    }

    public static T Title<T>(this T titleBar, string title)
        where T : ITitleBar
    {
        //titleBar.Title = title;
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.TitleProperty, title);
        return titleBar;
    }

    public static T Title<T>(this T titleBar, Func<string> titleFunc, IComponentWithState? componentWithState = null)
        where T : ITitleBar
    {
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.TitleProperty, new PropertyValue<string>(titleFunc, componentWithState));
        return titleBar;
    }

    public static T Subtitle<T>(this T titleBar, string subtitle)
        where T : ITitleBar
    {
        //titleBar.Subtitle = subtitle;
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.SubtitleProperty, subtitle);
        return titleBar;
    }

    public static T Subtitle<T>(this T titleBar, Func<string> subtitleFunc, IComponentWithState? componentWithState = null)
        where T : ITitleBar
    {
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.SubtitleProperty, new PropertyValue<string>(subtitleFunc, componentWithState));
        return titleBar;
    }

    public static T ForegroundColor<T>(this T titleBar, Microsoft.Maui.Graphics.Color foregroundColor)
        where T : ITitleBar
    {
        //titleBar.ForegroundColor = foregroundColor;
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.ForegroundColorProperty, foregroundColor);
        return titleBar;
    }

    public static T ForegroundColor<T>(this T titleBar, Func<Microsoft.Maui.Graphics.Color> foregroundColorFunc, IComponentWithState? componentWithState = null)
        where T : ITitleBar
    {
        titleBar.SetProperty(Microsoft.Maui.Controls.TitleBar.ForegroundColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(foregroundColorFunc, componentWithState));
        return titleBar;
    }
}

public static partial class TitleBarStyles
{
    public static Action<ITitleBar>? Default { get; set; }
    public static Dictionary<string, Action<ITitleBar>> Themes { get; } = [];
}