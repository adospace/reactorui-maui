using MauiReactorTemplate.StartupSampleXaml.Data;
using MauiReactorTemplate.StartupSampleXaml.Framework;
using MauiReactorTemplate.StartupSampleXaml.Models;
using MauiReactorTemplate.StartupSampleXaml.Resources.Styles;
using System.Threading.Tasks;

namespace MauiReactorTemplate.StartupSampleXaml.Components.Tasks;

class TaskDetailPageState
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public List<Project> Projects { get; set; } = [];
    public int SelectedProjectIndex { get; set; }

    public int? ProjectID { get; set; }
}

class TaskDetailPageProps
{
    public int? TaskId { get; set; }
}

partial class TaskDetailPage : Component<TaskDetailPageState, TaskDetailPageProps>
{
    [Inject]
    TaskRepository _taskRepository;

    [Inject]
    ProjectRepository _projectRepository;

    public override VisualNode Render()
    {
        return ContentPage(
            Props.TaskId.HasValue ? 
            ToolbarItem("Delete")
                .OnClicked(Delete)
                .Order(ToolbarItemOrder.Primary)
                .Priority(0)
                .IconImageSource(ResourceHelper.GetResource<ImageSource>("IconDelete")) : null,
            Grid(
                VScrollView(
                    VStack(

                        new SfTextInputLayout(
                            Entry()
                                .Text(State.Title)
                                .OnTextChanged(newText => State.Title = newText)
                        )
                        .Hint("Task"),

                        new SfTextInputLayout(
                            CheckBox()
                                .IsChecked(State.IsCompleted)
                                .OnCheckedChanged(newValue => State.IsCompleted = newValue)
                        )
                        .Hint("Completed"),

                        new SfTextInputLayout(
                            Picker()
                                .ItemsSource(State.Projects.Select(_=>_.Name).ToList())
                                .SelectedIndex(State.SelectedProjectIndex)
                                .OnSelectedIndexChanged(index => State.SelectedProjectIndex = index)
                        )
                        .IsVisible(!State.ProjectID.HasValue)
                        .Hint("Project"),

                        Button()
                            .Text("Save")
                            .HeightRequest(DeviceInfo.Current.Idiom == DeviceIdiom.Desktop ? 60 : 44)
                            .OnClicked(Save)
                    )                    
                )                
            )
        )
        .OnAppearing(LoadTask);
    }

    async Task LoadTask()
    {
        if (Props.TaskId.HasValue)
        {
            var task = await _taskRepository.GetAsync(Props.TaskId.Value) ?? throw new InvalidOperationException();
            SetState(s =>
            {
                s.Title = task.Title;
                s.IsCompleted = task.IsCompleted;
                s.ProjectID = task.ProjectID;
            });            
        }
        else
        {
            var projects = await _projectRepository.ListAsync();
            SetState(s => s.Projects = projects);
        }
    }

    async Task Save()
    {
        await _taskRepository.SaveItemAsync(new ProjectTask
        { 
            ID = Props.TaskId ?? 0,
            IsCompleted = State.IsCompleted,
            ProjectID = State.ProjectID ?? State.Projects[State.SelectedProjectIndex].ID,
            Title = State.Title
        });

        await Navigation.PopAsync();

        await AppUtils.DisplayToastAsync(Props.TaskId.HasValue ? "Task saved" : "Task created");
    }

    async Task Delete()
    {
        if (Props.TaskId == null)
            return;

        if (!await ContainerPage.DisplayAlert("Delete task", "Are you sure you want to delete this task?", "Yes", "No"))
            return;

        await _taskRepository.DeleteItemAsync(Props.TaskId.Value);
        
        await Navigation.PopAsync();

        await AppUtils.DisplayToastAsync("Task deleted");
    }
}
