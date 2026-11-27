using System.Linq;
using MauiReactor;
using Microsoft.Maui.Graphics;
using Shouldly;

namespace UnitTests;

class VisualStateCachingPageState
{
    public bool IsEnabled { get; set; } = true;
    public bool Highlight { get; set; }
    public bool ExtraState { get; set; }
    public int Counter { get; set; }
}

class VisualStateCachingPage : Component<VisualStateCachingPageState>
{
    public override VisualNode Render()
    {
        var button = Button($"Counter {State.Counter}")
            .AutomationId("button")
            .IsEnabled(State.IsEnabled)
            .VisualState("CommonStates", "Normal", MauiControls.VisualElement.BackgroundColorProperty, State.Highlight ? Colors.Red : Colors.Blue)
            .VisualState("CommonStates", "Disabled", MauiControls.VisualElement.BackgroundColorProperty, Colors.Gray);

        if (State.ExtraState)
        {
            button = button.VisualState("CommonStates", "PointerOver", MauiControls.VisualElement.BackgroundColorProperty, Colors.Green);
        }

        return ContentPage(
            VStack(
                button,
                Button("increment").AutomationId("increment").OnClicked(() => SetState(s => s.Counter++)),
                Button("toggle-enabled").AutomationId("toggle-enabled").OnClicked(() => SetState(s => s.IsEnabled = !s.IsEnabled)),
                Button("toggle-highlight").AutomationId("toggle-highlight").OnClicked(() => SetState(s => s.Highlight = !s.Highlight)),
                Button("add-state").AutomationId("add-state").OnClicked(() => SetState(s => s.ExtraState = true))
            )
        );
    }
}

public class TestVisualStateCaching
{
    private static MauiControls.Button Target(ITemplateHost host) => host.Find<MauiControls.Button>("button");

    private static void Click(ITemplateHost host, string automationId) => host.Find<MauiControls.Button>(automationId).SendClicked();

    [Test]
    public void NormalState_IsAppliedOnFirstRender()
    {
        var host = TemplateHost.Create(new VisualStateCachingPage());

        Target(host).BackgroundColor.ShouldBe(Colors.Blue);
    }

    [Test]
    public void DisabledState_IsAppliedWhenIsEnabledToggles()
    {
        var host = TemplateHost.Create(new VisualStateCachingPage());

        Click(host, "toggle-enabled");
        Target(host).BackgroundColor.ShouldBe(Colors.Gray);

        Click(host, "toggle-enabled");
        Target(host).BackgroundColor.ShouldBe(Colors.Blue);
    }

    [Test]
    public void ChangedValueOfCurrentState_IsAppliedOnRerender()
    {
        var host = TemplateHost.Create(new VisualStateCachingPage());

        Click(host, "toggle-highlight");
        Target(host).BackgroundColor.ShouldBe(Colors.Red);

        Click(host, "toggle-highlight");
        Target(host).BackgroundColor.ShouldBe(Colors.Blue);
    }

    [Test]
    public void ChangedValue_IsUsedWhenStateIsEnteredLater()
    {
        var host = TemplateHost.Create(new VisualStateCachingPage());

        Click(host, "toggle-enabled");      // -> Disabled
        Click(host, "toggle-highlight");    // Normal now Red, while not in Normal
        Target(host).BackgroundColor.ShouldBe(Colors.Gray);

        Click(host, "toggle-enabled");      // -> Normal, must pick the new value
        Target(host).BackgroundColor.ShouldBe(Colors.Red);
    }

    [Test]
    public void AddingAState_KeepsStatesWorking()
    {
        var host = TemplateHost.Create(new VisualStateCachingPage());

        Click(host, "add-state");
        Target(host).BackgroundColor.ShouldBe(Colors.Blue);

        var groups = MauiControls.VisualStateManager.GetVisualStateGroups(Target(host));
        groups.ShouldHaveSingleItem().States.Select(s => s.Name).ShouldBe(["Normal", "Disabled", "PointerOver"], ignoreOrder: true);

        Click(host, "toggle-enabled");
        Target(host).BackgroundColor.ShouldBe(Colors.Gray);
    }

    [Test]
    public void UnchangedVisualStates_AreNotRebuiltOnRerender()
    {
        var host = TemplateHost.Create(new VisualStateCachingPage());

        var before = MauiControls.VisualStateManager.GetVisualStateGroups(Target(host));

        Click(host, "increment");
        Target(host).Text.ShouldBe("Counter 1");

        MauiControls.VisualStateManager.GetVisualStateGroups(Target(host)).ShouldBeSameAs(before);
        Target(host).BackgroundColor.ShouldBe(Colors.Blue);
    }

    [Test]
    public void ChangedValues_AreUpdatedInPlace()
    {
        var host = TemplateHost.Create(new VisualStateCachingPage());

        var before = MauiControls.VisualStateManager.GetVisualStateGroups(Target(host));

        Click(host, "toggle-highlight");

        MauiControls.VisualStateManager.GetVisualStateGroups(Target(host)).ShouldBeSameAs(before);
        Target(host).BackgroundColor.ShouldBe(Colors.Red);
    }
}
