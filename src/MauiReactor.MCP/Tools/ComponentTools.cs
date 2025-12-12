using MauiReactor.MCP.Services;
using ModelContextProtocol;
using ModelContextProtocol.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Logging;

internal class ComponentTools
{
    private readonly Microsoft.Extensions.Logging.ILogger<ComponentTools> _logger;

    public ComponentTools(Microsoft.Extensions.Logging.ILogger<ComponentTools> logger)
    {
        _logger = logger;
        _logger.LogInformation("ComponentTools initialized.");
    }
    public record ComponentInfo(string Name, string Description, string? Alias = null);

    public record ConstructorInfo(string Signature, string Description);

    public record ComponentDetailedInfo(string Name, string Description, string? Base, PropertyInfo[] Properties, EventInfo[] Events, ConstructorInfo[] Constructors, PropertyInfo[] AttachedProperties);

    public record PropertyInfo(string Name, string Type, string Description);

    public record EventInfo(string Name, string EventArgsType, string Description);


    [McpServerTool(Name = "enumerate-standard-components")]
    [Description("Enumerate all the standard components provided by MauiReactor along with a small description.")]
    public ComponentInfo[] EnumerateStandardComponents()
    {
        _logger.LogInformation("enumerate-standard-components invoked");
        return
        [
            new("DatePicker","Control to select a date."),
            new("TimePicker","Control to select a time."),
            new("Page","Base page component."),
            new("ContentPage","Page that hosts a single content view."),
            new("NavigationPage","Provides navigation stack behavior."),
            new("TabbedPage","Hosts multiple tabs."),
            new("FlyoutPage","Page with a flyout (master/detail) layout."),
            new("TemplatedPage","Page that supports a ControlTemplate."),
            new("Shell","Application shell for routing, flyouts and tabs."),
            new("BaseShellItem","Base shell item used by shell structure."),
            new("ShellGroupItem","Group container for shell items."),
            new("ShellItem","Item in a Shell representing a logical section."),
            new("ShellSection","Section inside a ShellItem."),
            new("ShellContent","Shell content holder with templating support."),
            new("TabBar","Container for tabs in Shell."),
            new("Tab","Individual tab element."),

            new("Button","Interactive clickable control with text or image and click events."),
            new("ImageButton","Button that displays an image."),
            new("Label","Text display control with styling and formatting."),
            new("Entry","Single-line text input control."),
            new("Editor","Multi-line text input control."),
            new("SearchBar","Search input with built-in UI behavior."),
            new("Picker","Drop-down selection control."),
            new("DatePicker","Date selection control (listed above)."),
            new("TimePicker","Time selection control (listed above)."),

            new("StackLayout","Vertical or horizontal stack layout for arranging children."),
            new("VerticalStackLayout","Convenience vertical-only stack layout.", "VStack"),
            new("HorizontalStackLayout","Convenience horizontal-only stack layout.", "HStack"),
            new("Grid","Flexible row/column layout with attached placement properties."),
            new("AbsoluteLayout","Position children using absolute coordinates."),
            new("FlexLayout","Flexible box layout for complex arrangements."),
            new("ScrollView","Scrollable container for a single child."),
            new("ContentView","Container that hosts a single child view."),
            new("TemplatedView","View that supports a ControlTemplate."),
            new("Border","Container that draws a border and can host a child."),
            new("BoxView","Simple rectangular colored box."),
            new("Frame","Container with optional corner radius and shadow (accessed via Border/Shadow)."),

            new("CollectionView","High-performance items view for presenting collections."),
            new("GroupableItemsView","Collection view supporting grouping."),
            new("ReorderableItemsView","Collection view supporting reordering."),
            new("SelectableItemsView","Collection view supporting selection."),
            new("StructuredItemsView","Base for structured items views."),
            new("CarouselView","Paged carousel-style items view."),
            new("ListView","Legacy list view supporting templates (still available)."),
            new("IndicatorView","Indicator for paging controls."),

            new("Image","Displays an image from file, resource or URI."),
            new("GraphicsView","Host for immediate-mode graphics drawing."),
            new("HybridWebView","WebView with hybrid/native bridge support."),
            new("WebView","Control to render web content."),

            new("SwipeView","Container enabling swipe-to-reveal actions."),
            new("SwipeItem","Action revealed by a SwipeView."),
            new("SwipeItemView","View used inside swipe actions."),
            new("SwipeItems","Collection of swipe items."),

            new("MenuItem","Menu item used in menus and toolbars."),
            new("MenuBarItem","Top-level menu bar item."),
            new("MenuFlyout","Context/menu flyout container."),
            new("MenuFlyoutItem","Item inside a MenuFlyout."),
            new("MenuFlyoutSubItem","Sub-item inside a MenuFlyout."),
            new("MenuFlyoutSeparator","Separator used inside menu flyouts."),
            new("ToolbarItem","Item shown in a page toolbar."),

            new("Shadow","Declarative shadow settings for visuals."),
            new("TitleBar","Platform title bar customization element."),

            new("CheckBox","Checkbox control for boolean selection."),
            new("RadioButton","Radio button for mutually exclusive selection."),
            new("Switch","On/off toggle control."),
            new("Slider","Control to select a numeric value by sliding."),
            new("Stepper","Control to increment/decrement numeric values."),

            new("ActivityIndicator","Spinner to indicate ongoing work."),
            new("ProgressBar","Visual indicator for progress values."),
            new("IndicatorView","Paging indicator for collection controls."),

            new("Entry","Single-line input (listed above)."),
            new("Editor","Multi-line input (listed above)."),

            new("Shapes.EllipseGeometry","Geometry primitive for ellipse shapes."),
            new("Shapes.LineGeometry","Geometry primitive for lines."),
            new("Shapes.RectangleGeometry","Geometry primitive for rectangles."),
            new("Shapes.PathGeometry","Geometry primitive for arbitrary paths."),
            new("Shapes.RoundRectangleGeometry","Geometry primitive for rounded rectangles."),

            new("Shapes.RoundRectangle","Round-rectangle shape element."),
            new("Shapes.Line","Line shape primitive."),
            new("Shapes.Path","Path shape primitive."),
            new("Shapes.Polygon","Polygon shape primitive."),
            new("Shapes.Polyline","Polyline shape primitive."),
            new("Shapes.Rectangle","Rectangle shape primitive."),
            new("Shapes.GeometryGroup","Group of geometries."),
            new("Shapes.Ellipse","Ellipse shape primitive."),

            new("GestureRecognizer","Base for gesture recognizers."),
            new("TapGestureRecognizer","Recognizes tap gestures."),
            new("PanGestureRecognizer","Recognizes pan gestures and deltas."),
            new("PinchGestureRecognizer","Recognizes pinch/scale gestures."),
            new("PointerGestureRecognizer","Recognizes pointer enter/exit/move/press events."),
            new("DragGestureRecognizer","Recognizes drag gestures and supports drag start/drop."),
            new("DropGestureRecognizer","Recognizes drop gestures."),
            new("SwipeGestureRecognizer","Recognizes swipe gestures."),

            new("GestureElement","Low-level element for composing input/gesture behaviors."),

            new("Span","Inline span for formatted text."),
            new("FormattedString","Formatted text container for Label/Span."),

            new("Graphics/Canvas (CanvasView / CanvasNode)","Low-level retained-mode canvas and nodes for custom drawing (MauiReactor.Canvas)."),
            new("Timer","Lightweight timer node that can trigger Tick actions on a schedule."),
            new("AnimationController","Coordinates and runs animations declared in the visual tree."),

            new("Shadow","Visual shadow primitive (listed above)."),

            new("Window","Top-level application window host."),
            new("HybridWebView","Hybrid web bridge (listed above)."),

            new("GestureElement","Gesture composition element (listed above)."),

            new("Span","Inline text span (listed above)."),
            new("FormattedString","Formatted string container (listed above)."),
        ];
    }

