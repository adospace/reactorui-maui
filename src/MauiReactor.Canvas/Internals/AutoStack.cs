using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas.Internals;

public class AutoStackSizeChangedEventArgs : EventArgs
{
    public AutoStackSizeChangedEventArgs(SizeF size)
    {
        Size = size;
    }

    public SizeF Size { get; }
}

public class AutoStack : CanvasVisualElement
{

    public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(AutoStack), StackOrientation.Vertical);

    public StackOrientation Orientation
    {
        get => (StackOrientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }


    public static readonly BindableProperty DefaultWidthProperty = BindableProperty.Create(nameof(DefaultWidth), typeof(float), typeof(AutoStack), 100f,
        coerceValue: (bindableObject, value) => Math.Clamp((float)value, 0, float.MaxValue));

    public float DefaultWidth
    {
        get => (float)GetValue(DefaultWidthProperty);
        set => SetValue(DefaultWidthProperty, value);
    }

    public static readonly BindableProperty DefaultHeightProperty = BindableProperty.Create(nameof(DefaultHeight), typeof(float), typeof(AutoStack), 100f,
        coerceValue: (bindableObject, value) => Math.Clamp((float)value, 0, float.MaxValue));

    public float DefaultHeight
    {
        get => (float)GetValue(DefaultHeightProperty);
        set => SetValue(DefaultHeightProperty, value);
    }

    public static readonly BindableProperty WidthProperty = BindableProperty.CreateAttached("Width", typeof(float), typeof(AutoStack), float.NaN,
        coerceValue: (bindableObject, value) => float.IsNaN((float)value) ? float.NaN : Math.Clamp((float)value, 0, float.MaxValue));

    public static readonly BindableProperty HeightProperty = BindableProperty.CreateAttached("Height", typeof(float), typeof(AutoStack), float.NaN,
        coerceValue: (bindableObject, value) => float.IsNaN((float)value) ? float.NaN : Math.Clamp((float)value, 0, float.MaxValue));

    private SizeF? _currentSize;

    public event EventHandler<AutoStackSizeChangedEventArgs>? SizeChanged;

    protected override void OnDraw(DrawingContext context)
    {
        if (Orientation == StackOrientation.Vertical)
        {
            LayoutVertical(context);
        }
        else
        {
            LayoutHorizontal(context);
        }

        base.OnDraw(context);
    }

    private void LayoutVertical(DrawingContext context)
    {
        var oldRect = context.DirtyRect;

        float currentTop = oldRect.Top;
        float defaultHeight = DefaultHeight;
        float totalHeight = 0.0f;

        for (int i = 0; i < Children.Count; i++)
        {
            var childDesiredHeight = (float)Children[i].GetValue(HeightProperty);
            var childHeight = float.IsNaN(childDesiredHeight) ? defaultHeight : childDesiredHeight;
            context.DirtyRect = new RectF(oldRect.Left, currentTop, oldRect.Width, childHeight);

            Children[i].Draw(context);

            totalHeight += childHeight;
            currentTop += childHeight;
        }

        var newSize = new SizeF(oldRect.Width, totalHeight);
        if (_currentSize == null ||
            _currentSize.Value != newSize)
        {
            _currentSize = newSize;
            SizeChanged?.Invoke(this, new AutoStackSizeChangedEventArgs(newSize));
        }

        context.DirtyRect = oldRect;
    }

    private void LayoutHorizontal(DrawingContext context)
    {
        var oldRect = context.DirtyRect;

        float currentLeft = oldRect.Left;
        float defaultWidth = DefaultWidth;
        float totalWidth = 0.0f;

        for (int i = 0; i < Children.Count; i++)
        {
            var childDesiredWidth = (float)Children[i].GetValue(WidthProperty);
            var childWidth = float.IsNaN(childDesiredWidth) ? defaultWidth : childDesiredWidth;
            context.DirtyRect = new RectF(currentLeft, oldRect.Top, childWidth, oldRect.Height);

            Children[i].Draw(context);

            totalWidth += childWidth;
            currentLeft += childWidth;
        }

        var newSize = new SizeF(totalWidth, oldRect.Height);
        if (_currentSize == null ||
            _currentSize.Value != newSize)
        {
            _currentSize = newSize;
            SizeChanged?.Invoke(this, new AutoStackSizeChangedEventArgs(newSize));
        }

        context.DirtyRect = oldRect;
    }
}
