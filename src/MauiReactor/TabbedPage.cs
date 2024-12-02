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
    public TabbedPage()
    {
        TabbedPageStyles.Default?.Invoke(this);
    }

    public TabbedPage(Action<T?> componentRefAction) : base(componentRefAction)
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
    public TabbedPage()
    {
    }

    public TabbedPage(Action<Microsoft.Maui.Controls.TabbedPage?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class TabbedPageExtensions
{
    /*
    
    
    
    
    
    
    
    
    
    
    */
    public static T BarBackgroundColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color barBackgroundColor)
        where T : ITabbedPage
    {
        //tabbedPage.BarBackgroundColor = barBackgroundColor;
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarBackgroundColorProperty, barBackgroundColor);
        return tabbedPage;
    }

    public static T BarBackgroundColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> barBackgroundColorFunc)
        where T : ITabbedPage
    {
        //tabbedPage.BarBackgroundColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(barBackgroundColorFunc);
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarBackgroundColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(barBackgroundColorFunc));
        return tabbedPage;
    }

    public static T BarBackground<T>(this T tabbedPage, Microsoft.Maui.Controls.Brush barBackground)
        where T : ITabbedPage
    {
        //tabbedPage.BarBackground = barBackground;
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarBackgroundProperty, barBackground);
        return tabbedPage;
    }

    public static T BarBackground<T>(this T tabbedPage, Func<Microsoft.Maui.Controls.Brush> barBackgroundFunc)
        where T : ITabbedPage
    {
        //tabbedPage.BarBackground = new PropertyValue<Microsoft.Maui.Controls.Brush>(barBackgroundFunc);
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarBackgroundProperty, new PropertyValue<Microsoft.Maui.Controls.Brush>(barBackgroundFunc));
        return tabbedPage;
    }

    public static T BarTextColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color barTextColor)
        where T : ITabbedPage
    {
        //tabbedPage.BarTextColor = barTextColor;
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarTextColorProperty, barTextColor);
        return tabbedPage;
    }

    public static T BarTextColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> barTextColorFunc)
        where T : ITabbedPage
    {
        //tabbedPage.BarTextColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(barTextColorFunc);
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.BarTextColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(barTextColorFunc));
        return tabbedPage;
    }

    public static T UnselectedTabColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color unselectedTabColor)
        where T : ITabbedPage
    {
        //tabbedPage.UnselectedTabColor = unselectedTabColor;
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.UnselectedTabColorProperty, unselectedTabColor);
        return tabbedPage;
    }

    public static T UnselectedTabColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> unselectedTabColorFunc)
        where T : ITabbedPage
    {
        //tabbedPage.UnselectedTabColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(unselectedTabColorFunc);
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.UnselectedTabColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(unselectedTabColorFunc));
        return tabbedPage;
    }

    public static T SelectedTabColor<T>(this T tabbedPage, Microsoft.Maui.Graphics.Color selectedTabColor)
        where T : ITabbedPage
    {
        //tabbedPage.SelectedTabColor = selectedTabColor;
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.SelectedTabColorProperty, selectedTabColor);
        return tabbedPage;
    }

    public static T SelectedTabColor<T>(this T tabbedPage, Func<Microsoft.Maui.Graphics.Color> selectedTabColorFunc)
        where T : ITabbedPage
    {
        //tabbedPage.SelectedTabColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(selectedTabColorFunc);
        tabbedPage.SetProperty(Microsoft.Maui.Controls.TabbedPage.SelectedTabColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(selectedTabColorFunc));
        return tabbedPage;
    }
}

public static partial class TabbedPageStyles
{
    public static Action<ITabbedPage>? Default { get; set; }
    public static Dictionary<string, Action<ITabbedPage>> Themes { get; } = [];
}