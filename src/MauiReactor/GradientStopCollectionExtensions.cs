namespace MauiReactor;

public static class GradientStopCollectionExtensions
{
    public static GradientStopCollection CreateStopCollection(uint[] colors)
    {
        var stops = new GradientStopCollection();

        for (int i = 0; i < colors.Length; i++)
        {
            stops.Add(new GradientStop(Color.FromUint(colors[i]), i * 1.0f / colors.Length));
        }

        return stops;
    }

    public static GradientStopCollection CreateStopCollection(Color[] colors)
    {
        var stops = new GradientStopCollection();

        for (int i = 0; i < colors.Length; i++)
        {
            stops.Add(new GradientStop(colors[i], i * 1.0f / colors.Length));
        }

        return stops;
    }

    public static GradientStopCollection CreateStopCollection(string[] colors)
    {
        var stops = new GradientStopCollection();

        for (int i = 0; i < colors.Length; i++)
        {
            stops.Add(new GradientStop(Color.FromArgb(colors[i]), i * 1.0f / colors.Length));
        }

        return stops;
    }

    public static GradientStopCollection CreateStopCollection(params GradientStop[] stops)
    {
        var stopCollection = new GradientStopCollection();
        for (int i = 0; i < stops.Length; i++)
        {
            stopCollection.Add(stops[i]);
        }

        return stopCollection;
    }

    public static GradientStopCollection CreateStopCollection(params (Color, float)[] stops)
    {
        var stopCollection = new GradientStopCollection();
        for (int i = 0; i < stops.Length; i++)
        {
            stopCollection.Add(new GradientStop(stops[i].Item1, stops[i].Item2));
        }

        return stopCollection;
    }

    public static GradientStopCollection CreateStopCollection(params (uint, float)[] stops)
    {
        var stopCollection = new GradientStopCollection();
        for (int i = 0; i < stops.Length; i++)
        {
            stopCollection.Add(new GradientStop(Color.FromUint(stops[i].Item1), stops[i].Item2));
        }

        return stopCollection;
    }

    public static GradientStopCollection CreateStopCollection(params (string, float)[] stops)
    {
        var stopCollection = new GradientStopCollection();
        for (int i = 0; i < stops.Length; i++)
        {
            stopCollection.Add(new GradientStop(Color.FromArgb(stops[i].Item1), stops[i].Item2));
        }

        return stopCollection;
    }
}
