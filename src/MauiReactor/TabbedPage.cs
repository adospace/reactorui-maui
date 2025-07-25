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
}

public abstract partial class TabbedPage<T> : MultiPage<T, Microsoft.Maui.Controls.Page>, ITabbedPage where T : Microsoft.Maui.Controls.TabbedPage, new()
{
    public TabbedPage(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        TabbedPageStyles.Default?.Invoke(this);
    }

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

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class TabbedPage : TabbedPage<Microsoft.Maui.Controls.TabbedPage>
{
    public TabbedPage(Action<Microsoft.Maui.Controls.TabbedPage?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    public TabbedPage(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class TabbedPageExtensions
{
    public static T BarBackgroundColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color barBackgroundColor)
        where T : ITabbedPage
    {
        //tabbedPage.BarBackgroundColor = barBackgroundColor;
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarBackgroundColorProperty, barBackgroundColor);
        return tabbedPage;
    }

    public static T BarBackgroundColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> barBackgroundColorFunc, IComponentWithState? componentWithState = null)
        where T : ITabbedPage
    {
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarBackgroundColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(barBackgroundColorFunc, componentWithState));
        return tabbedPage;
    }

    public static T BarBackground<T>(this T tabbedPage, Microsoft.Maui.Controls.Brush barBackground)
        where T : ITabbedPage
    {
        //tabbedPage.BarBackground = barBackground;
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarBackgroundProperty, barBackground);
        return tabbedPage;
    }

    public static T BarBackground<T>(this T tabbedPage, Func<Microsoft.Maui.Controls.Brush> barBackgroundFunc, IComponentWithState? componentWithState = null)
        where T : ITabbedPage
    {
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarBackgroundProperty, new PropertyValue<Microsoft.Maui.Controls.Brush>(barBackgroundFunc, componentWithState));
        return tabbedPage;
    }

    public static T BarTextColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color barTextColor)
        where T : ITabbedPage
    {
        //tabbedPage.BarTextColor = barTextColor;
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarTextColorProperty, barTextColor);
        return tabbedPage;
    }

    public static T BarTextColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> barTextColorFunc, IComponentWithState? componentWithState = null)
        where T : ITabbedPage
    {
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarTextColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(barTextColorFunc, componentWithState));
        return tabbedPage;
    }

    public static T UnselectedTabColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color unselectedTabColor)
        where T : ITabbedPage
    {
        //tabbedPage.UnselectedTabColor = unselectedTabColor;
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.UnselectedTabColorProperty, unselectedTabColor);
        return tabbedPage;
    }

    public static T UnselectedTabColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> unselectedTabColorFunc, IComponentWithState? componentWithState = null)
        where T : ITabbedPage
    {
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.UnselectedTabColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(unselectedTabColorFunc, componentWithState));
        return tabbedPage;
    }

    public static T SelectedTabColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color selectedTabColor)
        where T : ITabbedPage
    {
        //tabbedPage.SelectedTabColor = selectedTabColor;
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.SelectedTabColorProperty, selectedTabColor);
        return tabbedPage;
    }

    public static T SelectedTabColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> selectedTabColorFunc, IComponentWithState? componentWithState = null)
        where T : ITabbedPage
    {
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.SelectedTabColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(selectedTabColorFunc, componentWithState));
        return tabbedPage;
    }
}

public static partial class TabbedPageStyles
{
    public static Action<ITabbedPage>? Default { get; set; }
    public static Dictionary<string, Action<ITabbedPage>> Themes { get; } = [];
}