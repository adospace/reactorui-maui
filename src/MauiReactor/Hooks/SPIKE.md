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

**Component identity is positional.** `VisualNode.MergeChildrenFrom` (`VisualNode.cs:471`)
pairs children by ordinal, and `WithKey` (`VisualNode.cs:102`) writes a `Key` that
reconciliation never reads. Same-type siblings whose position shifts therefore swap cell
tables. This already affects `Component<S>` today; hooks widen the blast radius from one
typed POCO to every cell. **Keyed reconciliation lands before hooks ship** — it is the
blocker, not a follow-up.

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
