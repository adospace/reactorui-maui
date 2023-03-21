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
        
        var mainPageNode = TemplateHost.Create(new CounterWithServicePage());

        // Check that the counter is 0
        mainPageNode.Find<MauiControls.Label>("Counter_Label")
            .Text
            .ShouldBe($"Counter: 0");

        // Click on the button
        mainPageNode.Find<MauiControls.Button>("Counter_Button")
            .SendClicked();

        // Check that the counter is 1
        mainPageNode.Find<MauiControls.Label>("Counter_Label")
            .Text
            .ShouldBe($"Counter: 1");
    }

    [Test]
    public void CanvasPage_Moving_Hover_Norway_Image_The_Label_Text_Should_Change_Accordingly()
    {
        var mainPageNode = TemplateHost.Create(new CanvasPage());

        // Check that the label is "Awesome Norway!"
        mainPageNode.Find<Text>("NorwayLabel")
            .Value
            .ShouldBe("Awesome Norway!");

        // Move hover on the image
        mainPageNode.Find<PointInteractionHandler>("NorwayImage")
            .MoveHover(new[] { new Microsoft.Maui.Graphics.PointF(10, 10) });

        // Check that the label is "Mouse hovering"
        mainPageNode.Find<Text>("NorwayLabel")
            .Value
            .ShouldBe("Mouse hovering");
    }

    [Test]
    public void NavigationTestPageShouldNavigateToChildPageAndBack()
    {
        using var navigationContainer = new NavigationContainer();

        var mainPageNode = TemplateHost.Create(new NavigationMainPage());

        // Verify that initially the value is 0
        mainPageNode.Find<MauiControls.Label>("MainPage_Label")
            .Text
            .ShouldBe("Value: 0");

        // Click the button to open the second page
        mainPageNode.Find<MauiControls.Button>("MoveToChildPage_Button")
            .SendClicked();

        var childPageNode = navigationContainer.AttachTopPage();

        // se entry text to 12
        childPageNode.Find<MauiControls.Entry>("ChildPage_Entry")
            .Text = "12";

        // click the button to go back to main page
        childPageNode.Find<MauiControls.Button>("MoveToMainPage_Button")
            .SendClicked();

        // Verify that now the label reports the updated text
        mainPageNode.Find<MauiControls.Label>("MainPage_Label")
            .Text
            .ShouldBe("Value: 12");
    }

}