using MauiReactorTemplate.StartupSampleXaml.Data;
using MauiReactorTemplate.StartupSampleXaml.Models;
using MauiReactorTemplate.StartupSampleXaml.Resources.Styles;

namespace MauiReactorTemplate.StartupSampleXaml.Components.Projects;

class ProjectListPageState
{
    public List<Project> Projects { get; set; } = [];
}

partial class ProjectListPage : Component<ProjectListPageState>
{
    [Inject]
    ProjectRepository _projectRepository;

    public override VisualNode Render()
    {
        return ContentPage("Projects",
            Grid(
                VStack(
                    State.Projects.Select(RenderProjectItem)
                )
                .Spacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing"))
                .Padding(ResourceHelper.GetResource<OnIdiom<Thickness>>("LayoutPadding")),

                Button()
                    .ThemeKey("AddButton")
                    .OnClicked(AddProject)
            )
        )
        .OnAppearing(LoadProjects);
    }

    VisualNode RenderProjectItem(Project project, int index)
    {
        return Border(
            VStack(
                Label(project.Name)
                    .FontSize(24),
                Label(project.Description)                
            )
            .Padding(10)
        )
        .OnTapped(() => Navigation.PushAsync<ProjectDetailPage, ProjectDetailPageProps>(props => props.ProjectID = project.ID));
    }
      
    async Task LoadProjects()
    {
        var projects = await _projectRepository.ListAsync();
        SetState(s => s.Projects = projects);
    }

    async Task AddProject()
        => await Navigation.PushAsync<ProjectDetailPage>();
}