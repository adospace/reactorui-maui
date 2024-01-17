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

    partial void OnReset()
    {
        var thisAsIVisualElement = (IVisualElement)this;
        thisAsIVisualElement.Clip = null;
        thisAsIVisualElement.Shadow = null;
        thisAsIVisualElement.VisualStateGroups = null;
        thisAsIVisualElement.Behaviors = null;
    }

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

    protected override void OnMount()
    {
        base.OnMount();

        var thisAsIVisualElement = (IVisualElement)this;

        if (thisAsIVisualElement.VisualStateGroups != null)
        {
            Validate.EnsureNotNull(NativeControl);

            thisAsIVisualElement.VisualStateGroups.SetToVisualElement(NativeControl);
        }
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
        visualElement.SizeChangedActionWithArgs = (sender, args)=>
        {
            if (sender is VisualElement element)
            {
                sizeChangedAction?.Invoke(new Size(element.Width, element.Height));
            }
        };
        return visualElement;
    }

    public static T VisualState<T>(this T itemsview, string groupName, string stateName, BindableProperty property, object? value, string? targetName = null) where T : IVisualElement
    {
        itemsview.VisualStateGroups ??= [];

        itemsview.VisualStateGroups.TryGetValue(groupName, out var group);

        if (group == null)
        {
            itemsview.VisualStateGroups.Add(groupName, group = []);
        }

        group.TryGetValue(stateName, out var state);
        if (state == null)
        {
            group.Add(stateName, state = []);
        }

        state.Add(new VisualStatePropertySetter(property, value, targetName));

        return itemsview;
    }

    public static T VisualState<T>(this T itemsview, VisualStateGroupList visualState) where T : IVisualElement
    {
        itemsview.VisualStateGroups = visualState;

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
}

//public class VisualStateNamedGroup
//{
//    public const string Common = "CommonStates";
//}

public class VisualStateGroupList : Dictionary<string, VisualStateGroup>
{
    private static Microsoft.Maui.Controls.VisualStateGroupList? _cachedNativeGroupList;

    private Microsoft.Maui.Controls.VisualStateGroupList CreateNativeGroupList()
    {
        var groupList = new Microsoft.Maui.Controls.VisualStateGroupList();

        foreach (var groupEntry in this)
        {
            var group = new Microsoft.Maui.Controls.VisualStateGroup() { Name = groupEntry.Key };

            foreach (var stateEntry in group.States)
            {
                var state = new Microsoft.Maui.Controls.VisualState() { Name = stateEntry.Name };

                foreach (var setterEntry in state.Setters)
                {
                    var setter = new Setter { Property = setterEntry.Property, Value = setterEntry.Value, TargetName = setterEntry.TargetName };
                    state.Setters.Add(setter);
                }

                group.States.Add(state);
            }

            groupList.Add(group);
        };

        return groupList;
    }

    private Microsoft.Maui.Controls.VisualStateGroupList ToNativeVisualStateGroupList()
    {
        return _cachedNativeGroupList ??= CreateNativeGroupList();
    }

    internal void SetToVisualElement(VisualElement? visualElement)
    {
        if (visualElement == null)
        {
            return;
        }

        var existingVisualStateGroups = VisualStateManager.GetVisualStateGroups(visualElement);
        if (existingVisualStateGroups == null ||
            _cachedNativeGroupList == null ||
            existingVisualStateGroups != _cachedNativeGroupList)
        {
            VisualStateManager.SetVisualStateGroups(visualElement, ToNativeVisualStateGroupList());
        }
    }
}

public class VisualStateGroup : Dictionary<string, VisualState>
{

}

public class VisualState : List<VisualStatePropertySetter> 
{

}

public class VisualStatePropertySetter(BindableProperty property, object? value, string? targetName)
{
    public BindableProperty Property { get; set; } = property;
    public object? Value { get; set; } = value;
    public string? TargetName { get; set; } = targetName;
}
