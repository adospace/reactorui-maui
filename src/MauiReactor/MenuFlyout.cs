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
public partial interface IMenuFlyout : IFlyoutBase
{
}

public partial class MenuFlyout<T> : FlyoutBase<T>, IMenuFlyout where T : Microsoft.Maui.Controls.MenuFlyout, new()
{
    public MenuFlyout()
    {
        MenuFlyoutStyles.Default?.Invoke(this);
    }

    public MenuFlyout(Action<T?> componentRefAction) : base(componentRefAction)
    {
        MenuFlyoutStyles.Default?.Invoke(this);
    }

    internal override void Reset()
    {
        base.Reset();
        OnReset();
    }

    partial void OnReset();
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
        if (ThemeKey != null && MenuFlyoutStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }
}

public partial class MenuFlyout : MenuFlyout<Microsoft.Maui.Controls.MenuFlyout>
{
    public MenuFlyout()
    {
    }

    public MenuFlyout(Action<Microsoft.Maui.Controls.MenuFlyout?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class MenuFlyoutExtensions
{
}

public static partial class MenuFlyoutStyles
{
    public static Action<IMenuFlyout>? Default { get; set; }
    public static Dictionary<string, Action<IMenuFlyout>> Themes { get; } = [];
}