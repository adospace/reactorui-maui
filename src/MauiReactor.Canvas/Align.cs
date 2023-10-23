using MauiReactor.Animations;
using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas;

[Scaffold(typeof(Internals.Align))]
public partial class Align { }


//public partial interface IAlign : ICanvasVisualElement
//{
//    PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? HorizontalAlignment { get; set; }
//    PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? VerticalAlignment { get; set; }
//    PropertyValue<float>? Width { get; set; }
//    PropertyValue<float>? Height { get; set; }
//}

//public partial class Align<T> : CanvasVisualElement<T>, IAlign where T : Internals.Align, new()
//{
//    public Align()
//    {

//    }

//    public Align(Action<T?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }

//    PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? IAlign.HorizontalAlignment { get; set; }
//    PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? IAlign.VerticalAlignment { get; set; }
//    PropertyValue<float>? IAlign.Width { get; set; }
//    PropertyValue<float>? IAlign.Height { get; set; }

//    protected override void OnUpdate()
//    {
//        Validate.EnsureNotNull(NativeControl);
//        var thisAsIAlign = (IAlign)this;

//        SetPropertyValue(NativeControl, Internals.Align.HorizontalAlignmentProperty, thisAsIAlign.HorizontalAlignment);
//        SetPropertyValue(NativeControl, Internals.Align.VerticalAlignmentProperty, thisAsIAlign.VerticalAlignment);
//        SetPropertyValue(NativeControl, Internals.Align.WidthProperty, thisAsIAlign.Width);
//        SetPropertyValue(NativeControl, Internals.Align.HeightProperty, thisAsIAlign.Height);

//        base.OnUpdate();
//    }

//    protected override void OnAnimate()
//    {
//        Validate.EnsureNotNull(NativeControl);
//        var thisAsIAlign = (IAlign)this;

//        SetPropertyValue(NativeControl, Internals.Align.WidthProperty, thisAsIAlign.Width);
//        SetPropertyValue(NativeControl, Internals.Align.HeightProperty, thisAsIAlign.Height);

//        base.OnAnimate();
//    }
//}

//public partial class Align : Align<Internals.Align>
//{
//    public Align()
//    {

//    }

//    public Align(Action<Internals.Align?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }
//}

public static partial class AlignExtensions
{
    public static T HStart<T>(this T view) where T : IAlign
    {
        view.HorizontalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.Start);
        return view;
    }

    public static T HCenter<T>(this T view) where T : IAlign
    {
        view.HorizontalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.Center);
        return view;
    }

    public static T HEnd<T>(this T view) where T : IAlign
    {
        view.HorizontalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.End);
        return view;
    }

    public static T VStart<T>(this T view) where T : IAlign
    {
        view.VerticalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.Start);
        return view;
    }

    public static T VCenter<T>(this T view) where T : IAlign
    {
        view.VerticalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.Center);
        return view;
    }

    public static T VEnd<T>(this T view) where T : IAlign
    {
        view.VerticalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.End);
        return view;
    }
    public static T Center<T>(this T view) where T : IAlign
    {
        view.HorizontalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.Center);
        view.VerticalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.Center);
        return view;
    }
}
