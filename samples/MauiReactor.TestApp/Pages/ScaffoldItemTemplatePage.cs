using MauiReactor.TestApp.Controls;

namespace MauiReactor.TestApp.Pages;

public sealed class ScaffoldItemTemplatePage : Component
{
    public override VisualNode Render() =>
        ContentPage(
            "Scaffold ItemTemplate Test",
            VStack(
                Label("Above the scaffold control").AutomationId("TopLabel"),
                new ScaffoldedTestItemsControl()
                    .AutomationId("TestItems")
                    .ItemsSource(
                        ["Item1", "Item2", "Item3"],
                        item => Label(item).AutomationId($"ItemLabel_{item}")
                    )
            )
        );
}

public sealed class ScaffoldItemTemplateEmptyPage : Component
{
    public override VisualNode Render() =>
        ContentPage(
            "Scaffold ItemTemplate Empty Test",
            VStack(
                new ScaffoldedTestItemsControl().AutomationId("EmptyTestItems"),
                Label("Below the scaffold control").AutomationId("BottomLabel")
            )
        );
}
