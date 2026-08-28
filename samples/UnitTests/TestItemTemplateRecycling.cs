using System.Linq;
using MauiReactor;
using Shouldly;

namespace UnitTests;

class CellTallyState
{
    public int Counter { get; set; }
}

class CellTally : Component<CellTallyState>
{
    public string Title { get; set; } = "";

    public override VisualNode Render()
        => Button($"{Title}:{State.Counter}")
            .OnClicked(() => SetState(s => s.Counter++));
}

class ItemTemplatePage : Component
{
    public override VisualNode Render()
        => ContentPage(
            CollectionView()
                .AutomationId("cv")
                .ItemsSource(new[] { "a", "b" }, item => new CellTally { Title = item })
        );
}

public class TestItemTemplateRecycling
{
    private static MauiControls.Button ButtonIn(MauiControls.ContentView cell)
        => (MauiControls.Button)cell.Content;

    [Test]
    public void RecyclingACellOntoAnotherItem_DoesNotCarryStateOver()
    {
        var host = TemplateHost.Create(new ItemTemplatePage());
        var collectionView = host.Find<MauiControls.CollectionView>("cv");

        var cell = (MauiControls.ContentView)collectionView.ItemTemplate.CreateContent();

        cell.BindingContext = "a";
        ButtonIn(cell).Text.ShouldBe("a:0");

        ButtonIn(cell).SendClicked();
        ButtonIn(cell).SendClicked();
        ButtonIn(cell).Text.ShouldBe("a:2");

        // What a CollectionView does when it scrolls: the same cell is bound to a
        // different item. The previous item's state must not come with it.
        cell.BindingContext = "b";

        ButtonIn(cell).Text.ShouldBe("b:0");
    }

    [Test]
    public void RebindingTheSameItem_KeepsItsState()
    {
        var host = TemplateHost.Create(new ItemTemplatePage());
        var collectionView = host.Find<MauiControls.CollectionView>("cv");

        var cell = (MauiControls.ContentView)collectionView.ItemTemplate.CreateContent();

        cell.BindingContext = "a";
        ButtonIn(cell).SendClicked();
        ButtonIn(cell).Text.ShouldBe("a:1");

        // Same item again - this is a re-render, not a recycle, so merging is correct
        // and the state belongs to this item.
        cell.BindingContext = "a";

        ButtonIn(cell).Text.ShouldBe("a:1");
    }

    [Test]
    public void RecyclingACellOntoAnotherItem_ResetsHookCells()
    {
        // The reason this matters for hooks: a cell table is positional, so a carried-over
        // subtree hands the previous item's UseState values to the new item wholesale.
        var host = TemplateHost.Create(new HookItemTemplatePage());
        var collectionView = host.Find<MauiControls.CollectionView>("cv");

        var cell = (MauiControls.ContentView)collectionView.ItemTemplate.CreateContent();

        cell.BindingContext = "a";
        ButtonIn(cell).SendClicked();
        ButtonIn(cell).SendClicked();
        ButtonIn(cell).SendClicked();
        ButtonIn(cell).Text.ShouldBe("a:3");

        cell.BindingContext = "b";

        ButtonIn(cell).Text.ShouldBe("b:0");
    }
}

class HookCellTally : HookComponent
{
    public string Title { get; set; } = "";

    public override VisualNode Render()
    {
        var (count, setCount) = UseState(0);

        return Button($"{Title}:{count}")
            .OnClicked(() => setCount(count + 1));
    }
}

class HookItemTemplatePage : Component
{
    public override VisualNode Render()
        => ContentPage(
            CollectionView()
                .AutomationId("cv")
                .ItemsSource(new[] { "a", "b" }, item => new HookCellTally { Title = item })
        );
}
