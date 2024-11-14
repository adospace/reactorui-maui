using MauiReactor.TestApp.Resources.Styles;

namespace MauiReactor.TestApp;

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
        this.UseTheme<AppTheme>();
    }
}