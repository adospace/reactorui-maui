using MauiReactor;
using MauiReactorTemplate.Startup.Components;
using MauiReactorTemplate.Startup.Resources.Styles;

namespace MauiReactorTemplate.StartupXaml;

public partial class App : MauiReactorApplication
{
    public App()
    {
        InitializeComponent();
    }
}


public abstract class MauiReactorApplication : ReactorApplication<HomePage>
{
    public MauiReactorApplication()
    {
        this.UseTheme<ApplicationTheme>();
    }
}