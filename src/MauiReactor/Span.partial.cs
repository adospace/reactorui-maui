using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class Span
{
    public Span(string text)
    {
        this.Text(text);
    }

    public Span(string text, Color textColor)
    {
        this.Text(text);
        this.TextColor(textColor);
    }

    public Span(string text, Color textColor, FontAttributes fontAttributes)
    {
        this.Text(text);
        this.TextColor(textColor);
        this.FontAttributes(fontAttributes);
    }

    public Span(string text, Color textColor, FontAttributes fontAttributes, double fontSize)
    {
        this.Text(text);
        this.TextColor(textColor);
        this.FontAttributes(fontAttributes);
        this.FontSize(fontSize);
    }

    public Span(string text, FontAttributes fontAttributes)
    {
        this.Text(text);
        this.FontAttributes(fontAttributes);
    }

    public Span(string text, FontAttributes fontAttributes, double fontSize)
    {
        this.Text(text);
        this.FontAttributes(fontAttributes);
        this.FontSize(fontSize);
    }
}

public partial class Component
{
    public static Span Span(string text) =>
        new(text);
    public static Span Span(string text, params VisualNode[] children) =>
        new(text) { children };

    public static Span Span(string text, Color textColor) =>
        new(text, textColor);
    public static Span Span(string text, Color textColor, params VisualNode[] children) =>
        new(text, textColor) { children };

    public static Span Span(string text, Color textColor, FontAttributes fontAttributes) =>
        new(text, textColor, fontAttributes);
    public static Span Span(string text, Color textColor, FontAttributes fontAttributes, params VisualNode[] children) =>
        new(text, textColor, fontAttributes) { children };
    public static Span Span(string text, Color textColor, FontAttributes fontAttributes, double fontSize) =>
        new(text, textColor, fontAttributes, fontSize);
    public static Span Span(string text, Color textColor, FontAttributes fontAttributes, double fontSize, params VisualNode[] children) =>
        new(text, textColor, fontAttributes, fontSize) { children };

    public static Span Span(string text, FontAttributes fontAttributes) =>
        new(text, fontAttributes);
    public static Span Span(string text,FontAttributes fontAttributes, params VisualNode[] children) =>
        new(text, fontAttributes) { children };
    public static Span Span(string text, FontAttributes fontAttributes, double fontSize) =>
        new(text, fontAttributes, fontSize);
    public static Span Span(string text, FontAttributes fontAttributes, double fontSize, params VisualNode[] children) =>
        new(text, fontAttributes, fontSize) { children };
}
