using MauiReactor;
using MauiReactor.Internals;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.TestApp.Pages;
using System.Reflection;

namespace UnitTests;

internal class TestBug220Page
{
    [Test]
    public async Task VerifyLabelOnModelPage()
    {
        var services = new ServiceCollection();

        var provider = services.BuildServiceProvider();
        using var serviceContext = new ServiceContext(provider);

        var mainPage = TemplateHost.Create(new TestBug220ShellPage());
        var shell = mainPage.Find<MauiControls.Shell>("MainShell");
        var models = shell.Find<MauiControls.ShellContent>("Models");
        var modelsPage = mainPage.Find<MauiControls.ContentPage>("Models_page");

        shell.CurrentItem = models;

        // first way
        var name = await mainPage.Find<MauiControls.Label>("m1-name", TimeSpan.FromSeconds(30));
        
        name.ShouldNotBeNull();
        name.Text.ShouldBe("model name 1");

        // get and click on item in collection view
        var stack = await mainPage.Find<MauiControls.VerticalStackLayout>("m1-stack", TimeSpan.FromSeconds(30));

        stack.ShouldNotBeNull();

        // how to tap the stack??
        var tapGestureRecognizer = stack.GestureRecognizers.OfType<MauiControls.TapGestureRecognizer>().Single();

        tapGestureRecognizer
            .GetType()
            .GetMethod("SendTapped", BindingFlags.Instance | BindingFlags.NonPublic)
            .ShouldNotBeNull().Invoke(tapGestureRecognizer, new[] { stack, null });

        // get ModelPage and get the label
        var label = await mainPage.Find<MauiControls.Label>("m2", TimeSpan.FromSeconds(30));

        label.ShouldNotBeNull();

        // test the label of ModelPage
        label.Text.ShouldBe("model name 2");
    }
}
