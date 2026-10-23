using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MauiReactor;
using Shouldly;

namespace UnitTests;

// Issue #375 https://github.com/adospace/reactorui-maui/issues/375
// When a parent renders again, a child component is migrated to a new instance: neither
// OnWillUnmount nor OnMounted runs, State and Props move to the new instance, private fields
// do not. These tests cover the patterns the lifecycle docs recommend for that:
// - events subscribed in OnMountedOrPropsChanged, unsubscribed in OnMigrating and OnWillUnmount;
// - resources created in OnMounted kept in State, released in OnWillUnmount.
public class TestBug375MigrationUnmount
{
    private class EventSource
    {
        public event EventHandler? Changed;

        public int SubscriberCount => Changed?.GetInvocationList().Length ?? 0;

        public List<Resource> Resources { get; } = [];

        public List<WeakReference> Instances { get; } = [];

        public void Raise() => Changed?.Invoke(this, EventArgs.Empty);

        public Resource Acquire()
        {
            var resource = new Resource();
            Resources.Add(resource);
            return resource;
        }
    }

    private class Resource : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose() => IsDisposed = true;
    }

    private class ChildState
    {
        public int Count { get; set; }

        public Resource? Resource { get; set; }
    }

    private class Child(EventSource source) : Component<ChildState>
    {
        protected override void OnMounted()
        {
            State.Resource = source.Acquire();
            base.OnMounted();
        }

        protected override void OnMountedOrPropsChanged()
        {
            source.Instances.Add(new WeakReference(this));
            source.Changed += HandleChanged;
            base.OnMountedOrPropsChanged();
        }

        protected override void OnMigrating(VisualNode newNode)
        {
            source.Changed -= HandleChanged;
            base.OnMigrating(newNode);
        }

        protected override void OnWillUnmount()
        {
            source.Changed -= HandleChanged;
            State.Resource?.Dispose();
            base.OnWillUnmount();
        }

        private void HandleChanged(object? sender, EventArgs e) => SetState(s => s.Count++);

        public override VisualNode Render()
            => Label($"Count: {State.Count}").AutomationId("Child_Label");
    }

    private class ParentState
    {
        public int Renders { get; set; }

        public bool ShowChild { get; set; } = true;
    }

    private class Parent(EventSource source) : Component<ParentState>
    {
        public override VisualNode Render()
            => ContentPage(
                VStack(
                    Button("Render again").AutomationId("Rerender_Button").OnClicked(() => SetState(s => s.Renders++)),
                    Button("Remove child").AutomationId("Remove_Button").OnClicked(() => SetState(s => s.ShowChild = false)),
                    Label($"Renders: {State.Renders}").AutomationId("Renders_Label"),
                    State.ShowChild ? new Child(source) : null
                )
            );
    }

    private static ITemplateHost CreateHost(EventSource source, int rerenders)
    {
        var host = TemplateHost.Create(new Parent(source));

        for (int i = 0; i < rerenders; i++)
        {
            host.Find<MauiControls.Button>("Rerender_Button").SendClicked();
        }

        host.Find<MauiControls.Label>("Renders_Label").Text.ShouldBe($"Renders: {rerenders}");

        return host;
    }

    private static void RemoveChild(ITemplateHost host)
    {
        host.Find<MauiControls.Button>("Remove_Button").SendClicked();
        host.FindOptional<MauiControls.Label>("Child_Label").ShouldBeNull();
    }

    [TestCase(0)]
    [TestCase(2)]
    public void Handler_Is_Attached_Once_And_Removed_With_The_Child(int rerenders)
    {
        var source = new EventSource();
        var host = CreateHost(source, rerenders);

        source.SubscriberCount.ShouldBe(1);

        RemoveChild(host);

        source.SubscriberCount.ShouldBe(0);
    }

    [Test]
    public void Handler_Updates_The_Migrated_Child()
    {
        var source = new EventSource();
        var host = CreateHost(source, rerenders: 2);

        source.Raise();

        host.Find<MauiControls.Label>("Child_Label").Text.ShouldBe("Count: 1");
    }

    [TestCase(0)]
    [TestCase(2)]
    public void Resource_Kept_In_State_Is_Disposed_With_The_Child(int rerenders)
    {
        var source = new EventSource();
        var host = CreateHost(source, rerenders);

        RemoveChild(host);

        source.Resources.Count.ShouldBe(1);
        source.Resources.ShouldAllBe(r => r.IsDisposed);
    }

    [TestCase(0)]
    [TestCase(2)]
    public void Child_Instances_Are_Collected_After_Removal(int rerenders)
    {
        var source = new EventSource();

        RenderAndRemoveChild(source, rerenders);

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        source.Instances.Count.ShouldBe(rerenders + 1);
        source.Instances.ShouldAllBe(w => !w.IsAlive);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void RenderAndRemoveChild(EventSource source, int rerenders)
    {
        var host = CreateHost(source, rerenders);
        RemoveChild(host);
    }
}
