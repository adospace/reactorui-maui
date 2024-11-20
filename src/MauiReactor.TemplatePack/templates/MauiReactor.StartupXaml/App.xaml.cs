using MauiReactor;
using MauiReactorTemplate.StartupXaml.Components;
using MauiReactorTemplate.StartupXaml.Resources.Styles;

namespace MauiReactorTemplate.StartupXaml;

public partial class App : MauiReactorApplication
{
    public App(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        InitializeComponent();
    }
}


public abstract class MauiReactorApplication : ReactorApplication<HomePage>
{
    public MauiReactorApplication(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        this.UseTheme<ApplicationTheme>();
    }
}