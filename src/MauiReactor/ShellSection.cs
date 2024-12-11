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
public partial interface IShellSection : IShellGroupItem
{
}

public partial class ShellSection<T> : ShellGroupItem<T>, IShellSection where T : Microsoft.Maui.Controls.ShellSection, new()
{
    public ShellSection()
    {
        ShellSectionStyles.Default?.Invoke(this);
    }

    public ShellSection(Action<T?> componentRefAction) : base(componentRefAction)
    {
        ShellSectionStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && ShellSectionStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class ShellSection : ShellSection<Microsoft.Maui.Controls.ShellSection>
{
    public ShellSection()
    {
    }

    public ShellSection(Action<Microsoft.Maui.Controls.ShellSection?> componentRefAction) : base(componentRefAction)
    {
    }

    public ShellSection(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class ShellSectionExtensions
{
/*
    */
}

public static partial class ShellSectionStyles
{
    public static Action<IShellSection>? Default { get; set; }
    public static Dictionary<string, Action<IShellSection>> Themes { get; } = [];
}