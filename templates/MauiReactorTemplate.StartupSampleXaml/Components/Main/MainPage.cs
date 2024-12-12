using CommunityToolkit.Maui.Core;
using MauiReactorTemplate.StartupSampleXaml.Components.Tasks;
using MauiReactorTemplate.StartupSampleXaml.Data;
using MauiReactorTemplate.StartupSampleXaml.Framework;
using MauiReactorTemplate.StartupSampleXaml.Models;
using MauiReactorTemplate.StartupSampleXaml.Resources.Styles;
using MauiReactorTemplate.StartupSampleXaml.Services;
using System.Threading.Tasks;

namespace MauiReactorTemplate.StartupSampleXaml.Components.Main;

class MainPageState
{
    public bool IsBusy { get; set; }

    public bool IsRefreshing { get; set; }

    public bool IsNavigatingTo { get; set; }

    public bool DataLoaded { get; set; }

    public List<Project> Projects { get; set; } = [];

    public List<ProjectTask> Tasks { get; set; } = [];

    public List<CategoryChartData> TodoCategoryData { get; set; } = [];

    public List<Brush> TodoCategoryColors { get; set; } = [];
}

partial class MainPage : Component<MainPageState>
{
    [Inject]
    SeedDataService _seedDataService;

    [Inject]
    ModalErrorHandler _errorHandler;

    [Inject]
    ProjectRepository _projectRepository;

    [Inject]
    CategoryRepository _categoryRepository;

    [Inject]
    TaskRepository _taskRepository;

    public override VisualNode Render()
    {
        return ContentPage(DateTime.Now.ToLongDateString(),
            Grid(
                new SfPullToRefresh(
                    RenderBody()
                )
                .IsRefreshing(State.IsRefreshing)
                .OnRefreshing(Refresh),

                Button()
                    .ThemeKey("AddButton")
                    .IsEnabled(!State.IsBusy)
                    .OnClicked(AddTask)
            )
        )
        .OnNavigatedTo(() => State.IsNavigatingTo = true)  
        .OnNavigatedFrom(() => State.IsNavigatingTo = false)
        .OnAppearing(LoadOrRefreshData);  
    }  

    VisualNode RenderBody()
    {
        return VScrollView(
            VStack(
                new CategoryChart()
                    .IsBusy(State.IsBusy)
                    .TodoCategoryData(State.TodoCategoryData)
                    .TodoCategoryColors(State.TodoCategoryColors),

                Label("Projects")
                    .Style(ResourceHelper.GetResource<Style>("Title2")),

                HScrollView(
                    HStack(
                        State.Projects.Select(project => new ProjectCardView()
                            .Project(project)
                            .IsBusy(State.IsBusy)
                        ).ToArray()
                    )
                    .Spacing(15)
                    .Padding(new Thickness(30, 0))
                )
                .Margin(new Thickness(-30, 0)),

                Grid(
                    Label("Tasks")
                            .Style(ResourceHelper.GetResource<Style>("Title2"))
                            .VerticalOptions(LayoutOptions.Center),
                    ImageButton()
                        .Source(ResourceHelper.GetResource<ImageSource>("IconClean"))
                        .HorizontalOptions(LayoutOptions.End)
                        .VerticalOptions(LayoutOptions.Center)
                        .Aspect(Aspect.Center)
                        .HeightRequest(44)
                        .WidthRequest(44)
                        .IsVisible(State.Tasks.Any(t => t.IsCompleted))
                        .OnClicked(CleanTasks)
                )
                .HeightRequest(44),

                VStack(
                    State.Tasks.Select(task => new TaskView()
                        .Task(task)
                        .IsBusy(State.IsBusy)
                        .OnTaskCompletionChanged(Invalidate)
                    ).ToArray()
                )
                .Spacing(15)

            )
            .Spacing(ResourceHelper.GetResource<OnIdiom<double>>("LayoutSpacing"))
            .Padding(ResourceHelper.GetResource<OnIdiom<Thickness>>("LayoutPadding"))
        );
    }

    async Task LoadOrRefreshData()
    {
        if (!State.DataLoaded)
        {
            bool isSeeded = Preferences.Default.ContainsKey("is_seeded");

            if (!isSeeded)
            {
                await _seedDataService.LoadSeedDataAsync();
            }

            Preferences.Default.Set("is_seeded", true);

            await Refresh();
        }
        // This means we are being navigated to
        else if (!State.IsNavigatingTo)
        {
            await Refresh();
        }
    }

    async Task LoadData()
    {
        try
        {
            SetState(s => s.IsBusy = true);

            State.Projects = await _projectRepository.ListAsync();

            var chartData = new List<CategoryChartData>();
            var chartColors = new List<Brush>();

            var categories = await _categoryRepository.ListAsync();
            foreach (var category in categories)
            {
                chartColors.Add(category.ColorBrush);

                var ps = State.Projects.Where(p => p.CategoryID == category.ID).ToList();
                int tasksCount = ps.SelectMany(p => p.Tasks).Count();

                chartData.Add(new(category.Title, tasksCount));
            }

            State.TodoCategoryData = chartData;
            State.TodoCategoryColors = chartColors;

            State.Tasks = await _taskRepository.ListAsync();
        }
        finally
        {
            SetState(s => s.IsBusy = false);
        }
    }

    async Task Refresh()
    {
        try
        {
            SetState(s => s.IsRefreshing = true);

            await LoadData();
        }
        catch (Exception e)
        {
            _errorHandler.HandleError(e);
        }
        finally
        {
            SetState(s => s.IsRefreshing = false);
        }
    }

    async Task CleanTasks()
    {
        var completedTasks = State.Tasks.Where(t => t.IsCompleted).ToArray();
        foreach (var task in completedTasks)
        {
            await _taskRepository.DeleteItemAsync(task);
        }

        SetState(s => s.Tasks.RemoveAll(t => t.IsCompleted));

        await AppUtils.DisplayToastAsync("All cleaned up!");
    }

    async Task AddTask()
        => await Navigation.PushAsync<TaskDetailPage>();
}
