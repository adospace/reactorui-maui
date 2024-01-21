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
public partial interface IProgressBar : IView
{
    object? ProgressColor { get; set; }

    object? Progress { get; set; }
}

public partial class ProgressBar<T> : View<T>, IProgressBar where T : Microsoft.Maui.Controls.ProgressBar, new()
{
    public ProgressBar()
    {
    }

    public ProgressBar(Action<T?> componentRefAction) : base(componentRefAction)
    {
    }

    object? IProgressBar.ProgressColor { get; set; }

    object? IProgressBar.Progress { get; set; }

    internal override void Reset()
    {
        base.Reset();
        var thisAsIProgressBar = (IProgressBar)this;
        thisAsIProgressBar.ProgressColor = null;
        thisAsIProgressBar.Progress = null;
        OnReset();
    }

    partial void OnReset();
    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsIProgressBar = (IProgressBar)this;
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.ProgressBar.ProgressColorProperty, thisAsIProgressBar.ProgressColor);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.ProgressBar.ProgressProperty, thisAsIProgressBar.Progress);
        base.OnUpdate();
        OnEndUpdate();
    }

    protected override void OnAnimate()
    {
        OnBeginAnimate();
        var thisAsIProgressBar = (IProgressBar)this;
        AnimateProperty(Microsoft.Maui.Controls.ProgressBar.ProgressProperty, thisAsIProgressBar.Progress);
        base.OnAnimate();
        OnEndAnimate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
}

public partial class ProgressBar : ProgressBar<Microsoft.Maui.Controls.ProgressBar>
{
    public ProgressBar()
    {
    }

    public ProgressBar(Action<Microsoft.Maui.Controls.ProgressBar?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class ProgressBarExtensions
{
    static void SetProgress(object progressBar, RxAnimation animation) => ((IProgressBar)progressBar).Progress = ((RxDoubleAnimation)animation).CurrentValue();
    public static T ProgressColor<T>(this T progressBar, Microsoft.Maui.Graphics.Color progressColor)
        where T : IProgressBar
    {
        progressBar.ProgressColor = progressColor;
        return progressBar;
    }

    public static T ProgressColor<T>(this T progressBar, Func<Microsoft.Maui.Graphics.Color> progressColorFunc)
        where T : IProgressBar
    {
        progressBar.ProgressColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(progressColorFunc);
        return progressBar;
    }

    public static T Progress<T>(this T progressBar, double progress, RxDoubleAnimation? customAnimation = null)
        where T : IProgressBar
    {
        progressBar.Progress = progress;
        progressBar.AppendAnimatable(Microsoft.Maui.Controls.ProgressBar.ProgressProperty, customAnimation ?? new RxDoubleAnimation(progress), SetProgress);
        return progressBar;
    }

    public static T Progress<T>(this T progressBar, Func<double> progressFunc)
        where T : IProgressBar
    {
        progressBar.Progress = new PropertyValue<double>(progressFunc);
        return progressBar;
    }
}