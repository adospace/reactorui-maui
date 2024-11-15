using MauiReactor.Internals;

namespace MauiReactor;

public partial interface INavigableElement
{ 
    List<string>? Class { get; set; }
}

public partial class NavigableElement<T>
{
    List<string>? INavigableElement.Class { get; set; }


    partial void OnBeginUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsINavigableElement = (INavigableElement)this;
        if (!NativeControl.StyleClass.NullableSequenceEqual(thisAsINavigableElement.Class))
        {
//#if DEBUG
//                System.Diagnostics.Debug.WriteLine($"[{this}] Update 'StyleClass' to {(thisAsINavigableElement.Class == null ? "null" : string.Join(",", thisAsINavigableElement.Class))}");
//#endif
            NativeControl.StyleClass = thisAsINavigableElement.Class ?? [];
        }

    }
}

public static partial class NavigableElementExtensions
{
    public static T Class<T>(this T navigableElement, string className) where T : INavigableElement
    {
        navigableElement.Class ??= [];
        navigableElement.Class.Add(className);
        return navigableElement;
    }

    public static T Style<T>(this T navigableElement, string styleKey) where T : INavigableElement
    {
        var style = ResourceManager.FindStyle(styleKey);
        if (style != null)
        {
            navigableElement.Style(style);
        }
        return navigableElement;
    }
}


