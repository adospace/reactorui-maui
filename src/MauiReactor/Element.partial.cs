using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace MauiReactor
{
    public partial interface IElement : IVisualNode
    {
        void SetAttachedProperty(BindableProperty property, object value);

    }

    public partial class Element<T> : VisualNode<T>, IElement where T : Element, new()
    {
    }

    public static partial class ElementExtensions
    {

    }
}