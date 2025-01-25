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
public partial interface IVerticalStackLayout : IStackBase
{
}

public partial class VerticalStackLayout<T> : StackBase<T>, IVerticalStackLayout where T : Microsoft.Maui.Controls.VerticalStackLayout, new()
{
    public VerticalStackLayout(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        VerticalStackLayoutStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && VerticalStackLayoutStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class VerticalStackLayout : VerticalStackLayout<Microsoft.Maui.Controls.VerticalStackLayout>
{
    public VerticalStackLayout(Action<Microsoft.Maui.Controls.VerticalStackLayout?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    public VerticalStackLayout(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class VerticalStackLayoutExtensions
{
}

public static partial class VerticalStackLayoutStyles
{
    public static Action<IVerticalStackLayout>? Default { get; set; }
    public static Dictionary<string, Action<IVerticalStackLayout>> Themes { get; } = [];
}