// <auto-generated />
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;

#nullable enable
namespace MauiReactor;
public partial interface IGenericBehavior : IBehavior
{
}

public abstract partial class Behavior<T, TChild> : Behavior<T>, IGenericBehavior where T : Microsoft.Maui.Controls.Behavior<TChild>, new()
    where TChild : Microsoft.Maui.Controls.BindableObject
{
    public Behavior(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    protected override void OnUpdate()
    {
        OnBeginUpdate();
        base.OnUpdate();
        OnEndUpdate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public static partial class BehaviorExtensions
{
}