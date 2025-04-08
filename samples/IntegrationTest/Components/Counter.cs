using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;

namespace IntegrationTest.Components;
class CounterState
{
    public int Counter { get; set; }
}

partial class Counter : Component<CounterState>
{
    [Inject] SampleService? _incrementService;
    public override VisualNode Render()
    {
        return new MauiReactor.Button(State.Counter == 0 ? "Click To Increment With MauiReactor" : $"Clicked {State.Counter} times with MauiReactor")
            .OnClicked(() => SetState(s => s.Counter = _incrementService.Increment(s.Counter)));
    }
}
