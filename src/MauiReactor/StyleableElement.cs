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
public partial interface IStyleableElement : IElement
{
}

public abstract partial class StyleableElement<T> : Element<T>, IStyleableElement where T : Microsoft.Maui.Controls.StyleableElement, new()
{
    protected StyleableElement(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        StyleableElementStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && StyleableElementStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public static partial class StyleableElementExtensions
{
    public static T Style<T>(this T styleableElement, Microsoft.Maui.Controls.Style style)
        where T : IStyleableElement
    {
        //styleableElement.Style = style;
        styleableElement.SetProperty(Microsoft.Maui.Controls.StyleableElement.StyleProperty, style);
        return styleableElement;
    }

    public static T Style<T>(this T styleableElement, Func<Microsoft.Maui.Controls.Style> styleFunc)
        where T : IStyleableElement
    {
        //styleableElement.Style = new PropertyValue<Microsoft.Maui.Controls.Style>(styleFunc);
        styleableElement.SetProperty(Microsoft.Maui.Controls.StyleableElement.StyleProperty, new PropertyValue<Microsoft.Maui.Controls.Style>(styleFunc));
        return styleableElement;
    }
}

public static partial class StyleableElementStyles
{
    public static Action<IStyleableElement>? Default { get; set; }
    public static Dictionary<string, Action<IStyleableElement>> Themes { get; } = [];
}