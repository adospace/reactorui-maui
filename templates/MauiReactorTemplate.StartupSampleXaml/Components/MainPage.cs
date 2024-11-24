using MauiReactorTemplate.StartupSampleXaml.Data;
using MauiReactorTemplate.StartupSampleXaml.Framework;
using MauiReactorTemplate.StartupSampleXaml.Models;
using MauiReactorTemplate.StartupSampleXaml.Resources.Styles;
using MauiReactorTemplate.StartupSampleXaml.Services;
using System.Threading.Tasks;

namespace MauiReactorTemplate.StartupSampleXaml.Components;

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
                new SfPullToRefresh
                {
                    RenderBody()
                }
                .IsRefreshing(State.IsRefreshing)
                .OnRefreshing(Refresh)
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

            await Task.Delay(100);

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

            await Task.Delay(1000);

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

    Task CleanTasks()
    {
        throw new NotImplementedException();
    }
}

partial class CategoryChart : Component
{

    [Prop]
    List<CategoryChartData>? _todoCategoryData;

    [Prop]
    List<Brush>? _todoCategoryColors;

    [Prop]
    bool _isBusy;

    public override VisualNode Render()
    {
        return Border(
            new SfShimmer
            {
                new SfCircularChart
                {
                    //Legend
                    new ChartLegend()
                    {
                        new ChartLegendLabelStyle()
                            .TextColor(Theme.IsLightTheme ? 
                                ResourceHelper.GetResource<Color>("DarkOnLightBackground") :
                                ResourceHelper.GetResource<Color>("LightOnDarkBackground"))
                            .Margin(5)
                            .FontSize(18)
                    }
                    .Placement(Syncfusion.Maui.Toolkit.LegendPlacement.Right),

                    //Series
                    new RadialBarSeries()
                        .ItemsSource(_todoCategoryData)
                        .PaletteBrushes(_todoCategoryColors ?? [])
                        .XBindingPath("Title")
                        .YBindingPath("Count")
                        .ShowDataLabels(true)
                        .EnableTooltip(true)
                        .TrackFill(Theme.IsLightTheme ?
                            ResourceHelper.GetResource<Color>("LightBackground") :
                            ResourceHelper.GetResource<Color>("DarkBackground"))
                        .CapStyle(Syncfusion.Maui.Toolkit.Charts.CapStyle.BothCurve),

                }
            }
            .CustomView(RenderCustomView())
            .IsActive(_isBusy)
            .BackgroundColor(Colors.Transparent)
            .VFill()
        )
        .Style(ResourceHelper.GetResource<Style>("CardStyle"))
        .Margin(0, 12)
        .HeightRequest(DeviceInfo.Idiom == DeviceIdiom.Phone ? 200 : 300);
    }

    static VisualNode RenderCustomView()
    {
        return Grid(
            BoxView()
                .CornerRadius(12)
                .VFill()
                .Style(ResourceHelper.GetResource<Style>("ShimmerCustomViewStyle"))
            );
    }
}


partial class ProjectCardView : Component
{
    [Prop]
    Project _project = default!;

    [Prop]
    bool _isBusy;

    public override VisualNode Render()
    {
        return Border(
            new SfShimmer
            {
                RenderContent()
            }
            .IsActive(_isBusy)
            .CustomView(RenderCustomView())
            .BackgroundColor(Colors.Transparent)
            .VFill()
        )
        .Style(ResourceHelper.GetResource<Style>("CardStyle"))
        .WidthRequest(200);
    }

    VStack RenderContent()
    {
        return VStack(spacing: 15,
            Image()
                .HStart()
                .Aspect(Aspect.Center)
                .Source(Icon(_project.Icon)),
            Label(_project.Name)
                .TextColor(ResourceHelper.GetResource<Color>("Gray400"))
                .FontSize(14)
                .TextTransform(TextTransform.Uppercase),
            Label(_project.Description)
                .LineBreakMode(LineBreakMode.WordWrap),
            HStack(spacing: 15,
                RenderTags().ToArray()
            )
        );
    }

    static FontImageSource Icon(string glyph)
        => new()
        {
            Glyph = glyph,
            FontFamily = "FluentUI",
            Color = Theme.IsLightTheme ?
                        ResourceHelper.GetResource<Color>("DarkOnLightBackground") :
                        ResourceHelper.GetResource<Color>("LightOnDarkBackground"),
            Size = ResourceHelper.GetResource<OnIdiom<double>>("IconSize")
        };

