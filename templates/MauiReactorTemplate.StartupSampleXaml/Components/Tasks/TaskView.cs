using MauiReactorTemplate.StartupSampleXaml.Data;
using MauiReactorTemplate.StartupSampleXaml.Framework;
using MauiReactorTemplate.StartupSampleXaml.Models;
using MauiReactorTemplate.StartupSampleXaml.Resources.Styles;

namespace MauiReactorTemplate.StartupSampleXaml.Components.Tasks;

partial class TaskView : Component
{
    [Inject]
    TaskRepository _taskRepository;

    [Prop]
    bool _isBusy;

    [Prop]
    ProjectTask _task = default!;

    [Prop]
    Action _onTaskCompletionChanged;

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

            _onTaskCompletionChanged?.Invoke();
        }
    }

    Task NavigateToTask() 
        => Navigation.PushAsync<TaskDetailPage, TaskDetailPageProps>(props =>
        {
            props.TaskId = _task.ID;
        });

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
