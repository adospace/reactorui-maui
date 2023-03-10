using MauiReactor;
using MauiReactor.TestApp.Pages;
using MauiReactor.TestApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace UnitTests;

public class TestAppTests
{
    [Test]
    public void Clicking_Button_Correctly_Increments_The_Counter()
    {
        var mainPageNode = new TemplateHost<MauiControls.ContentPage>(
            new CounterWithServicePage(), services => services.AddSingleton<IncrementService>());

        mainPageNode.ShouldNotBeNull();

        mainPageNode.NativeElement.ShouldNotBeNull();

        mainPageNode.NativeElement.FindByAutomationId<MauiControls.Button>("Counter_Button")
            .ShouldNotBeNull()
            .Text
            .ShouldBe($"Click To Increment");

        mainPageNode.NativeElement.FindByAutomationId<MauiControls.Button>("Counter_Button")
            .ShouldNotBeNull()
            .SendClicked();

        mainPageNode.NativeElement.FindByAutomationId<MauiControls.Label>("Counter_Label")
            .ShouldNotBeNull()
            .Text
            .ShouldBe($"Counter: 1");
    }
}