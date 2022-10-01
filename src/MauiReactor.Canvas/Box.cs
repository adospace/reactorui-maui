﻿using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{
    public partial interface IBox : ICanvasNode
    {
        PropertyValue<Color?>? BackgroundColor { get; set; }
        PropertyValue<Color?>? BorderColor { get; set; }
        PropertyValue<float>? CornerRadius { get; set; }
        PropertyValue<float>? BorderSize { get; set; }
    }

    public partial class Box<T> : CanvasNode<T>, IBox, IEnumerable where T : Internals.Box, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public Box()
        {

        }

        public Box(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Color?>? IBox.BackgroundColor { get; set; }
        PropertyValue<Color?>? IBox.BorderColor { get; set; }
        PropertyValue<float>? IBox.CornerRadius { get; set; }
        PropertyValue<float>? IBox.BorderSize { get; set; }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }


        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(params VisualNode?[]? animations)
        {
            if (animations is null)
            {
                return;
            }

            foreach (var node in animations)
            {
                if (node != null)
                {
                    _internalChildren.Add(node);
                }
            }
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.Child = node;
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node &&
                node == NativeControl.Child)
            {
                NativeControl.Child = null;
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBorder = (IBox)this;

            SetPropertyValue(NativeControl, Internals.Box.BackgroundColorProperty, thisAsIBorder.BackgroundColor);
            SetPropertyValue(NativeControl, Internals.Box.BorderColorProperty, thisAsIBorder.BorderColor);
            SetPropertyValue(NativeControl, Internals.Box.CornerRadiusProperty, thisAsIBorder.CornerRadius);
            SetPropertyValue(NativeControl, Internals.Box.BorderSizeProperty, thisAsIBorder.BorderSize);

            base.OnUpdate();
        }
    }

    public partial class Box : Box<Internals.Box>
    {
        public Box()
        {

        }

        public Box(Action<Internals.Box?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class BoxExtensions
    {
        public static T BackgroundColor<T>(this T ndoe, Color? value) where T : IBox
        {
            ndoe.BackgroundColor = new PropertyValue<Color?>(value);
            return ndoe;
        }

        public static T BackgroundColor<T>(this T node, Func<Color?> valueFunc) where T : IBox
        {
            node.BackgroundColor = new PropertyValue<Color?>(valueFunc);
            return node;
        }
        public static T BorderColor<T>(this T ndoe, Color? value) where T : IBox
        {
            ndoe.BorderColor = new PropertyValue<Color?>(value);
            return ndoe;
        }

        public static T BorderColor<T>(this T node, Func<Color?> valueFunc) where T : IBox
        {
            node.BorderColor = new PropertyValue<Color?>(valueFunc);
            return node;
        }
        public static T BorderSize<T>(this T node, float value) where T : IBox
        {
            node.BorderSize = new PropertyValue<float>(value);
            return node;
        }

        public static T BorderSize<T>(this T node, Func<float> valueFunc) where T : IBox
        {
            node.BorderSize = new PropertyValue<float>(valueFunc);
            return node;
        }
        public static T CornerRadius<T>(this T ndoe, float value) where T : IBox
        {
            ndoe.CornerRadius = new PropertyValue<float>(value);
            return ndoe;
        }

        public static T CornerRadius<T>(this T node, Func<float> valueFunc) where T : IBox
        {
            node.CornerRadius = new PropertyValue<float>(valueFunc);
            return node;
        }
    }
}