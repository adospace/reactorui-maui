using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;

namespace MauiReactor.Startup.Pages;

class MainPage : Component
{
    public override VisualNode Render()
    {
        return new ContentPage
        {
            new Label("Happy coding with MauiReactor!")
                .VCenter()
                .HCenter()
        };
    }
}
