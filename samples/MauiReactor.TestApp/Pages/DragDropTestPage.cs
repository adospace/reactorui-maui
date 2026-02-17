using MauiReactor.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

public enum TaskState
{
    New,
    Progress,
    Review,
    Done
}

public class TaskItem
{
    public required string Name { get; set; }

    public required TaskState State { get; set; }
}

class DragDropTestPageState
{
    public int NewTaskCount { get; set; }

    public TaskState SelectedOption { get; set; }

    public bool IsBusy { get; set; }

    public List<TaskItem> Tasks { get; set; } = new();

    public TaskItem? DraggingItem { get; set; }  
}

class DragDropTestPage : Component<DragDropTestPageState>
{
    protected override void OnMounted()
    {
        State.Tasks.AddRange(new[]
        {
            new TaskItem { Name = "Task 1", State = TaskState.New },
            new TaskItem { Name = "Task 2", State = TaskState.New },
            new TaskItem { Name = "Task 3", State = TaskState.Review },
            new TaskItem { Name = "Task 4", State = TaskState.New },
            new TaskItem { Name = "Task 5", State = TaskState.Progress },
            new TaskItem { Name = "Task 6", State = TaskState.Done },
            new TaskItem { Name = "Task 7", State = TaskState.Done },
            new TaskItem { Name = "Task 8", State = TaskState.Progress },
            new TaskItem { Name = "Task 9", State = TaskState.Review },
        });
        base.OnMounted();
    }

    public override VisualNode Render()
    {
        return new ContentPage("Drag & Drop")
        {
            new VStack
            {
                new VStack()
                {
                    Enum.GetValues<TaskState>().Select(state =>
                    {
                        return new Border
                        {
                            new VStack
                            {
                                new Label(state),
                                new Label(State.Tasks.Count(_=>_.State == state))
                            }
                        }
                        .BackgroundColor(State.SelectedOption == state ? Colors.LightGray : Colors.White)
                        .Padding(10)
                        .Margin(5)
                        .OnTapped(()=>SetState(s => s.SelectedOption = state))
                        .AllowDrop(State.SelectedOption != state)
                        .OnDrop(async ()=>
                        {
                            SetState(s => s.IsBusy = true);

                            await Task.Delay(500);

                            SetState(s =>
                            {
                                if (s.DraggingItem != null)
                                {
                                    s.DraggingItem.State = state;
                                    s.DraggingItem = null;
                                }

                                s.IsBusy = false;
                            });
                        });
                    })
                },


                new ActivityIndicator()
                    .IsVisible(State.IsBusy)
                    .IsRunning(true)
                    .HeightRequest(30)
                    .WidthRequest(30),

                new CollectionView()
                    .ItemsSource(State.Tasks.Where(_=>_.State == State.SelectedOption), taskItem => 
                        new Border
                        {
                            new Label(taskItem.Name),
                        }
                        .Padding(10,20)
                        .Margin(0,5)
                        .OnDragStarting(() => SetState(s => s.DraggingItem = taskItem))
                        )
                    .VFill()
                    .Margin(0,20,0,0)
            }
            .Padding(10)
        };
    }
}
