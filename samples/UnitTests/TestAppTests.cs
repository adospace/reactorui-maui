using MauiReactor;
using MauiReactor.Canvas.Internals;
using MauiReactor.Internals;
using MauiReactor.TestApp.Pages;
using MauiReactor.TestApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace UnitTests;

public class TestAppTests
{
    [Test]
    public void CounterWithServicePage_Clicking_Button_Correctly_Increments_The_Counter()
    {
        using var serviceContext = new ServiceContext(services => services.AddSingleton<IncrementService>());
        
        var mainPageNode = new TemplateHost<MauiControls.ContentPage>(new CounterWithServicePage());

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

    [Test]
    public void CanvasPage_Moving_Hover_Norway_Image_The_Label_Text_Should_Change_Accordingly()
    {
        var mainPageNode = new TemplateHost<MauiControls.ContentPage>(new CanvasPage());

        mainPageNode.ShouldNotBeNull();

        mainPageNode.NativeElement.ShouldNotBeNull();

        mainPageNode.NativeElement.FindByAutomationId<Text>("NorwayLabel")
            .ShouldNotBeNull()
            .Value
            .ShouldBe("Awesome Norway!");

        mainPageNode.NativeElement.FindByAutomationId<PointInteractionHandler>("NorwayImage")
            .ShouldNotBeNull()
            .MoveHover(new[] { new Microsoft.Maui.Graphics.PointF(10, 10) });

        mainPageNode.NativeElement.FindByAutomationId<Text>("NorwayLabel")
            .ShouldNotBeNull()
            .Value
            .ShouldBe("Mouse hovering");
    }
}