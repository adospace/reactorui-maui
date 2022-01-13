using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface IElement
    {
        string AutomationId { get; set; }
        string ClassId { get; set; }

        Action? ChildAddedAction { get; set; }
        Action<ElementEventArgs>? ChildAddedActionWithArgs { get; set; }
        Action? ChildRemovedAction { get; set; }
        Action<ElementEventArgs>? ChildRemovedActionWithArgs { get; set; }
        Action? DescendantAddedAction { get; set; }
        Action<ElementEventArgs>? DescendantAddedActionWithArgs { get; set; }
        Action? DescendantRemovedAction { get; set; }
        Action<ElementEventArgs>? DescendantRemovedActionWithArgs { get; set; }
        Action? ParentChangingAction { get; set; }
        Action<ParentChangingEventArgs>? ParentChangingActionWithArgs { get; set; }
        Action? ParentChangedAction { get; set; }
        Action<EventArgs>? ParentChangedActionWithArgs { get; set; }
        Action? HandlerChangingAction { get; set; }
        Action<HandlerChangingEventArgs>? HandlerChangingActionWithArgs { get; set; }
        Action? HandlerChangedAction { get; set; }
        Action<EventArgs>? HandlerChangedActionWithArgs { get; set; }
        Action? PropertyChangedAction { get; set; }
        Action<EventArgs>? PropertyChangedActionWithArgs { get; set; }
        Action? PropertyChangingAction { get; set; }
        Action<EventArgs>? PropertyChangingActionWithArgs { get; set; }
        Action? BindingContextChangedAction { get; set; }
        Action<EventArgs>? BindingContextChangedActionWithArgs { get; set; }

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

        string IElement.AutomationId { get; set; } = (string)Element.AutomationIdProperty.DefaultValue;
        string IElement.ClassId { get; set; } = (string)Element.ClassIdProperty.DefaultValue;

        Action? IElement.ChildAddedAction { get; set; }
        Action<ElementEventArgs>? IElement.ChildAddedActionWithArgs { get; set; }
        Action? IElement.ChildRemovedAction { get; set; }
        Action<ElementEventArgs>? IElement.ChildRemovedActionWithArgs { get; set; }
        Action? IElement.DescendantAddedAction { get; set; }
        Action<ElementEventArgs>? IElement.DescendantAddedActionWithArgs { get; set; }
        Action? IElement.DescendantRemovedAction { get; set; }
        Action<ElementEventArgs>? IElement.DescendantRemovedActionWithArgs { get; set; }
        Action? IElement.ParentChangingAction { get; set; }
        Action<ParentChangingEventArgs>? IElement.ParentChangingActionWithArgs { get; set; }
        Action? IElement.ParentChangedAction { get; set; }
        Action<EventArgs>? IElement.ParentChangedActionWithArgs { get; set; }
        Action? IElement.HandlerChangingAction { get; set; }
        Action<HandlerChangingEventArgs>? IElement.HandlerChangingActionWithArgs { get; set; }
        Action? IElement.HandlerChangedAction { get; set; }
        Action<EventArgs>? IElement.HandlerChangedActionWithArgs { get; set; }
        Action? IElement.PropertyChangedAction { get; set; }
        Action<EventArgs>? IElement.PropertyChangedActionWithArgs { get; set; }
        Action? IElement.PropertyChangingAction { get; set; }
        Action<EventArgs>? IElement.PropertyChangingActionWithArgs { get; set; }
        Action? IElement.BindingContextChangedAction { get; set; }
        Action<EventArgs>? IElement.BindingContextChangedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIElement = (IElement)this;
            if (NativeControl.AutomationId != thisAsIElement.AutomationId) NativeControl.AutomationId = thisAsIElement.AutomationId;
            if (NativeControl.ClassId != thisAsIElement.ClassId) NativeControl.ClassId = thisAsIElement.ClassId;


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
            if (thisAsIElement.PropertyChangedAction != null || thisAsIElement.PropertyChangedActionWithArgs != null)
            {
                NativeControl.PropertyChanged += NativeControl_PropertyChanged;
            }
            if (thisAsIElement.PropertyChangingAction != null || thisAsIElement.PropertyChangingActionWithArgs != null)
            {
                NativeControl.PropertyChanging += NativeControl_PropertyChanging;
            }
            if (thisAsIElement.BindingContextChangedAction != null || thisAsIElement.BindingContextChangedActionWithArgs != null)
            {
                NativeControl.BindingContextChanged += NativeControl_BindingContextChanged;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_ChildAdded(object? sender, ElementEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.ChildAddedAction?.Invoke();
            thisAsIElement.ChildAddedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_ChildRemoved(object? sender, ElementEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.ChildRemovedAction?.Invoke();
            thisAsIElement.ChildRemovedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_DescendantAdded(object? sender, ElementEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.DescendantAddedAction?.Invoke();
            thisAsIElement.DescendantAddedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_DescendantRemoved(object? sender, ElementEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.DescendantRemovedAction?.Invoke();
            thisAsIElement.DescendantRemovedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_ParentChanging(object? sender, ParentChangingEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.ParentChangingAction?.Invoke();
            thisAsIElement.ParentChangingActionWithArgs?.Invoke(e);
        }
        private void NativeControl_ParentChanged(object? sender, EventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.ParentChangedAction?.Invoke();
            thisAsIElement.ParentChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_HandlerChanging(object? sender, HandlerChangingEventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.HandlerChangingAction?.Invoke();
            thisAsIElement.HandlerChangingActionWithArgs?.Invoke(e);
        }
        private void NativeControl_HandlerChanged(object? sender, EventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.HandlerChangedAction?.Invoke();
            thisAsIElement.HandlerChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PropertyChanged(object? sender, EventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.PropertyChangedAction?.Invoke();
            thisAsIElement.PropertyChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PropertyChanging(object? sender, EventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.PropertyChangingAction?.Invoke();
            thisAsIElement.PropertyChangingActionWithArgs?.Invoke(e);
        }
        private void NativeControl_BindingContextChanged(object? sender, EventArgs e)
        {
            var thisAsIElement = (IElement)this;
            thisAsIElement.BindingContextChangedAction?.Invoke();
            thisAsIElement.BindingContextChangedActionWithArgs?.Invoke(e);
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
                NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
                NativeControl.PropertyChanging -= NativeControl_PropertyChanging;
                NativeControl.BindingContextChanged -= NativeControl_BindingContextChanged;
            }

            base.OnDetachNativeEvents();
        }

    }


    public static partial class ElementExtensions
    {
        public static T AutomationId<T>(this T element, string automationId) where T : IElement
        {
            element.AutomationId = automationId;
            return element;
        }

        public static T ClassId<T>(this T element, string classId) where T : IElement
        {
            element.ClassId = classId;
            return element;
        }


        public static T OnChildAdded<T>(this T element, Action childaddedAction) where T : IElement
        {
            element.ChildAddedAction = childaddedAction;
            return element;
        }

        public static T OnChildAdded<T>(this T element, Action<ElementEventArgs> childaddedActionWithArgs) where T : IElement
        {
            element.ChildAddedActionWithArgs = childaddedActionWithArgs;
            return element;
        }
        public static T OnChildRemoved<T>(this T element, Action childremovedAction) where T : IElement
        {
            element.ChildRemovedAction = childremovedAction;
            return element;
        }

        public static T OnChildRemoved<T>(this T element, Action<ElementEventArgs> childremovedActionWithArgs) where T : IElement
        {
            element.ChildRemovedActionWithArgs = childremovedActionWithArgs;
            return element;
        }
        public static T OnDescendantAdded<T>(this T element, Action descendantaddedAction) where T : IElement
        {
            element.DescendantAddedAction = descendantaddedAction;
            return element;
        }

        public static T OnDescendantAdded<T>(this T element, Action<ElementEventArgs> descendantaddedActionWithArgs) where T : IElement
        {
            element.DescendantAddedActionWithArgs = descendantaddedActionWithArgs;
            return element;
        }
        public static T OnDescendantRemoved<T>(this T element, Action descendantremovedAction) where T : IElement
        {
            element.DescendantRemovedAction = descendantremovedAction;
            return element;
        }

        public static T OnDescendantRemoved<T>(this T element, Action<ElementEventArgs> descendantremovedActionWithArgs) where T : IElement
        {
            element.DescendantRemovedActionWithArgs = descendantremovedActionWithArgs;
            return element;
        }
        public static T OnParentChanging<T>(this T element, Action parentchangingAction) where T : IElement
        {
            element.ParentChangingAction = parentchangingAction;
            return element;
        }

        public static T OnParentChanging<T>(this T element, Action<ParentChangingEventArgs> parentchangingActionWithArgs) where T : IElement
        {
            element.ParentChangingActionWithArgs = parentchangingActionWithArgs;
            return element;
        }
        public static T OnParentChanged<T>(this T element, Action parentchangedAction) where T : IElement
        {
            element.ParentChangedAction = parentchangedAction;
            return element;
        }

        public static T OnParentChanged<T>(this T element, Action<EventArgs> parentchangedActionWithArgs) where T : IElement
        {
            element.ParentChangedActionWithArgs = parentchangedActionWithArgs;
            return element;
        }
        public static T OnHandlerChanging<T>(this T element, Action handlerchangingAction) where T : IElement
        {
            element.HandlerChangingAction = handlerchangingAction;
            return element;
        }

        public static T OnHandlerChanging<T>(this T element, Action<HandlerChangingEventArgs> handlerchangingActionWithArgs) where T : IElement
        {
            element.HandlerChangingActionWithArgs = handlerchangingActionWithArgs;
            return element;
        }
        public static T OnHandlerChanged<T>(this T element, Action handlerchangedAction) where T : IElement
        {
            element.HandlerChangedAction = handlerchangedAction;
            return element;
        }

        public static T OnHandlerChanged<T>(this T element, Action<EventArgs> handlerchangedActionWithArgs) where T : IElement
        {
            element.HandlerChangedActionWithArgs = handlerchangedActionWithArgs;
            return element;
        }
        public static T OnPropertyChanged<T>(this T element, Action propertychangedAction) where T : IElement
        {
            element.PropertyChangedAction = propertychangedAction;
            return element;
        }

        public static T OnPropertyChanged<T>(this T element, Action<EventArgs> propertychangedActionWithArgs) where T : IElement
        {
            element.PropertyChangedActionWithArgs = propertychangedActionWithArgs;
            return element;
        }
        public static T OnPropertyChanging<T>(this T element, Action propertychangingAction) where T : IElement
        {
            element.PropertyChangingAction = propertychangingAction;
            return element;
        }

        public static T OnPropertyChanging<T>(this T element, Action<EventArgs> propertychangingActionWithArgs) where T : IElement
        {
            element.PropertyChangingActionWithArgs = propertychangingActionWithArgs;
            return element;
        }
        public static T OnBindingContextChanged<T>(this T element, Action bindingcontextchangedAction) where T : IElement
        {
            element.BindingContextChangedAction = bindingcontextchangedAction;
            return element;
        }

        public static T OnBindingContextChanged<T>(this T element, Action<EventArgs> bindingcontextchangedActionWithArgs) where T : IElement
        {
            element.BindingContextChangedActionWithArgs = bindingcontextchangedActionWithArgs;
            return element;
        }
    }
}
