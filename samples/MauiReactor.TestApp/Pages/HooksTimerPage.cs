using System;
using System.Threading;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

// The case that actually earns hooks: a value that changes on a timer.
// Cleanup lives next to the code that needs it, instead of being split across
// OnMounted/OnWillUnmount with a field in between.
class HooksTimerPage : HookComponent
{
    public override VisualNode Render()
    {
        var (count, updateCount) = UseReducer(0);
        var (running, setRunning) = UseState(false);

        // Restarts whenever `running` flips; the returned action cancels the old timer.
        UseEffect(() =>
        {
            if (!running)
            {
                return () => { };
            }

            var cts = new CancellationTokenSource();
            _ = TickAsync(cts.Token);
            return () => cts.Cancel();

            async Task TickAsync(CancellationToken token)
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        await Task.Delay(500, token);

                        // Functional update: reads the live value, never a stale capture.
                        // This is why UseReducer exists alongside UseState.
                        updateCount(c => c + 1);
                    }
                }
                catch (OperationCanceledException)
                {
                }
            }
        }, running);

        var label = UseMemo(() => count switch
        {
            0 => "Tap to start",
            < 10 => $"{count}",
            _ => $"{count} - warmed up"
        }, count);

        return ContentPage("Hooks - Timer",
            VStack(
                Label(label)
                    .FontSize(48)
                    .HCenter(),

                HStack(
                    Button(running ? "Pause" : "Play")
                        .OnClicked(() => setRunning(!running)),

                    Button("Reset")
                        .OnClicked(() => updateCount(_ => 0))
                        .IsEnabled(count > 0)
                )
                .Spacing(12)
                .HCenter()
            )
            .VCenter()
            .Spacing(24)
            .Padding(30, 0)
        );
    }
}
