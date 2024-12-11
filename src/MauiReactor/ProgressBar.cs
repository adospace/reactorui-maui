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
}

public partial class ProgressBar<T> : View<T>, IProgressBar where T : Microsoft.Maui.Controls.ProgressBar, new()
{
    public ProgressBar()
    {
        ProgressBarStyles.Default?.Invoke(this);
    }

    public ProgressBar(Action<T?> componentRefAction) : base(componentRefAction)
    {
        ProgressBarStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && ProgressBarStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class ProgressBar : ProgressBar<Microsoft.Maui.Controls.ProgressBar>
{
    public ProgressBar()
    {
    }

    public ProgressBar(Action<Microsoft.Maui.Controls.ProgressBar?> componentRefAction) : base(componentRefAction)
    {
    }

    public ProgressBar(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class ProgressBarExtensions
{
    /*
    
    
    
    
    static object? SetProgress(object progressBar, RxAnimation animation)
        => ((IProgressBar)progressBar).Progress = ((RxDoubleAnimation)animation).CurrentValue();

    
    */
    public static T ProgressColor<T>(this T progressBar, Microsoft.Maui.Graphics.Color progressColor)
        where T : IProgressBar
    {
        //progressBar.ProgressColor = progressColor;
        progressBar.SetProperty(Microsoft.Maui.Controls.ProgressBar.ProgressColorProperty, progressColor);
        return progressBar;
    }

    public static T ProgressColor<T>(this T progressBar, Func<Microsoft.Maui.Graphics.Color> progressColorFunc)
        where T : IProgressBar
    {
        //progressBar.ProgressColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(progressColorFunc);
        progressBar.SetProperty(Microsoft.Maui.Controls.ProgressBar.ProgressColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(progressColorFunc));
        return progressBar;
    }

    public static T Progress<T>(this T progressBar, double progress, RxDoubleAnimation? customAnimation = null)
        where T : IProgressBar
    {
        //progressBar.Progress = progress;
        progressBar.SetProperty(Microsoft.Maui.Controls.ProgressBar.ProgressProperty, progress);
        progressBar.AppendAnimatable(Microsoft.Maui.Controls.ProgressBar.ProgressProperty, customAnimation ?? new RxDoubleAnimation(progress));
        return progressBar;
    }

    public static T Progress<T>(this T progressBar, Func<double> progressFunc)
        where T : IProgressBar
    {
        //progressBar.Progress = new PropertyValue<double>(progressFunc);
        progressBar.SetProperty(Microsoft.Maui.Controls.ProgressBar.ProgressProperty, new PropertyValue<double>(progressFunc));
        return progressBar;
    }
}

public static partial class ProgressBarStyles
{
    public static Action<IProgressBar>? Default { get; set; }
    public static Dictionary<string, Action<IProgressBar>> Themes { get; } = [];
}