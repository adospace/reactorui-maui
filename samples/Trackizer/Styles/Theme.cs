using MauiReactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackizer.Styles;

abstract class Theme
{
    public static Theme Current { get; } = new DefaultTheme();

    //Colors

    //Grayscale
    public abstract Color Gray100 { get; }
    public abstract Color Gray80 { get; }
    public abstract Color Gray70 { get; }
    public abstract Color Gray60 { get; }
    public abstract Color Gray50 { get; }
    public abstract Color Gray40 { get; }
    public abstract Color Gray30 { get; }
    public abstract Color Gray20 { get; }
    public abstract Color Gray10 { get; }
    public abstract Color White { get; }

    //Primary
    public abstract Color Primary100 { get; }
    public abstract Color Primary500 { get; }
    public abstract Color Primary20 { get; }
    public abstract Color Primary10 { get; }
    public abstract Color Primary05 { get; }
    public abstract Color Primary0 { get; }

    //AccentPrimary
    public abstract Color AccentP100 { get; }
    public abstract Color AccentP50 { get; }
    public abstract Color AccentP0 { get; }

    //AccentSecondary
    public abstract Color AccentS100 { get; }
    public abstract Color AccentS50 { get; }

    //Typography
    public virtual Label Display(string text)
        => new Label(text)
        .FontSize(72)
        .FontAttributes(MauiControls.FontAttributes.Bold);
    public virtual Label H8(string text)
        => new Label(text)
        .FontSize(56)
        .FontAttributes(MauiControls.FontAttributes.Bold);
    public virtual Label H7(string text)
        => new Label(text)
        .FontSize(40)
        .FontAttributes(MauiControls.FontAttributes.Bold);
    public virtual Label H6(string text)
        => new Label(text)
        .FontSize(32)
        .FontAttributes(MauiControls.FontAttributes.Bold);
    public virtual Label H5(string text)
        => new Label(text)
        .FontSize(24)
        .FontAttributes(MauiControls.FontAttributes.Bold);
    public virtual Label H4(string text)
        => new Label(text)
        .FontSize(20)
        .FontAttributes(MauiControls.FontAttributes.Bold);
    public virtual Label H3(string text)
        => new Label(text)
        .FontSize(16)
        .FontAttributes(MauiControls.FontAttributes.Bold);
    public virtual Label H2(string text)
        => new Label(text)
        .FontSize(14)
        .FontAttributes(MauiControls.FontAttributes.Bold);
    public virtual Label H1(string text)
        => new Label(text)
        .FontSize(12)
        .FontAttributes(MauiControls.FontAttributes.Bold);
    public virtual Label Subtitle(string text)
        => new Label(text)
        .FontSize(20);
    public virtual Label BodyLarge(string text)
        => new Label(text)
        .FontSize(16);
    public virtual Label BodyMedium(string text)
        => new Label(text)
        .FontSize(14);
    public virtual Label BodySmall(string text)
        => new Label(text)
        .FontSize(12);

    //Buttons
    public virtual CustomButton Button(string text)
        => new CustomButton()
        .Text(text)
        .FillBrushBackground(new SolidPaint(Gray100))
        .FillBrushForeground(new SolidPaint(White.WithAlpha(0.1f)))
        .StrokeBrush(new LinearGradientPaint
        {
            StartColor = White.WithAlpha(0.15f),
            EndColor = Colors.Transparent,
            StartPoint = new Point(0.49, 0.0),
            EndPoint = new Point(0.51, 1.0)
        });

    public virtual CustomButton PrimaryButton(string text)
        => new CustomButton()
        .Text(text)
        .FillBrushBackground(new SolidPaint(AccentP100))
        .FillBrushForeground(new RadialGradientPaint
        {
            Center = new Point(0.5, 4.0),
            Radius = 1
        }
        .WithStops(new[]
        {
            new PaintGradientStop(0.0f, Colors.Transparent),
            new PaintGradientStop(0.4325f, Colors.Transparent),
            new PaintGradientStop(1.0f, AccentP100.WithAlpha(0.5f))
        }))
        .StrokeBrush(new LinearGradientPaint
        {
            StartColor = White.WithAlpha(0.15f),
            EndColor = Colors.Transparent,
            StartPoint = new Point(0.49, 0.0),
            EndPoint = new Point(0.51, 1.0)
        })
        .Shadow(AccentP100);

    public virtual CustomEntry Entry()
        => new CustomEntry();

}

static class GradientPaintExtensions
{
    public static T AddStop<T>(this T gradientPaint, float offset, Color color) where T : GradientPaint
    {
        gradientPaint.AddOffset(offset, color);
        return gradientPaint;
    }

    public static T WithStops<T>(this T gradientPaint, PaintGradientStop[] stops) where T : GradientPaint
    {
        gradientPaint.GradientStops = stops;
        return gradientPaint;
    }
}

class DefaultTheme : Theme
{
    public override Color Gray100 => Color.FromArgb("#0E0E12");

    public override Color Gray80 => Color.FromArgb("#1C1C23");

    public override Color Gray70 => Color.FromArgb("#353542");

    public override Color Gray60 => Color.FromArgb("#4E4E61");

    public override Color Gray50 => Color.FromArgb("#666680");

    public override Color Gray40 => Color.FromArgb("#83839C");

    public override Color Gray30 => Color.FromArgb("#A2A2B5");

    public override Color Gray20 => Color.FromArgb("#C1C1CD");

    public override Color Gray10 => Color.FromArgb("#E0E0E6");

    public override Color White => Color.FromArgb("#FFFFFF");

    public override Color Primary100 => Color.FromArgb("#5E00F5");

    public override Color Primary500 => Color.FromArgb("#7722FF");

    public override Color Primary20 => Color.FromArgb("#924EFF");

    public override Color Primary10 => Color.FromArgb("#AD7BFF");

    public override Color Primary05 => Color.FromArgb("#C9A7FF");

    public override Color Primary0 => Color.FromArgb("#E4D3FF");

    public override Color AccentP100 => Color.FromArgb("#FF7966");

    public override Color AccentP50 => Color.FromArgb("#FFA699");

    public override Color AccentP0 => Color.FromArgb("#FFD2CC");

    public override Color AccentS100 => Color.FromArgb("#00FAD9");

    public override Color AccentS50 => Color.FromArgb("#7DFFEE");

}
