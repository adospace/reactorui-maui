using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas;

[Scaffold(typeof(Internals.AutoStack))]
public partial class AutoStack { }


public class VAutoStack : AutoStack
{
    public VAutoStack()
    {
        this.Orientation(Microsoft.Maui.Controls.StackOrientation.Vertical);
    }
}

public class HAutoStack : AutoStack
{
    public HAutoStack()
    {
        this.Orientation(Microsoft.Maui.Controls.StackOrientation.Horizontal);
    }
}

public static partial class AutoVerticalStackExtensions
{
    public static T AutoStackWidth<T>(this T visualNodeWithAttachedProperties, float width) where T : IVisualNodeWithAttachedProperties
    {
        visualNodeWithAttachedProperties.SetAttachedProperty(Canvas.Internals.AutoStack.WidthProperty, width);

        return visualNodeWithAttachedProperties;
    }

    public static T AutoStackHeight<T>(this T visualNodeWithAttachedProperties, float height) where T : IVisualNodeWithAttachedProperties
    {
        visualNodeWithAttachedProperties.SetAttachedProperty(Canvas.Internals.AutoStack.HeightProperty, height);

        return visualNodeWithAttachedProperties;
    }
}
