using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp
{
    internal class HomePageState : IState
    { 
        public int Counter { get; set; }
    }

    internal class HomePage : Component<HomePageState>
    {
        public override VisualNode Render()
        {
            return new ContentPage("Title")
            {
                new HStack(spacing: 10)
                {
                    new Label($"Counter: {State.Counter}")
                        .VerticalOptions(LayoutOptions.Center)
                        .HorizontalOptions(LayoutOptions.Center),

                    new Button("Click To Increment", ()=> SetState(s => s.Counter++))                    
                }
                .VCenter()
                .HCenter()
            };
        }
    }
}
