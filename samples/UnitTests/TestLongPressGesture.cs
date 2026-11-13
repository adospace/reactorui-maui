using System.Linq;
using MauiReactor;
using Shouldly;

namespace UnitTests;

class LongPressPageState
{
    public int LongPresses { get; set; }
}

class LongPressPage : Component<LongPressPageState>
{
    public override VisualNode Render()
        => ContentPage(
            VStack(
                Label($"pressed {State.LongPresses}")
                    .AutomationId("counter"),

                // Gesture recognizers are passed as children; the view routes them to GestureRecognizers.
                LongPressGestureRecognizer(() => SetState(s => s.LongPresses++), minimumPressDuration: 800)
                    .NumberOfTouchesRequired(1)
            )
            .AutomationId("target")
        );
}

public class TestLongPressGesture
{
    [Test]
    public void LongPress_SetsPropertiesOnNativeRecognizer()
    {
        var host = TemplateHost.Create(new LongPressPage());

        var recognizer = host.Find<MauiControls.VerticalStackLayout>("target")
            .GestureRecognizers
            .OfType<MauiControls.LongPressGestureRecognizer>()
            .Single();

        recognizer.MinimumPressDuration.ShouldBe(800);
        recognizer.NumberOfTouchesRequired.ShouldBe(1);
    }

    [Test]
    public void LongPress_InvokesHandlerAndRerenders()
    {
        var host = TemplateHost.Create(new LongPressPage());

        var target = host.Find<MauiControls.VerticalStackLayout>("target");
        var recognizer = target.GestureRecognizers.OfType<MauiControls.LongPressGestureRecognizer>().Single();

        recognizer.SendLongPressed(target, null);

        host.Find<MauiControls.Label>("counter").Text.ShouldBe("pressed 1");

        // The re-render must reuse the same native recognizer and keep the event wired.
        target.GestureRecognizers.OfType<MauiControls.LongPressGestureRecognizer>().Single().ShouldBeSameAs(recognizer);
        recognizer.SendLongPressed(target, null);

        host.Find<MauiControls.Label>("counter").Text.ShouldBe("pressed 2");
    }
}
