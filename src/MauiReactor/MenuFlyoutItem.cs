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
public partial interface IMenuFlyoutItem : IMenuItem
{
}

public partial class MenuFlyoutItem<T> : MenuItem<T>, IMenuFlyoutItem where T : Microsoft.Maui.Controls.MenuFlyoutItem, new()
{
    public MenuFlyoutItem()
    {
        MenuFlyoutItemStyles.Default?.Invoke(this);
    }

    public MenuFlyoutItem(Action<T?> componentRefAction) : base(componentRefAction)
    {
        MenuFlyoutItemStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && MenuFlyoutItemStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class MenuFlyoutItem : MenuFlyoutItem<Microsoft.Maui.Controls.MenuFlyoutItem>
{
    public MenuFlyoutItem()
    {
    }

    public MenuFlyoutItem(Action<Microsoft.Maui.Controls.MenuFlyoutItem?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class MenuFlyoutItemExtensions
{
/*
    */
}

public static partial class MenuFlyoutItemStyles
{
    public static Action<IMenuFlyoutItem>? Default { get; set; }
    public static Dictionary<string, Action<IMenuFlyoutItem>> Themes { get; } = [];
}