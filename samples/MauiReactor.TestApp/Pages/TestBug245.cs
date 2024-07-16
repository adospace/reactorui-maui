using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class TestBug245State
{
    public int TabIndex {  get; set; }

    public int GlobalConter {  get; set; }
}


class TestBug245 : Component<TestBug245State>
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

    Component RenderCurrentTab()
    {
        return State.TabIndex switch
        {
            0 => new TestBug245Page1()
                .State(State.GlobalConter)
                .NewState(newValue => SetState(s => s.GlobalConter = newValue)),
            1 => new TestBug245Page2()
                .State(State.GlobalConter)
                .NewState(newValue => SetState(s => s.GlobalConter = newValue)),
            _ => throw new NotImplementedException(),
        };
    }
}


partial class TestBug245Page1 : Component
{
    [Prop]
    int _state;

    [Prop]
    Action<int>? _newState;

    public override VisualNode Render()
    {
        return Button($"({_state}) Add 1", () => _newState?.Invoke(_state + 1))
            .Center();
    }
}


partial class TestBug245Page2 : Component
{
    [Prop]
    int _state;

    [Prop]
    Action<int>? _newState;

    public override VisualNode Render()
    {
        return Button($"({_state}) Double it", () => _newState?.Invoke(_state * 2))
            .Center();
    }
}