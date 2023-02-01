using MauiReactor.Animations;
using MauiReactor.Internals;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MauiReactor.Canvas;

[Scaffold(typeof(Internals.CanvasVisualElement))]
public partial class CanvasVisualElement { }


//public static partial class CanvasVisualElementExtensions
//{
//    public static T Translate<T>(this T node, float x, float y, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
//    {
//        node.Translation = new PropertyValue<Vector2>(new Vector2(x, y));
//        node.AppendAnimatable(Internals.CanvasVisualElement.TranslationProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(x, y)), v => node.Translation = new PropertyValue<Vector2>(v.CurrentValue()));
//        return node;
//    }

//    //public static T Translation<T>(this T node, Func<Vector2> valueFunc) where T : ICanvasVisualElement
//    //{
//    //    node.Translation = new PropertyValue<Vector2>(valueFunc);
//    //    node.AppendAnimatable(Internals.CanvasVisualElement.TranslationProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(x, y)), v => node.Translation = new PropertyValue<Vector2>(v.CurrentValue()));
//    //    return node;
//    //}

//    public static T TranslateX<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
//    {
//        node.Translation = new PropertyValue<Vector2>(new Vector2(value, 0.0f));
//        node.AppendAnimatable(Internals.CanvasVisualElement.TranslationProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(value, 0.0f)), v => node.Translation = new PropertyValue<Vector2>(v.CurrentValue()));
//        return node;
//    }

//    public static T TranslateX<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
//    {
//        node.Translation = new PropertyValue<Vector2>(() => new Vector2(valueFunc(), 0.0f));
//        return node;
//    }

//    public static T TranslateY<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
//    {
//        node.Translation = new PropertyValue<Vector2>(new Vector2(0.0f, value));
//        node.AppendAnimatable(Internals.CanvasVisualElement.TranslationProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(0.0f, value)), v => node.Translation = new PropertyValue<Vector2>(v.CurrentValue()));
//        return node;
//    }

//    public static T TranslateY<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
//    {
//        node.Translation = new PropertyValue<Vector2>(() => new Vector2(0.0f, valueFunc()));
//        return node;
//    }

//    public static T ScaleX<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
//    {
//        node.Scale = new PropertyValue<Vector2>(new Vector2(value, 0.0f));
//        node.AppendAnimatable(Internals.CanvasVisualElement.ScaleProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(value, 0.0f)), v => node.Scale = new PropertyValue<Vector2>(v.CurrentValue()));
//        return node;
//    }

//    public static T ScaleX<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
//    {
//        node.Scale = new PropertyValue<Vector2>(() => new Vector2(valueFunc(), 0.0f));
//        return node;
//    }

//    public static T ScaleY<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
//    {
//        node.Scale = new PropertyValue<Vector2>(new Vector2(0.0f, value));
//        node.AppendAnimatable(Internals.CanvasVisualElement.ScaleProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(0.0f, value)), v => node.Scale = new PropertyValue<Vector2>(v.CurrentValue()));
//        return node;
//    }

//    public static T ScaleY<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
//    {
//        node.Scale = new PropertyValue<Vector2>(() => new Vector2(0.0f, valueFunc()));
//        return node;
//    }

//    //public static T Scale<T>(this T node, Vector2 value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
//    //{
//    //    node.Scale = new PropertyValue<Vector2>(value);
//    //    node.AppendAnimatable(Internals.CanvasVisualElement.ScaleProperty, customAnimation ?? new RxSimpleVector2Animation(value), v => node.Scale = new PropertyValue<Vector2>(v.CurrentValue()));
//    //    return node;
//    //}

//    public static T Scale<T>(this T node, float x, float y, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
//    {
//        node.Scale = new PropertyValue<Vector2>(new Vector2(x, y));
//        node.AppendAnimatable(Internals.CanvasVisualElement.ScaleProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(x, y)), v => node.Scale = new PropertyValue<Vector2>(v.CurrentValue()));
//        return node;
//    }

//    //public static T Scale<T>(this T node, Func<Vector2> valueFunc) where T : ICanvasVisualElement
//    //{
//    //    node.Scale = new PropertyValue<Vector2>(valueFunc);
//    //    return node;
//    //}

//    public static T Rotate<T>(this T node, float value, RxFloatAnimation? customAnimation = null) where T : ICanvasVisualElement
//    {
//        node.Rotation = new PropertyValue<float>(value);
//        node.AppendAnimatable(Internals.CanvasVisualElement.RotationProperty, customAnimation ?? new RxFloatAnimation(value), v => node.Rotation = new PropertyValue<float>(v.CurrentValue()));
//        return node;
//    }

//    public static T Rotate<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
//    {
//        node.Rotation = new PropertyValue<float>(valueFunc);
//        return node;
//    }

//    public static T AnchorX<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
//    {
//        node.Anchor = new PropertyValue<Vector2>(new Vector2(value, 0.0f));
//        node.AppendAnimatable(Internals.CanvasVisualElement.AnchorProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(value, 0.0f)), v => node.Anchor = new PropertyValue<Vector2>(v.CurrentValue()));
//        return node;
//    }

//    public static T AnchorX<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
//    {
//        node.Anchor = new PropertyValue<Vector2>(() => new Vector2(valueFunc(), 0.0f));
//        return node;
//    }

//    public static T AnchorY<T>(this T node, float value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
//    {
//        node.Anchor = new PropertyValue<Vector2>(new Vector2(0.0f, value));
//        node.AppendAnimatable(Internals.CanvasVisualElement.AnchorProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(0.0f, value)), v => node.Anchor = new PropertyValue<Vector2>(v.CurrentValue()));
//        return node;
//    }

//    public static T AnchorY<T>(this T node, Func<float> valueFunc) where T : ICanvasVisualElement
//    {
//        node.Anchor = new PropertyValue<Vector2>(() => new Vector2(0.0f, valueFunc()));
//        return node;
//    }

//    //public static T Anchor<T>(this T node, Vector2 value, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
//    //{
//    //    node.Anchor = new PropertyValue<Vector2>(value);
//    //    node.AppendAnimatable(Internals.CanvasVisualElement.AnchorProperty, customAnimation ?? new RxSimpleVector2Animation(value), v => node.Anchor = new PropertyValue<Vector2>(v.CurrentValue()));
//    //    return node;
//    //}

//    public static T Anchor<T>(this T node, float x, float y, RxVector2Animation? customAnimation = null) where T : ICanvasVisualElement
//    {
//        node.Anchor = new PropertyValue<Vector2>(new Vector2(x, y));
//        node.AppendAnimatable(Internals.CanvasVisualElement.AnchorProperty, customAnimation ?? new RxSimpleVector2Animation(new Vector2(x, y)), v => node.Anchor = new PropertyValue<Vector2>(v.CurrentValue()));
//        return node;
//    }

//    //public static T Anchor<T>(this T node, Func<Vector2> valueFunc) where T : ICanvasVisualElement
//    //{
//    //    node.Anchor = new PropertyValue<Vector2>(valueFunc);
//    //    return node;
//    //}
//}
