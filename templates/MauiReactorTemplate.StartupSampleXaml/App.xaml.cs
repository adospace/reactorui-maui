using MauiReactor;
using MauiReactorTemplate.StartupSampleXaml.Components;
using MauiReactorTemplate.StartupSampleXaml.Resources.Styles;
using System;

namespace MauiReactorTemplate.StartupSampleXaml;

public partial class App : MauiReactorApplication
{
    public App(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        InitializeComponent();
    }
}


public abstract class MauiReactorApplication : ReactorApplication<AppShell>
{
    public MauiReactorApplication(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        this.UseTheme<ApplicationTheme>();
    }
}