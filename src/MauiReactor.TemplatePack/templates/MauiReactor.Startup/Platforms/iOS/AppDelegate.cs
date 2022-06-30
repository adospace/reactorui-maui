using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace MauiReactor.Startup;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
