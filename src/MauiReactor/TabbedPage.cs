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
public partial interface ITabbedPage : IGenericMultiPage
{
    object? BarBackgroundColor { get; set; }

    object? BarBackground { get; set; }

    object? BarTextColor { get; set; }

    object? UnselectedTabColor { get; set; }

    object? SelectedTabColor { get; set; }
}

public abstract partial class TabbedPage<T> : MultiPage<T, Microsoft.Maui.Controls.Page>, ITabbedPage where T : Microsoft.Maui.Controls.TabbedPage, new()
{
    public TabbedPage()
    {
        TabbedPageStyles.Default?.Invoke(this);
    }

    public TabbedPage(Action<T?> componentRefAction) : base(componentRefAction)
    {
        TabbedPageStyles.Default?.Invoke(this);
    }

    object? ITabbedPage.BarBackgroundColor { get; set; }

    object? ITabbedPage.BarBackground { get; set; }

    object? ITabbedPage.BarTextColor { get; set; }

    object? ITabbedPage.UnselectedTabColor { get; set; }

    object? ITabbedPage.SelectedTabColor { get; set; }

    internal override void Reset()
    {
        base.Reset();
        var thisAsITabbedPage = (ITabbedPage)this;
        thisAsITabbedPage.BarBackgroundColor = null;
        thisAsITabbedPage.BarBackground = null;
        thisAsITabbedPage.BarTextColor = null;
        thisAsITabbedPage.UnselectedTabColor = null;
        thisAsITabbedPage.SelectedTabColor = null;
        OnReset();
    }

    partial void OnReset();
    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsITabbedPage = (ITabbedPage)this;
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TabbedPage.BarBackgroundColorProperty, thisAsITabbedPage.BarBackgroundColor);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TabbedPage.BarBackgroundProperty, thisAsITabbedPage.BarBackground);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TabbedPage.BarTextColorProperty, thisAsITabbedPage.BarTextColor);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TabbedPage.UnselectedTabColorProperty, thisAsITabbedPage.UnselectedTabColor);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TabbedPage.SelectedTabColorProperty, thisAsITabbedPage.SelectedTabColor);
        base.OnUpdate();
        OnEndUpdate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && TabbedPageStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }
}

public partial class TabbedPage : TabbedPage<Microsoft.Maui.Controls.TabbedPage>
{
    public TabbedPage()
    {
    }

    public TabbedPage(Action<Microsoft.Maui.Controls.TabbedPage?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class TabbedPageExtensions
{
    public static T BarBackgroundColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color barBackgroundColor)
        where T : ITabbedPage
    {
        tabbedPage.BarBackgroundColor = barBackgroundColor;
        return tabbedPage;
    }

    public static T BarBackgroundColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> barBackgroundColorFunc)
        where T : ITabbedPage
    {
        tabbedPage.BarBackgroundColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(barBackgroundColorFunc);
        return tabbedPage;
    }

    public static T BarBackground<T>(this T tabbedPage, Microsoft.Maui.Controls.Brush barBackground)
        where T : ITabbedPage
    {
        tabbedPage.BarBackground = barBackground;
        return tabbedPage;
    }

    public static T BarBackground<T>(this T tabbedPage, Func<Microsoft.Maui.Controls.Brush> barBackgroundFunc)
        where T : ITabbedPage
    {
        tabbedPage.BarBackground = new PropertyValue<Microsoft.Maui.Controls.Brush>(barBackgroundFunc);
        return tabbedPage;
    }

    public static T BarTextColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color barTextColor)
        where T : ITabbedPage
    {
        tabbedPage.BarTextColor = barTextColor;
        return tabbedPage;
    }

    public static T BarTextColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> barTextColorFunc)
        where T : ITabbedPage
    {
        tabbedPage.BarTextColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(barTextColorFunc);
        return tabbedPage;
    }

    public static T UnselectedTabColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color unselectedTabColor)
        where T : ITabbedPage
    {
        tabbedPage.UnselectedTabColor = unselectedTabColor;
        return tabbedPage;
    }

    public static T UnselectedTabColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> unselectedTabColorFunc)
        where T : ITabbedPage
    {
        tabbedPage.UnselectedTabColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(unselectedTabColorFunc);
        return tabbedPage;
    }

    public static T SelectedTabColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color selectedTabColor)
        where T : ITabbedPage
    {
        tabbedPage.SelectedTabColor = selectedTabColor;
        return tabbedPage;
    }

    public static T SelectedTabColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> selectedTabColorFunc)
        where T : ITabbedPage
    {
        tabbedPage.SelectedTabColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(selectedTabColorFunc);
        return tabbedPage;
    }
}

public static partial class TabbedPageStyles
{
    public static Action<ITabbedPage>? Default { get; set; }
    public static Dictionary<string, Action<ITabbedPage>> Themes { get; } = [];
}