using MauiReactor.TestApp.Controls;

namespace MauiReactor.TestApp.Pages;

public sealed class ScaffoldItemTemplatePage : Component
{
    public override VisualNode Render() =>
        ContentPage(
            "Scaffold ItemTemplate Test",
            new ScaffoldedTestItemsControl().ItemsSource(
                ["Item1", "Item2", "Item3"],
                item => Label(item)
            )
        );
}

public sealed class ScaffoldItemTemplateFailingPage : Component
{
    public override VisualNode Render() =>
        ContentPage(
            "Scaffold ItemTemplate Failing Test",
            new ScaffoldedTestItemsControl(native => native?.SimulateHandlerDependency = true)
                .AutomationId("TestItems")
                .ItemsSource(["Item1"], item => Label(item))
        );
}

public sealed class ScaffoldItemTemplateWithoutItemsSourcePage : Component
{
    public override VisualNode Render() =>
        ContentPage(
            "Scaffold ItemTemplate Without ItemsSource Test",
            new ScaffoldedTestItemsControl()
        );
}
