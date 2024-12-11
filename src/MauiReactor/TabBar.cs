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
public partial interface ITabBar : IShellItem
{
}

public partial class TabBar<T> : ShellItem<T>, ITabBar where T : Microsoft.Maui.Controls.TabBar, new()
{
    public TabBar()
    {
        TabBarStyles.Default?.Invoke(this);
    }

    public TabBar(Action<T?> componentRefAction) : base(componentRefAction)
    {
        TabBarStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && TabBarStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class TabBar : TabBar<Microsoft.Maui.Controls.TabBar>
{
    public TabBar()
    {
    }

    public TabBar(Action<Microsoft.Maui.Controls.TabBar?> componentRefAction) : base(componentRefAction)
    {
    }

    public TabBar(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class TabBarExtensions
{
/*
    */
}

public static partial class TabBarStyles
{
    public static Action<ITabBar>? Default { get; set; }
    public static Dictionary<string, Action<ITabBar>> Themes { get; } = [];
}