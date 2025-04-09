using System.Linq;
using System.Threading.Tasks;
using MauiReactor;
using MauiReactor.Parameters;
using MauiReactor.TestApp.Pages;
using Shouldly;

namespace UnitTests;

public class TestBug314ParameterIsolation
{
    // Issue #314 https://github.com/adospace/reactorui-maui/issues/314
    [Test]
    public async Task RunRenderTestsInParallel_ExpectTestsToRunToCompletion()
    {
        var tasks = Enumerable.Range(0, 1000).Select(_ => Task.Run(CounterWithParametersPage_Clicking_Button_Correctly_Increments_The_Counter)).ToList();

        await Task.WhenAll(tasks);

        tasks.ShouldAllBe(t => t.IsCompletedSuccessfully);
    }


    [Test]
    public void CounterWithParametersPage_Clicking_Button_Correctly_Increments_The_Counter()
    {        
        using var parameterContext = new ParameterContext();

        var mainPageNode = TemplateHost.Create(new ParametersPage());

        // Check that the counter is 0
        mainPageNode.Find<MauiControls.Label>("Increment_Label")
            .Text
            .ShouldBe("0");

        // Click on the button
        mainPageNode.Find<MauiControls.Button>("Increment_Button")
            .SendClicked();

        // Check that the counter is 1
        mainPageNode.Find<MauiControls.Label>("Increment_Label")
            .Text
            .ShouldBe("1");
    }
}
