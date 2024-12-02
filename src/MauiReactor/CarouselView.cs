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
public partial interface ICarouselView : IItemsView
{
    EventCommand<CurrentItemChangedEventArgs>? CurrentItemChangedEvent { get; set; }

    EventCommand<PositionChangedEventArgs>? PositionChangedEvent { get; set; }
}

public partial class CarouselView<T> : ItemsView<T>, ICarouselView where T : Microsoft.Maui.Controls.CarouselView, new()
{
    public CarouselView()
    {
        CarouselViewStyles.Default?.Invoke(this);
    }

    public CarouselView(Action<T?> componentRefAction) : base(componentRefAction)
    {
        CarouselViewStyles.Default?.Invoke(this);
    }

    EventCommand<CurrentItemChangedEventArgs>? ICarouselView.CurrentItemChangedEvent { get; set; }

    EventCommand<PositionChangedEventArgs>? ICarouselView.PositionChangedEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && CarouselViewStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<CurrentItemChangedEventArgs>? _executingCurrentItemChangedEvent;
    private EventCommand<PositionChangedEventArgs>? _executingPositionChangedEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsICarouselView = (ICarouselView)this;
        if (thisAsICarouselView.CurrentItemChangedEvent != null)
        {
            NativeControl.CurrentItemChanged += NativeControl_CurrentItemChanged;
        }

        if (thisAsICarouselView.PositionChangedEvent != null)
        {
            NativeControl.PositionChanged += NativeControl_PositionChanged;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_CurrentItemChanged(object? sender, CurrentItemChangedEventArgs e)
    {
        var thisAsICarouselView = (ICarouselView)this;
        if (_executingCurrentItemChangedEvent == null || _executingCurrentItemChangedEvent.IsCompleted)
        {
            _executingCurrentItemChangedEvent = thisAsICarouselView.CurrentItemChangedEvent;
            _executingCurrentItemChangedEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_PositionChanged(object? sender, PositionChangedEventArgs e)
    {
        var thisAsICarouselView = (ICarouselView)this;
        if (_executingPositionChangedEvent == null || _executingPositionChangedEvent.IsCompleted)
        {
            _executingPositionChangedEvent = thisAsICarouselView.PositionChangedEvent;
            _executingPositionChangedEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.CurrentItemChanged -= NativeControl_CurrentItemChanged;
            NativeControl.PositionChanged -= NativeControl_PositionChanged;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is CarouselView<T> @carouselview)
        {
            if (_executingCurrentItemChangedEvent != null && !_executingCurrentItemChangedEvent.IsCompleted)
            {
                @carouselview._executingCurrentItemChangedEvent = _executingCurrentItemChangedEvent;
            }

            if (_executingPositionChangedEvent != null && !_executingPositionChangedEvent.IsCompleted)
            {
                @carouselview._executingPositionChangedEvent = _executingPositionChangedEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class CarouselView : CarouselView<Microsoft.Maui.Controls.CarouselView>
{
    public CarouselView()
    {
    }

    public CarouselView(Action<Microsoft.Maui.Controls.CarouselView?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class CarouselViewExtensions
{
    /*
    
    
    
            
    static object? SetPeekAreaInsets(object carouselView, RxAnimation animation)
        => ((ICarouselView)carouselView).PeekAreaInsets = ((RxThicknessAnimation)animation).CurrentValue();

    
    
    
    
    
    
    
    
    
    
    
    */
    public static T Loop<T>(this T carouselView, bool loop)
        where T : ICarouselView
    {
        //carouselView.Loop = loop;
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.LoopProperty, loop);
        return carouselView;
    }

    public static T Loop<T>(this T carouselView, Func<bool> loopFunc)
        where T : ICarouselView
    {
        //carouselView.Loop = new PropertyValue<bool>(loopFunc);
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.LoopProperty, new PropertyValue<bool>(loopFunc));
        return carouselView;
    }

    public static T PeekAreaInsets<T>(this T carouselView, Microsoft.Maui.Thickness peekAreaInsets, RxThicknessAnimation? customAnimation = null)
        where T : ICarouselView
    {
        //carouselView.PeekAreaInsets = peekAreaInsets;
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.PeekAreaInsetsProperty, peekAreaInsets);
        carouselView.AppendAnimatable(Microsoft.Maui.Controls.CarouselView.PeekAreaInsetsProperty, customAnimation ?? new RxSimpleThicknessAnimation(peekAreaInsets));
        return carouselView;
    }

    public static T PeekAreaInsets<T>(this T carouselView, Func<Microsoft.Maui.Thickness> peekAreaInsetsFunc)
        where T : ICarouselView
    {
        //carouselView.PeekAreaInsets = new PropertyValue<Microsoft.Maui.Thickness>(peekAreaInsetsFunc);
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.PeekAreaInsetsProperty, new PropertyValue<Microsoft.Maui.Thickness>(peekAreaInsetsFunc));
        return carouselView;
    }

    public static T PeekAreaInsets<T>(this T carouselView, double leftRight, double topBottom, RxThicknessAnimation? customAnimation = null)
        where T : ICarouselView
    {
        //carouselView.PeekAreaInsets = new Thickness(leftRight, topBottom);
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.PeekAreaInsetsProperty, new Thickness(leftRight, topBottom));
        carouselView.AppendAnimatable(Microsoft.Maui.Controls.CarouselView.PeekAreaInsetsProperty, customAnimation ?? new RxSimpleThicknessAnimation(new Thickness(leftRight, topBottom)));
        return carouselView;
    }

    public static T PeekAreaInsets<T>(this T carouselView, double uniformSize, RxThicknessAnimation? customAnimation = null)
        where T : ICarouselView
    {
        //carouselView.PeekAreaInsets = new Thickness(uniformSize);
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.PeekAreaInsetsProperty, new Thickness(uniformSize));
        carouselView.AppendAnimatable(Microsoft.Maui.Controls.CarouselView.PeekAreaInsetsProperty, customAnimation ?? new RxSimpleThicknessAnimation(new Thickness(uniformSize)));
        return carouselView;
    }

    public static T PeekAreaInsets<T>(this T carouselView, double left, double top, double right, double bottom, RxThicknessAnimation? customAnimation = null)
        where T : ICarouselView
    {
        //carouselView.PeekAreaInsets = new Thickness(left, top, right, bottom);
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.PeekAreaInsetsProperty, new Thickness(left, top, right, bottom));
        carouselView.AppendAnimatable(Microsoft.Maui.Controls.CarouselView.PeekAreaInsetsProperty, customAnimation ?? new RxSimpleThicknessAnimation(new Thickness(left, top, right, bottom)));
        return carouselView;
    }

    public static T IsBounceEnabled<T>(this T carouselView, bool isBounceEnabled)
        where T : ICarouselView
    {
        //carouselView.IsBounceEnabled = isBounceEnabled;
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.IsBounceEnabledProperty, isBounceEnabled);
        return carouselView;
    }

    public static T IsBounceEnabled<T>(this T carouselView, Func<bool> isBounceEnabledFunc)
        where T : ICarouselView
    {
        //carouselView.IsBounceEnabled = new PropertyValue<bool>(isBounceEnabledFunc);
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.IsBounceEnabledProperty, new PropertyValue<bool>(isBounceEnabledFunc));
        return carouselView;
    }

