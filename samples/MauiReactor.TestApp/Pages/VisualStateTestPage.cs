using MauiReactor.TestApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.Controls.VisualStateManager;
namespace MauiReactor.TestApp.Pages;

class VisualStateTestPage : Component
{
    public override VisualNode Render()
        => ContentPage("Visual States Test Page",
            VStack(
                Entry()
                    .Placeholder("Entry 1")
                    .VisualState(nameof(CommonStates), CommonStates.Normal, MauiControls.Entry.FontSizeProperty, 35)
                    .VisualState(nameof(CommonStates), CommonStates.Focused, MauiControls.VisualElement.BackgroundColorProperty, Colors.Red)
                    .VisualState(nameof(CommonStates), "Unfocused", MauiControls.VisualElement.BackgroundColorProperty, Colors.Yellow),
                Entry()
                    .Placeholder("Entry 2")
                    .VisualState(nameof(CommonStates), CommonStates.Normal, MauiControls.Entry.FontSizeProperty, 35)
                    .VisualState(nameof(CommonStates), CommonStates.Focused, MauiControls.VisualElement.BackgroundColorProperty, Colors.Red)
                    .VisualState(nameof(CommonStates), "Unfocused", MauiControls.VisualElement.BackgroundColorProperty, Colors.Yellow),

                Button("Button")
                    .VisualState(nameof(CommonStates), CommonStates.Normal)
                    .VisualState(nameof(CommonStates), "Pressed", MauiControls.VisualElement.BackgroundColorProperty, Colors.Aqua)



            )
            .Spacing(10)
            .Center()
        );
    
}