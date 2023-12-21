using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.TestApp.Models;

namespace MauiReactor.TestApp.Pages;

public class Bug187_2StartPage : Component
{
    public override VisualNode Render() => new NavigationPage()
    {
        new ContentPage()
        {
            new Button("Open Child Page")
                .OnClicked(async () => await Navigation!.PushModalAsync<Bug187_2ChildPage>())
        }
    };
}

public class Bug187_2ChildPageState
{
    public string? SearchText { get; set; }
}

public class Bug187_2ChildPage : Component<Bug187_2ChildPageState>
{
    public override VisualNode Render() 
        => NavigationPage(
            ContentPage(
                ToolbarItem("Close")
                    .OnClicked(() => Navigation?.PopModalAsync()),
                Grid(
                    new MudEditor()
                        .Placeholder("Test")
                )
                .VCenter()
            )
            .Title("Child Page")
            .BackgroundColor(Colors.Aquamarine)
        )
        .OniOS(page => page.Set(MauiControls.PlatformConfiguration.iOSSpecific.Page.ModalPresentationStyleProperty, MauiControls.PlatformConfiguration.iOSSpecific.UIModalPresentationStyle.FormSheet));
}

class MudEditorState
{
    public string? Text { get; set; }

    public bool Focused { get; set; }

    public bool IsEmpty { get; set; } = true;
    //public bool Invalidate { get; set; }
}

class MudEditor : Component<MudEditorState>
{
    private MauiControls.Editor? _editorRef;
    private Action<string>? _textChangedAction;
    private string? _label;

    public MudEditor OnTextChanged(Action<string> textChangedAction)
    {
        _textChangedAction = textChangedAction;
        return this;
    }

    public MudEditor Placeholder(string label)
    {
        _label = label;
        return this;
    }

    public override VisualNode Render()
    {
        return Grid("Auto", "*",        
            Editor(editorRef => _editorRef = editorRef)
                .Text(State.Text ?? "")
                // .OnAfterTextChanged(OnTextChanged)
                .OnTextChanged((s, e) =>
                {
                    //avoid to call SetState here otherwise we'll just invalidate the component setting the Invalidate property of the state
                    //SetState(state => state.Invalidate = (State.Text?.Length == 0 && e.NewTextValue.Length > 0) || (State.Text?.Length > 0 && e.NewTextValue.Length == 0));
                    bool invalidate = (State.Text?.Length == 0 && e.NewTextValue.Length > 0) || (State.Text?.Length > 0 && e.NewTextValue.Length == 0);
                    SetState(state => state.Text = e.NewTextValue, invalidate);
                    _textChangedAction?.Invoke(e.NewTextValue);
                })
                .HFill()
                .VFill()
                .OnFocused(() => SetState(s => s.Focused = true))
                .OnUnfocused(() => SetState(s => s.Focused = false)),

            Label(_label)
                .OnTapped(() => _editorRef?.Focus())
                .Margin(5,0)
                .HStart()
                .VCenter()
                .AnchorX(0)
                .TextColor(!State.Focused || State.IsEmpty ? Colors.Gray : Colors.Red)
                .WithOutAnimation()
                //these 2 properties will be animated
                .TranslationY(State.Focused || !State.IsEmpty ? -20 : 0)
                .ScaleX(State.Focused || !State.IsEmpty ? 0.8 : 1.0)
                .WithAnimation(duration: 20)
        )
        .HFill()
        .VFill();
    }
}