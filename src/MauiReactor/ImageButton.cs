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
public partial interface IImageButton : IView
{
    EventCommand<EventArgs>? ClickedEvent { get; set; }

    EventCommand<EventArgs>? PressedEvent { get; set; }

    EventCommand<EventArgs>? ReleasedEvent { get; set; }
}

public partial class ImageButton<T> : View<T>, IImageButton where T : Microsoft.Maui.Controls.ImageButton, new()
{
    public ImageButton()
    {
        ImageButtonStyles.Default?.Invoke(this);
    }

    public ImageButton(Action<T?> componentRefAction) : base(componentRefAction)
    {
        ImageButtonStyles.Default?.Invoke(this);
    }

    EventCommand<EventArgs>? IImageButton.ClickedEvent { get; set; }

    EventCommand<EventArgs>? IImageButton.PressedEvent { get; set; }

    EventCommand<EventArgs>? IImageButton.ReleasedEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && ImageButtonStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<EventArgs>? _executingClickedEvent;
    private EventCommand<EventArgs>? _executingPressedEvent;
    private EventCommand<EventArgs>? _executingReleasedEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIImageButton = (IImageButton)this;
        if (thisAsIImageButton.ClickedEvent != null)
        {
            NativeControl.Clicked += NativeControl_Clicked;
        }

        if (thisAsIImageButton.PressedEvent != null)
        {
            NativeControl.Pressed += NativeControl_Pressed;
        }

