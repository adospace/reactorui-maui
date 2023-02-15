using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages
{
    internal class CounterPageState
    {
        public int Counter { get; set; }
    }

    internal class CounterPage : Component<CounterPageState>
    {
        public override VisualNode Render()
        {
            return new ContentPage("Counter Sample")
            {
                new VerticalStackLayout(spacing: 10)
                {
                    new Label($"Counter: {State.Counter}")
                        .VCenter()
                        .HCenter(),

                    new Button("Click To Increment", () =>
                        SetState(s => s.Counter++))
                }
                .VCenter()
                .HCenter()
            };
        }
    }

}
