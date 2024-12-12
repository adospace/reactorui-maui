namespace MauiReactor;

public class RadialGradient
{
    private readonly RadialGradientBrush _brush;

    public RadialGradient(params ReadOnlySpan<uint> colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops);
    }

    public RadialGradient(double radius, params ReadOnlySpan<uint> colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, radius);
    }

    public RadialGradient(Point center, double radius, params ReadOnlySpan<uint> colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, center, radius);
    }

    public RadialGradient(params ReadOnlySpan<Color> colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops);
    }

    public RadialGradient(double radius, params ReadOnlySpan<Color> colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, radius);
    }

    public RadialGradient(Point center, double radius, params ReadOnlySpan<Color> colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, center, radius);
    }

    public RadialGradient(params ReadOnlySpan<string> colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops);
    }

    public RadialGradient(double radius, params ReadOnlySpan<string> colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, radius);
    }

    public RadialGradient(Point center, double radius, params ReadOnlySpan<string> colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, center, radius);
    }

    public RadialGradient(params ReadOnlySpan<GradientStop> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection);
    }

    public RadialGradient(double radius, params ReadOnlySpan<GradientStop> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, radius);
    }

    public RadialGradient(Point center, double radius, params ReadOnlySpan<GradientStop> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, center, radius);
    }

    public RadialGradient(params ReadOnlySpan<(Color, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection);
    }

    public RadialGradient(double radius, params ReadOnlySpan<(Color, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, radius);
    }

    public RadialGradient(Point center, double radius, params ReadOnlySpan<(Color, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, center, radius);
    }

    public RadialGradient(params ReadOnlySpan<(uint, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection);
    }

    public RadialGradient(double radius, params ReadOnlySpan<(uint, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, radius);
    }

    public RadialGradient(Point center, double radius, params ReadOnlySpan<(uint, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, center, radius);
    }

    public RadialGradient(params ReadOnlySpan<(string, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection);
    }

    public RadialGradient(double radius, params ReadOnlySpan<(string, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, radius);
    }

    public RadialGradient(Point center, double radius, params ReadOnlySpan<(string, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, center, radius);
    }

    public static implicit operator RadialGradientBrush(RadialGradient d) => d._brush;
}
