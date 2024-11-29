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
public partial interface IImage : IView
{
    object? Source { get; set; }

    object? Aspect { get; set; }

    object? IsOpaque { get; set; }

    object? IsAnimationPlaying { get; set; }
}

public partial class Image<T> : View<T>, IImage where T : Microsoft.Maui.Controls.Image, new()
{
    public Image()
    {
        ImageStyles.Default?.Invoke(this);
    }

    public Image(Action<T?> componentRefAction) : base(componentRefAction)
    {
        ImageStyles.Default?.Invoke(this);
    }

    object? IImage.Source { get; set; }

    object? IImage.Aspect { get; set; }

    object? IImage.IsOpaque { get; set; }

    object? IImage.IsAnimationPlaying { get; set; }

    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsIImage = (IImage)this;
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Image.SourceProperty, thisAsIImage.Source);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Image.AspectProperty, thisAsIImage.Aspect);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Image.IsOpaqueProperty, thisAsIImage.IsOpaque);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Image.IsAnimationPlayingProperty, thisAsIImage.IsAnimationPlaying);
        base.OnUpdate();
        OnEndUpdate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && ImageStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class Image : Image<Microsoft.Maui.Controls.Image>
{
    public Image()
    {
    }

    public Image(Action<Microsoft.Maui.Controls.Image?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class ImageExtensions
{
    public static T Source<T>(this T image, Microsoft.Maui.Controls.ImageSource source)
        where T : IImage
    {
        image.Source = source;
        return image;
    }

    public static T Source<T>(this T image, Func<Microsoft.Maui.Controls.ImageSource> sourceFunc)
        where T : IImage
    {
        image.Source = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(sourceFunc);
        return image;
    }

    public static T Source<T>(this T image, string file)
        where T : IImage
    {
        image.Source = Microsoft.Maui.Controls.ImageSource.FromFile(file);
        return image;
    }

    public static T Source<T>(this T image, Func<string> action)
        where T : IImage
    {
        image.Source = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(() => Microsoft.Maui.Controls.ImageSource.FromFile(action()));
        return image;
    }

    public static T Source<T>(this T image, string resourceName, Assembly sourceAssembly)
        where T : IImage
    {
        image.Source = Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly);
        return image;
    }

    public static T Source<T>(this T image, Uri imageUri)
        where T : IImage
    {
        image.Source = Microsoft.Maui.Controls.ImageSource.FromUri(imageUri);
        return image;
    }

    public static T Source<T>(this T image, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity)
        where T : IImage
    {
        image.Source = new Microsoft.Maui.Controls.UriImageSource
        {
            Uri = imageUri,
            CachingEnabled = cachingEnabled,
            CacheValidity = cacheValidity
        };
        return image;
    }

    public static T Source<T>(this T image, Func<Stream> imageStream)
        where T : IImage
    {
        image.Source = Microsoft.Maui.Controls.ImageSource.FromStream(imageStream);
        return image;
    }

    public static T Aspect<T>(this T image, Microsoft.Maui.Aspect aspect)
        where T : IImage
    {
        image.Aspect = aspect;
        return image;
    }

    public static T Aspect<T>(this T image, Func<Microsoft.Maui.Aspect> aspectFunc)
        where T : IImage
    {
        image.Aspect = new PropertyValue<Microsoft.Maui.Aspect>(aspectFunc);
        return image;
    }

    public static T IsOpaque<T>(this T image, bool isOpaque)
        where T : IImage
    {
        image.IsOpaque = isOpaque;
        return image;
    }

    public static T IsOpaque<T>(this T image, Func<bool> isOpaqueFunc)
        where T : IImage
    {
        image.IsOpaque = new PropertyValue<bool>(isOpaqueFunc);
        return image;
    }

    public static T IsAnimationPlaying<T>(this T image, bool isAnimationPlaying)
        where T : IImage
    {
        image.IsAnimationPlaying = isAnimationPlaying;
        return image;
    }

    public static T IsAnimationPlaying<T>(this T image, Func<bool> isAnimationPlayingFunc)
        where T : IImage
    {
        image.IsAnimationPlaying = new PropertyValue<bool>(isAnimationPlayingFunc);
        return image;
    }
}

public static partial class ImageStyles
{
    public static Action<IImage>? Default { get; set; }
    public static Dictionary<string, Action<IImage>> Themes { get; } = [];
}