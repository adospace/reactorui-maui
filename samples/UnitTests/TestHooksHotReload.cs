using System;
using MauiReactor;
using Shouldly;

namespace UnitTests;

class ReloadProbePage : HookComponent
{
    public static bool ExtraHookAtTop;
    public static bool ExtraHookAtEnd;
    public static int MemoComputations;
    public static int CleanupsRun;

    public void ForceRender() => Invalidate();

    public override VisualNode Render()
    {
        // Deliberately shape-varying: this is what an edit-and-save looks like to the
        // cell table, which is exactly what the reload policy has to cope with.
        if (ExtraHookAtTop)
        {
            UseState("inserted");
        }

        var (count, setCount) = UseState(0);

        var doubled = UseMemo(() =>
        {
            MemoComputations++;
            return count * 2;
        }, count);

        if (ExtraHookAtEnd)
        {
            UseEffect(() => () => CleanupsRun++, "appended");
        }

        return ContentPage(
            Button($"{count}/{doubled}")
                .AutomationId("counter")
                .OnClicked(() => setCount(count + 1))
        );
    }
}

public class TestHooksHotReload
{
    [SetUp]
    public void Reset()
    {
        ReloadProbePage.ExtraHookAtTop = false;
        ReloadProbePage.ExtraHookAtEnd = false;
        ReloadProbePage.MemoComputations = 0;
        ReloadProbePage.CleanupsRun = 0;
    }

    private static string Text(ITemplateHost host)
        => host.Find<MauiControls.Button>("counter").Text;

    private static void Click(ITemplateHost host)
        => host.Find<MauiControls.Button>("counter").SendClicked();

    [Test]
    public void ReloadWithUnchangedShape_KeepsState()
    {
        var page = new ReloadProbePage();
        var host = TemplateHost.Create(page);

        Click(host);
        Click(host);
        Text(host).ShouldBe("2/4");

        HotReloadGeneration.Bump();
        page.ForceRender();

        // The common edit - tweak markup, save - must not cost the user their state.
        Text(host).ShouldBe("2/4");
    }

    [Test]
    public void ReloadWithUnchangedShape_RecomputesMemo()
    {
        var page = new ReloadProbePage();
        var host = TemplateHost.Create(page);

        Click(host);
        var before = ReloadProbePage.MemoComputations;

        HotReloadGeneration.Bump();
        page.ForceRender();

        // The memo factory is new IL now, so its cached result cannot be trusted even
        // though the dependency is unchanged.
        ReloadProbePage.MemoComputations.ShouldBe(before + 1);
    }

    [Test]
    public void ReloadAppendingAHook_KeepsEverythingBeforeIt()
    {
        var page = new ReloadProbePage();
        var host = TemplateHost.Create(page);

        Click(host);
        Click(host);
        Click(host);
        Text(host).ShouldBe("3/6");

        ReloadProbePage.ExtraHookAtEnd = true;
        HotReloadGeneration.Bump();
        page.ForceRender();

        Text(host).ShouldBe("3/6");
    }

    [Test]
    public void ReloadInsertingAHookAtTheTop_ResetsFromThatPoint()
    {
        var page = new ReloadProbePage();
        var host = TemplateHost.Create(page);

        Click(host);
        Click(host);
        Text(host).ShouldBe("2/4");

        ReloadProbePage.ExtraHookAtTop = true;
        HotReloadGeneration.Bump();
        page.ForceRender();

        // Everything from the divergence point is unrecoverable - the cells after it may
        // not be the same hooks any more. Documented, not accidental.
        Text(host).ShouldBe("0/0");
    }

    [Test]
    public void ReloadDroppingAHook_RunsItsCleanup()
    {
        ReloadProbePage.ExtraHookAtEnd = true;

        var page = new ReloadProbePage();
        var host = TemplateHost.Create(page);

        ReloadProbePage.CleanupsRun.ShouldBe(0);

        ReloadProbePage.ExtraHookAtEnd = false;
        HotReloadGeneration.Bump();
        page.ForceRender();

        // The trailing effect is gone from the new code; its cleanup must still run.
        ReloadProbePage.CleanupsRun.ShouldBe(1);
    }

    [Test]
    public void OutsideAReload_AChangedHookSequenceIsReported()
    {
        var page = new ReloadProbePage();
        var host = TemplateHost.Create(page);

        Click(host);

        Exception? reported = null;
        var previous = ReactorApplicationHost.UnhandledException;
        ReactorApplicationHost.UnhandledException = args => reported = args.ExceptionObject as Exception;

        try
        {
            ReloadProbePage.ExtraHookAtTop = true;

            // No reload happened, so this is a rules-of-hooks violation rather than an
            // edit, and must not be quietly tolerated. TemplateHost catches render
            // exceptions and surfaces them through the unhandled-exception event rather
            // than tearing the app down, so that is where it shows up.
            page.ForceRender();
        }
        finally
        {
            ReactorApplicationHost.UnhandledException = previous;
        }

        reported.ShouldBeOfType<HookOrderException>();
    }
}
