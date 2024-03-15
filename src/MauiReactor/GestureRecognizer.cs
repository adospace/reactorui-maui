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
public partial interface IGestureRecognizer : IElement
{
}

public partial class GestureRecognizer<T> : Element<T>, IGestureRecognizer where T : Microsoft.Maui.Controls.GestureRecognizer, new()
{
    public GestureRecognizer()
    {
        GestureRecognizerStyles.Default?.Invoke(this);
    }

    public GestureRecognizer(Action<T?> componentRefAction) : base(componentRefAction)
    {
        GestureRecognizerStyles.Default?.Invoke(this);
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
        if (ThemeKey != null && GestureRecognizerStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }
}

public partial class GestureRecognizer : GestureRecognizer<Microsoft.Maui.Controls.GestureRecognizer>
{
    public GestureRecognizer()
    {
    }

    public GestureRecognizer(Action<Microsoft.Maui.Controls.GestureRecognizer?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class GestureRecognizerExtensions
{
}

public static partial class GestureRecognizerStyles
{
    public static Action<IGestureRecognizer>? Default { get; set; }
    public static Dictionary<string, Action<IGestureRecognizer>> Themes { get; } = [];
}