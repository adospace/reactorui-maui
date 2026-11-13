using System.Collections.Generic;
using System.Linq;
using MauiReactor;
using Shouldly;

namespace UnitTests;

class OrderPageState
{
    public List<string> Items { get; set; } = ["a", "b", "c"];
}

class OrderPage : Component<OrderPageState>
{
    public bool UseKeys { get; set; }

    public override VisualNode Render()
        => ContentPage(
            VStack(
                Button("swap")
                    .AutomationId("swap")
                    .OnClicked(() => SetState(s => (s.Items[0], s.Items[2]) = (s.Items[2], s.Items[0]))),

                VStack(
                    State.Items.Select(RenderItem).ToArray()
                )
                .AutomationId("container")
            )
        );

    VisualNode RenderItem(string id)
    {
        var label = Label(id).AutomationId($"lbl_{id}");
        return UseKeys ? label.WithKey(id) : label;
    }
}

class TallyState
{
    public int Counter { get; set; }
}

class Tally : Component<TallyState>
{
    public string Title { get; set; } = "";

    public override VisualNode Render()
        => Button($"{Title}:{State.Counter}")
            .AutomationId($"tally_{Title}")
            .OnClicked(() => SetState(s => s.Counter++));
}

class StatefulOrderPageState
{
    public List<string> Items { get; set; } = ["a", "b", "c"];
}

class StatefulOrderPage : Component<StatefulOrderPageState>
{
    public override VisualNode Render()
        => ContentPage(
            VStack(
                Button("swap")
                    .AutomationId("swap")
                    .OnClicked(() => SetState(s => (s.Items[0], s.Items[2]) = (s.Items[2], s.Items[0]))),

                VStack(
                    State.Items.Select(id => new Tally { Title = id }.WithKey(id)).ToArray()
                )
                .AutomationId("container")
            )
        );
}

public class TestKeyedNativeOrder
{
    private static string[] LabelOrder(ITemplateHost host)
        => host.Find<MauiControls.VerticalStackLayout>("container")
            .Children
            .OfType<MauiControls.Label>()
            .Select(l => l.Text)
            .ToArray();

    private static string[] ButtonOrder(ITemplateHost host)
        => host.Find<MauiControls.VerticalStackLayout>("container")
            .Children
            .OfType<MauiControls.Button>()
            .Select(b => b.Text)
            .ToArray();

    [Test]
    public void Unkeyed_Reorder_ProducesCorrectNativeOrder()
    {
        var host = TemplateHost.Create(new OrderPage { UseKeys = false });

        LabelOrder(host).ShouldBe(["a", "b", "c"]);

        host.Find<MauiControls.Button>("swap").SendClicked();

        LabelOrder(host).ShouldBe(["c", "b", "a"]);
    }

    [Test]
    public void Keyed_Reorder_ProducesCorrectNativeOrder()
    {
        var host = TemplateHost.Create(new OrderPage { UseKeys = true });

        LabelOrder(host).ShouldBe(["a", "b", "c"]);

        host.Find<MauiControls.Button>("swap").SendClicked();

        LabelOrder(host).ShouldBe(["c", "b", "a"]);
    }

    [Test]
    public void Keyed_Reorder_MovesStateAndPixelsTogether()
    {
        // The case that matters: stateful components, reordered. State must follow the
        // key, and the native controls must follow the state.
        var host = TemplateHost.Create(new StatefulOrderPage());

        host.Find<MauiControls.Button>("tally_a").SendClicked();
        host.Find<MauiControls.Button>("tally_c").SendClicked();
        host.Find<MauiControls.Button>("tally_c").SendClicked();

        ButtonOrder(host).ShouldBe(["a:1", "b:0", "c:2"]);

        host.Find<MauiControls.Button>("swap").SendClicked();

        ButtonOrder(host).ShouldBe(["c:2", "b:0", "a:1"]);
    }
}
