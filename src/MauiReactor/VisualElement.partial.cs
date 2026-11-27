using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using System.Linq;

namespace MauiReactor;

public partial interface IVisualElement
{
    Shapes.IGeometry? Clip { get; set; }
    IShadow? Shadow { get; set; }
    VisualStateGroupList? VisualStateGroups { get; set; }
    List<IBehavior>? Behaviors { get; set; }
}

public abstract partial class VisualElement<T>
{
    Shapes.IGeometry? IVisualElement.Clip { get; set; }
    
    IShadow? IVisualElement.Shadow { get; set; }

    VisualStateGroupList? IVisualElement.VisualStateGroups { get; set; }

    List<IBehavior>? IVisualElement.Behaviors { get; set; }

    protected override void OnChildAdd(VisualNode node)
    {
        if (node is IBehavior behavior)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.Behaviors ??= [];
            thisAsIVisualElement.Behaviors.Add(behavior);
            return;
        }

        base.OnChildAdd(node);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        var thisAsIVisualElement = (IVisualElement)this;

        if (thisAsIVisualElement.VisualStateGroups != null)
        {
            Validate.EnsureNotNull(NativeControl);

            thisAsIVisualElement.VisualStateGroups.SetToVisualElement(NativeControl);
        }
    }

    protected override void OnMount()
    {
        base.OnMount();

        //var thisAsIVisualElement = (IVisualElement)this;

        //if (thisAsIVisualElement.VisualStateGroups != null)
        //{
        //    Validate.EnsureNotNull(NativeControl);

        //    thisAsIVisualElement.VisualStateGroups.SetToVisualElement(NativeControl);
        //}
    }

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        var thisAsIVisualElement = (IVisualElement)this;

        var children = base.RenderChildren();

        if (thisAsIVisualElement.Clip != null)
        {
            children = children.Concat(new[] { (VisualNode)thisAsIVisualElement.Clip });
        }

        if (thisAsIVisualElement.Shadow != null)
        {
            children = children.Concat(new[] { (VisualNode)thisAsIVisualElement.Shadow });
        }

        if (thisAsIVisualElement.Behaviors != null)
        {
            children = children.Concat(thisAsIVisualElement.Behaviors.Cast<VisualNode>());
        }

        return children;
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsIVisualElement = (IVisualElement)this;

        if (widget == thisAsIVisualElement.Clip &&
            childNativeControl is Microsoft.Maui.Controls.Shapes.Geometry geometry)
        {
            NativeControl.Clip = geometry;
        }
        else if (widget == thisAsIVisualElement.Shadow &&
            childNativeControl is Microsoft.Maui.Controls.Shadow shadow)
        {
            NativeControl.Shadow = shadow;
        }
        else if (childNativeControl is Microsoft.Maui.Controls.Behavior behavior)
        {
            NativeControl.Behaviors.Add(behavior);
        }

        base.OnAddChild(widget, childNativeControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsIVisualElement = (IVisualElement)this;

        if (widget == thisAsIVisualElement.Clip &&
            childNativeControl is Microsoft.Maui.Controls.Shapes.Geometry)
        {
            NativeControl.Clip = null;
        }
        else if (widget == thisAsIVisualElement.Shadow &&
            childNativeControl is Microsoft.Maui.Controls.Shadow)
        {
            NativeControl.Shadow = null!;
        }
        else if (childNativeControl is Microsoft.Maui.Controls.Behavior behavior)
        {
            NativeControl.Behaviors.Remove(behavior);
        }

        base.OnRemoveChild(widget, childNativeControl);
    }
}

public static partial class VisualElementExtensions
{
    public static T Clip<T>(this T visualElement, Shapes.IGeometry geometry) where T : IVisualElement
    {
        visualElement.Clip = geometry;
        return visualElement;
    }

    public static T Shadow<T>(this T visualElement, IShadow shadow) where T : IVisualElement
    {
        visualElement.Shadow = shadow;
        return visualElement;
    }

    public static T OnSizeChanged<T>(this T visualElement, Action<Size>? sizeChangedAction) where T : IVisualElement
    {
        visualElement.SizeChangedEvent = new SyncEventCommand<EventArgs>((sender, args)=>
        {
            if (sender is VisualElement element)
            {
                sizeChangedAction?.Invoke(new Size(element.Width, element.Height));
            }
        });
        return visualElement;
    }

