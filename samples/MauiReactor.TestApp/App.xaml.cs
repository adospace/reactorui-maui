using MauiReactor.TestApp.Pages;
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


public abstract class MauiReactorApplication : ReactorApplication<TestBug335_2Page>
{
    public MauiReactorApplication(IServiceProvider serviceProvider)
        :base(serviceProvider)
    {
        this.UseTheme<AppTheme>();
    }
}