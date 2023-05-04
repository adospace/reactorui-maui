using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class InlineComponentsPage : Component
{
    public override VisualNode Render()
    {
        return new ContentPage
        {
            new VStack
            {
                Buttons.IncrementButton()
            }
            .HCenter()
            .VCenter()
        };
    }
}

static class Buttons
{
    public static VisualNode IncrementButton()
    {
        return Component.Render(context =>
        {
            var state = context.UseState<int>();
            return new Button(
                state.Value == 0 ? "Click me!" : $"Counter is {state.Value}", 
                () => state.Set(s => ++s));
        });
    }
}

