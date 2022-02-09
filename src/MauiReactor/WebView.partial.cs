namespace MauiReactor
{
    //public partial interface IWebView
    //{
    //    Uri? Source { get; set; }
    //}

    //public partial class WebView<T>
    //{
    //    Uri? IWebView.Source { get; set; }

    //    partial void OnBeginUpdate()
    //    {
    //        Validate.EnsureNotNull(NativeControl);
    //        var thisAsIWebView = (IWebView)this;
    //        if (NativeControl)
    //        NativeControl.Source = thisAsIWebView.Source;
    //    }
    //}

    public partial class WebView
    {
        public WebView(Uri source) => this.Source(source);

        public WebView(string source) => this.Source(new Uri(source));
    }


    //public static partial class WebViewExtensions
    //{
    //    public static T Source<T>(this T webView, Uri source) where T : IWebView
    //    {
    //        webView.Source = source;
    //        return webView;
    //    }
    //}
}
