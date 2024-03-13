using System.Collections.Generic;

namespace MauiReactor.Maps;

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.Map), implementItemTemplate: true)]
[ScaffoldChildren(typeof(Microsoft.Maui.Controls.Maps.Pin), nameof(Microsoft.Maui.Controls.Maps.Map.Pins))]
[ScaffoldChildren(typeof(Microsoft.Maui.Controls.Maps.Polygon), nameof(Microsoft.Maui.Controls.Maps.Map.MapElements))]
[ScaffoldChildren(typeof(Microsoft.Maui.Controls.Maps.Polyline), nameof(Microsoft.Maui.Controls.Maps.Map.MapElements))]
[ScaffoldChildren(typeof(Microsoft.Maui.Controls.Maps.Circle), nameof(Microsoft.Maui.Controls.Maps.Map.MapElements))]
public partial class Map
{
    //Dictionary<string, Microsoft.Maui.Controls.Maps.Pin> _pinElementItemMap = [];

    //protected override void OnAddChild(VisualNode widget, Microsoft.Maui.Controls.BindableObject childControl)
    //{
    //    //Validate.EnsureNotNull(NativeControl);
    //    var thisAsIMap = (IMap)this;
    //    if (childControl is Microsoft.Maui.Controls.Maps.Pin pinItem)
    //    {
    //        NativeControl.Pins.Insert(widget.ChildIndex, pinItem);
    //        //_pinElementItemMap[pinItem.Label] = pinItem;
    //    }
    //    //else if (childControl is Microsoft.Maui.Controls.Maps.Polygon polygonItem)
    //    //{
    //    //    NativeControl.MapElements.Insert(widget.ChildIndex, polygonItem);
    //    //    _polygonElementItemMap[childControl] = polygonItem;
    //    //}
    //    //else if (childControl is Microsoft.Maui.Controls.Maps.Polyline polylineItem)
    //    //{
    //    //    NativeControl.MapElements.Insert(widget.ChildIndex, polylineItem);
    //    //    _polylineElementItemMap[childControl] = polylineItem;
    //    //}
    //    //else if (childControl is Microsoft.Maui.Controls.Maps.Circle circleItem)
    //    //{
    //    //    NativeControl.MapElements.Insert(widget.ChildIndex, circleItem);
    //    //    _circleElementItemMap[childControl] = circleItem;
    //    //}

    //    base.OnAddChild(widget, childControl);
    //}

    //protected override void OnRemoveChild(VisualNode widget, Microsoft.Maui.Controls.BindableObject childControl)
    //{
    //    //Validate.EnsureNotNull(NativeControl);
    //    var thisAsIMap = (IMap)this;
    //    if (childControl is Microsoft.Maui.Controls.Maps.Pin pinItem)
    //    {
    //        NativeControl.Pins.Remove(pinItem);
    //    }
    //    //else if (_polygonElementItemMap.TryGetValue(childControl, out var polygonItem))
    //    //{
    //    //    NativeControl.MapElements.Remove(polygonItem);
    //    //}
    //    //else if (_polylineElementItemMap.TryGetValue(childControl, out var polylineItem))
    //    //{
    //    //    NativeControl.MapElements.Remove(polylineItem);
    //    //}
    //    //else if (_circleElementItemMap.TryGetValue(childControl, out var circleItem))
    //    //{
    //    //    NativeControl.MapElements.Remove(circleItem);
    //    //}

    //    base.OnRemoveChild(widget, childControl);
    //}
}

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.Pin))]
public partial class Pin
{
    protected override void MergeWith(VisualNode newNode)
    {
        base.OnUnmount();
    }
}

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.MapElement))]
public partial class MapElement
{
    protected override void MergeWith(VisualNode newNode)
    {
        base.OnUnmount();
    }
}

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.Polygon))]
public partial class Polygon
{
    protected override void MergeWith(VisualNode newNode)
    {
        base.OnUnmount();
    }
}

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.Polyline))]
public partial class Polyline
{
    protected override void MergeWith(VisualNode newNode)
    {
        base.OnUnmount();
    }
}

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.Circle))]
public partial class Circle
{
    protected override void MergeWith(VisualNode newNode)
    {
        base.OnUnmount();
    }
}
