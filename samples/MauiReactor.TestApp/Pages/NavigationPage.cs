using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

public class NavigationMainPageState
{
    public int Value { get; set; }
}

public class NavigationMainPage : Component<NavigationMainPageState>
{
    protected override void OnMounted()
    {
        System.Diagnostics.Debug.WriteLine("NavigationMainPage.OnMounted()");

        base.OnMounted();
    }

    protected override void OnWillUnmount()
    {
        System.Diagnostics.Debug.WriteLine("NavigationMainPage.OnWillUnmount()");

        base.OnWillUnmount();
    }

    public override VisualNode Render()
    {
        return new ContentPage()
        {
            new StackLayout()
            {
                new Label($"Value: {State.Value}")
                    .AutomationId("MainPage_Label") //<-- required for sample test
                    ,
                new Button("Move To Page")
                    .AutomationId("MoveToChildPage_Button") //<-- required for sample test
                    .OnClicked(OpenChildPage)
            }
            .VCenter()
            .HCenter()
        }
        .Title("Main Page");
    }

    private async Task OpenChildPage()
    {
        await Navigation.PushAsync<ChildPage, ChildPageProps>(_ =>
        {
            _.InitialValue = State.Value;
            _.OnValueSet = this.OnValueSetFromChildPage;
        });
    }

    private void OnValueSetFromChildPage(int newValue)
    {
        SetState(s => s.Value = newValue);
    }
}

public class ChildPageState
{
    public int Value { get; set; }
}

public class ChildPageProps
{
    public int InitialValue { get; set; }

    public Action<int>? OnValueSet { get; set; }
}

public class ChildPage : Component<ChildPageState, ChildPageProps>
{
    protected override void OnMounted()
    {
        State.Value = Props.InitialValue;
        System.Diagnostics.Debug.WriteLine("ChildPage.OnMounted()");

        base.OnMounted();
    }

    protected override void OnWillUnmount()
    {
        System.Diagnostics.Debug.WriteLine("ChildPage.OnWillUnmount()");

        base.OnWillUnmount();
    }

    public override VisualNode Render()
    {
        return new ContentPage()
        {
            new StackLayout()
            {
                new Entry()
                    .AutomationId("ChildPage_Entry") //<-- required for sample test
                    .Text(State.Value.ToString())
                    .OnTextChanged(newText =>
                    {
                        if (int.TryParse(newText, out int value))
                        {
                            State.Value = value;
                        }                        
                    })
                    .Keyboard(Keyboard.Numeric),
                new Button("Back")
                    .AutomationId("MoveToMainPage_Button") //<-- required for sample test
                    .OnClicked(GoBack)
            }
            .VCenter()
            .HCenter()
        }
        .Title("Child Page");
    }

    private async Task GoBack()
    {
        Props.OnValueSet?.Invoke(State.Value);

        await Navigation.PopAsync();
    }
}

