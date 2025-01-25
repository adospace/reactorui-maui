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
public partial interface ITemplatedPage : IPage
{
}

public partial class TemplatedPage<T> : Page<T>, ITemplatedPage where T : Microsoft.Maui.Controls.TemplatedPage, new()
{
    public TemplatedPage(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        TemplatedPageStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && TemplatedPageStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class TemplatedPage : TemplatedPage<Microsoft.Maui.Controls.TemplatedPage>
{
    public TemplatedPage(Action<Microsoft.Maui.Controls.TemplatedPage?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    public TemplatedPage(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class TemplatedPageExtensions
{
}

public static partial class TemplatedPageStyles
{
    public static Action<ITemplatedPage>? Default { get; set; }
    public static Dictionary<string, Action<ITemplatedPage>> Themes { get; } = [];
}