using MauiReactor;
using MauiReactor.TestApp.Controls.Native;
using MauiReactor.TestApp.Pages;
using Shouldly;

namespace UnitTests;

/// <summary>
/// Tests for scaffold-generated controls with <c>implementItemTemplate: true</c>
/// running in the headless test host (<see cref="TemplateHost"/>).
/// <para/>
/// These tests demonstrate two bugs in the scaffold source generator:
/// <list type="number">
///   <item>
///     <c>ForceItemsLoad()</c> returns early without initializing
///     <c>_loadedForciblyChildren</c> when <c>NativeControl.ItemsSource</c> or
///     <c>NativeControl.ItemTemplate</c> is null, causing
///     <c>Validate.EnsureNotNull</c> to throw in <c>Descendants&lt;T&gt;()</c>.
///   </item>
///   <item>
///     <c>OnUpdate()</c> can crash during template setup (setting ItemsSource /
///     ItemTemplate on the native control), preventing <c>base.OnUpdate()</c>
///     from running. This leaves <c>AutomationId</c> and other properties from
///     <c>_propertiesToSet</c> unapplied.
///   </item>
/// </list>
/// </summary>
public sealed class TestScaffoldTemplateHost
{
    [TearDown]
    public void TearDown()
    {
        TestItemsControl.SimulateHandlerDependency = false;
    }

    [Test]
    public void ScaffoldWithItemTemplate_RenderSucceeds()
    {
        var host = TemplateHost.Create(new ScaffoldItemTemplatePage());
        host.ShouldNotBeNull();
    }

    [Test]
    public void ScaffoldWithItemTemplate_RenderSucceeds_WhenEmpty()
    {
        var host = TemplateHost.Create(new ScaffoldItemTemplateEmptyPage());
        host.ShouldNotBeNull();
    }

    /// <summary>
    /// Bug 2: When the native control throws during template setup in <c>OnUpdate()</c>,
    /// <c>base.OnUpdate()</c> never runs, so <c>AutomationId</c> from
    /// <c>_propertiesToSet</c> is never applied to the native control.
    /// <para/>
    /// This test enables <see cref="TestItemsControl.SimulateHandlerDependency"/> to
    /// make the control throw when <c>ItemTemplate</c> is set (simulating the behavior
    /// of handler-dependent controls like PanCardView.CardsView in headless mode).
    /// The fix wraps the template setup in a try-catch so <c>base.OnUpdate()</c>
    /// always runs.
    /// </summary>
    [Test]
    public void ScaffoldWithItemTemplate_AutomationIdApplied_WhenOnUpdateThrows()
    {
        TestItemsControl.SimulateHandlerDependency = true;

        var host = TemplateHost.Create(new ScaffoldItemTemplatePage());

        // With the bug, base.OnUpdate() never runs when the template setup throws,
        // so AutomationId is never applied. Find<T>() throws "not found".
        var control = host.Find<TestItemsControl>("TestItems");
        control.ShouldNotBeNull();
        control.AutomationId.ShouldBe("TestItems");
    }
}
