using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

//for the binding to work we can't use a record, so using a class with read-write properties
public class TheItem(string id, string name)
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
}

class PickerPageState
{
    public DateTime TimeStamp { get; set; } = DateTime.Now;

    public List<string> StringItems { get; set; } = ["Linear", "Radial"];

    public int SelectedStringItemIndex { get; set; }
}

class PickerPage : Component<PickerPageState>
{
    public override VisualNode Render()
    {
        return ContentPage("Pickers Page",
            VStack(spacing: 10,
                DatePicker()
                    .Date(State.TimeStamp)
                    .OnDateSelected(newDate =>
                    {
                        if (newDate != null)
                        {
                            SetState(s => s.TimeStamp = new DateTime(DateOnly.FromDateTime(newDate.Value), TimeOnly.FromDateTime(s.TimeStamp)));
                        }
                    }),

                TimePicker()
                    .Time(State.TimeStamp.TimeOfDay)
                    .OnTimeSelected(newTime =>
                    {
                        if (newTime != null)
                        {
                            SetState(s => s.TimeStamp = new DateTime(DateOnly.FromDateTime(s.TimeStamp), TimeOnly.FromTimeSpan(newTime.Value)));
                        }
                    }),

                new Picker()
                    .ItemsSource(State.StringItems)
                    .SelectedIndex(State.SelectedStringItemIndex)
                    .OnSelectedIndexChanged(selectedIndex => SetState(s => s.SelectedStringItemIndex = selectedIndex)),
                
                new Picker()
                    .Title("Select Pose")
                    // just noticed that this ItemsSource should allow IEnumerable<object> and not just string since Picker's ItemsSource is IList
                    // I do realize I could override ToString(), but the native control allows more flexibility
                    .ItemsSource([new TheItem("1", "Me"), new TheItem("1", "You")]) // want to be able to provide a list of objects.  hard-coded in example, but could be in state.  I now I could keep a list of strings also in state by using Select( x=>x.Name), but I don't think I should have to.
                    .ItemDisplayBinding(MauiControls.Binding.Create(static (TheItem item) => item.Name)) // want to be able to specify which property to display
                    .OnSelectedIndexChanged((s, e) =>
                    {
                        if (s is null)
                            return;
                        var item = (TheItem)((MauiControls.Picker)s).SelectedItem;

                        // do something with item.  
                        Console.WriteLine($"Selected: {item.Name}");
                    })

                )
                .Center()
            );
    }
}
