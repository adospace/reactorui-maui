using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

//public class TestBug335NavigationPage : Component
//{
//    public override VisualNode Render() =>
//        NavigationPage(
//            new TestBug335Page()
//        );
//}

public class TestBug335PageState
{
    public int Value { get; set; }
}

public class TestBug335Page : Component<TestBug335PageState>
{
    public override VisualNode Render() =>
        NavigationPage(
            ContentPage(
                VStack(
                    Label($"Value: {State.Value}").FontSize(32),
                    Button("Move To Page").OnClicked(OpenChildPage)
                )
                .Center()
            )
            .Title("Main Page")
        );

    private async void OpenChildPage()
    {
        await Navigation.PushAsync<TestBug335ChildPage, TestBug335ChildPageProps>(_ =>
        {
            _.InitialValue = State.Value;
            _.OnValueSet = this.OnValueSetFromChilPage;
        });
    }

    private void OnValueSetFromChilPage(int newValue)
    {
        SetState(s => s.Value = newValue);
    }
}

public class TestBug335ChildPageState
{
    public int Value { get; set; }
}

public class TestBug335ChildPageProps
{
    public int InitialValue { get; set; }
    public Action<int>? OnValueSet { get; set; }
}

public class TestBug335ChildPage : Component<TestBug335ChildPageState, TestBug335ChildPageProps>
{
    protected override void OnMounted()
    {
        State.Value = Props.InitialValue;
        base.OnMounted();
    }

    public override VisualNode Render() =>
        ContentPage(
            VStack(
                    Entry()
                        .Text(State.Value.ToString())
                        .OnTextChanged(newText =>
                        {
                            if (int.TryParse(newText, out int value))
                            {
                                State.Value = value;
                            }
                        })
                        .Keyboard(Keyboard.Numeric),
                    Button("Back").OnClicked(GoBack)
            )
            .Center()
        )
        .Title("Child Page");

    private async void GoBack()
    {
        // This throws an error getting back to the home page
        // Cannot access a disposed object. Object name: 'Microsoft.Maui.Controls.Handlers.Compatibility.NavigationRenderer'.

        //Props.OnValueSet(State.Value);

        await Navigation!.PopAsync();

        // Here the navigation to the child page stops working after the first navigation back to the main page. 

        Props.OnValueSet?.Invoke(State.Value);
    }
}



//alternative approach using OnAppearing

public class TestBug335_2PageState
{
    public DateTime LastUpdated { get; set; }
}

public partial class TestBug335_2Page : Component<TestBug335_2PageState>
{
    public override VisualNode Render() =>
        NavigationPage(
            ContentPage(
                ScrollView(
                    VStack(
                            Label($"Last updated {State.LastUpdated:HH:mm:ss}")
                                .FontSize(32)
                                .HCenter(),
                            Button("Move to child page").HCenter().OnClicked(OpenChildPage)
                        )
                        .VCenter()
                        .Spacing(25)
                        .Padding(30, 0)
                )
            )
            .OnAppearing(ForceUpdate)
        );

    private async void OpenChildPage()
    {
        await Navigation.PushAsync<TestBug335_2ChildPage>();
    }

    private async Task ForceUpdate()
    {
        // simulate async call to database
        await Task.Delay(200);
        var lastUpdated = DateTime.Now;
        SetState(s => s.LastUpdated = lastUpdated);
    }
}

public class TestBug335_2ChildPage : Component
{
    public override VisualNode Render() =>
        ContentPage(Button("Back and Update").Center().OnClicked(GoBack));

    private async Task GoBack()
    {
        await Navigation!.PopAsync();
    }
}


