using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

public class TestBug190StartPage : Component
{
    public override VisualNode Render() => NavigationPage(
        ContentPage(
            Button("Go to Tabbed Page")
                .OnClicked(async () => await Navigation!.PushAsync<TestBug190MyTabbedPage>())
        )
    );
}

public class TestBug190MyTabbedPage : Component
{
    public override VisualNode Render() => TabbedPage(
        new ToolbarItem("Test"),

        new TestBug190FirstPage(),
        new TestBug190SecondPage()
    );

    // Workaround is to add ToolbarItem using componentRefAction and not add if one was previously added.
    //
    /*MauiControls.ToolbarItem AddToolbarItem(MauiControls.Page page, string text, EventHandler? clicked = null)
    {
        if (!page.ToolbarItems.Any(i => i.Text == text))
        {
            var toolbarItem = new MauiControls.ToolbarItem
            {
                Text = text
            };

            if (clicked != null)
                toolbarItem.Clicked += clicked;

            page.ToolbarItems.Add(toolbarItem);

            return toolbarItem;
        }

        return null!;
    }*/
}

public class TestBug190FirstPage : Component
{
    public override VisualNode Render() => ContentPage(
        Label("Page 1")
    )
    .Title("Page 1");
}

public class TestBug190SecondPage : Component
{
    public override VisualNode Render() => ContentPage(
        Label("Page 2")
    )
    .Title("Page 2");
}