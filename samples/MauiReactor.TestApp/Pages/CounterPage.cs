using MauiReactor.TestApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class CounterPageState
{
    public int Counter { get; set; }
}

class CounterPage : Component<CounterPageState>
{
    public override VisualNode Render()
        => ContentPage("Counter Sample",
        [
            VStack(
            [
                Label($"Counter: {State.Counter}"),

                Button("Click To Increment", () =>
                    SetState(s => s.Counter++))
            ])
            .Spacing(10)
            .Center()
        ]);
    
}

internal class CounterWithServicePage : Component<CounterPageState>
{
    public override VisualNode Render()
    {
        var incrementService = Services.GetRequiredService<IncrementService>();
        return new ContentPage("Counter Sample")
        {
            new VerticalStackLayout(spacing: 10)
            {
                new Label($"Counter: {State.Counter}")
                    .AutomationId("Counter_Label")
                    .VCenter()
                    .HCenter(),

                new Button("Click To Increment", () =>
                    SetState(s => s.Counter = incrementService.Increment(s.Counter)))
                    .AutomationId("Counter_Button")
            }
            .VCenter()
            .HCenter()
        };
    }
}
