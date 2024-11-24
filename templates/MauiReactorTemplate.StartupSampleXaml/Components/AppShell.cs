using MauiReactorTemplate.StartupSampleXaml.Components.Main;
using MauiReactorTemplate.StartupSampleXaml.Components.Meta;
using MauiReactorTemplate.StartupSampleXaml.Components.Projects;
using MauiReactorTemplate.StartupSampleXaml.Framework;
using MauiReactorTemplate.StartupSampleXaml.Resources.Styles;

namespace MauiReactorTemplate.StartupSampleXaml.Components;

public partial class AppShell : Component
{
    public override VisualNode Render()
        => Shell(
            ShellContent()
                .Title("Dashboard")
                .Route("main")
                .Icon(ResourceHelper.GetResource<FontImageSource>("IconDashboard"))
                .RenderContent(() => new MainPage()),

            ShellContent()
                .Title("Projects")
                .Route("projects")
                .Icon(ResourceHelper.GetResource<FontImageSource>("IconProjects"))
                .RenderContent(() => new ProjectListPage()),

            ShellContent()
                .Title("Manage Meta")
                .Route("manage")
                .Icon(ResourceHelper.GetResource<FontImageSource>("IconMeta"))
                .RenderContent(() => new ManageMetaPage())
        )
        .FlyoutFooter(
            Grid(
                new SfSegmentedControl
                {
                    new SfSegmentItem()
                        .ImageSource(ResourceHelper.GetResource<ImageSource>("IconLight")),
                    new SfSegmentItem()
                        .ImageSource(ResourceHelper.GetResource<ImageSource>("IconDark"))
                }
                .SelectedIndex(Theme.CurrentAppTheme == AppTheme.Light ? 0 : 1)
                .OnSelectionChanged((s, e) => Theme.UserTheme = e.NewIndex == 0 ? AppTheme.Light : AppTheme.Dark)
                .VerticalOptions(LayoutOptions.Center)
                .HorizontalOptions(LayoutOptions.Center)
                .SegmentWidth(40)
                .SegmentHeight(40)
            )
            .Padding(15)
        );
}
