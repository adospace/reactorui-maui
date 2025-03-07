using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

public class TestBug257 : Component
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
                        .AutomationId("Home")
                        .Route("Home")
                        .RenderContent(() => new TestBug257_HomePage()),
                    ShellContent("Tab2")
                        .Route("Tab2")
                        .AutomationId("Tab2")
                        .RenderContent(() => new TestBug257_Page2()),
                    ShellContent("Tab3")
                        .RenderContent(() => new TestBug257_Page3()),
                    ShellContent("Tab4")
                        .RenderContent(() => new TestBug257_Page4()),
                    ShellContent("Tab5")
                        .RenderContent(() => new TestBug257_Page5())
                )
                .AutomationId("MainTabBar")
                .Route("mainpage")
        )
        .AutomationId("MainShell"));
}

partial class TestBug257_HomePage : Component
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
                .AutomationId("ToPage6_Button")
                .Center()
                .OnClicked(async () => await CurrentShell!.GoToAsync("page-6"))
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

class TestBug257_Page6State
{
    public int ClickCount { get; set; }
}

class TestBug257_Page6 : Component<TestBug257_Page6State>
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

            ToolbarItem("Menu Item 1")
                .OnClicked(() => System.Diagnostics.Debug.WriteLine("Menu Item 1 Clicked")),

            Button(State.ClickCount == 0 ? "Click me!" : $"Clicked: {State.ClickCount}")
                .AutomationId("Page6_ClickCount_Button")
                .Center()
                .OnClicked(() => SetState(s => s.ClickCount++))

        )
        .AutomationId("page6");
    }
}