using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

public class TestBug220ShellPage : Component
{
    private MauiControls.Shell? shell;

    /// <inheritdoc />
    public override VisualNode Render() =>
        new Shell(x => shell = x!)
            {
                new ShellContent("Models")
                    .AutomationId("Models")
                    //.Icon(FontImages.Feature)
                    .RenderContent(() => new ListPage().Shell(shell))
            }
            .AutomationId("MainShell");
}

public record Model(string Id, string Name);

public partial class ListPage : Component<ListPage.PageState>
{
    [Prop("Shell")] protected MauiControls.Shell? shellRef;

    public override VisualNode Render() =>
        new ContentPage
        {
            new CollectionView()
                .AutomationId("list")
                .ItemsSource(State.Items, Render)
        }
        .AutomationId("Models_page");

    protected override void OnMounted()
    {
        Routing.RegisterRoute<TestBug220ModelPage>("model");

        Task.Run(
            async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                SetState(s => s.Items = [new Model("m1", "model name 1")]);
            });
        base.OnMounted();
    }

    private VisualNode Render(Model item) =>
        new VStack(5)
            {
                new Label(item.Name).AutomationId(item.Id + "-name"),
                new Label(item.Id).AutomationId(item.Id + "-id")
                    .FontSize(12)
                    .TextColor(Colors.Gray)
            }.AutomationId(item.Id + "-stack")
            .OnTapped(
                async () => await shellRef!.GoToAsync<TestBug220ModelPage.Props2>(
                    "model",
                    props => props.Id = item.Id))
            .Margin(5, 10);

    public class PageState
    {
        public Model[] Items { get; set; } =
        [
        ];
    }
}

public class TestBug220ModelPage : Component<TestBug220ModelPage.PageState, TestBug220ModelPage.Props2>
{
    public override VisualNode Render() =>
        new ContentPage("Model")
        {
            new Label(State.Item?.Name)
                .AutomationId(State.Item?.Id ?? string.Empty)
                .VCenter()
                .HCenter()
        };

    protected override void OnMountedOrPropsChanged()
    {
        Task.Run(
            async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                SetState(s => s.Item = new Model("m2", "model name 2"));
            });

        base.OnMounted();
    }

    public class PageState
    {
        public Model? Item { get; set; }
    }

    public class Props2
    {
        public string Id { get; set; } = null!;
    }
}