using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class TestBug245_2State
{
    public int TabIndex {  get; set; }
}


class TestBug245_2 : Component<TestBug245_2State>
{
    public override VisualNode Render()
    {
        return ContentPage(
            Grid("Auto,*", "*",
            
                Grid("*","*,*",
                    Button("Tab 1", () => SetState(s => s.TabIndex = 0)),
                    Button("Tab 2", () => SetState(s => s.TabIndex = 1)).GridColumn(1)
                    ),

                RenderCurrentTab()
                    .GridRow(1)
            )
        );
    }

    Grid RenderCurrentTab()
    {
        return Grid(
            new TestBug245_2Page1()
                .IsVisible(State.TabIndex == 0),

            new TestBug245_2Page2()
                .IsVisible(State.TabIndex == 1)

            );
    }
}


partial class TestBug245_2Page1 : Component
{
    [Prop]
    bool _isVisible;

    public override VisualNode Render()
    {
        return WebView("https://www.google.com")
            .IsVisible(_isVisible);
    }
}


partial class TestBug245_2Page2 : Component
{
    [Prop]
    bool _isVisible;

    public override VisualNode Render()
    {
        return Label("Other Page")
            .Center()
            .IsVisible(_isVisible);
            ;
    }
}