using MauiReactor.TestApp.Services;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

abstract partial class BaseComponent2<TState> : Component<TState> where TState : class, new()
{
    [Inject] protected readonly IncrementService? _injectedService;
    [Prop] protected MauiControls.Shell? shellRef;
}

abstract partial class BaseComponent2<TState, TProps> : Component<TState, TProps> where TState : class, new()
    where TProps : class, new()
{
    [Inject] protected readonly IncrementService? _injectedService;
    [Prop] protected MauiControls.Shell? shellRef;
}

class TestBug218_1 : BaseComponent2<TestBug218_1.MyState>
{
    public class MyState
    { }

    public override VisualNode Render()
    {
        throw new NotImplementedException();
    }
}

class TestBug218_2 : BaseComponent2<TestBug218_2.MyState, TestBug218_2.MyProps>
{
    public class MyState
    { }
    public class MyProps
    { }

    public override VisualNode Render()
    {
        throw new NotImplementedException();
    }
}
