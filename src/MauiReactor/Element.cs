using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface IElement : IVisualNode
    {
        PropertyValue<string>? AutomationId { get; set; }
        PropertyValue<string>? ClassId { get; set; }

        Action? ChildAddedAction { get; set; }
        Action<object?, ElementEventArgs>? ChildAddedActionWithArgs { get; set; }
        Action? ChildRemovedAction { get; set; }
        Action<object?, ElementEventArgs>? ChildRemovedActionWithArgs { get; set; }
        Action? DescendantAddedAction { get; set; }
        Action<object?, ElementEventArgs>? DescendantAddedActionWithArgs { get; set; }
        Action? DescendantRemovedAction { get; set; }
        Action<object?, ElementEventArgs>? DescendantRemovedActionWithArgs { get; set; }
        Action? ParentChangingAction { get; set; }
        Action<object?, ParentChangingEventArgs>? ParentChangingActionWithArgs { get; set; }
        Action? ParentChangedAction { get; set; }
        Action<object?, EventArgs>? ParentChangedActionWithArgs { get; set; }
        Action? HandlerChangingAction { get; set; }
        Action<object?, HandlerChangingEventArgs>? HandlerChangingActionWithArgs { get; set; }
        Action? HandlerChangedAction { get; set; }
        Action<object?, EventArgs>? HandlerChangedActionWithArgs { get; set; }

    }
    public abstract partial class Element<T> : VisualNode<T>, IElement where T : Microsoft.Maui.Controls.Element, new()
    {
        protected Element()
        {

        }

        protected Element(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<string>? IElement.AutomationId { get; set; }
        PropertyValue<string>? IElement.ClassId { get; set; }

        Action? IElement.ChildAddedAction { get; set; }
        Action<object?, ElementEventArgs>? IElement.ChildAddedActionWithArgs { get; set; }
        Action? IElement.ChildRemovedAction { get; set; }
        Action<object?, ElementEventArgs>? IElement.ChildRemovedActionWithArgs { get; set; }
        Action? IElement.DescendantAddedAction { get; set; }
        Action<object?, ElementEventArgs>? IElement.DescendantAddedActionWithArgs { get; set; }
        Action? IElement.DescendantRemovedAction { get; set; }
        Action<object?, ElementEventArgs>? IElement.DescendantRemovedActionWithArgs { get; set; }
        Action? IElement.ParentChangingAction { get; set; }
        Action<object?, ParentChangingEventArgs>? IElement.ParentChangingActionWithArgs { get; set; }
        Action? IElement.ParentChangedAction { get; set; }
        Action<object?, EventArgs>? IElement.ParentChangedActionWithArgs { get; set; }
        Action? IElement.HandlerChangingAction { get; set; }
        Action<object?, HandlerChangingEventArgs>? IElement.HandlerChangingActionWithArgs { get; set; }
        Action? IElement.HandlerChangedAction { get; set; }
        Action<object?, EventArgs>? IElement.HandlerChangedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIElement = (IElement)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Element.AutomationIdProperty, thisAsIElement.AutomationId);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Element.ClassIdProperty, thisAsIElement.ClassId);


            base.OnUpdate();

            OnEndUpdate();
        }


        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIElement = (IElement)this;
            if (thisAsIElement.ChildAddedAction != null || thisAsIElement.ChildAddedActionWithArgs != null)
            {
                NativeControl.ChildAdded += NativeControl_ChildAdded;
            }
            if (thisAsIElement.ChildRemovedAction != null || thisAsIElement.ChildRemovedActionWithArgs != null)
            {
                NativeControl.ChildRemoved += NativeControl_ChildRemoved;
            }
            if (thisAsIElement.DescendantAddedAction != null || thisAsIElement.DescendantAddedActionWithArgs != null)
            {
                NativeControl.DescendantAdded += NativeControl_DescendantAdded;
            }
            if (thisAsIElement.DescendantRemovedAction != null || thisAsIElement.DescendantRemovedActionWithArgs != null)
            {
                NativeControl.DescendantRemoved += NativeControl_DescendantRemoved;
            }
            if (thisAsIElement.ParentChangingAction != null || thisAsIElement.ParentChangingActionWithArgs != null)
            {
                NativeControl.ParentChanging += NativeControl_ParentChanging;
            }
            if (thisAsIElement.ParentChangedAction != null || thisAsIElement.ParentChangedActionWithArgs != null)
            {
                NativeControl.ParentChanged += NativeControl_ParentChanged;
            }
            if (thisAsIElement.HandlerChangingAction != null || thisAsIElement.HandlerChangingActionWithArgs != null)
            {
                NativeControl.HandlerChanging += NativeControl_HandlerChanging;
            }
            if (thisAsIElement.HandlerChangedAction != null || thisAsIElement.HandlerChangedActionWithArgs != null)
            {
                NativeControl.HandlerChanged += NativeControl_HandlerChanged;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_ChildAdded(object? sender, ElementEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.ChildAddedAction?.Invoke();
            thisAsIElement.ChildAddedActionWithArgs?.Invoke(sender, e);
        }
        private void NativeControl_ChildRemoved(object? sender, ElementEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.ChildRemovedAction?.Invoke();
            thisAsIElement.ChildRemovedActionWithArgs?.Invoke(sender, e);
        }
        private void NativeControl_DescendantAdded(object? sender, ElementEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.DescendantAddedAction?.Invoke();
            thisAsIElement.DescendantAddedActionWithArgs?.Invoke(sender, e);
        }
        private void NativeControl_DescendantRemoved(object? sender, ElementEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.DescendantRemovedAction?.Invoke();
            thisAsIElement.DescendantRemovedActionWithArgs?.Invoke(sender, e);
        }
        private void NativeControl_ParentChanging(object? sender, ParentChangingEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.ParentChangingAction?.Invoke();
            thisAsIElement.ParentChangingActionWithArgs?.Invoke(sender, e);
        }
        private void NativeControl_ParentChanged(object? sender, EventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.ParentChangedAction?.Invoke();
            thisAsIElement.ParentChangedActionWithArgs?.Invoke(sender, e);
        }
        private void NativeControl_HandlerChanging(object? sender, HandlerChangingEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.HandlerChangingAction?.Invoke();
            thisAsIElement.HandlerChangingActionWithArgs?.Invoke(sender, e);
        }
        private void NativeControl_HandlerChanged(object? sender, EventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.HandlerChangedAction?.Invoke();
            thisAsIElement.HandlerChangedActionWithArgs?.Invoke(sender, e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.ChildAdded -= NativeControl_ChildAdded;
                NativeControl.ChildRemoved -= NativeControl_ChildRemoved;
                NativeControl.DescendantAdded -= NativeControl_DescendantAdded;
                NativeControl.DescendantRemoved -= NativeControl_DescendantRemoved;
                NativeControl.ParentChanging -= NativeControl_ParentChanging;
                NativeControl.ParentChanged -= NativeControl_ParentChanged;
                NativeControl.HandlerChanging -= NativeControl_HandlerChanging;
                NativeControl.HandlerChanged -= NativeControl_HandlerChanged;
            }

            base.OnDetachNativeEvents();
        }

    }


    public static partial class ElementExtensions
    {
        public static T AutomationId<T>(this T element, string automationId) where T : IElement
        {
            element.AutomationId = new PropertyValue<string>(automationId);
            return element;
        }

        public static T AutomationId<T>(this T element, Func<string> automationIdFunc) where T : IElement
        {
            element.AutomationId = new PropertyValue<string>(automationIdFunc);
            return element;
        }



        public static T ClassId<T>(this T element, string classId) where T : IElement
        {
            element.ClassId = new PropertyValue<string>(classId);
            return element;
        }

        public static T ClassId<T>(this T element, Func<string> classIdFunc) where T : IElement
        {
            element.ClassId = new PropertyValue<string>(classIdFunc);
            return element;
        }




        public static T OnChildAdded<T>(this T element, Action childAddedAction) where T : IElement
        {
            element.ChildAddedAction = childAddedAction;
            return element;
        }

        public static T OnChildAdded<T>(this T element, Action<object?, ElementEventArgs> childAddedActionWithArgs) where T : IElement
        {
            element.ChildAddedActionWithArgs = childAddedActionWithArgs;
            return element;
        }
        public static T OnChildRemoved<T>(this T element, Action childRemovedAction) where T : IElement
        {
            element.ChildRemovedAction = childRemovedAction;
            return element;
        }

        public static T OnChildRemoved<T>(this T element, Action<object?, ElementEventArgs> childRemovedActionWithArgs) where T : IElement
        {
            element.ChildRemovedActionWithArgs = childRemovedActionWithArgs;
            return element;
        }
        public static T OnDescendantAdded<T>(this T element, Action descendantAddedAction) where T : IElement
        {
            element.DescendantAddedAction = descendantAddedAction;
            return element;
        }

        public static T OnDescendantAdded<T>(this T element, Action<object?, ElementEventArgs> descendantAddedActionWithArgs) where T : IElement
        {
            element.DescendantAddedActionWithArgs = descendantAddedActionWithArgs;
            return element;
        }
        public static T OnDescendantRemoved<T>(this T element, Action descendantRemovedAction) where T : IElement
        {
            element.DescendantRemovedAction = descendantRemovedAction;
            return element;
        }

        public static T OnDescendantRemoved<T>(this T element, Action<object?, ElementEventArgs> descendantRemovedActionWithArgs) where T : IElement
        {
            element.DescendantRemovedActionWithArgs = descendantRemovedActionWithArgs;
            return element;
        }
        public static T OnParentChanging<T>(this T element, Action parentChangingAction) where T : IElement
        {
            element.ParentChangingAction = parentChangingAction;
            return element;
        }

        public static T OnParentChanging<T>(this T element, Action<object?, ParentChangingEventArgs> parentChangingActionWithArgs) where T : IElement
        {
            element.ParentChangingActionWithArgs = parentChangingActionWithArgs;
            return element;
        }
        public static T OnParentChanged<T>(this T element, Action parentChangedAction) where T : IElement
        {
            element.ParentChangedAction = parentChangedAction;
            return element;
        }

        public static T OnParentChanged<T>(this T element, Action<object?, EventArgs> parentChangedActionWithArgs) where T : IElement
        {
            element.ParentChangedActionWithArgs = parentChangedActionWithArgs;
            return element;
        }
        public static T OnHandlerChanging<T>(this T element, Action handlerChangingAction) where T : IElement
        {
            element.HandlerChangingAction = handlerChangingAction;
            return element;
        }

        public static T OnHandlerChanging<T>(this T element, Action<object?, HandlerChangingEventArgs> handlerChangingActionWithArgs) where T : IElement
        {
            element.HandlerChangingActionWithArgs = handlerChangingActionWithArgs;
            return element;
        }
        public static T OnHandlerChanged<T>(this T element, Action handlerChangedAction) where T : IElement
        {
            element.HandlerChangedAction = handlerChangedAction;
            return element;
        }

        public static T OnHandlerChanged<T>(this T element, Action<object?, EventArgs> handlerChangedActionWithArgs) where T : IElement
        {
            element.HandlerChangedActionWithArgs = handlerChangedActionWithArgs;
            return element;
        }
    }
}
