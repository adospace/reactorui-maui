namespace MauiReactor.Maps;

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.Map), implementItemTemplate: true)]
[ScaffoldChildren(typeof(Microsoft.Maui.Controls.Maps.Pin), nameof(Microsoft.Maui.Controls.Maps.Map.Pins))]
[ScaffoldChildren(typeof(Microsoft.Maui.Controls.Maps.Polygon), nameof(Microsoft.Maui.Controls.Maps.Map.MapElements))]
[ScaffoldChildren(typeof(Microsoft.Maui.Controls.Maps.Polyline), nameof(Microsoft.Maui.Controls.Maps.Map.MapElements))]
[ScaffoldChildren(typeof(Microsoft.Maui.Controls.Maps.Circle), nameof(Microsoft.Maui.Controls.Maps.Map.MapElements))]
public partial class Map
{
}

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.Pin))]
public partial class Pin
{
}

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.MapElement))]
public partial class MapElement
{
}

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.Polygon))]
public partial class Polygon
{
}

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.Polyline))]
public partial class Polyline
{
}

[Scaffold(typeof(Microsoft.Maui.Controls.Maps.Circle))]
public partial class Circle
{
}
