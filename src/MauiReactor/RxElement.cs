using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace MauiReactor
{
    public interface IRxElement : IVisualNode
    {
        void SetAttachedProperty(BindableProperty property, object value);

        Action<object?, System.ComponentModel.PropertyChangingEventArgs>? PropertyChangingAction { get; set; }

        Action<object?, PropertyChangedEventArgs>? PropertyChangedAction { get; set; }
    }

    public abstract class RxElement<T> : VisualNode<T>, IRxElement where T : Element, new()
    {
        protected RxElement()
        { }

        protected RxElement(Action<T?> componentRefAction)
            :base(componentRefAction)
        {
        }
    }

    public static class RxElementExtensions
    {

    }
}