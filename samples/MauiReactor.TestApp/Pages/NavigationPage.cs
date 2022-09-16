using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

public class NavigationMainPageState : IState
{
    public int Value { get; set; }
}

public class NavigationMainPage : Component<NavigationMainPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage()
        {
            new StackLayout()
            {
                new Label($"Value: {State.Value}"),
                new Button("Move To Page")
                    .OnClicked(OpenChildPage)
            }
            .VCenter()
            .HCenter()
        }
        .Title("Main Page");
    }

    private async void OpenChildPage()
    {
        if (Navigation == null)
        {
            return;
        }

        await Navigation.PushAsync<ChildPage, ChildPageProps>(_ =>
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

public class ChildPageState : IState
{
    public int Value { get; set; }
}

public class ChildPageProps : IProps
{
    public int InitialValue { get; set; }

    public Action<int>? OnValueSet { get; set; }
}

public class ChildPage : Component<ChildPageState, ChildPageProps>
{
    protected override void OnMounted()
    {
        State.Value = Props.InitialValue;

        base.OnMounted();
    }

    public override VisualNode Render()
    {
        return new ContentPage()
        {
            new StackLayout()
            {
                new Entry()
                    .Text(State.Value.ToString())
                    .OnTextChanged(newText => State.Value = int.Parse(newText))
                    .Keyboard(Keyboard.Numeric),
                new Button("Back")
                    .OnClicked(GoBack)
            }
            .VCenter()
            .HCenter()
        }
        .Title("Child Page");
    }

    private async void GoBack()
    {
        if (Navigation == null)
        {
            return;
        }

        Props.OnValueSet?.Invoke(State.Value);

        await Navigation.PopAsync();
    }
}

