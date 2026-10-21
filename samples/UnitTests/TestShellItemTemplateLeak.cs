using MauiReactor;
using Shouldly;
using System;
using System.Runtime.CompilerServices;

namespace UnitTests;

internal class TestShellItemTemplateLeak
{
    [Test]
    public void ShellItemTemplate_PresenterNoLongerReferenced_IsCollected()
    {
        var mainPage = TemplateHost.Create(new ShellWithReplacedItem());
        var shell = mainPage.Find<MauiControls.Shell>("MainShell");

        var presenterRef = WeakRefToDroppedPresenter(shell);
        Collect();

        IsAlive(presenterRef).ShouldBeFalse();
    }

    [Test]
    public void ShellItemTemplate_ItemReplaced_PageOfTheReplacedItemIsCollected()
    {
        var component = new ShellWithReplacedItem();
        var mainPage = TemplateHost.Create(component);
        var shell = mainPage.Find<MauiControls.Shell>("MainShell");
        // Stands in for the flyout, which holds the presenter of an item that stays.
        var presenter = CreatePresenter(shell, shell.Items[0]);

        var pageRef = WeakRefToPageOf(mainPage, "Replaced0");
        Collect();
        IsAlive(pageRef).ShouldBeTrue();
        component.ReplaceItem();
        Collect();

        IsAlive(pageRef).ShouldBeFalse();
        GC.KeepAlive(presenter);
    }

    [Test]
    public void ShellItemTemplate_ItemRemoved_PageOfTheRemovedItemIsCollected()
    {
        var component = new ShellWithReplacedItem();
        var mainPage = TemplateHost.Create(component);
        var shell = mainPage.Find<MauiControls.Shell>("MainShell");
        // Stands in for the flyout, which holds the presenter of an item that stays.
        var presenter = CreatePresenter(shell, shell.Items[0]);

        var pageRef = WeakRefToPageOf(mainPage, "Replaced0");
        Collect();
        IsAlive(pageRef).ShouldBeTrue();
        component.RemoveItem();
        Collect();

        IsAlive(pageRef).ShouldBeFalse();
        GC.KeepAlive(presenter);
    }

    [Test]
    public void ShellItemTemplate_ShellRenderedAgain_PresenterShowsTheNewTemplate()
    {
        var component = new ShellWithReplacedItem();
        var mainPage = TemplateHost.Create(component);
        var shell = mainPage.Find<MauiControls.Shell>("MainShell");
        var presenter = CreatePresenter(shell, shell.Items[0]);

        component.ReplaceItem();

        ((MauiControls.Label)presenter.Content).Text.ShouldBe("Stays 1");
    }

    private static void Collect()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    private static MauiControls.ContentView CreatePresenter(MauiControls.Shell shell, MauiControls.BaseShellItem item)
    {
        var presenter = (MauiControls.ContentView)shell.ItemTemplate.CreateContent();
        presenter.BindingContext = item;
        return presenter;
    }

    // Not inlined, so that an object these create or look up leaves no reference to it in the test's own frame.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static WeakReference<object> WeakRefToDroppedPresenter(MauiControls.Shell shell) =>
        new(CreatePresenter(shell, shell.Items[0]));

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static WeakReference<object> WeakRefToPageOf(ITemplateHost mainPage, string automationId) =>
        new(((MauiControls.IShellContentController)mainPage.Find<MauiControls.ShellContent>(automationId)).GetOrCreateContent());

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static bool IsAlive(WeakReference<object> reference) => reference.TryGetTarget(out _);

    private class ShellWithReplacedItem : Component<ShellWithReplacedItem.ShellState>
    {
        public class ShellState
        {
            public int Version { get; set; }

            public bool ShowsReplaced { get; set; } = true;
        }

        public void RemoveItem()
        {
            SetState(s => s.ShowsReplaced = false);
        }

        /// <summary>Renders twice, so that the old item is removed rather than merged into its replacement.</summary>
        public void ReplaceItem()
        {
            RemoveItem();
            SetState(s =>
            {
                s.ShowsReplaced = true;
                s.Version++;
            });
        }

        public override VisualNode Render()
        {
            var version = State.Version;
            return new Shell
                {
                    new ShellContent("Stays")
                        .RenderContent(() => new ContentPage()),
                    State.ShowsReplaced
                        ? new ShellContent("Replaced")
                            .AutomationId($"Replaced{version}")
                            .RenderContent(() => new ContentPage())
                        : null
                }
                .ItemTemplate(item => new Label($"{item.Title} {version}"))
                .AutomationId("MainShell");
        }
    }
}
