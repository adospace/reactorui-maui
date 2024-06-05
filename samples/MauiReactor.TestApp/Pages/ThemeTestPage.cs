using MauiReactor.TestApp.Resources.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class ThemeTestPage : Component
{
    public override VisualNode Render()
    {
        System.Diagnostics.Debug.WriteLine(Theme.CurrentAppTheme);
        return ContentPage("Theming test page",
            VStack(
                Label($"Current Theme: {Theme.CurrentAppTheme} "),

                Button("Toggle", ()=>AppTheme.ToggleCurrentAppTheme())
                )
            .Spacing(10)
            .Center()
        );
    }
}
