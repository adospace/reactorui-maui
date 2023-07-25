using MauiReactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Components;

class ChildPage : Component
{
    public override VisualNode Render()
    {
        return new MauiReactor.ContentPage()
        {
            new MauiReactor.Label("Hello from MauiReactor!")
        };
    }
}
