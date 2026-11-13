using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using MauiReactor;
using Shouldly;

namespace UnitTests;

class LeakProbeChild : HookComponent
{
    public static readonly List<WeakReference> Instances = [];

    public static void Reset()
    {
        lock (Instances)
        {
            Instances.Clear();
        }
    }

    public static int AliveCount
    {
        get
        {
            lock (Instances)
            {
                return Instances.Count(r => r.IsAlive);
            }
        }
    }

    public static HookSlots? LastSlots;

    public int Seed { get; set; }

    public LeakProbeChild()
    {
        lock (Instances)
        {
            Instances.Add(new WeakReference(this));
        }
    }

    public override VisualNode Render()
    {
        var (count, setCount) = UseState(0);

        // An effect that allocates a cleanup closure every time its dependency moves.
        UseEffect(() =>
        {
            var payload = new byte[256];
            return () => GC.KeepAlive(payload);
        }, Seed);

        var doubled = UseMemo(() => count * 2, count);

        LastSlots = State;

        return VStack(
            Button($"{Seed}:{count}:{doubled}")
                .AutomationId("child")
                .OnClicked(() => setCount(count + 1))
        );
    }
}

class LeakProbeParentState
{
    public int Revision { get; set; }
}

class LeakProbeParent : Component<LeakProbeParentState>
{
    public override VisualNode Render()
        => ContentPage(
            VStack(
                Button("bump")
                    .AutomationId("bump")
                    .OnClicked(() => SetState(s => s.Revision++)),

                new LeakProbeChild { Seed = State.Revision }
            )
        );
}

public class TestHooksMemory
{
    private static void Collect()
    {
        for (var i = 0; i < 3; i++)
        {
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true);
            GC.WaitForPendingFinalizers();
        }
    }

    [Test]
    public void ManyGenerations_DoNotAccumulateComponentInstances()
    {
        LeakProbeChild.Reset();

        var host = TemplateHost.Create(new LeakProbeParent());

        // Each bump re-renders the parent, which constructs a fresh child component whose
        // hook table is handed forward. If a generation is retained - by the _newComponent
        // chain, by a setter closure captured in a native handler, or by a pending effect -
        // these pile up.
        for (var i = 0; i < 300; i++)
        {
            host.Find<MauiControls.Button>("bump").SendClicked();
        }

        Collect();

        var alive = LeakProbeChild.AliveCount;

        TestContext.Out.WriteLine($"child generations created: 301, still alive after GC: {alive}");

        alive.ShouldBeLessThan(10);
    }

    [Test]
    public void HookTableIsTransferred_NotRebuilt_AndDoesNotGrow()
    {
        LeakProbeChild.Reset();

        var host = TemplateHost.Create(new LeakProbeParent());

        var firstSlots = LeakProbeChild.LastSlots;
        firstSlots.ShouldNotBeNull();

        var cellsAfterFirstRender = firstSlots.Cells.Count;

        for (var i = 0; i < 200; i++)
        {
            host.Find<MauiControls.Button>("bump").SendClicked();
        }

        // Same table object all the way through - it rides the state-transfer channel
        // rather than being rebuilt per generation.
        ReferenceEquals(firstSlots, LeakProbeChild.LastSlots).ShouldBeTrue();

        // Three hooks per render, forever. A cursor that failed to reset would grow this.
        LeakProbeChild.LastSlots!.Cells.Count.ShouldBe(cellsAfterFirstRender);
        cellsAfterFirstRender.ShouldBe(3);
    }

    [Test]
    public void StateChangesOnTheChild_DoNotAccumulateEither()
    {
        LeakProbeChild.Reset();

        var host = TemplateHost.Create(new LeakProbeParent());

        // The child re-renders itself here rather than being reconstructed by the parent,
        // which exercises the setter-closure path instead of the generation chain.
        for (var i = 0; i < 300; i++)
        {
            host.Find<MauiControls.Button>("child").SendClicked();
        }

        Collect();

        LeakProbeChild.AliveCount.ShouldBeLessThan(10);
        LeakProbeChild.LastSlots!.Cells.Count.ShouldBe(3);
    }

    // The tree has to be built and torn down in a separate non-inlined frame: in a Debug
    // build locals stay rooted for the whole enclosing method, which would keep the host
    // alive and make this pass or fail for the wrong reason.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static WeakReference BuildThenTearDown()
    {
        var host = TemplateHost.Create(new LeakProbeParent());

        for (var i = 0; i < 20; i++)
        {
            host.Find<MauiControls.Button>("bump").SendClicked();
        }

        var slotsRef = new WeakReference(LeakProbeChild.LastSlots);
        LeakProbeChild.LastSlots = null;

        ((TemplateHost)host).Stop();

        return slotsRef;
    }

    [Test]
    public void UnmountingReleasesTheHookTableAndItsClosures()
    {
        LeakProbeChild.Reset();

        var slotsRef = BuildThenTearDown();

        Collect();

        // Once the tree is torn down nothing should still be holding the cell table.
        slotsRef.IsAlive.ShouldBeFalse();
    }

    [Test]
    public void AFlushThatLandsAfterUnmount_DoesNotRunEffectBodies()
    {
        // OnAfterRender dispatches the flush, so it can arrive after the component was
        // torn down. Running the body then would create a resource whose cleanup has
        // already been and gone.
        var bodyRan = false;
        var cleanupRan = false;

        var slots = new HookSlots();
        slots.Cells.Add(new EffectCell
        {
            Pending = () =>
            {
                bodyRan = true;
                return () => cleanupRan = true;
            }
        });

        slots.RunCleanups();   // unmount
        slots.FlushEffects();  // the dispatch that was already in flight

        bodyRan.ShouldBeFalse();
        cleanupRan.ShouldBeFalse();
    }

    [Test]
    public void AFlushBeforeUnmount_StillRunsAndItsCleanupIsCollected()
    {
        var bodyRan = false;
        var cleanupRan = false;

        var slots = new HookSlots();
        slots.Cells.Add(new EffectCell
        {
            Pending = () =>
            {
                bodyRan = true;
                return () => cleanupRan = true;
            }
        });

        slots.FlushEffects();
        bodyRan.ShouldBeTrue();

        slots.RunCleanups();
        cleanupRan.ShouldBeTrue();
    }
}
