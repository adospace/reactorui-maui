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
public partial interface ITab : IShellSection
{
}

public partial class Tab<T> : ShellSection<T>, ITab where T : Microsoft.Maui.Controls.Tab, new()
{
    public Tab()
    {
        TabStyles.Default?.Invoke(this);
    }

    public Tab(Action<T?> componentRefAction) : base(componentRefAction)
    {
        TabStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && TabStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class Tab : Tab<Microsoft.Maui.Controls.Tab>
{
    public Tab()
    {
    }

    public Tab(Action<Microsoft.Maui.Controls.Tab?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class TabExtensions
{
/*
    */
}

public static partial class TabStyles
{
    public static Action<ITab>? Default { get; set; }
    public static Dictionary<string, Action<ITab>> Themes { get; } = [];
}