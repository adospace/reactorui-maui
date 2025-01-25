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
public partial interface IShellContent : IBaseShellItem
{
}

public partial class ShellContent<T> : BaseShellItem<T>, IShellContent where T : Microsoft.Maui.Controls.ShellContent, new()
{
    public ShellContent(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        ShellContentStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && ShellContentStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class ShellContent : ShellContent<Microsoft.Maui.Controls.ShellContent>
{
    public ShellContent(Action<Microsoft.Maui.Controls.ShellContent?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    public ShellContent(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class ShellContentExtensions
{
}

public static partial class ShellContentStyles
{
    public static Action<IShellContent>? Default { get; set; }
    public static Dictionary<string, Action<IShellContent>> Themes { get; } = [];
}