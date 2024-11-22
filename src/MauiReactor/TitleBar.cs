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
    object? Icon { get; set; }

    object? Title { get; set; }

    object? Subtitle { get; set; }

    object? ForegroundColor { get; set; }
}

public partial class TitleBar<T> : TemplatedView<T>, ITitleBar where T : Microsoft.Maui.Controls.TitleBar, new()
{
    public TitleBar()
    {
        TitleBarStyles.Default?.Invoke(this);
    }

    public TitleBar(Action<T?> componentRefAction) : base(componentRefAction)
    {
        TitleBarStyles.Default?.Invoke(this);
    }

    object? ITitleBar.Icon { get; set; }

    object? ITitleBar.Title { get; set; }

    object? ITitleBar.Subtitle { get; set; }

    object? ITitleBar.ForegroundColor { get; set; }

    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsITitleBar = (ITitleBar)this;
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TitleBar.IconProperty, thisAsITitleBar.Icon);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TitleBar.TitleProperty, thisAsITitleBar.Title);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TitleBar.SubtitleProperty, thisAsITitleBar.Subtitle);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TitleBar.ForegroundColorProperty, thisAsITitleBar.ForegroundColor);
        base.OnUpdate();
        OnEndUpdate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
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
    public TitleBar()
    {
    }

    public TitleBar(Action<Microsoft.Maui.Controls.TitleBar?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class TitleBarExtensions
{
    public static T Icon<T>(this T titleBar, Microsoft.Maui.Controls.ImageSource icon)
        where T : ITitleBar
    {
        titleBar.Icon = icon;
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, Func<Microsoft.Maui.Controls.ImageSource> iconFunc)
        where T : ITitleBar
    {
        titleBar.Icon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(iconFunc);
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, string file)
        where T : ITitleBar
    {
        titleBar.Icon = Microsoft.Maui.Controls.ImageSource.FromFile(file);
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, Func<string> action)
        where T : ITitleBar
    {
        titleBar.Icon = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(() => Microsoft.Maui.Controls.ImageSource.FromFile(action()));
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, string resourceName, Assembly sourceAssembly)
        where T : ITitleBar
    {
        titleBar.Icon = Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly);
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, Uri imageUri)
        where T : ITitleBar
    {
        titleBar.Icon = Microsoft.Maui.Controls.ImageSource.FromUri(imageUri);
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity)
        where T : ITitleBar
    {
        titleBar.Icon = new UriImageSource
        {
            Uri = imageUri,
            CachingEnabled = cachingEnabled,
            CacheValidity = cacheValidity
        };
        return titleBar;
    }

    public static T Icon<T>(this T titleBar, Func<Stream> imageStream)
        where T : ITitleBar
    {
        titleBar.Icon = Microsoft.Maui.Controls.ImageSource.FromStream(imageStream);
        return titleBar;
    }

    public static T Title<T>(this T titleBar, string title)
        where T : ITitleBar
    {
        titleBar.Title = title;
        return titleBar;
    }

    public static T Title<T>(this T titleBar, Func<string> titleFunc)
        where T : ITitleBar
    {
        titleBar.Title = new PropertyValue<string>(titleFunc);
        return titleBar;
    }

    public static T Subtitle<T>(this T titleBar, string subtitle)
        where T : ITitleBar
    {
        titleBar.Subtitle = subtitle;
        return titleBar;
    }

    public static T Subtitle<T>(this T titleBar, Func<string> subtitleFunc)
        where T : ITitleBar
    {
        titleBar.Subtitle = new PropertyValue<string>(subtitleFunc);
        return titleBar;
    }

    public static T ForegroundColor<T>(this T titleBar, Microsoft.Maui.Graphics.Color foregroundColor)
        where T : ITitleBar
    {
        titleBar.ForegroundColor = foregroundColor;
        return titleBar;
    }

    public static T ForegroundColor<T>(this T titleBar, Func<Microsoft.Maui.Graphics.Color> foregroundColorFunc)
        where T : ITitleBar
    {
        titleBar.ForegroundColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(foregroundColorFunc);
        return titleBar;
    }
}

public static partial class TitleBarStyles
{
    public static Action<ITitleBar>? Default { get; set; }
    public static Dictionary<string, Action<ITitleBar>> Themes { get; } = [];
}