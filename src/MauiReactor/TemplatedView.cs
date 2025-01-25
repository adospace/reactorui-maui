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
public partial interface ITemplatedView : Compatibility.ILayout
{
}

public partial class TemplatedView<T> : Compatibility.Layout<T>, ITemplatedView where T : Microsoft.Maui.Controls.TemplatedView, new()
{
    public TemplatedView(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        TemplatedViewStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && TemplatedViewStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class TemplatedView : TemplatedView<Microsoft.Maui.Controls.TemplatedView>
{
    public TemplatedView(Action<Microsoft.Maui.Controls.TemplatedView?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    public TemplatedView(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class TemplatedViewExtensions
{
}

public static partial class TemplatedViewStyles
{
    public static Action<ITemplatedView>? Default { get; set; }
    public static Dictionary<string, Action<ITemplatedView>> Themes { get; } = [];
}