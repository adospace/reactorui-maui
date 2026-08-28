namespace MauiReactor.TestApp.Pages;

// The project-template counter, written with hooks.
// Compare with the Component<S> version: no state class, no State. prefix,
// no round trip through SetState(s => ...). Everything else is identical.
class HooksCounterPage : HookComponent
{
    public override VisualNode Render()
    {
        var (counter, setCounter) = UseState(0);

        return ContentPage("Hooks - Counter",
            VStack(
                Label("Hello, World!")
                    .FontSize(32)
                    .HCenter(),

                Button(counter == 0 ? "Click me" : $"Clicked {counter} times!")
                    .OnClicked(() => setCounter(counter + 1))
                    .HCenter()
            )
            .VCenter()
            .Spacing(25)
            .Padding(30, 0)
        );
    }
}
