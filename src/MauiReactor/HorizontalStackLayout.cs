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
public partial interface IHorizontalStackLayout : IStackBase
{
}

public partial class HorizontalStackLayout<T> : StackBase<T>, IHorizontalStackLayout where T : Microsoft.Maui.Controls.HorizontalStackLayout, new()
{
    public HorizontalStackLayout()
    {
        HorizontalStackLayoutStyles.Default?.Invoke(this);
    }

    public HorizontalStackLayout(Action<T?> componentRefAction) : base(componentRefAction)
    {
        HorizontalStackLayoutStyles.Default?.Invoke(this);
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
        if (ThemeKey != null && HorizontalStackLayoutStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class HorizontalStackLayout : HorizontalStackLayout<Microsoft.Maui.Controls.HorizontalStackLayout>
{
    public HorizontalStackLayout()
    {
    }

    public HorizontalStackLayout(Action<Microsoft.Maui.Controls.HorizontalStackLayout?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class HorizontalStackLayoutExtensions
{
}

public static partial class HorizontalStackLayoutStyles
{
    public static Action<IHorizontalStackLayout>? Default { get; set; }
    public static Dictionary<string, Action<IHorizontalStackLayout>> Themes { get; } = [];
}