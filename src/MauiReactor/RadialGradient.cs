namespace MauiReactor;

public class RadialGradient
{
    private readonly RadialGradientBrush _brush;

    public RadialGradient(params uint[] colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops);
    }

    public RadialGradient(double radius, params uint[] colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, radius);
    }

    public RadialGradient(Point center, double radius, params uint[] colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, center, radius);
    }

    public RadialGradient(params Color[] colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops);
    }

    public RadialGradient(double radius, params Color[] colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, radius);
    }

    public RadialGradient(Point center, double radius, params Color[] colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, center, radius);
    }

    public RadialGradient(params string[] colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops);
    }

    public RadialGradient(double radius, params string[] colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, radius);
    }

    public RadialGradient(Point center, double radius, params string[] colors)
    {
        var stops = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = new RadialGradientBrush(stops, center, radius);
    }

    public RadialGradient(params GradientStop[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection);
    }

    public RadialGradient(double radius, params GradientStop[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, radius);
    }

    public RadialGradient(Point center, double radius, params GradientStop[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, center, radius);
    }

    public RadialGradient(params (Color, float)[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection);
    }

    public RadialGradient(double radius, params (Color, float)[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, radius);
    }

    public RadialGradient(Point center, double radius, params (Color, float)[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, center, radius);
    }

    public RadialGradient(params (uint, float)[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection);
    }

    public RadialGradient(double radius, params (uint, float)[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, radius);
    }

    public RadialGradient(Point center, double radius, params (uint, float)[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, center, radius);
    }

    public RadialGradient(params (string, float)[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection);
    }

    public RadialGradient(double radius, params (string, float)[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, radius);
    }

    public RadialGradient(Point center, double radius, params (string, float)[] stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = new RadialGradientBrush(stopCollection, center, radius);
    }

    public static implicit operator RadialGradientBrush(RadialGradient d) => d._brush;
}
