using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;

using MauiReactor.AppShell.Pages;

namespace MauiReactor.AppShell;

class AppShell : Component
{
    public override VisualNode Render()
        => new Shell
        {
            new FlyoutItem("MainPage")
            {
                new ShellContent()
                    .Title("MainPage")
                    .RenderContent(()=>new MainPage())
            },
            new FlyoutItem("OtherPage")
            {
                new ShellContent()
                    .Title("OtherPage")
                    .RenderContent(()=>new OtherPage())
            }
        };
}

