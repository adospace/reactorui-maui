using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface INavigableElement
    {
        Microsoft.Maui.Controls.Style Style { get; set; }

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

    public abstract partial class NavigableElement<T> : Element<T>, INavigableElement where T : Microsoft.Maui.Controls.NavigableElement, new()
    {
        protected NavigableElement()
        {

        }

        protected NavigableElement(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Controls.Style INavigableElement.Style { get; set; } = (Microsoft.Maui.Controls.Style)NavigableElement.StyleProperty.DefaultValue;

        Action? INavigableElement.ChildAddedAction { get; set; }
        Action<ElementEventArgs>? INavigableElement.ChildAddedActionWithArgs { get; set; }
        Action? INavigableElement.ChildRemovedAction { get; set; }
        Action<ElementEventArgs>? INavigableElement.ChildRemovedActionWithArgs { get; set; }
        Action? INavigableElement.DescendantAddedAction { get; set; }
        Action<ElementEventArgs>? INavigableElement.DescendantAddedActionWithArgs { get; set; }
        Action? INavigableElement.DescendantRemovedAction { get; set; }
        Action<ElementEventArgs>? INavigableElement.DescendantRemovedActionWithArgs { get; set; }
        Action? INavigableElement.ParentChangingAction { get; set; }
        Action<ParentChangingEventArgs>? INavigableElement.ParentChangingActionWithArgs { get; set; }
        Action? INavigableElement.ParentChangedAction { get; set; }
        Action<EventArgs>? INavigableElement.ParentChangedActionWithArgs { get; set; }
        Action? INavigableElement.HandlerChangingAction { get; set; }
        Action<HandlerChangingEventArgs>? INavigableElement.HandlerChangingActionWithArgs { get; set; }
        Action? INavigableElement.HandlerChangedAction { get; set; }
        Action<EventArgs>? INavigableElement.HandlerChangedActionWithArgs { get; set; }
        Action? INavigableElement.PropertyChangedAction { get; set; }
        Action<EventArgs>? INavigableElement.PropertyChangedActionWithArgs { get; set; }
        Action? INavigableElement.PropertyChangingAction { get; set; }
        Action<EventArgs>? INavigableElement.PropertyChangingActionWithArgs { get; set; }
        Action? INavigableElement.BindingContextChangedAction { get; set; }
        Action<EventArgs>? INavigableElement.BindingContextChangedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsINavigableElement = (INavigableElement)this;
            if (NativeControl.Style != thisAsINavigableElement.Style) NativeControl.Style = thisAsINavigableElement.Style;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsINavigableElement = (INavigableElement)this;
            if (thisAsINavigableElement.ChildAddedAction != null || thisAsINavigableElement.ChildAddedActionWithArgs != null)
            {
                NativeControl.ChildAdded += NativeControl_ChildAdded;
            }
            if (thisAsINavigableElement.ChildRemovedAction != null || thisAsINavigableElement.ChildRemovedActionWithArgs != null)
            {
                NativeControl.ChildRemoved += NativeControl_ChildRemoved;
            }
            if (thisAsINavigableElement.DescendantAddedAction != null || thisAsINavigableElement.DescendantAddedActionWithArgs != null)
            {
                NativeControl.DescendantAdded += NativeControl_DescendantAdded;
            }
            if (thisAsINavigableElement.DescendantRemovedAction != null || thisAsINavigableElement.DescendantRemovedActionWithArgs != null)
            {
                NativeControl.DescendantRemoved += NativeControl_DescendantRemoved;
            }
            if (thisAsINavigableElement.ParentChangingAction != null || thisAsINavigableElement.ParentChangingActionWithArgs != null)
            {
                NativeControl.ParentChanging += NativeControl_ParentChanging;
            }
            if (thisAsINavigableElement.ParentChangedAction != null || thisAsINavigableElement.ParentChangedActionWithArgs != null)
            {
                NativeControl.ParentChanged += NativeControl_ParentChanged;
            }
            if (thisAsINavigableElement.HandlerChangingAction != null || thisAsINavigableElement.HandlerChangingActionWithArgs != null)
            {
                NativeControl.HandlerChanging += NativeControl_HandlerChanging;
            }
            if (thisAsINavigableElement.HandlerChangedAction != null || thisAsINavigableElement.HandlerChangedActionWithArgs != null)
            {
                NativeControl.HandlerChanged += NativeControl_HandlerChanged;
            }
            if (thisAsINavigableElement.PropertyChangedAction != null || thisAsINavigableElement.PropertyChangedActionWithArgs != null)
            {
                NativeControl.PropertyChanged += NativeControl_PropertyChanged;
            }
            if (thisAsINavigableElement.PropertyChangingAction != null || thisAsINavigableElement.PropertyChangingActionWithArgs != null)
            {
                NativeControl.PropertyChanging += NativeControl_PropertyChanging;
            }
            if (thisAsINavigableElement.BindingContextChangedAction != null || thisAsINavigableElement.BindingContextChangedActionWithArgs != null)
            {
                NativeControl.BindingContextChanged += NativeControl_BindingContextChanged;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_ChildAdded(object? sender, ElementEventArgs e)
        {
            var thisAsINavigableElement = (INavigableElement)this;
            thisAsINavigableElement.ChildAddedAction?.Invoke();
            thisAsINavigableElement.ChildAddedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_ChildRemoved(object? sender, ElementEventArgs e)
        {
            var thisAsINavigableElement = (INavigableElement)this;
            thisAsINavigableElement.ChildRemovedAction?.Invoke();
            thisAsINavigableElement.ChildRemovedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_DescendantAdded(object? sender, ElementEventArgs e)
        {
            var thisAsINavigableElement = (INavigableElement)this;
            thisAsINavigableElement.DescendantAddedAction?.Invoke();
            thisAsINavigableElement.DescendantAddedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_DescendantRemoved(object? sender, ElementEventArgs e)
        {
            var thisAsINavigableElement = (INavigableElement)this;
            thisAsINavigableElement.DescendantRemovedAction?.Invoke();
            thisAsINavigableElement.DescendantRemovedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_ParentChanging(object? sender, ParentChangingEventArgs e)
        {
            var thisAsINavigableElement = (INavigableElement)this;
            thisAsINavigableElement.ParentChangingAction?.Invoke();
            thisAsINavigableElement.ParentChangingActionWithArgs?.Invoke(e);
        }
        private void NativeControl_ParentChanged(object? sender, EventArgs e)
        {
            var thisAsINavigableElement = (INavigableElement)this;
            thisAsINavigableElement.ParentChangedAction?.Invoke();
            thisAsINavigableElement.ParentChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_HandlerChanging(object? sender, HandlerChangingEventArgs e)
        {
            var thisAsINavigableElement = (INavigableElement)this;
            thisAsINavigableElement.HandlerChangingAction?.Invoke();
            thisAsINavigableElement.HandlerChangingActionWithArgs?.Invoke(e);
        }
        private void NativeControl_HandlerChanged(object? sender, EventArgs e)
        {
            var thisAsINavigableElement = (INavigableElement)this;
            thisAsINavigableElement.HandlerChangedAction?.Invoke();
            thisAsINavigableElement.HandlerChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PropertyChanged(object? sender, EventArgs e)
        {
            var thisAsINavigableElement = (INavigableElement)this;
            thisAsINavigableElement.PropertyChangedAction?.Invoke();
            thisAsINavigableElement.PropertyChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PropertyChanging(object? sender, EventArgs e)
        {
            var thisAsINavigableElement = (INavigableElement)this;
            thisAsINavigableElement.PropertyChangingAction?.Invoke();
            thisAsINavigableElement.PropertyChangingActionWithArgs?.Invoke(e);
        }
        private void NativeControl_BindingContextChanged(object? sender, EventArgs e)
        {
            var thisAsINavigableElement = (INavigableElement)this;
            thisAsINavigableElement.BindingContextChangedAction?.Invoke();
            thisAsINavigableElement.BindingContextChangedActionWithArgs?.Invoke(e);
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


    public static partial class NavigableElementExtensions
    {
        public static T Style<T>(this T navigableelement, Microsoft.Maui.Controls.Style style) where T : INavigableElement
        {
            navigableelement.Style = style;
            return navigableelement;
        }


        public static T OnChildAdded<T>(this T navigableelement, Action childaddedAction) where T : INavigableElement
        {
            navigableelement.ChildAddedAction = childaddedAction;
            return navigableelement;
        }

        public static T OnChildAdded<T>(this T navigableelement, Action<ElementEventArgs> childaddedActionWithArgs) where T : INavigableElement
        {
            navigableelement.ChildAddedActionWithArgs = childaddedActionWithArgs;
            return navigableelement;
        }
        public static T OnChildRemoved<T>(this T navigableelement, Action childremovedAction) where T : INavigableElement
        {
            navigableelement.ChildRemovedAction = childremovedAction;
            return navigableelement;
        }

        public static T OnChildRemoved<T>(this T navigableelement, Action<ElementEventArgs> childremovedActionWithArgs) where T : INavigableElement
        {
            navigableelement.ChildRemovedActionWithArgs = childremovedActionWithArgs;
            return navigableelement;
        }
        public static T OnDescendantAdded<T>(this T navigableelement, Action descendantaddedAction) where T : INavigableElement
        {
            navigableelement.DescendantAddedAction = descendantaddedAction;
            return navigableelement;
        }

        public static T OnDescendantAdded<T>(this T navigableelement, Action<ElementEventArgs> descendantaddedActionWithArgs) where T : INavigableElement
        {
            navigableelement.DescendantAddedActionWithArgs = descendantaddedActionWithArgs;
            return navigableelement;
        }
        public static T OnDescendantRemoved<T>(this T navigableelement, Action descendantremovedAction) where T : INavigableElement
        {
            navigableelement.DescendantRemovedAction = descendantremovedAction;
            return navigableelement;
        }

        public static T OnDescendantRemoved<T>(this T navigableelement, Action<ElementEventArgs> descendantremovedActionWithArgs) where T : INavigableElement
        {
            navigableelement.DescendantRemovedActionWithArgs = descendantremovedActionWithArgs;
            return navigableelement;
        }
        public static T OnParentChanging<T>(this T navigableelement, Action parentchangingAction) where T : INavigableElement
        {
            navigableelement.ParentChangingAction = parentchangingAction;
            return navigableelement;
        }

        public static T OnParentChanging<T>(this T navigableelement, Action<ParentChangingEventArgs> parentchangingActionWithArgs) where T : INavigableElement
        {
            navigableelement.ParentChangingActionWithArgs = parentchangingActionWithArgs;
            return navigableelement;
        }
        public static T OnParentChanged<T>(this T navigableelement, Action parentchangedAction) where T : INavigableElement
        {
            navigableelement.ParentChangedAction = parentchangedAction;
            return navigableelement;
        }

        public static T OnParentChanged<T>(this T navigableelement, Action<EventArgs> parentchangedActionWithArgs) where T : INavigableElement
        {
            navigableelement.ParentChangedActionWithArgs = parentchangedActionWithArgs;
            return navigableelement;
        }
        public static T OnHandlerChanging<T>(this T navigableelement, Action handlerchangingAction) where T : INavigableElement
        {
            navigableelement.HandlerChangingAction = handlerchangingAction;
            return navigableelement;
        }

        public static T OnHandlerChanging<T>(this T navigableelement, Action<HandlerChangingEventArgs> handlerchangingActionWithArgs) where T : INavigableElement
        {
            navigableelement.HandlerChangingActionWithArgs = handlerchangingActionWithArgs;
            return navigableelement;
        }
        public static T OnHandlerChanged<T>(this T navigableelement, Action handlerchangedAction) where T : INavigableElement
        {
            navigableelement.HandlerChangedAction = handlerchangedAction;
            return navigableelement;
        }

        public static T OnHandlerChanged<T>(this T navigableelement, Action<EventArgs> handlerchangedActionWithArgs) where T : INavigableElement
        {
            navigableelement.HandlerChangedActionWithArgs = handlerchangedActionWithArgs;
            return navigableelement;
        }
        public static T OnPropertyChanged<T>(this T navigableelement, Action propertychangedAction) where T : INavigableElement
        {
            navigableelement.PropertyChangedAction = propertychangedAction;
            return navigableelement;
        }

        public static T OnPropertyChanged<T>(this T navigableelement, Action<EventArgs> propertychangedActionWithArgs) where T : INavigableElement
        {
            navigableelement.PropertyChangedActionWithArgs = propertychangedActionWithArgs;
            return navigableelement;
        }
        public static T OnPropertyChanging<T>(this T navigableelement, Action propertychangingAction) where T : INavigableElement
        {
            navigableelement.PropertyChangingAction = propertychangingAction;
            return navigableelement;
        }

        public static T OnPropertyChanging<T>(this T navigableelement, Action<EventArgs> propertychangingActionWithArgs) where T : INavigableElement
        {
            navigableelement.PropertyChangingActionWithArgs = propertychangingActionWithArgs;
            return navigableelement;
        }
        public static T OnBindingContextChanged<T>(this T navigableelement, Action bindingcontextchangedAction) where T : INavigableElement
        {
            navigableelement.BindingContextChangedAction = bindingcontextchangedAction;
            return navigableelement;
        }

        public static T OnBindingContextChanged<T>(this T navigableelement, Action<EventArgs> bindingcontextchangedActionWithArgs) where T : INavigableElement
        {
            navigableelement.BindingContextChangedActionWithArgs = bindingcontextchangedActionWithArgs;
            return navigableelement;
        }
    }
}
