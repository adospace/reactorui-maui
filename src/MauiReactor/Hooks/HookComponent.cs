namespace MauiReactor;

/// <summary>
/// Base class for components that declare state with hooks instead of a state class.
/// <para>
/// Additive: <c>Component</c> and <c>Component&lt;S&gt;</c> are untouched and both models
/// coexist freely as parent and child at any depth.
/// </para>
/// <para>
/// Signatures deliberately mirror Microsoft.UI.Reactor so the same mental model - and the
/// same samples and docs - work on WinUI and on MAUI. The implementation underneath differs:
/// their components are long-lived and the reconciler owns the RenderContext, while
/// MauiReactor recreates components every render, so the cell table rides the
/// <c>Component&lt;S&gt;</c> state-transfer channel instead.
/// </para>
/// </summary>
public abstract class HookComponent : Component<HookSlots>
{
    internal override void OnBeforeRender()
    {
        State.BeginRender();
    }

    internal override void OnAfterRender()
    {
        if (!State.HasPendingEffects())
        {
            return;
        }

        // Effects must observe a committed tree, so flush after the current layout
        // cycle rather than inline. See SPIKE.md - the real fix is a post-Layout hook.
        var slots = State;
        var dispatcher = Microsoft.Maui.Controls.Application.Current?.Dispatcher;

        if (dispatcher == null)
        {
            slots.FlushEffects();
        }
        else
        {
            dispatcher.Dispatch(slots.FlushEffects);
        }
    }

    protected override void OnWillUnmount()
    {
        State.RunCleanups();

        base.OnWillUnmount();
    }

    // ── State ────────────────────────────────────────────────────────────────

    /// <summary>Declares a state cell. The setter replaces the value and re-renders.</summary>
    protected (T Value, Action<T> Set) UseState<T>(T initialValue)
    {
        var cell = State.Next(() => new ValueCell<T> { Value = initialValue });

        return (cell.Value, newValue => SetState(_ => cell.Value = newValue));
    }

    /// <summary>
    /// Like <see cref="UseState"/>, but the setter takes a transform of the previous value.
    /// Use this whenever the next value depends on the current one - a timer, an
    /// increment, anything captured in a closure that outlives the render.
    /// </summary>
    protected (T Value, Action<Func<T, T>> Update) UseReducer<T>(T initialValue)
    {
        var cell = State.Next(() => new ValueCell<T> { Value = initialValue });

        return (cell.Value, update => SetState(_ => cell.Value = update(cell.Value)));
    }

    /// <summary>Mutable storage that survives renders and never triggers one.</summary>
    protected Ref<T> UseRef<T>(T initialValue = default!)
        => State.Next(() => new RefCell<T> { Ref = new Ref<T>(initialValue) }).Ref;

    // ── Effects ──────────────────────────────────────────────────────────────

    /// <summary>Runs after render when any dependency changed. Return an action to clean up.</summary>
    protected void UseEffect(Func<Action> effectWithCleanup, params object?[] dependencies)
    {
        var cell = State.Next(() => new EffectCell());

        if (DepsUnchanged(cell.Deps, dependencies))
        {
            return;
        }

        cell.Deps = dependencies;
        cell.Pending = effectWithCleanup;
    }

    protected void UseEffect(Action effect, params object?[] dependencies)
        => UseEffect(() => { effect(); return static () => { }; }, dependencies);

    // Typed arities avoid boxing and the per-render array allocation, and leave the
    // parameter list open for caller attributes later. Same shape as theirs.
    protected void UseEffect<T1>(Func<Action> effectWithCleanup, T1 d1)
        => UseEffect(effectWithCleanup, [d1]);

    protected void UseEffect<T1, T2>(Func<Action> effectWithCleanup, T1 d1, T2 d2)
        => UseEffect(effectWithCleanup, [d1, d2]);

    protected void UseEffect<T1>(Action effect, T1 d1)
        => UseEffect(effect, [d1]);

    protected void UseEffect<T1, T2>(Action effect, T1 d1, T2 d2)
        => UseEffect(effect, [d1, d2]);

    // ── Memo ─────────────────────────────────────────────────────────────────

    /// <summary>Caches a computed value until a dependency changes.</summary>
    protected T UseMemo<T>(Func<T> factory, params object?[] dependencies)
    {
        var cell = State.Next(() => new MemoCell<T>());

        if (cell.Deps == null || !DepsUnchanged(cell.Deps, dependencies))
        {
            cell.Deps = dependencies;
            cell.Value = factory();
        }

        return cell.Value;
    }

    protected T UseMemo<T, T1>(Func<T> factory, T1 d1)
        => UseMemo(factory, [d1]);

    protected T UseMemo<T, T1, T2>(Func<T> factory, T1 d1, T2 d2)
        => UseMemo(factory, [d1, d2]);

    private static bool DepsUnchanged(object?[]? previous, object?[] next)
    {
        if (previous == null || previous.Length != next.Length)
        {
            return false;
        }

        for (var i = 0; i < next.Length; i++)
        {
            if (!Equals(previous[i], next[i]))
            {
                return false;
            }
        }

        return true;
    }
}
