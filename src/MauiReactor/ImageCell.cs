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
public partial interface IImageCell : ITextCell
{
}

public partial class ImageCell<T> : TextCell<T>, IImageCell where T : Microsoft.Maui.Controls.ImageCell, new()
{
    public ImageCell()
    {
        ImageCellStyles.Default?.Invoke(this);
    }

    public ImageCell(Action<T?> componentRefAction) : base(componentRefAction)
    {
        ImageCellStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && ImageCellStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class ImageCell : ImageCell<Microsoft.Maui.Controls.ImageCell>
{
    public ImageCell()
    {
    }

    public ImageCell(Action<Microsoft.Maui.Controls.ImageCell?> componentRefAction) : base(componentRefAction)
    {
    }

    public ImageCell(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class ImageCellExtensions
{
    /*
    
    
    */
    public static T ImageSource<T>(this T imageCell, Microsoft.Maui.Controls.ImageSource imageSource)
        where T : IImageCell
    {
        //imageCell.ImageSource = imageSource;
        imageCell.SetProperty(Microsoft.Maui.Controls.ImageCell.ImageSourceProperty, imageSource);
        return imageCell;
    }

    public static T ImageSource<T>(this T imageCell, Func<Microsoft.Maui.Controls.ImageSource> imageSourceFunc)
        where T : IImageCell
    {
        //imageCell.ImageSource = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(imageSourceFunc);
        imageCell.SetProperty(Microsoft.Maui.Controls.ImageCell.ImageSourceProperty, new PropertyValue<Microsoft.Maui.Controls.ImageSource>(imageSourceFunc));
        return imageCell;
    }

    public static T ImageSource<T>(this T imageCell, string file)
        where T : IImageCell
    {
        //imageCell.ImageSource = Microsoft.Maui.Controls.ImageSource.FromFile(file);
        imageCell.SetProperty(Microsoft.Maui.Controls.ImageCell.ImageSourceProperty, Microsoft.Maui.Controls.ImageSource.FromFile(file));
        return imageCell;
    }

    public static T ImageSource<T>(this T imageCell, Func<string> action)
        where T : IImageCell
    {
        /*imageCell.ImageSource = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(
            () => Microsoft.Maui.Controls.ImageSource.FromFile(action()));*/
        imageCell.SetProperty(Microsoft.Maui.Controls.ImageCell.ImageSourceProperty, new PropertyValue<Microsoft.Maui.Controls.ImageSource>(() => Microsoft.Maui.Controls.ImageSource.FromFile(action())));
        return imageCell;
    }

    public static T ImageSource<T>(this T imageCell, string resourceName, Assembly sourceAssembly)
        where T : IImageCell
    {
        //imageCell.ImageSource = Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly);
        imageCell.SetProperty(Microsoft.Maui.Controls.ImageCell.ImageSourceProperty, Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly));
        return imageCell;
    }

    public static T ImageSource<T>(this T imageCell, Uri imageUri)
        where T : IImageCell
    {
        //imageCell.ImageSource = Microsoft.Maui.Controls.ImageSource.FromUri(imageUri);
        imageCell.SetProperty(Microsoft.Maui.Controls.ImageCell.ImageSourceProperty, Microsoft.Maui.Controls.ImageSource.FromUri(imageUri));
        return imageCell;
    }

    public static T ImageSource<T>(this T imageCell, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity)
        where T : IImageCell
    {
        //imageCell.ImageSource = new Microsoft.Maui.Controls.UriImageSource
        //{
        //    Uri = imageUri,
        //    CachingEnabled = cachingEnabled,
        //    CacheValidity = cacheValidity
        //};
        var newValue = new Microsoft.Maui.Controls.UriImageSource
        {
            Uri = imageUri,
            CachingEnabled = cachingEnabled,
            CacheValidity = cacheValidity
        };
        imageCell.SetProperty(Microsoft.Maui.Controls.ImageCell.ImageSourceProperty, newValue);
        return imageCell;
    }

    public static T ImageSource<T>(this T imageCell, Func<Stream> imageStream)
        where T : IImageCell
    {
        //imageCell.ImageSource = Microsoft.Maui.Controls.ImageSource.FromStream(imageStream);
        imageCell.SetProperty(Microsoft.Maui.Controls.ImageCell.ImageSourceProperty, Microsoft.Maui.Controls.ImageSource.FromStream(imageStream));
        return imageCell;
    }
}

public static partial class ImageCellStyles
{
    public static Action<IImageCell>? Default { get; set; }
    public static Dictionary<string, Action<IImageCell>> Themes { get; } = [];
}