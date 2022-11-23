using MauiReactor.Animations;
using MauiReactor.Canvas.Internals;
using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas;

[Scaffold(typeof(Internals.DropShadow))]
public partial class DropShadow { }


//public partial interface IDropShadow : ICanvasNode
//{
//    PropertyValue<Color>? Color { get; set; }
//    PropertyValue<SizeF>? Size { get; set; }
//    PropertyValue<float>? Blur { get; set; }
//}

//public partial class DropShadow<T> : CanvasNode<T>, IDropShadow where T : Internals.DropShadow, new()
//{

//    public DropShadow()
//    {

//    }

//    public DropShadow(Action<T?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }

//    PropertyValue<Color>? IDropShadow.Color { get; set; }
//    PropertyValue<SizeF>? IDropShadow.Size { get; set; }
//    PropertyValue<float>? IDropShadow.Blur { get; set; }


//    protected override void OnUpdate()
//    {
//        Validate.EnsureNotNull(NativeControl);
//        var thisAsIBorder = (IDropShadow)this;

//        SetPropertyValue(NativeControl, Internals.DropShadow.ColorProperty, thisAsIBorder.Color);
//        SetPropertyValue(NativeControl, Internals.DropShadow.SizeProperty, thisAsIBorder.Size);
//        SetPropertyValue(NativeControl, Internals.DropShadow.BlurProperty, thisAsIBorder.Blur);

//        base.OnUpdate();
//    }

//    protected override void OnAnimate()
//    {
//        Validate.EnsureNotNull(NativeControl);
//        var thisAsIBorder = (IDropShadow)this;

//        SetPropertyValue(NativeControl, Internals.DropShadow.ColorProperty, thisAsIBorder.Color);
//        SetPropertyValue(NativeControl, Internals.DropShadow.SizeProperty, thisAsIBorder.Size);
//        SetPropertyValue(NativeControl, Internals.DropShadow.BlurProperty, thisAsIBorder.Blur);

//        base.OnAnimate();
//    }
//}

//public partial class DropShadow : DropShadow<Internals.DropShadow>
//{
//    public DropShadow()
//    {

//    }

//    public DropShadow(Action<Internals.DropShadow?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }
//}

public static partial class DropShadowExtensions
{
    public static T Size<T>(this T node, float x, float y, RxSizeFAnimation? customAnimation = null) where T : IDropShadow
    {
        node.Size = new PropertyValue<SizeF>(new SizeF(x, y));
        node.AppendAnimatable(Internals.DropShadow.SizeProperty, customAnimation ?? new RxSimpleSizeFAnimation(new SizeF(x, y)), v => node.Size = new PropertyValue<SizeF>(v.CurrentValue()));
        return node;
    }
}
