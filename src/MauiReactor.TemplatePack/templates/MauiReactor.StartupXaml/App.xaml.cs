using MauiReactor;
using MauiReactorTemplate.StartupXaml.Components;
using MauiReactorTemplate.StartupXaml.Resources.Styles;

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