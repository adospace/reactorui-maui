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
public partial interface IFlexLayout : ILayout
{
}

public partial class FlexLayout<T> : Layout<T>, IFlexLayout where T : Microsoft.Maui.Controls.FlexLayout, new()
{
    public FlexLayout()
    {
        FlexLayoutStyles.Default?.Invoke(this);
    }

    public FlexLayout(Action<T?> componentRefAction) : base(componentRefAction)
    {
        FlexLayoutStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && FlexLayoutStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class FlexLayout : FlexLayout<Microsoft.Maui.Controls.FlexLayout>
{
    public FlexLayout()
    {
    }

    public FlexLayout(Action<Microsoft.Maui.Controls.FlexLayout?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class FlexLayoutExtensions
{
    /*
    
    
    
    
    
    
    
    
    
    
    
    
    */
    public static T Direction<T>(this T flexLayout, Microsoft.Maui.Layouts.FlexDirection direction)
        where T : IFlexLayout
    {
        //flexLayout.Direction = direction;
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.DirectionProperty, direction);
        return flexLayout;
    }

    public static T Direction<T>(this T flexLayout, Func<Microsoft.Maui.Layouts.FlexDirection> directionFunc)
        where T : IFlexLayout
    {
        //flexLayout.Direction = new PropertyValue<Microsoft.Maui.Layouts.FlexDirection>(directionFunc);
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.DirectionProperty, new PropertyValue<Microsoft.Maui.Layouts.FlexDirection>(directionFunc));
        return flexLayout;
    }

    public static T JustifyContent<T>(this T flexLayout, Microsoft.Maui.Layouts.FlexJustify justifyContent)
        where T : IFlexLayout
    {
        //flexLayout.JustifyContent = justifyContent;
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.JustifyContentProperty, justifyContent);
        return flexLayout;
    }

    public static T JustifyContent<T>(this T flexLayout, Func<Microsoft.Maui.Layouts.FlexJustify> justifyContentFunc)
        where T : IFlexLayout
    {
        //flexLayout.JustifyContent = new PropertyValue<Microsoft.Maui.Layouts.FlexJustify>(justifyContentFunc);
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.JustifyContentProperty, new PropertyValue<Microsoft.Maui.Layouts.FlexJustify>(justifyContentFunc));
        return flexLayout;
    }

    public static T AlignContent<T>(this T flexLayout, Microsoft.Maui.Layouts.FlexAlignContent alignContent)
        where T : IFlexLayout
    {
        //flexLayout.AlignContent = alignContent;
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.AlignContentProperty, alignContent);
        return flexLayout;
    }

    public static T AlignContent<T>(this T flexLayout, Func<Microsoft.Maui.Layouts.FlexAlignContent> alignContentFunc)
        where T : IFlexLayout
    {
        //flexLayout.AlignContent = new PropertyValue<Microsoft.Maui.Layouts.FlexAlignContent>(alignContentFunc);
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.AlignContentProperty, new PropertyValue<Microsoft.Maui.Layouts.FlexAlignContent>(alignContentFunc));
        return flexLayout;
    }

    public static T AlignItems<T>(this T flexLayout, Microsoft.Maui.Layouts.FlexAlignItems alignItems)
        where T : IFlexLayout
    {
        //flexLayout.AlignItems = alignItems;
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.AlignItemsProperty, alignItems);
        return flexLayout;
    }

    public static T AlignItems<T>(this T flexLayout, Func<Microsoft.Maui.Layouts.FlexAlignItems> alignItemsFunc)
        where T : IFlexLayout
    {
        //flexLayout.AlignItems = new PropertyValue<Microsoft.Maui.Layouts.FlexAlignItems>(alignItemsFunc);
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.AlignItemsProperty, new PropertyValue<Microsoft.Maui.Layouts.FlexAlignItems>(alignItemsFunc));
        return flexLayout;
    }

    public static T Position<T>(this T flexLayout, Microsoft.Maui.Layouts.FlexPosition position)
        where T : IFlexLayout
    {
        //flexLayout.Position = position;
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.PositionProperty, position);
        return flexLayout;
    }

    public static T Position<T>(this T flexLayout, Func<Microsoft.Maui.Layouts.FlexPosition> positionFunc)
        where T : IFlexLayout
    {
        //flexLayout.Position = new PropertyValue<Microsoft.Maui.Layouts.FlexPosition>(positionFunc);
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.PositionProperty, new PropertyValue<Microsoft.Maui.Layouts.FlexPosition>(positionFunc));
        return flexLayout;
    }

    public static T Wrap<T>(this T flexLayout, Microsoft.Maui.Layouts.FlexWrap wrap)
        where T : IFlexLayout
    {
        //flexLayout.Wrap = wrap;
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.WrapProperty, wrap);
        return flexLayout;
    }

    public static T Wrap<T>(this T flexLayout, Func<Microsoft.Maui.Layouts.FlexWrap> wrapFunc)
        where T : IFlexLayout
    {
        //flexLayout.Wrap = new PropertyValue<Microsoft.Maui.Layouts.FlexWrap>(wrapFunc);
        flexLayout.SetProperty(Microsoft.Maui.Controls.FlexLayout.WrapProperty, new PropertyValue<Microsoft.Maui.Layouts.FlexWrap>(wrapFunc));
        return flexLayout;
    }
}

public static partial class FlexLayoutStyles
{
    public static Action<IFlexLayout>? Default { get; set; }
    public static Dictionary<string, Action<IFlexLayout>> Themes { get; } = [];
}