    IEnumerable<VisualNode> RenderTags()
    {
        foreach (var tag in _project?.Tags ?? Enumerable.Empty<Tag>())
        {
            yield return Border(
                Label(tag.Title)
                    .TextColor(Theme.IsLightTheme ?
                        ResourceHelper.GetResource<Color>("LightBackground") :
                        ResourceHelper.GetResource<Color>("DarkBackground"))
                    .FontSize(14)
                    .VCenter()
                    .VerticalTextAlignment(TextAlignment.Center)
            )
            .Padding(12, 0, 12, 8)
            .OnAndroid(_ => _.Padding(12, 0, 12, 0))
            .StrokeCornerRadius(16)
            .HeightRequest(32)
            .StrokeThickness(0)
            .Background(tag.DisplayColor);
        }
    }

    static VStack RenderCustomView()
    {
        return VStack(spacing: 15,
            BoxView()
                .CornerRadius(48)
                .WidthRequest(24)
                .HeightRequest(24)
                .Center()
                .Style(ResourceHelper.GetResource<Style>("ShimmerCustomViewStyle")),
            BoxView()
                .HeightRequest(24)
                .Style(ResourceHelper.GetResource<Style>("ShimmerCustomViewStyle")),
            BoxView()
                .HeightRequest(48)
                .Style(ResourceHelper.GetResource<Style>("ShimmerCustomViewStyle")),
            BoxView()
                .HeightRequest(24)
                .Margin(new Thickness(0, 12))
                .Style(ResourceHelper.GetResource<Style>("ShimmerCustomViewStyle"))
        );
    }
}


partial class TaskView : Component
{
    [Inject]
    TaskRepository _taskRepository;

    [Prop]
    bool _isBusy;

    [Prop]
    ProjectTask _task = default!;

    public override VisualNode Render()
    {
        return Border(
            new SfEffectsView
            {
                new SfShimmer
                {
                    RenderContent()
                }
                .CustomView(RenderCustomView())
                .IsActive(_isBusy)
                .BackgroundColor(Colors.Transparent)
                .VFill()
            }
            .TouchDownEffects(Syncfusion.Maui.Toolkit.EffectsView.SfEffects.Highlight)
            .HighlightBackground(Theme.IsLightTheme ?
                ResourceHelper.GetResource<Color>("DarkOnLightBackground") :
                ResourceHelper.GetResource<Color>("LightOnDarkBackground"))
        )
        .StrokeCornerRadius(20)
        .Background(Theme.IsLightTheme ?
            ResourceHelper.GetResource<Color>("LightSecondaryBackground") :
            ResourceHelper.GetResource<Color>("DarkSecondaryBackground"));
    }

    VisualNode RenderContent()
    {
        return Grid("*", "Auto,*",
            CheckBox()
                .GridColumn(0)
                .IsChecked(_task.IsCompleted)
                .VerticalOptions(LayoutOptions.Center)
                .OnCheckedChanged(OnTaskCheckCompletedChanged),
            Label()
                .GridColumn(1)
                .Text(_task.Title)
                .VerticalOptions(LayoutOptions.Center)
                .LineBreakMode(LineBreakMode.TailTruncation)
        )
        .OnTapped(NavigateToTask)
        .ColumnSpacing(15)
        .Padding(DeviceInfo.Idiom == DeviceIdiom.Desktop ? new Thickness(20) : new Thickness(15));
    }

    async Task OnTaskCheckCompletedChanged(CheckedChangedEventArgs e)
    {
        if (_task.IsCompleted != e.Value)
        {
            _task.IsCompleted = e.Value;
            await _taskRepository.SaveItemAsync(_task);
        }
    }

    Task NavigateToTask() 
        => MauiControls.Shell.Current.GoToAsync($"task?id={_task.ID}");

    static VisualNode RenderCustomView()
    {
        return Grid("Auto,*", "*",
            BoxView()
                .WidthRequest(24)
                .HeightRequest(24)
                .Margin(new Thickness(12, 0))
                .Style(ResourceHelper.GetResource<Style>("ShimmerCustomViewStyle")),
            BoxView()
                .GridColumn(1)
                .HeightRequest(24)
                .Margin(new Thickness(12, 0))
                .Style(ResourceHelper.GetResource<Style>("ShimmerCustomViewStyle"))
        )
        .Padding(DeviceInfo.Idiom == DeviceIdiom.Desktop ? new Thickness(20) : new Thickness(15));
    }
}
