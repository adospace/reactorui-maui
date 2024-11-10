using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class TestBug257 : Component
{
    protected override void OnMounted()
    {
        Routing.RegisterRoute<TestBug257_Page6>("page-6");
        base.OnMounted();
    }

    public override VisualNode Render()
        => Window(
            Shell(
                TabBar(
                    ShellContent("Home")
                        .RenderContent(() => new TestBug257_HomePage()),
                    ShellContent("Tab2")
                        .RenderContent(() => new TestBug257_Page2()),
                    ShellContent("Tab3")
                        .RenderContent(() => new TestBug257_Page3()),
                    ShellContent("Tab4")
                        .RenderContent(() => new TestBug257_Page4()),
                    ShellContent("Tab5")
                        .RenderContent(() => new TestBug257_Page5())
                )
                .Route("mainpage")
    ));
}

class TestBug257_HomePage : Component
{
    protected override void OnMounted()
    {
        System.Diagnostics.Debug.WriteLine("HomePage OnMounted");
        base.OnMounted();
    }

    protected override void OnWillUnmount()
    {
        System.Diagnostics.Debug.WriteLine("HomePage OnWillUnmount");
        base.OnWillUnmount();
    }

    public override VisualNode Render()
    {
        return ContentPage("HomePage",

            Button("To Page 6")
                .Center()
                .OnClicked(async () => await MauiControls.Shell.Current.GoToAsync("page-6"))
                //.OnClicked(async () => await Navigation.PushAsync<TestBug257_Page6>())

            );
    }
}

class TestBug257_Page2 : Component
{
    protected override void OnMounted()
    {
        System.Diagnostics.Debug.WriteLine("Page2 OnMounted");
        base.OnMounted();
    }

    protected override void OnWillUnmount()
    {
        System.Diagnostics.Debug.WriteLine("Page2 OnWillUnmount");
        base.OnWillUnmount();
    }
    public override VisualNode Render()
    {
        return ContentPage("Page2");
    }
}

class TestBug257_Page3 : Component
{
    protected override void OnMounted()
    {
        System.Diagnostics.Debug.WriteLine("Page3 OnMounted");
        base.OnMounted();
    }

    protected override void OnWillUnmount()
    {
        System.Diagnostics.Debug.WriteLine("Page3 OnWillUnmount");
        base.OnWillUnmount();
    }
    public override VisualNode Render()
    {
        return ContentPage("Page3");
    }
}

class TestBug257_Page4 : Component
{
    protected override void OnMounted()
    {
        System.Diagnostics.Debug.WriteLine("Page4 OnMounted");
        base.OnMounted();
    }

    protected override void OnWillUnmount()
    {
        System.Diagnostics.Debug.WriteLine("Page4 OnWillUnmount");
        base.OnWillUnmount();
    }
    public override VisualNode Render()
    {
        return ContentPage("Page4");
    }
}

class TestBug257_Page5 : Component
{
    protected override void OnMounted()
    {
        System.Diagnostics.Debug.WriteLine("Page5 OnMounted");
        base.OnMounted();
    }

    protected override void OnWillUnmount()
    {
        System.Diagnostics.Debug.WriteLine("Page5 OnWillUnmount");
        base.OnWillUnmount();
    }
    public override VisualNode Render()
    {
        return ContentPage("Page5");
    }
}

class TestBug257_Page6 : Component
{
    protected override void OnMounted()
    {
        System.Diagnostics.Debug.WriteLine("Page6 OnMounted");
        base.OnMounted();
    }

    protected override void OnWillUnmount()
    {
        System.Diagnostics.Debug.WriteLine("Page6 OnWillUnmount");
        base.OnWillUnmount();
    }
    public override VisualNode Render()
    {
        return ContentPage("Page6",

            Button("Click me!")
                .Center()
                .OnClicked(async () => await ContainerPage.DisplayAlert("Message", "Hello!", "OK"))

            );
    }
}