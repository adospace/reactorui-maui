namespace MauiReactor;

/// <summary>
/// Opaque per-component hook cell table. This is the <c>S</c> of
/// <c>Component&lt;S&gt;</c> for <see cref="HookComponent"/>: it rides the existing
/// state-transfer channel (<c>Component.cs:527</c>), so hook state survives the
/// merge into the next component generation for free.
/// Nothing here is public on purpose - see SPIKE.md.
/// </summary>
public sealed class HookSlots
{
    internal List<HookCell> Cells { get; } = [];

    internal int Cursor { get; private set; }

    internal bool CleanupsRun { get; set; }

    internal void BeginRender() => Cursor = 0;

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
        else if (index > Cells.Count)
        {
            throw new HookOrderException(
                $"Hook cursor {index} is past the end of the cell table ({Cells.Count}).");
        }

        Cursor = index + 1;

        if (Cells[index] is not TCell cell)
        {
            throw new HookOrderException(
                $"Hook at index {index} is {Cells[index].GetType().Name}, expected {typeof(TCell).Name}. " +
                "Hooks must be called in the same order on every render - no hooks inside " +
                "conditionals, loops or nested functions.");
        }

        return cell;
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

    /// <summary>Hot reload: reset on shape change, matching Microsoft.UI.Reactor's ResetForHotReload().</summary>
    internal void ResetForHotReload()
    {
        RunCleanups();
        Cells.Clear();
        Cursor = 0;
        CleanupsRun = false;
    }
}

internal abstract class HookCell;

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
}

internal sealed class EffectCell : HookCell
{
    public object?[]? Deps;
    public Func<Action>? Pending;
    public Action? Cleanup;
    public bool HasRunBody;
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