    public static T VisualState<T>(this T visualElement, string groupName, string stateName, BindableProperty? property = null, object? value = null, string? targetName = null) where T : IVisualElement
    {
        visualElement.VisualStateGroups ??= [];

        visualElement.VisualStateGroups.Set(groupName, stateName, property, value, targetName);

        return visualElement;
    }

    public static T VisualState<T>(this T itemsview, VisualStateGroupList visualState) where T : IVisualElement
    {
        itemsview.VisualStateGroups = visualState;

        return itemsview;
    }

    public static T Width<T>(this T itemsview, double width) where T : IVisualElement
    {
        itemsview.WidthRequest(width);

        return itemsview;
    }


    public static T Height<T>(this T itemsview, double height) where T : IVisualElement
    {
        itemsview.HeightRequest(height);

        return itemsview;
    }


}

public static class VisualElementNativeExtensions
{ 
    public static Rect BoundsToScreenSize(this VisualElement visualElement)
    {
        if (visualElement.Parent is VisualElement parentVisualElement)
        {
            var parentFrameToScreenSize = parentVisualElement.BoundsToScreenSize();
            return new Rect(parentFrameToScreenSize.Left + visualElement.Bounds.Left, parentFrameToScreenSize.Top + visualElement.Bounds.Top, visualElement.Bounds.Width, visualElement.Bounds.Height);
        }

        return visualElement.Bounds;
    }

    //public static T IsVisible<T>(this T component, bool isVisible) where T : VisualNode
    //{
    //    component.SetProperty(Microsoft.Maui.Controls.VisualElement.IsVisibleProperty, isVisible);
    //    return component;
    //}
}

public class VisualStateGroupList : Dictionary<string, VisualStateGroup>
{
    private Microsoft.Maui.Controls.VisualStateGroupList CreateNativeGroupList()
    {
        var groupList = new Microsoft.Maui.Controls.VisualStateGroupList();

        foreach (var groupEntry in this)
        {
            var group = new Microsoft.Maui.Controls.VisualStateGroup() { Name = groupEntry.Key };

            foreach (var stateEntry in groupEntry.Value)
            {
                var state = new Microsoft.Maui.Controls.VisualState() { Name = stateEntry.Key };

                foreach (var setterEntry in stateEntry.Value)
                {
                    if (setterEntry.Property != null)
                    {
                        var setter = new Setter { Property = setterEntry.Property, Value = setterEntry.Value, TargetName = setterEntry.TargetName };
                        state.Setters.Add(setter);
                    }
                }

                group.States.Add(state);
            }

            groupList.Add(group);
        };

        return groupList;
    }

    // What was last applied to a native element: a snapshot of the declared groups and the
    // native list built from them. Stored per element (an earlier attempt cached one native list
    // in a static field, shared by every control - see #229).
    private sealed class AppliedVisualStates(VisualStateGroupList declared, Microsoft.Maui.Controls.VisualStateGroupList native)
    {
        public VisualStateGroupList Declared { get; } = declared;
        public Microsoft.Maui.Controls.VisualStateGroupList Native { get; } = native;
    }

    private static readonly BindableProperty _appliedVisualStatesProperty = BindableProperty.CreateAttached(
        "MauiReactorAppliedVisualStates", typeof(AppliedVisualStates), typeof(VisualStateGroupList), null);

    internal void SetToVisualElement(VisualElement? visualElement)
    {
        if (visualElement == null)
        {
            return;
        }

        // Re-rendering re-declares the visual states every time, usually unchanged (a theme puts the
        // same states on every Button, for example). Rebuilding and re-assigning the native groups on
        // each render allocates a full group/state/setter tree and re-applies the current state, so:
        // - unchanged declaration: nothing to do;
        // - only setter values changed (state-dependent or theme-dependent values): update the native
        //   setters in place and re-apply the current state (VisualStateManager.InvalidateVisualStates,
        //   new in MAUI 11);
        // - anything else (groups, states or setters added/removed), or the native groups were replaced
        //   by someone else: rebuild, as before.
        var applied = (AppliedVisualStates?)visualElement.GetValue(_appliedVisualStatesProperty);

        if (applied != null &&
            ReferenceEquals(VisualStateManager.GetVisualStateGroups(visualElement), applied.Native) &&
            HasSameShape(applied.Declared))
        {
            if (!HaveSameValues(applied.Declared))
            {
                UpdateNativeValues(applied.Native);
                visualElement.SetValue(_appliedVisualStatesProperty, new AppliedVisualStates(Snapshot(), applied.Native));
                VisualStateManager.InvalidateVisualStates(visualElement);
            }

            return;
        }

        var nativeGroupList = CreateNativeGroupList();
        VisualStateManager.SetVisualStateGroups(visualElement, nativeGroupList);
        visualElement.SetValue(_appliedVisualStatesProperty, new AppliedVisualStates(Snapshot(), nativeGroupList));
    }

