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
namespace MauiReactor.Shapes;
public partial interface IPath : Shapes.IShape
{
}

public sealed partial class Path : Shapes.Shape<Microsoft.Maui.Controls.Shapes.Path>, IPath
{
    public Path(Action<Microsoft.Maui.Controls.Shapes.Path?>? componentRefAction = null) : base(componentRefAction)
    {
        PathStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && PathStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public static partial class PathExtensions
{
    public static T RenderTransform<T>(this T path, Microsoft.Maui.Controls.Shapes.Transform renderTransform)
        where T : IPath
    {
        //path.RenderTransform = renderTransform;
        path.SetProperty(Microsoft.Maui.Controls.Shapes.Path.RenderTransformProperty, renderTransform);
        return path;
    }

    public static T RenderTransform<T>(this T path, Func<Microsoft.Maui.Controls.Shapes.Transform> renderTransformFunc, IComponentWithState? componentWithState = null)
        where T : IPath
    {
        path.SetProperty(Microsoft.Maui.Controls.Shapes.Path.RenderTransformProperty, new PropertyValue<Microsoft.Maui.Controls.Shapes.Transform>(renderTransformFunc, componentWithState));
        return path;
    }
}

public static partial class PathStyles
{
    public static Action<IPath>? Default { get; set; }
    public static Dictionary<string, Action<IPath>> Themes { get; } = [];
}