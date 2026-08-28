using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MauiReactor;
using Shouldly;

namespace UnitTests;

class CounterItemState
{
    public int Counter { get; set; }
}

class CounterItem : Component<CounterItemState>
{
    public string Title { get; set; } = "";

    public override VisualNode Render()
        => VStack(
            Label($"{Title}:{State.Counter}")
                .AutomationId($"lbl_{Title}"),

            Button("+")
                .AutomationId($"btn_{Title}")
                .OnClicked(() => SetState(s => s.Counter++))
        );
}

class KeyedListPageState
{
    public List<string> Items { get; set; } = ["a", "b", "c"];
}

class KeyedListPage : Component<KeyedListPageState>
{
    public bool UseKeys { get; set; }

    public override VisualNode Render()
        => ContentPage(
            VStack(
                Button("insert")
                    .AutomationId("insert")
                    .OnClicked(() => SetState(s => s.Items.Insert(0, "z"))),

                Button("swap")
                    .AutomationId("swap")
                    .OnClicked(() => SetState(s => (s.Items[0], s.Items[2]) = (s.Items[2], s.Items[0]))),

                Button("remove")
                    .AutomationId("remove")
                    .OnClicked(() => SetState(s => s.Items.Remove("a"))),

                VStack(
                    State.Items.Select(RenderItem).ToArray()
                )
            )
        );

    VisualNode RenderItem(string id)
    {
        var item = new CounterItem { Title = id };
        return UseKeys ? item.WithKey(id) : item;
    }
}

public class TestKeyedReconciliation
{
    private static string Text(ITemplateHost host, string id)
        => host.Find<MauiControls.Label>(id).Text;

    private static void Click(ITemplateHost host, string id)
        => host.Find<MauiControls.Button>(id).SendClicked();

    [Test]
    public void WithoutKeys_InsertingAtTop_ShiftsStateOntoTheWrongItem()
    {
        // Documents the pre-existing behaviour: state follows position, not identity.
        var host = TemplateHost.Create(new KeyedListPage { UseKeys = false });

        Click(host, "btn_b");
        Click(host, "btn_b");
        Text(host, "lbl_b").ShouldBe("b:2");

        Click(host, "insert");

        // 'a' inherits what belonged to 'b'.
        Text(host, "lbl_a").ShouldBe("a:2");
        Text(host, "lbl_b").ShouldBe("b:0");
    }

    [Test]
    public void WithKeys_InsertingAtTop_KeepsStateWithItsItem()
    {
        var host = TemplateHost.Create(new KeyedListPage { UseKeys = true });

        Click(host, "btn_b");
        Click(host, "btn_b");
        Text(host, "lbl_b").ShouldBe("b:2");

        Click(host, "insert");

        Text(host, "lbl_b").ShouldBe("b:2");
        Text(host, "lbl_a").ShouldBe("a:0");
        Text(host, "lbl_c").ShouldBe("c:0");
        Text(host, "lbl_z").ShouldBe("z:0");
    }

    [Test]
    public void WithKeys_ReorderingItems_KeepsStateWithItsItem()
    {
        var host = TemplateHost.Create(new KeyedListPage { UseKeys = true });

        Click(host, "btn_a");
        Click(host, "btn_c");
        Click(host, "btn_c");

        Click(host, "swap");   // a,b,c -> c,b,a

        Text(host, "lbl_a").ShouldBe("a:1");
        Text(host, "lbl_c").ShouldBe("c:2");
        Text(host, "lbl_b").ShouldBe("b:0");
    }

    [Test]
    public void WithKeys_RemovingAnItem_LeavesTheOthersIntact()
    {
        var host = TemplateHost.Create(new KeyedListPage { UseKeys = true });

        Click(host, "btn_b");
        Click(host, "btn_c");
        Click(host, "btn_c");

        Click(host, "remove");   // drop 'a'

        Text(host, "lbl_b").ShouldBe("b:1");
        Text(host, "lbl_c").ShouldBe("c:2");
    }

    [Test]
    public void UnkeyedTrees_AreUnaffected()
    {
        // The ordinal path must stay byte-for-byte equivalent for trees with no keys.
        var host = TemplateHost.Create(new KeyedListPage { UseKeys = false });

        Click(host, "btn_a");
        Text(host, "lbl_a").ShouldBe("a:1");
        Text(host, "lbl_b").ShouldBe("b:0");
        Text(host, "lbl_c").ShouldBe("c:0");
    }

    [Test]
    public void MergeThroughput_KeyedPathCostIsBounded()
    {
        const int items = 200;
        const int iterations = 300;

        // Two rounds, alternating, reporting the second of each: the first pass through
        // either path absorbs JIT and pool-warmup cost and would otherwise be attributed
        // to whichever path happened to run first.
        MeasureMerges(useKeys: false, items, iterations);
        MeasureMerges(useKeys: true, items, iterations);

        var unkeyed = MeasureMerges(useKeys: false, items, iterations);
        var keyed = MeasureMerges(useKeys: true, items, iterations);

        TestContext.Out.WriteLine($"unkeyed: {unkeyed.TotalMilliseconds:F1} ms / {iterations} merges of {items} children");
        TestContext.Out.WriteLine($"keyed:   {keyed.TotalMilliseconds:F1} ms / {iterations} merges of {items} children");
        TestContext.Out.WriteLine($"ratio:   {keyed.TotalMilliseconds / unkeyed.TotalMilliseconds:F2}x");

        // Generous bound - this guards against an accidental O(n^2) or a per-merge
        // allocation storm, not against normal variance on a shared CI box.
        (keyed.TotalMilliseconds / unkeyed.TotalMilliseconds).ShouldBeLessThan(4.0);
    }

    private static TimeSpan MeasureMerges(bool useKeys, int items, int iterations)
    {
        var page = new BenchPage { UseKeys = useKeys, Count = items };
        var host = TemplateHost.Create(page);

        // warm up
        for (var i = 0; i < 20; i++)
        {
            Click(host, "bump");
        }

        var sw = Stopwatch.StartNew();

        for (var i = 0; i < iterations; i++)
        {
            Click(host, "bump");
        }

        sw.Stop();
        return sw.Elapsed;
    }
}

class BenchPageState
{
    public int Revision { get; set; }
}

class BenchPage : Component<BenchPageState>
{
    public bool UseKeys { get; set; }

    public int Count { get; set; }

    public override VisualNode Render()
        => ContentPage(
            VStack(
                Button("bump")
                    .AutomationId("bump")
                    .OnClicked(() => SetState(s => s.Revision++)),

                VStack(
                    Enumerable.Range(0, Count).Select(RenderRow).ToArray()
                )
            )
        );

    VisualNode RenderRow(int index)
    {
        var row = Label($"row {index} r{State.Revision}");
        return UseKeys ? row.WithKey(index) : row;
    }
}
