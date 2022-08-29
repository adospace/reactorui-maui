using MauiReactor;
//using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Trackizer.Styles;

class CustomButton : Component
{
    private string? _text;
    private float _cornerRadius = 999;
    private Paint? _foregroundPaint;
    private Paint? _backgroundPaint;
    private Paint? _strokePaint;
    private Action? _clickedAction;
    private Color? _shadowColor;

    public CustomButton Text(string text)
    {
        _text = text;
        return this;
    }

    public CustomButton CornerRadius(float cornerRadius)
    {
        _cornerRadius = cornerRadius;
        return this;
    }

    public CustomButton FillBrushBackground(Paint backgroundPaint)
    {
        _backgroundPaint = backgroundPaint;
        return this;
    }

    public CustomButton FillBrushForeground(Paint foregroundPaint)
    {
        _foregroundPaint = foregroundPaint;
        return this;
    }

    public CustomButton StrokeBrush(Paint strokePaint)
    {
        _strokePaint = strokePaint;
        return this;
    }

    public CustomButton OnClick(Action clickedAction)
    {
        _clickedAction = clickedAction;
        return this;
    }

    public CustomButton Shadow(Color shadowColor)
    {
        _shadowColor = shadowColor;
        return this;
    }

    public override VisualNode Render()
    {
        return new GraphicsView()
            .OnDraw(OnDraw)
            .OnEndInteraction(_clickedAction)
            .HeightRequest(48)
            ;
    }

    private void OnDraw(ICanvas canvas, RectF dirtyRect)
    {

        if (_strokePaint != null)
        {
            if (_shadowColor != null)
            {
                canvas.SetShadow(new SizeF(), 25, _shadowColor);
            }
            canvas.SetFillPaint(_strokePaint, dirtyRect);
            canvas.FillRoundedRectangle(dirtyRect, _cornerRadius);
        }

        dirtyRect = dirtyRect.Inflate(-2.0f, -2.0f);

        if (_backgroundPaint != null)
        {
            canvas.SetFillPaint(_backgroundPaint, dirtyRect);
            canvas.FillRoundedRectangle(dirtyRect, _cornerRadius);
        }

        if (_foregroundPaint != null)
        {
            canvas.SetFillPaint(_foregroundPaint, dirtyRect);
            canvas.FillRoundedRectangle(dirtyRect, _cornerRadius);
        }

        if (_text != null)
        {
            canvas.FontColor = Theme.Current.White;
            canvas.FontSize = 14;
            canvas.Font = Microsoft.Maui.Graphics.Font.DefaultBold;
            canvas.DrawString(_text, dirtyRect, HorizontalAlignment.Center, VerticalAlignment.Center);
        }

    }
}
