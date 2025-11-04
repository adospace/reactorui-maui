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
            VStack(
                
                Label($"Counter: {State.Counter}")
                    .AutomationId("Counter_Label"),

                Button("Click To Increment")
                    .OnClicked(() => SetState(s => s.Counter++))
                    .AutomationId("Counter_Button")
                
            )
            .Spacing(10)
            .Center()
        );
}

//partial class CounterWithServicePage : Component<CounterPageState>
//{
//    [Inject]
//    IncrementService _incrementService;

//    public override VisualNode Render() 
//        => ContentPage("Counter Sample",
//            VStack(spacing: 10,
//                Label($"Counter: {State.Counter}")
//                    .AutomationId("Counter_Label")
//                    .VCenter()
//                    .HCenter(),

//                Button("Click To Increment", () => SetState(s => s.Counter = _incrementService.Increment(s.Counter)))
//                    .AutomationId("Counter_Button")
//            )
//            .VCenter()
//            .HCenter()
//        );
     
//    IEnumerable<VisualNode> GetLabels()
//        =>
//        [
//            Label("Label 1"),
//            Label("Label 2"),
//            Grid()  
//        ];
     
//}


partial class CounterWithTaskAwaiting : Component<CounterPageState>
{
    public override VisualNode Render()
        => ContentPage("Counter Sample",
            VStack(
                Label($"Counter: {State.Counter}")
                    .AutomationId("Counter_Label"),
                Button("Click To Increment")
                    .OnClicked(IncrementCounter)
                    .AutomationId("Counter_Button")
            )
            .Spacing(10)
            .Center()
        );
    async Task IncrementCounter()
    {
        SetState(s => s.Counter++);

        //by default MauiReactor 3 doesnt allow reentrant calls to IncrementCounter
        //that is the IncrementCounter() is not called again while the previous call is still running
        await Task.Delay(10000);
    }
}