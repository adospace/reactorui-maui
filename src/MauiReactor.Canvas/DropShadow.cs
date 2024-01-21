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


public static partial class DropShadowExtensions
{
    static void SetSize(object DropShadow, RxAnimation animation) 
        => ((IDropShadow)DropShadow).Size = ((RxSizeFAnimation)animation).CurrentValue();

    public static T Size<T>(this T node, float x, float y, RxSizeFAnimation? customAnimation = null) where T : IDropShadow
    {
        node.Size = new SizeF(x, y);
        node.AppendAnimatable(Internals.DropShadow.SizeProperty, customAnimation ?? new RxSimpleSizeFAnimation(new SizeF(x, y)), SetSize);
        return node;
    }
}
