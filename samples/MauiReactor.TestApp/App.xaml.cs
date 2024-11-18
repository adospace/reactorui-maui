using MauiReactor.TestApp.Resources.Styles;
using System;

namespace MauiReactor.TestApp;

public partial class App : MauiReactorApplication
{
    public App(IServiceProvider serviceProvider)
        :base(serviceProvider)
    {
        InitializeComponent();
    }
}


public abstract class MauiReactorApplication : ReactorApplication<HomePage>
{
    public MauiReactorApplication(IServiceProvider serviceProvider)
        :base(serviceProvider)
    {
        this.UseTheme<AppTheme>();
    }
}