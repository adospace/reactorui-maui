namespace MauiReactor;

/// <summary>
/// Opaque per-component hook cell table. This is the <c>S</c> of
/// <c>Component&lt;S&gt;</c> for <see cref="HookComponent"/>: it rides the existing
/// state-transfer channel (<c>Component.cs:527</c>), so hook state survives the
/// merge into the next component generation for free.
/// Nothing here is public on purpose - see SPIKE.md.
/// </summary>
public sealed class HookSlots : IHotReloadTransferable
{
    internal List<HookCell> Cells { get; } = [];

    internal int Cursor { get; private set; }

    internal bool CleanupsRun { get; set; }

    private int _generation = HotReloadGeneration.Current;

    private bool _toleratingReload;

    internal void BeginRender(int hotReloadGeneration)
    {
        Cursor = 0;

        if (hotReloadGeneration == _generation)
        {
            return;
        }

        _generation = hotReloadGeneration;

        // The code behind this table was just replaced. Values in UseState/UseRef are data
        // and survive; memoized results and effect bodies close over IL that no longer
        // exists, so they are recomputed and re-run once rather than trusted.
        _toleratingReload = true;

        foreach (var cell in Cells)
        {
            cell.OnHotReload();
        }
    }

    /// <summary>
    /// Ends a render pass. After a reload, any cells the new code no longer reaches are
    /// cleaned up and dropped.
    /// </summary>
    internal void EndRender()
    {
        if (!_toleratingReload)
        {
            return;
        }

        _toleratingReload = false;

        if (Cursor < Cells.Count)
        {
            TruncateFrom(Cursor);
        }
    }

    /// <summary>Runs cleanups for every cell from <paramref name="index"/> on, then drops them.</summary>
    private void TruncateFrom(int index)
    {
        for (var i = index; i < Cells.Count; i++)
        {
            if (Cells[i] is EffectCell { HasRunBody: true, Cleanup: not null } effect)
            {
                effect.Cleanup.Invoke();
                effect.Cleanup = null;
            }
        }

        Cells.RemoveRange(index, Cells.Count - index);
    }

    /// <summary>
    /// Returns the cell at the cursor, creating it on first render. Throws if the
    /// cell type at this index does not match - which is a rules-of-hooks violation
    /// during normal rendering, and an expected edit during hot reload.
    /// </summary>
    internal TCell Next<TCell>(Func<TCell> factory) where TCell : HookCell
    {
        var index = Cursor;

        if (index == Cells.Count)
        {
            Cells.Add(factory());
        }
        else if (Cells[index] is not TCell)
        {
            if (!_toleratingReload)
            {
                throw new HookOrderException(
                    $"Hook at index {index} is {Cells[index].GetType().Name}, expected {typeof(TCell).Name}. " +
                    "Hooks must be called in the same order on every render - no hooks inside " +
                    "conditionals, loops or nested functions.");
            }

            // Hot reload: the edit changed the hook sequence at this point. Everything from
            // here on refers to hooks that may no longer be the same ones, and there is no
            // sound way to re-key them, so clean them up and start fresh from this index.
            // Cells before it are untouched, so appending a hook keeps all existing state.
            TruncateFrom(index);
            Cells.Add(factory());
        }

        Cursor = index + 1;

        return (TCell)Cells[index];
    }

    internal bool HasPendingEffects()
    {
        foreach (var cell in Cells)
        {
            if (cell is EffectCell { Pending: not null })
            {
                return true;
            }
        }

        return false;
    }

    internal void FlushEffects()
    {
        // The flush is dispatched, so it can land after the component was unmounted. Running
        // effect bodies then would install cleanups that nothing will ever call - a resource
        // leak from a component that no longer exists.
        if (CleanupsRun)
        {
            return;
        }

        foreach (var cell in Cells)
        {
            if (cell is not EffectCell { Pending: not null } effect)
            {
                continue;
            }

            effect.Cleanup?.Invoke();
            effect.Cleanup = effect.Pending();
            effect.Pending = null;
            effect.HasRunBody = true;
        }
    }

    /// <summary>
    /// Runs every cleanup once and only once. Guarded because
    /// <c>Component.OnUnmount</c> (<c>Component.cs:150</c>) has no <c>_isMounted</c>
    /// check, so unmount is reachable twice, and reachable for a component that
    /// rendered but never mounted.
    /// </summary>
    internal void RunCleanups()
    {
        if (CleanupsRun)
        {
            return;
        }

        CleanupsRun = true;

        foreach (var cell in Cells)
        {
            if (cell is EffectCell { HasRunBody: true, Cleanup: not null } effect)
            {
                effect.Cleanup.Invoke();
                effect.Cleanup = null;
            }
        }
    }

    /// <summary>Drops every cell, running cleanups first.</summary>
    internal void ResetForHotReload()
    {
        TruncateFrom(0);
        Cursor = 0;
    }
}

internal abstract class HookCell
{
    /// <summary>
    /// Called on every cell once per hot-reload pass. Cells holding data survive; cells
    /// holding compiled behaviour invalidate themselves.
    /// </summary>
    internal virtual void OnHotReload()
    {
    }
}

internal sealed class ValueCell<T> : HookCell
{
    public T Value = default!;
}

internal sealed class RefCell<T> : HookCell
{
    public Ref<T> Ref = null!;
}

internal sealed class MemoCell<T> : HookCell
{
    public object?[]? Deps;
    public T Value = default!;

    internal override void OnHotReload() => Deps = null;
}

internal sealed class EffectCell : HookCell
{
    public object?[]? Deps;
    public Func<Action>? Pending;
    public Action? Cleanup;
    public bool HasRunBody;

    internal override void OnHotReload() => Deps = null;
}

/// <summary>
/// Thrown when a component's hook call sequence diverges from the previous render.
/// Mirrors Microsoft.UI.Reactor's HookOrderException so the diagnostic reads the same
/// on both frameworks.
/// </summary>
public sealed class HookOrderException(string message) : InvalidOperationException(message);

/// <summary>Mutable box returned by <c>UseRef</c>. Changing <see cref="Current"/> never re-renders.</summary>
public sealed class Ref<T>(T initialValue)
{
    public T Current { get; set; } = initialValue;
}
