namespace MauiReactor;

public partial class WebView
{
    public WebView(Uri source) => this.Source(source);

    public WebView(string source) => this.Source(new Uri(source));
}

public partial class Component
{
    public WebView WebView(string source) =>
        GetNodeFromPool<WebView>().Source(source);

    public WebView WebView(Uri source) =>
        GetNodeFromPool<WebView>().Source(source);
}
