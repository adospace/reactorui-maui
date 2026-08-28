# Hooks spike

Status: **spike**, not the implementation. It compiles, it runs, and the API feels right —
that is the whole purpose. Several things below are known-wrong and are deliberately not
solved here.

## What works

- `UseState`, `UseReducer`, `UseRef`, `UseEffect` (with cleanup + dependencies), `UseMemo`
- Hook state survives across renders by riding the `Component<S>` state-transfer channel
  (`Component.cs:527`), so the `_newComponent` generation chain, dispatcher marshalling in
  `SetState` and props transfer all come along for free
- Cleanups run once on unmount, guarded against the double-unmount paths
- Signatures mirror Microsoft.UI.Reactor so samples and docs travel between the two

## Known gaps

**Component identity — fixed.** `WithKey` is now honoured. `MergeChildrenFrom` and
`VisualNode.MergeWith` share one pairing implementation: ordinal when no child on either
side carries a `Key`, identity-based when any does. Whether a list is keyed is computed
once while the children list is built (inside the existing loop in the `Children` getter),
so an unkeyed tree pays two field reads and nothing else. Covered by
`TestKeyedReconciliation`: insert-at-top, reorder and remove all keep state with its item,
and the unkeyed shift is pinned by a test that documents it.

Native controls reorder too. Merging transfers a native control but never re-parents it,
so keyed pairing alone moved the state and left the pixels behind - verified failing, then
fixed with a `MoveChild` / `OnMoveChild` protocol symmetric to the existing add/remove one.
After a keyed merge that changed any position, each child repositions its own native control
under its parent, in ascending order. `Keyed_Reorder_MovesStateAndPixelsTogether` pins state
and visual order together.

Cost, 300 merges of a 200-child list (same machine, ~20% run-to-run variance, so treat these
as shapes not constants):

| | |
|---|---|
| unkeyed | baseline |
| keyed, order unchanged | ~1x - within noise |
| keyed, two items swapped | ~1x - within noise |
| keyed, every item shifts | ~2.6-3.1x |

The move pass uses `IndexOf` on the native children collection, so a merge in which *every*
child moves is O(n^2). An O(1) check skips children already in place, which is why a normal
reorder (a drag, a sort tweak) costs nothing measurable - only a wholesale shuffle pays.
Worth revisiting with a single-pass reorder if anyone shuffles a large non-virtualized list.

**Effect flush timing is approximate.** `OnAfterRender` dispatches the flush rather than
running it after `Layout` returns, because `Component.RenderChildren` is `sealed`
(`Component.cs:74`) and there is no post-commit seam yet. Consequences: an effect can
observe a tree one dispatch older than it should, and a component whose
`IsLayoutCycleRequired` was consumed by an ancestor pass may not flush at all. The real fix
is a post-`Layout` hook, plus a `PendingDeps` staging slot so deps are recorded at acquire
time rather than flush time.

**Item templates are not handled.** `ItemDataTemplateNode` reassigns `Root` on every update
and calls `Layout()` synchronously, so a recycled `CollectionView` cell index-merges one
item's subtree into another's. A `UseState` cell inside an item template will show the
previous item's value. `ItemControlTemplateNode` has the same shape. Do not use hooks inside
item templates yet.

**Hot reload is unimplemented.** `HookSlots.ResetForHotReload()` exists and is not wired up.
The intended policy, matching Microsoft's: reset the table on hook *order* change, migrate
values on *shape* change. Adding a `UseState` line mid-session currently does whatever the
positional table happens to do — which is the single most common edit, so this needs to be
real before anyone iterates with it.

**No analyzer.** Nothing stops a hook inside an `if`, a loop, or a nested function. The
runtime type check in `HookSlots.Next` catches some of it as a `HookOrderException`, late and
with a worse message than a squiggle. Four rules are planned: outside-render, conditional,
loop, nested-function.

**`State` is public.** `Component<S>.State` is `public` (`Component.cs:337`), so
`((Component<HookSlots>)page).State.Cells` reaches the raw table from anywhere. `HookSlots`
keeps its members `internal`, which is the mitigation, not a fix.

## Core change carried by this spike

`Component` gains `internal virtual OnBeforeRender()` / `OnAfterRender()`, called around
`Render()` in `RenderChildren()`. They are `internal` on purpose: a `protected virtual` method
with either name would be CS0114 — or CS0508 on a differing return type — for any user who
already has one on their own component base.