        if (thisAsIImageButton.ReleasedEvent != null)
        {
            NativeControl.Released += NativeControl_Released;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_Clicked(object? sender, EventArgs e)
    {
        var thisAsIImageButton = (IImageButton)this;
        if (_executingClickedEvent == null || _executingClickedEvent.IsCompleted)
        {
            _executingClickedEvent = thisAsIImageButton.ClickedEvent;
            _executingClickedEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_Pressed(object? sender, EventArgs e)
    {
        var thisAsIImageButton = (IImageButton)this;
        if (_executingPressedEvent == null || _executingPressedEvent.IsCompleted)
        {
            _executingPressedEvent = thisAsIImageButton.PressedEvent;
            _executingPressedEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_Released(object? sender, EventArgs e)
    {
        var thisAsIImageButton = (IImageButton)this;
        if (_executingReleasedEvent == null || _executingReleasedEvent.IsCompleted)
        {
            _executingReleasedEvent = thisAsIImageButton.ReleasedEvent;
            _executingReleasedEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.Clicked -= NativeControl_Clicked;
            NativeControl.Pressed -= NativeControl_Pressed;
            NativeControl.Released -= NativeControl_Released;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is ImageButton<T> @imagebutton)
        {
            if (_executingClickedEvent != null && !_executingClickedEvent.IsCompleted)
            {
                @imagebutton._executingClickedEvent = _executingClickedEvent;
            }

            if (_executingPressedEvent != null && !_executingPressedEvent.IsCompleted)
            {
                @imagebutton._executingPressedEvent = _executingPressedEvent;
            }

            if (_executingReleasedEvent != null && !_executingReleasedEvent.IsCompleted)
            {
                @imagebutton._executingReleasedEvent = _executingReleasedEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class ImageButton : ImageButton<Microsoft.Maui.Controls.ImageButton>
{
    public ImageButton()
    {
    }

    public ImageButton(Action<Microsoft.Maui.Controls.ImageButton?> componentRefAction) : base(componentRefAction)
    {
    }

    public ImageButton(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class ImageButtonExtensions
{
    /*
    
    
    
    
    static object? SetBorderWidth(object imageButton, RxAnimation animation)
        => ((IImageButton)imageButton).BorderWidth = ((RxDoubleAnimation)animation).CurrentValue();

    
    
    
    
    
    
    
    
    
    
            
    static object? SetPadding(object imageButton, RxAnimation animation)
        => ((IImageButton)imageButton).Padding = ((RxThicknessAnimation)animation).CurrentValue();

    
    */
    public static T CornerRadius<T>(this T imageButton, int cornerRadius)
        where T : IImageButton
    {
        //imageButton.CornerRadius = cornerRadius;
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.CornerRadiusProperty, cornerRadius);
        return imageButton;
    }

    public static T CornerRadius<T>(this T imageButton, Func<int> cornerRadiusFunc)
        where T : IImageButton
    {
        //imageButton.CornerRadius = new PropertyValue<int>(cornerRadiusFunc);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.CornerRadiusProperty, new PropertyValue<int>(cornerRadiusFunc));
        return imageButton;
    }

    public static T BorderWidth<T>(this T imageButton, double borderWidth, RxDoubleAnimation? customAnimation = null)
        where T : IImageButton
    {
        //imageButton.BorderWidth = borderWidth;
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.BorderWidthProperty, borderWidth);
        imageButton.AppendAnimatable(Microsoft.Maui.Controls.ImageButton.BorderWidthProperty, customAnimation ?? new RxDoubleAnimation(borderWidth));
        return imageButton;
    }

    public static T BorderWidth<T>(this T imageButton, Func<double> borderWidthFunc)
        where T : IImageButton
    {
        //imageButton.BorderWidth = new PropertyValue<double>(borderWidthFunc);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.BorderWidthProperty, new PropertyValue<double>(borderWidthFunc));
        return imageButton;
    }

    public static T BorderColor<T>(this T imageButton, Microsoft.Maui.Graphics.Color borderColor)
        where T : IImageButton
    {
        //imageButton.BorderColor = borderColor;
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.BorderColorProperty, borderColor);
        return imageButton;
    }

    public static T BorderColor<T>(this T imageButton, Func<Microsoft.Maui.Graphics.Color> borderColorFunc)
        where T : IImageButton
    {
        //imageButton.BorderColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(borderColorFunc);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.BorderColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(borderColorFunc));
        return imageButton;
    }

    public static T Source<T>(this T imageButton, Microsoft.Maui.Controls.ImageSource source)
        where T : IImageButton
    {
        //imageButton.Source = source;
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.SourceProperty, source);
        return imageButton;
    }

    public static T Source<T>(this T imageButton, Func<Microsoft.Maui.Controls.ImageSource> sourceFunc)
        where T : IImageButton
    {
        //imageButton.Source = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(sourceFunc);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.SourceProperty, new PropertyValue<Microsoft.Maui.Controls.ImageSource>(sourceFunc));
        return imageButton;
    }

    public static T Source<T>(this T imageButton, string file)
        where T : IImageButton
    {
        //imageButton.Source = Microsoft.Maui.Controls.ImageSource.FromFile(file);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.SourceProperty, Microsoft.Maui.Controls.ImageSource.FromFile(file));
        return imageButton;
    }

    public static T Source<T>(this T imageButton, Func<string> action)
        where T : IImageButton
    {
        /*imageButton.Source = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(
            () => Microsoft.Maui.Controls.ImageSource.FromFile(action()));*/
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.SourceProperty, new PropertyValue<Microsoft.Maui.Controls.ImageSource>(() => Microsoft.Maui.Controls.ImageSource.FromFile(action())));
        return imageButton;
    }

    public static T Source<T>(this T imageButton, string resourceName, Assembly sourceAssembly)
        where T : IImageButton
    {
        //imageButton.Source = Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.SourceProperty, Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly));
        return imageButton;
    }

    public static T Source<T>(this T imageButton, Uri imageUri)
        where T : IImageButton
    {
        //imageButton.Source = Microsoft.Maui.Controls.ImageSource.FromUri(imageUri);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.SourceProperty, Microsoft.Maui.Controls.ImageSource.FromUri(imageUri));
        return imageButton;
    }

    public static T Source<T>(this T imageButton, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity)
        where T : IImageButton
    {
        //imageButton.Source = new Microsoft.Maui.Controls.UriImageSource
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
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.SourceProperty, newValue);
        return imageButton;
    }

    public static T Source<T>(this T imageButton, Func<Stream> imageStream)
        where T : IImageButton
    {
        //imageButton.Source = Microsoft.Maui.Controls.ImageSource.FromStream(imageStream);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.SourceProperty, Microsoft.Maui.Controls.ImageSource.FromStream(imageStream));
        return imageButton;
    }

    public static T Aspect<T>(this T imageButton, Microsoft.Maui.Aspect aspect)
        where T : IImageButton
    {
        //imageButton.Aspect = aspect;
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.AspectProperty, aspect);
        return imageButton;
    }

    public static T Aspect<T>(this T imageButton, Func<Microsoft.Maui.Aspect> aspectFunc)
        where T : IImageButton
    {
        //imageButton.Aspect = new PropertyValue<Microsoft.Maui.Aspect>(aspectFunc);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.AspectProperty, new PropertyValue<Microsoft.Maui.Aspect>(aspectFunc));
        return imageButton;
    }

    public static T IsOpaque<T>(this T imageButton, bool isOpaque)
        where T : IImageButton
    {
        //imageButton.IsOpaque = isOpaque;
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.IsOpaqueProperty, isOpaque);
        return imageButton;
    }

    public static T IsOpaque<T>(this T imageButton, Func<bool> isOpaqueFunc)
        where T : IImageButton
    {
        //imageButton.IsOpaque = new PropertyValue<bool>(isOpaqueFunc);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.IsOpaqueProperty, new PropertyValue<bool>(isOpaqueFunc));
        return imageButton;
    }

    public static T Padding<T>(this T imageButton, Microsoft.Maui.Thickness padding, RxThicknessAnimation? customAnimation = null)
        where T : IImageButton
    {
        //imageButton.Padding = padding;
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.PaddingProperty, padding);
        imageButton.AppendAnimatable(Microsoft.Maui.Controls.ImageButton.PaddingProperty, customAnimation ?? new RxSimpleThicknessAnimation(padding));
        return imageButton;
    }

    public static T Padding<T>(this T imageButton, Func<Microsoft.Maui.Thickness> paddingFunc)
        where T : IImageButton
    {
        //imageButton.Padding = new PropertyValue<Microsoft.Maui.Thickness>(paddingFunc);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.PaddingProperty, new PropertyValue<Microsoft.Maui.Thickness>(paddingFunc));
        return imageButton;
    }

    public static T Padding<T>(this T imageButton, double leftRight, double topBottom, RxThicknessAnimation? customAnimation = null)
        where T : IImageButton
    {
        //imageButton.Padding = new Thickness(leftRight, topBottom);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.PaddingProperty, new Thickness(leftRight, topBottom));
        imageButton.AppendAnimatable(Microsoft.Maui.Controls.ImageButton.PaddingProperty, customAnimation ?? new RxSimpleThicknessAnimation(new Thickness(leftRight, topBottom)));
        return imageButton;
    }

    public static T Padding<T>(this T imageButton, double uniformSize, RxThicknessAnimation? customAnimation = null)
        where T : IImageButton
    {
        //imageButton.Padding = new Thickness(uniformSize);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.PaddingProperty, new Thickness(uniformSize));
        imageButton.AppendAnimatable(Microsoft.Maui.Controls.ImageButton.PaddingProperty, customAnimation ?? new RxSimpleThicknessAnimation(new Thickness(uniformSize)));
        return imageButton;
    }

    public static T Padding<T>(this T imageButton, double left, double top, double right, double bottom, RxThicknessAnimation? customAnimation = null)
        where T : IImageButton
    {
        //imageButton.Padding = new Thickness(left, top, right, bottom);
        imageButton.SetProperty(Microsoft.Maui.Controls.ImageButton.PaddingProperty, new Thickness(left, top, right, bottom));
        imageButton.AppendAnimatable(Microsoft.Maui.Controls.ImageButton.PaddingProperty, customAnimation ?? new RxSimpleThicknessAnimation(new Thickness(left, top, right, bottom)));
        return imageButton;
    }

    public static T OnClicked<T>(this T imageButton, Action? clickedAction)
        where T : IImageButton
    {
        imageButton.ClickedEvent = new SyncEventCommand<EventArgs>(execute: clickedAction);
        return imageButton;
    }

    public static T OnClicked<T>(this T imageButton, Action<EventArgs>? clickedAction)
        where T : IImageButton
    {
        imageButton.ClickedEvent = new SyncEventCommand<EventArgs>(executeWithArgs: clickedAction);
        return imageButton;
    }

    public static T OnClicked<T>(this T imageButton, Action<object?, EventArgs>? clickedAction)
        where T : IImageButton
    {
        imageButton.ClickedEvent = new SyncEventCommand<EventArgs>(executeWithFullArgs: clickedAction);
        return imageButton;
    }

    public static T OnClicked<T>(this T imageButton, Func<Task>? clickedAction)
        where T : IImageButton
    {
        imageButton.ClickedEvent = new AsyncEventCommand<EventArgs>(execute: clickedAction);
        return imageButton;
    }

    public static T OnClicked<T>(this T imageButton, Func<EventArgs, Task>? clickedAction)
        where T : IImageButton
    {
        imageButton.ClickedEvent = new AsyncEventCommand<EventArgs>(executeWithArgs: clickedAction);
        return imageButton;
    }

    public static T OnClicked<T>(this T imageButton, Func<object?, EventArgs, Task>? clickedAction)
        where T : IImageButton
    {
        imageButton.ClickedEvent = new AsyncEventCommand<EventArgs>(executeWithFullArgs: clickedAction);
        return imageButton;
    }

    public static T OnPressed<T>(this T imageButton, Action? pressedAction)
        where T : IImageButton
    {
        imageButton.PressedEvent = new SyncEventCommand<EventArgs>(execute: pressedAction);
        return imageButton;
    }

    public static T OnPressed<T>(this T imageButton, Action<EventArgs>? pressedAction)
        where T : IImageButton
    {
        imageButton.PressedEvent = new SyncEventCommand<EventArgs>(executeWithArgs: pressedAction);
        return imageButton;
    }

    public static T OnPressed<T>(this T imageButton, Action<object?, EventArgs>? pressedAction)
        where T : IImageButton
    {
        imageButton.PressedEvent = new SyncEventCommand<EventArgs>(executeWithFullArgs: pressedAction);
        return imageButton;
    }

    public static T OnPressed<T>(this T imageButton, Func<Task>? pressedAction)
        where T : IImageButton
    {
        imageButton.PressedEvent = new AsyncEventCommand<EventArgs>(execute: pressedAction);
        return imageButton;
    }

    public static T OnPressed<T>(this T imageButton, Func<EventArgs, Task>? pressedAction)
        where T : IImageButton
    {
        imageButton.PressedEvent = new AsyncEventCommand<EventArgs>(executeWithArgs: pressedAction);
        return imageButton;
    }

    public static T OnPressed<T>(this T imageButton, Func<object?, EventArgs, Task>? pressedAction)
        where T : IImageButton
    {
        imageButton.PressedEvent = new AsyncEventCommand<EventArgs>(executeWithFullArgs: pressedAction);
        return imageButton;
    }

    public static T OnReleased<T>(this T imageButton, Action? releasedAction)
        where T : IImageButton
    {
        imageButton.ReleasedEvent = new SyncEventCommand<EventArgs>(execute: releasedAction);
        return imageButton;
    }

    public static T OnReleased<T>(this T imageButton, Action<EventArgs>? releasedAction)
        where T : IImageButton
    {
        imageButton.ReleasedEvent = new SyncEventCommand<EventArgs>(executeWithArgs: releasedAction);
        return imageButton;
    }

    public static T OnReleased<T>(this T imageButton, Action<object?, EventArgs>? releasedAction)
        where T : IImageButton
    {
        imageButton.ReleasedEvent = new SyncEventCommand<EventArgs>(executeWithFullArgs: releasedAction);
        return imageButton;
    }

    public static T OnReleased<T>(this T imageButton, Func<Task>? releasedAction)
        where T : IImageButton
    {
        imageButton.ReleasedEvent = new AsyncEventCommand<EventArgs>(execute: releasedAction);
        return imageButton;
    }

    public static T OnReleased<T>(this T imageButton, Func<EventArgs, Task>? releasedAction)
        where T : IImageButton
    {
        imageButton.ReleasedEvent = new AsyncEventCommand<EventArgs>(executeWithArgs: releasedAction);
        return imageButton;
    }

    public static T OnReleased<T>(this T imageButton, Func<object?, EventArgs, Task>? releasedAction)
        where T : IImageButton
    {
        imageButton.ReleasedEvent = new AsyncEventCommand<EventArgs>(executeWithFullArgs: releasedAction);
        return imageButton;
    }
}

public static partial class ImageButtonStyles
{
    public static Action<IImageButton>? Default { get; set; }
    public static Dictionary<string, Action<IImageButton>> Themes { get; } = [];
}