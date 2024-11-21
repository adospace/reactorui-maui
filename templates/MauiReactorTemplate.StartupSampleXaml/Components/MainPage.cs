using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactorTemplate.StartupSampleXaml.Components;

partial class MainPage : Component
{
    public override VisualNode Render()
    {
        return ContentPage("Main Page");
    }
}