    public static T IsSwipeEnabled<T>(this T carouselView, bool isSwipeEnabled)
        where T : ICarouselView
    {
        //carouselView.IsSwipeEnabled = isSwipeEnabled;
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.IsSwipeEnabledProperty, isSwipeEnabled);
        return carouselView;
    }

    public static T IsSwipeEnabled<T>(this T carouselView, Func<bool> isSwipeEnabledFunc)
        where T : ICarouselView
    {
        //carouselView.IsSwipeEnabled = new PropertyValue<bool>(isSwipeEnabledFunc);
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.IsSwipeEnabledProperty, new PropertyValue<bool>(isSwipeEnabledFunc));
        return carouselView;
    }

    public static T IsScrollAnimated<T>(this T carouselView, bool isScrollAnimated)
        where T : ICarouselView
    {
        //carouselView.IsScrollAnimated = isScrollAnimated;
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.IsScrollAnimatedProperty, isScrollAnimated);
        return carouselView;
    }

    public static T IsScrollAnimated<T>(this T carouselView, Func<bool> isScrollAnimatedFunc)
        where T : ICarouselView
    {
        //carouselView.IsScrollAnimated = new PropertyValue<bool>(isScrollAnimatedFunc);
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.IsScrollAnimatedProperty, new PropertyValue<bool>(isScrollAnimatedFunc));
        return carouselView;
    }

    public static T CurrentItem<T>(this T carouselView, object? currentItem)
        where T : ICarouselView
    {
        //carouselView.CurrentItem = currentItem;
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.CurrentItemProperty, currentItem);
        return carouselView;
    }

    public static T CurrentItem<T>(this T carouselView, Func<object?> currentItemFunc)
        where T : ICarouselView
    {
        //carouselView.CurrentItem = new PropertyValue<object?>(currentItemFunc);
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.CurrentItemProperty, new PropertyValue<object?>(currentItemFunc));
        return carouselView;
    }

    public static T Position<T>(this T carouselView, int position)
        where T : ICarouselView
    {
        //carouselView.Position = position;
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.PositionProperty, position);
        return carouselView;
    }

    public static T Position<T>(this T carouselView, Func<int> positionFunc)
        where T : ICarouselView
    {
        //carouselView.Position = new PropertyValue<int>(positionFunc);
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.PositionProperty, new PropertyValue<int>(positionFunc));
        return carouselView;
    }

    public static T OnCurrentItemChanged<T>(this T carouselView, Action? currentItemChangedAction)
        where T : ICarouselView
    {
        carouselView.CurrentItemChangedEvent = new SyncEventCommand<CurrentItemChangedEventArgs>(execute: currentItemChangedAction);
        return carouselView;
    }

    public static T OnCurrentItemChanged<T>(this T carouselView, Action<CurrentItemChangedEventArgs>? currentItemChangedAction)
        where T : ICarouselView
    {
        carouselView.CurrentItemChangedEvent = new SyncEventCommand<CurrentItemChangedEventArgs>(executeWithArgs: currentItemChangedAction);
        return carouselView;
    }

    public static T OnCurrentItemChanged<T>(this T carouselView, Action<object?, CurrentItemChangedEventArgs>? currentItemChangedAction)
        where T : ICarouselView
    {
        carouselView.CurrentItemChangedEvent = new SyncEventCommand<CurrentItemChangedEventArgs>(executeWithFullArgs: currentItemChangedAction);
        return carouselView;
    }

    public static T OnCurrentItemChanged<T>(this T carouselView, Func<Task>? currentItemChangedAction)
        where T : ICarouselView
    {
        carouselView.CurrentItemChangedEvent = new AsyncEventCommand<CurrentItemChangedEventArgs>(execute: currentItemChangedAction);
        return carouselView;
    }

    public static T OnCurrentItemChanged<T>(this T carouselView, Func<CurrentItemChangedEventArgs, Task>? currentItemChangedAction)
        where T : ICarouselView
    {
        carouselView.CurrentItemChangedEvent = new AsyncEventCommand<CurrentItemChangedEventArgs>(executeWithArgs: currentItemChangedAction);
        return carouselView;
    }

    public static T OnCurrentItemChanged<T>(this T carouselView, Func<object?, CurrentItemChangedEventArgs, Task>? currentItemChangedAction)
        where T : ICarouselView
    {
        carouselView.CurrentItemChangedEvent = new AsyncEventCommand<CurrentItemChangedEventArgs>(executeWithFullArgs: currentItemChangedAction);
        return carouselView;
    }

    public static T OnPositionChanged<T>(this T carouselView, Action? positionChangedAction)
        where T : ICarouselView
    {
        carouselView.PositionChangedEvent = new SyncEventCommand<PositionChangedEventArgs>(execute: positionChangedAction);
        return carouselView;
    }

    public static T OnPositionChanged<T>(this T carouselView, Action<PositionChangedEventArgs>? positionChangedAction)
        where T : ICarouselView
    {
        carouselView.PositionChangedEvent = new SyncEventCommand<PositionChangedEventArgs>(executeWithArgs: positionChangedAction);
        return carouselView;
    }

    public static T OnPositionChanged<T>(this T carouselView, Action<object?, PositionChangedEventArgs>? positionChangedAction)
        where T : ICarouselView
    {
        carouselView.PositionChangedEvent = new SyncEventCommand<PositionChangedEventArgs>(executeWithFullArgs: positionChangedAction);
        return carouselView;
    }

    public static T OnPositionChanged<T>(this T carouselView, Func<Task>? positionChangedAction)
        where T : ICarouselView
    {
        carouselView.PositionChangedEvent = new AsyncEventCommand<PositionChangedEventArgs>(execute: positionChangedAction);
        return carouselView;
    }

    public static T OnPositionChanged<T>(this T carouselView, Func<PositionChangedEventArgs, Task>? positionChangedAction)
        where T : ICarouselView
    {
        carouselView.PositionChangedEvent = new AsyncEventCommand<PositionChangedEventArgs>(executeWithArgs: positionChangedAction);
        return carouselView;
    }

    public static T OnPositionChanged<T>(this T carouselView, Func<object?, PositionChangedEventArgs, Task>? positionChangedAction)
        where T : ICarouselView
    {
        carouselView.PositionChangedEvent = new AsyncEventCommand<PositionChangedEventArgs>(executeWithFullArgs: positionChangedAction);
        return carouselView;
    }
}

public static partial class CarouselViewStyles
{
    public static Action<ICarouselView>? Default { get; set; }
    public static Dictionary<string, Action<ICarouselView>> Themes { get; } = [];
}