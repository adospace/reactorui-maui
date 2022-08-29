using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;
using Trackizer.Styles;

namespace Trackizer.Pages;

class MainPageState : IState
{
    public int Counter { get; set; }
}

class MainPage : Component<MainPageState>
{
    public override VisualNode Render()
    {
        return new NavigationPage
        {
            new LandingPage()
        }
        .BackgroundColor(Theme.Current.Gray100);
    }
}
