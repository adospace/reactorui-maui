using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MauiReactor.FigmaPlugin.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }

    //public static class Program
    //{
    //    //[global::System.Runtime.InteropServices.DllImport("Microsoft.ui.xaml.dll")]
    //    //private static extern void XamlCheckProcessRequirements();

    //    //[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler", " 1.0.0.0")]
    //    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    //    [global::System.STAThreadAttribute]
    //    static void Main(string[] args)
    //    {
    //        //XamlCheckProcessRequirements();

    //        global::WinRT.ComWrappersSupport.InitializeComWrappers();
    //        global::Microsoft.UI.Xaml.Application.Start((p) => {
    //            var context = new global::Microsoft.UI.Dispatching.DispatcherQueueSynchronizationContext(global::Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread());
    //            global::System.Threading.SynchronizationContext.SetSynchronizationContext(context);
    //            new App();
    //        });
    //    }
    //}
}