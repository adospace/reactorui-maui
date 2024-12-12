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

public static partial class AlignExtensions
{
    public static T HStart<T>(this T view) where T : IAlign
    {
        view.HorizontalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.Start);
        return view;
    }

    public static T HCenter<T>(this T view) where T : IAlign
    {
        view.HorizontalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.Center);
        return view;
    }

    public static T HEnd<T>(this T view) where T : IAlign
    {
        view.HorizontalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.End);
        return view;
    }

    public static T VStart<T>(this T view) where T : IAlign
    {
        view.VerticalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.Start);
        return view;
    }

    public static T VCenter<T>(this T view) where T : IAlign
    {
        view.VerticalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.Center);
        return view;
    }

    public static T VEnd<T>(this T view) where T : IAlign
    {
        view.VerticalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.End);
        return view;
    }
    public static T Center<T>(this T view) where T : IAlign
    {
        view.HorizontalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.Center);
        view.VerticalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.Center);
        return view;
    }
}
