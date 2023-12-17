using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages2;

public class Bug179_2StartPage : Component
{
    public override VisualNode Render() => new NavigationPage()
    {
        new ContentPage()
        {
            new Button("Open Child Page")
                .OnClicked(async () => await Navigation!.PushModalAsync<Bug179_2ChildPage>())
        }
    };
}

public class Bug179_2ChildPageState
{
    public bool IsFlyoutExpanded { get; set; }
}

public class Bug179_2ChildPage : Component<Bug179_2ChildPageState>
{
    public override VisualNode Render() => new NavigationPage()
    {
        new ContentPage()
        {
            new ToolbarItem("Flyout")
                .OnClicked(()=>SetState(state => state.IsFlyoutExpanded = !state.IsFlyoutExpanded)),

            new ToolbarItem("Close")
                .OnClicked(()=>Navigation?.PopModalAsync()),

            new Grid
            {
                new Bug179_2Flyout()
                    .IsExpanded(State.IsFlyoutExpanded)
                    .ExpandedChangedAction((isExpanded) => SetState(state => state.IsFlyoutExpanded = isExpanded))
            }
        }
        .Title("Child Page")
        .BackgroundColor(Colors.Aquamarine)
    }
    .OniOS(page => page.Set(MauiControls.PlatformConfiguration.iOSSpecific.Page.ModalPresentationStyleProperty, MauiControls.PlatformConfiguration.iOSSpecific.UIModalPresentationStyle.FormSheet));
}

class Bug179_2FlyoutState 
{
    public bool IsExpanded { get; set; }
}

class Bug179_2Flyout : Component<Bug179_2FlyoutState>
{
    const int FlyoutAnimationSpeed = 200;

    private bool _isExpanded;
    private Action<bool>? _expandedChangedAction;

    public Bug179_2Flyout IsExpanded(bool isExpanded)
    {
        _isExpanded = isExpanded;
        return this;
    }

    public Bug179_2Flyout ExpandedChangedAction(Action<bool>? expandedChangedAction)
    {
        _expandedChangedAction = expandedChangedAction;
        return this;
    }

    protected override void OnMountedOrPropsChanged()
    {
        base.OnMountedOrPropsChanged();

        SetState(state => state.IsExpanded = _isExpanded);
    }

    public override VisualNode Render()
    {
        var layout = new AbsoluteLayout
        {
            RenderBackdrop(),
            RenderBody()
        };

        if (State.IsExpanded)
        {
            layout.HFill();
            layout.VFill();
        }
        else
        {
            layout.WidthRequest(1);
            layout.HeightRequest(1);
            layout.VStart();
            layout.HEnd();
        }

        return layout;
    }

    private Grid RenderBody() => new Grid
    {
        new Frame
        {
            new StackLayout()
            {
                new Button("Open Child Page")
                    .OnClicked(async () => await Navigation!.PushModalAsync<Bug179_2ChildPage>())
            }
        }
        .WidthRequest(300)
        .HeightRequest(400)
        .HasShadow(false)
        .BorderColor(Colors.LightGray)
    }
    .Padding(10)
    .Opacity(State.IsExpanded ? 1 : 0)
    .Scale(State.IsExpanded ? 1 : 0.8)
    .WithAnimation(easing: Easing.SpringOut, duration: FlyoutAnimationSpeed)
    .AbsoluteLayoutBounds(new Rect(1, 0, MauiControls.AbsoluteLayout.AutoSize, MauiControls.AbsoluteLayout.AutoSize))
    .AbsoluteLayoutFlags(AbsoluteLayoutFlags.PositionProportional);

    Grid RenderBackdrop() => new Grid()
        .BackgroundColor(Colors.Red)
        .Opacity(State.IsExpanded ? 0.5f : 0)
        .WithAnimation(easing: Easing.CubicOut, duration: FlyoutAnimationSpeed)
        .AbsoluteLayoutBounds(new Rect(0, 0, 1, 1))
        .AbsoluteLayoutFlags(AbsoluteLayoutFlags.All)
        .OnTapped(() =>
        {
            SetState(state => state.IsExpanded = false);
            _expandedChangedAction?.Invoke(State.IsExpanded);
        });
}