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
public partial interface IBaseMenuItem : IStyleableElement
{
}

public abstract partial class BaseMenuItem<T> : StyleableElement<T>, IBaseMenuItem where T : Microsoft.Maui.Controls.BaseMenuItem, new()
{
    protected BaseMenuItem()
    {
        BaseMenuItemStyles.Default?.Invoke(this);
    }

    protected BaseMenuItem(Action<T?> componentRefAction) : base(componentRefAction)
    {
        BaseMenuItemStyles.Default?.Invoke(this);
    }

    protected override void OnUpdate()
    {
        OnBeginUpdate();
        base.OnUpdate();
        OnEndUpdate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && BaseMenuItemStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public static partial class BaseMenuItemExtensions
{
}

public static partial class BaseMenuItemStyles
{
    public static Action<IBaseMenuItem>? Default { get; set; }
    public static Dictionary<string, Action<IBaseMenuItem>> Themes { get; } = [];
}