    [McpServerTool(Name = "get-component-info")]
    [Description("Get detailed info for a standard component by name.")]
    public ComponentDetailedInfo GetComponentInfo(string componentName)
    {
        _logger.LogInformation("get-component-info invoked with componentName={ComponentName}", componentName);
        if (string.IsNullOrWhiteSpace(componentName))
        {
            throw new ArgumentException("Component name must be provided.", nameof(componentName));
        }

        var asm = Assembly.GetExecutingAssembly();
        var resourceNames = asm.GetManifestResourceNames();
        _logger.LogDebug("Assembly manifest contains {Count} resources", resourceNames.Length);

        // Find a resource that ends with Tools.Resources.Components.{componentName}.json
        var targetSuffix = $"Tools.Resources.Components.{componentName}.json";
        string? resourceName = resourceNames.FirstOrDefault(_=>_.EndsWith(targetSuffix, StringComparison.OrdinalIgnoreCase));

        if (resourceName is null)
        {
            _logger.LogWarning("Component info not found for {ComponentName}", componentName);
            throw new InvalidOperationException($"Component info not found for '{componentName}'.");
        }

        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        if (stream is null)
        {
            _logger.LogError("Failed to read resource stream for {ComponentName} (resource={ResourceName})", componentName, resourceName);
            throw new InvalidOperationException($"Failed to read embedded resource for '{componentName}'.");
        }

        _logger.LogDebug("Opened resource stream for {ComponentName}. CanRead={CanRead}", componentName, stream.CanRead);

        // For additional visibility, log the resource size if seekable
        if (stream.CanSeek)
        {
            var length = stream.Length;
            _logger.LogDebug("Resource stream length for {ComponentName} is {Length} bytes", componentName, length);
            stream.Position = 0;
        }

        var detailedInfo = JsonSerializer.Deserialize<ComponentDetailedInfo>(stream);
        if (detailedInfo is null)
        {
            _logger.LogError("Invalid component info JSON for {ComponentName}", componentName);
            throw new InvalidOperationException($"Invalid component info JSON for '{componentName}'.");
        }

        _logger.LogInformation("get-component-info succeeded for {ComponentName}", componentName);
        return detailedInfo;
    }


}
