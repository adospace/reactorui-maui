namespace MauiReactor;

public static class GradientStopCollectionExtensions
{
    public static GradientStopCollection CreateStopCollection(ReadOnlySpan<uint> colors)
    {
        var stops = new GradientStopCollection();
        
        for (int i = 0; i < colors.Length; i++)
        {
            stops.Add(new GradientStop(Color.FromUint(colors[i]), i * 1.0f / colors.Length));
        }

        return stops;
    }

    public static GradientStopCollection CreateStopCollection(ReadOnlySpan<Color> colors)
    {
        var stops = new GradientStopCollection();

        for (int i = 0; i < colors.Length; i++)
        {
            stops.Add(new GradientStop(colors[i], i * 1.0f / colors.Length));
        }

        return stops;
    }

    public static GradientStopCollection CreateStopCollection(ReadOnlySpan<string> colors)
    {
        var stops = new GradientStopCollection();

        for (int i = 0; i < colors.Length; i++)
        {
            stops.Add(new GradientStop(Color.FromArgb(colors[i]), i * 1.0f / colors.Length));
        }

        return stops;
    }

    public static GradientStopCollection CreateStopCollection(params ReadOnlySpan<GradientStop> stops)
    {
        var stopCollection = new GradientStopCollection();
        foreach (var stop in stops)
        {
            stopCollection.Add(stop);
        }

        return stopCollection;
    }

    public static GradientStopCollection CreateStopCollection(params ReadOnlySpan<(Color, float)> stops)
    {
        var stopCollection = new GradientStopCollection();
        foreach (var stop in stops)
        {
            stopCollection.Add(new GradientStop(stop.Item1, stop.Item2));
        }

        return stopCollection;
    }

    public static GradientStopCollection CreateStopCollection(params ReadOnlySpan<(uint, float)> stops)
    {
        var stopCollection = new GradientStopCollection();
        foreach (var stop in stops)
        {
            stopCollection.Add(new GradientStop(Color.FromUint(stop.Item1), stop.Item2));
        }

        return stopCollection;
    }

    public static GradientStopCollection CreateStopCollection(params ReadOnlySpan<(string, float)> stops)
    {
        var stopCollection = new GradientStopCollection();
        foreach (var stop in stops)
        {
            stopCollection.Add(new GradientStop(Color.FromArgb(stop.Item1), stop.Item2));
        }

        return stopCollection;
    }
}
