using System.Linq;
using System.Threading.Tasks;
using MauiReactor;
using MauiReactor.Internals;
using MauiReactor.TestApp.Pages;
using MauiReactor.TestApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace UnitTests;

public class TestBug305Isolation
{
    // Issue #305 https://github.com/adospace/reactorui-maui/issues/305
    [Test]
    public async Task RunRenderTestsInParallel_ExpectTestsToRunToCompletion()
    {
        var tasks = Enumerable.Range(0, 1000).Select(_ => Task.Run(CounterWithServicePage_Clicking_Button_Correctly_Increments_The_Counter)).ToList();

        await Task.WhenAll(tasks);

        tasks.ShouldAllBe(t => t.IsCompletedSuccessfully);
    }


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
}