    // Setters without a property are declared (a state with no setters) but never reach the native
    // list, so every comparison below works on the same filtered sequence CreateNativeGroupList uses.
    private static IEnumerable<VisualStatePropertySetter> NativeSetters(VisualState state)
        => state.Where(_ => _.Property != null);

    private bool HasSameShape(VisualStateGroupList other)
    {
        if (Count != other.Count)
        {
            return false;
        }

        foreach (var (groupName, group) in this)
        {
            if (!other.TryGetValue(groupName, out var otherGroup) || group.Count != otherGroup.Count)
            {
                return false;
            }

            foreach (var (stateName, state) in group)
            {
                if (!otherGroup.TryGetValue(stateName, out var otherState) ||
                    !NativeSetters(state).Select(_ => (_.Property, _.TargetName))
                        .SequenceEqual(NativeSetters(otherState).Select(_ => (_.Property, _.TargetName))))
                {
                    return false;
                }
            }
        }

        return true;
    }

    // Only called once HasSameShape holds, so groups/states exist and setter sequences line up.
    private bool HaveSameValues(VisualStateGroupList other)
    {
        foreach (var (groupName, group) in this)
        {
            foreach (var (stateName, state) in group)
            {
                using var otherSetters = NativeSetters(other[groupName][stateName]).GetEnumerator();
                foreach (var setter in NativeSetters(state))
                {
                    otherSetters.MoveNext();
                    if (!CompareUtils.AreEquals(setter.Value, otherSetters.Current.Value))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private void UpdateNativeValues(Microsoft.Maui.Controls.VisualStateGroupList nativeGroupList)
    {
        foreach (var nativeGroup in nativeGroupList)
        {
            var group = this[nativeGroup.Name];
            foreach (var nativeState in nativeGroup.States)
            {
                using var setters = NativeSetters(group[nativeState.Name]).GetEnumerator();
                foreach (var nativeSetter in nativeState.Setters)
                {
                    setters.MoveNext();
                    nativeSetter.Value = setters.Current.Value;
                }
            }
        }
    }

    // The declared list may be a user-held instance passed to .VisualState(list) and mutated later,
    // so keep a copy rather than a reference.
    private VisualStateGroupList Snapshot()
    {
        var copy = new VisualStateGroupList();
        foreach (var (groupName, group) in this)
        {
            var groupCopy = new VisualStateGroup();
            foreach (var (stateName, state) in group)
            {
                var stateCopy = new VisualState();
                stateCopy.AddRange(state.Select(_ => new VisualStatePropertySetter(_.Property, _.Value, _.TargetName)));
                groupCopy.Add(stateName, stateCopy);
            }
            copy.Add(groupName, groupCopy);
        }
        return copy;
    }

    internal VisualStateGroupList Set(string groupName, string stateName, BindableProperty? property = null, object? value = null, string? targetName = null)
    {
        TryGetValue(groupName, out var group);

        if (group == null)
        {
            Add(groupName, group = []);
        }

        group.TryGetValue(stateName, out var state);
        if (state == null)
        {
            group.Add(stateName, state = []);
        }

        state.Add(new VisualStatePropertySetter(property, value, targetName));

        return this;
    }

}

public class VisualStateGroup : Dictionary<string, VisualState>
{

}

public class VisualState : List<VisualStatePropertySetter> 
{

}

public class VisualStatePropertySetter(BindableProperty? property, object? value, string? targetName)
{
    public BindableProperty? Property { get; set; } = property;
    public object? Value { get; set; } = value;
    public string? TargetName { get; set; } = targetName;
}


