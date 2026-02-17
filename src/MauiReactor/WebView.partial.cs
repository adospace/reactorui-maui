namespace MauiReactor;

public partial class WebView
{
    public WebView(Uri source) => this.Source(source);

    public WebView(string source) => this.Source(new Uri(source));
}

public partial class Component
{
    public static WebView WebView(string source) =>
        new WebView().Source(source);

    public static WebView WebView(Uri source) =>
        new WebView().Source(source);
}
