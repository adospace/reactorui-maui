using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface IVisualElement
    {
        Microsoft.Maui.Controls.Shadow Shadow { get; set; }
        Microsoft.Maui.Controls.Style Style { get; set; }
        bool InputTransparent { get; set; }
        bool IsEnabled { get; set; }
        double AnchorX { get; set; }
        double AnchorY { get; set; }
        double TranslationX { get; set; }
        double TranslationY { get; set; }
        double Rotation { get; set; }
        double RotationX { get; set; }
        double RotationY { get; set; }
        double Scale { get; set; }
        double ScaleX { get; set; }
        double ScaleY { get; set; }
        Microsoft.Maui.Controls.Shapes.Geometry Clip { get; set; }
        Microsoft.Maui.Controls.IVisual Visual { get; set; }
        bool IsVisible { get; set; }
        double Opacity { get; set; }
        Microsoft.Maui.Graphics.Color BackgroundColor { get; set; }
        Microsoft.Maui.Controls.Brush Background { get; set; }
        double WidthRequest { get; set; }
        double HeightRequest { get; set; }
        double MinimumWidthRequest { get; set; }
        double MinimumHeightRequest { get; set; }
        double MaximumWidthRequest { get; set; }
        double MaximumHeightRequest { get; set; }
        Microsoft.Maui.FlowDirection FlowDirection { get; set; }

        Action? ChildrenReorderedAction { get; set; }
        Action<EventArgs>? ChildrenReorderedActionWithArgs { get; set; }
        Action? FocusedAction { get; set; }
        Action<FocusEventArgs>? FocusedActionWithArgs { get; set; }
        Action? MeasureInvalidatedAction { get; set; }
        Action<EventArgs>? MeasureInvalidatedActionWithArgs { get; set; }
        Action? SizeChangedAction { get; set; }
        Action<EventArgs>? SizeChangedActionWithArgs { get; set; }
        Action? UnfocusedAction { get; set; }
        Action<FocusEventArgs>? UnfocusedActionWithArgs { get; set; }
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

    public abstract partial class VisualElement<T> : NavigableElement<T>, IVisualElement where T : Microsoft.Maui.Controls.VisualElement, new()
    {
        protected VisualElement()
        {

        }

        protected VisualElement(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Controls.Shadow IVisualElement.Shadow { get; set; } = (Microsoft.Maui.Controls.Shadow)VisualElement.ShadowProperty.DefaultValue;
        Microsoft.Maui.Controls.Style IVisualElement.Style { get; set; } = (Microsoft.Maui.Controls.Style)VisualElement.StyleProperty.DefaultValue;
        bool IVisualElement.InputTransparent { get; set; } = (bool)VisualElement.InputTransparentProperty.DefaultValue;
        bool IVisualElement.IsEnabled { get; set; } = (bool)VisualElement.IsEnabledProperty.DefaultValue;
        double IVisualElement.AnchorX { get; set; } = (double)VisualElement.AnchorXProperty.DefaultValue;
        double IVisualElement.AnchorY { get; set; } = (double)VisualElement.AnchorYProperty.DefaultValue;
        double IVisualElement.TranslationX { get; set; } = (double)VisualElement.TranslationXProperty.DefaultValue;
        double IVisualElement.TranslationY { get; set; } = (double)VisualElement.TranslationYProperty.DefaultValue;
        double IVisualElement.Rotation { get; set; } = (double)VisualElement.RotationProperty.DefaultValue;
        double IVisualElement.RotationX { get; set; } = (double)VisualElement.RotationXProperty.DefaultValue;
        double IVisualElement.RotationY { get; set; } = (double)VisualElement.RotationYProperty.DefaultValue;
        double IVisualElement.Scale { get; set; } = (double)VisualElement.ScaleProperty.DefaultValue;
        double IVisualElement.ScaleX { get; set; } = (double)VisualElement.ScaleXProperty.DefaultValue;
        double IVisualElement.ScaleY { get; set; } = (double)VisualElement.ScaleYProperty.DefaultValue;
        Microsoft.Maui.Controls.Shapes.Geometry IVisualElement.Clip { get; set; } = (Microsoft.Maui.Controls.Shapes.Geometry)VisualElement.ClipProperty.DefaultValue;
        Microsoft.Maui.Controls.IVisual IVisualElement.Visual { get; set; } = (Microsoft.Maui.Controls.IVisual)VisualElement.VisualProperty.DefaultValue;
        bool IVisualElement.IsVisible { get; set; } = (bool)VisualElement.IsVisibleProperty.DefaultValue;
        double IVisualElement.Opacity { get; set; } = (double)VisualElement.OpacityProperty.DefaultValue;
        Microsoft.Maui.Graphics.Color IVisualElement.BackgroundColor { get; set; } = (Microsoft.Maui.Graphics.Color)VisualElement.BackgroundColorProperty.DefaultValue;
        Microsoft.Maui.Controls.Brush IVisualElement.Background { get; set; } = (Microsoft.Maui.Controls.Brush)VisualElement.BackgroundProperty.DefaultValue;
        double IVisualElement.WidthRequest { get; set; } = (double)VisualElement.WidthRequestProperty.DefaultValue;
        double IVisualElement.HeightRequest { get; set; } = (double)VisualElement.HeightRequestProperty.DefaultValue;
        double IVisualElement.MinimumWidthRequest { get; set; } = (double)VisualElement.MinimumWidthRequestProperty.DefaultValue;
        double IVisualElement.MinimumHeightRequest { get; set; } = (double)VisualElement.MinimumHeightRequestProperty.DefaultValue;
        double IVisualElement.MaximumWidthRequest { get; set; } = (double)VisualElement.MaximumWidthRequestProperty.DefaultValue;
        double IVisualElement.MaximumHeightRequest { get; set; } = (double)VisualElement.MaximumHeightRequestProperty.DefaultValue;
        Microsoft.Maui.FlowDirection IVisualElement.FlowDirection { get; set; } = (Microsoft.Maui.FlowDirection)VisualElement.FlowDirectionProperty.DefaultValue;

        Action? IVisualElement.ChildrenReorderedAction { get; set; }
        Action<EventArgs>? IVisualElement.ChildrenReorderedActionWithArgs { get; set; }
        Action? IVisualElement.FocusedAction { get; set; }
        Action<FocusEventArgs>? IVisualElement.FocusedActionWithArgs { get; set; }
        Action? IVisualElement.MeasureInvalidatedAction { get; set; }
        Action<EventArgs>? IVisualElement.MeasureInvalidatedActionWithArgs { get; set; }
        Action? IVisualElement.SizeChangedAction { get; set; }
        Action<EventArgs>? IVisualElement.SizeChangedActionWithArgs { get; set; }
        Action? IVisualElement.UnfocusedAction { get; set; }
        Action<FocusEventArgs>? IVisualElement.UnfocusedActionWithArgs { get; set; }
        Action? IVisualElement.ChildAddedAction { get; set; }
        Action<ElementEventArgs>? IVisualElement.ChildAddedActionWithArgs { get; set; }
        Action? IVisualElement.ChildRemovedAction { get; set; }
        Action<ElementEventArgs>? IVisualElement.ChildRemovedActionWithArgs { get; set; }
        Action? IVisualElement.DescendantAddedAction { get; set; }
        Action<ElementEventArgs>? IVisualElement.DescendantAddedActionWithArgs { get; set; }
        Action? IVisualElement.DescendantRemovedAction { get; set; }
        Action<ElementEventArgs>? IVisualElement.DescendantRemovedActionWithArgs { get; set; }
        Action? IVisualElement.ParentChangingAction { get; set; }
        Action<ParentChangingEventArgs>? IVisualElement.ParentChangingActionWithArgs { get; set; }
        Action? IVisualElement.ParentChangedAction { get; set; }
        Action<EventArgs>? IVisualElement.ParentChangedActionWithArgs { get; set; }
        Action? IVisualElement.HandlerChangingAction { get; set; }
        Action<HandlerChangingEventArgs>? IVisualElement.HandlerChangingActionWithArgs { get; set; }
        Action? IVisualElement.HandlerChangedAction { get; set; }
        Action<EventArgs>? IVisualElement.HandlerChangedActionWithArgs { get; set; }
        Action? IVisualElement.PropertyChangedAction { get; set; }
        Action<EventArgs>? IVisualElement.PropertyChangedActionWithArgs { get; set; }
        Action? IVisualElement.PropertyChangingAction { get; set; }
        Action<EventArgs>? IVisualElement.PropertyChangingActionWithArgs { get; set; }
        Action? IVisualElement.BindingContextChangedAction { get; set; }
        Action<EventArgs>? IVisualElement.BindingContextChangedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIVisualElement = (IVisualElement)this;
            if (NativeControl.Shadow != thisAsIVisualElement.Shadow) NativeControl.Shadow = thisAsIVisualElement.Shadow;
            if (NativeControl.Style != thisAsIVisualElement.Style) NativeControl.Style = thisAsIVisualElement.Style;
            if (NativeControl.InputTransparent != thisAsIVisualElement.InputTransparent) NativeControl.InputTransparent = thisAsIVisualElement.InputTransparent;
            if (NativeControl.IsEnabled != thisAsIVisualElement.IsEnabled) NativeControl.IsEnabled = thisAsIVisualElement.IsEnabled;
            if (NativeControl.AnchorX != thisAsIVisualElement.AnchorX) NativeControl.AnchorX = thisAsIVisualElement.AnchorX;
            if (NativeControl.AnchorY != thisAsIVisualElement.AnchorY) NativeControl.AnchorY = thisAsIVisualElement.AnchorY;
            if (NativeControl.TranslationX != thisAsIVisualElement.TranslationX) NativeControl.TranslationX = thisAsIVisualElement.TranslationX;
            if (NativeControl.TranslationY != thisAsIVisualElement.TranslationY) NativeControl.TranslationY = thisAsIVisualElement.TranslationY;
            if (NativeControl.Rotation != thisAsIVisualElement.Rotation) NativeControl.Rotation = thisAsIVisualElement.Rotation;
            if (NativeControl.RotationX != thisAsIVisualElement.RotationX) NativeControl.RotationX = thisAsIVisualElement.RotationX;
            if (NativeControl.RotationY != thisAsIVisualElement.RotationY) NativeControl.RotationY = thisAsIVisualElement.RotationY;
            if (NativeControl.Scale != thisAsIVisualElement.Scale) NativeControl.Scale = thisAsIVisualElement.Scale;
            if (NativeControl.ScaleX != thisAsIVisualElement.ScaleX) NativeControl.ScaleX = thisAsIVisualElement.ScaleX;
            if (NativeControl.ScaleY != thisAsIVisualElement.ScaleY) NativeControl.ScaleY = thisAsIVisualElement.ScaleY;
            if (NativeControl.Clip != thisAsIVisualElement.Clip) NativeControl.Clip = thisAsIVisualElement.Clip;
            if (NativeControl.Visual != thisAsIVisualElement.Visual) NativeControl.Visual = thisAsIVisualElement.Visual;
            if (NativeControl.IsVisible != thisAsIVisualElement.IsVisible) NativeControl.IsVisible = thisAsIVisualElement.IsVisible;
            if (NativeControl.Opacity != thisAsIVisualElement.Opacity) NativeControl.Opacity = thisAsIVisualElement.Opacity;
            if (NativeControl.BackgroundColor != thisAsIVisualElement.BackgroundColor) NativeControl.BackgroundColor = thisAsIVisualElement.BackgroundColor;
            if (NativeControl.Background != thisAsIVisualElement.Background) NativeControl.Background = thisAsIVisualElement.Background;
            if (NativeControl.WidthRequest != thisAsIVisualElement.WidthRequest) NativeControl.WidthRequest = thisAsIVisualElement.WidthRequest;
            if (NativeControl.HeightRequest != thisAsIVisualElement.HeightRequest) NativeControl.HeightRequest = thisAsIVisualElement.HeightRequest;
            if (NativeControl.MinimumWidthRequest != thisAsIVisualElement.MinimumWidthRequest) NativeControl.MinimumWidthRequest = thisAsIVisualElement.MinimumWidthRequest;
            if (NativeControl.MinimumHeightRequest != thisAsIVisualElement.MinimumHeightRequest) NativeControl.MinimumHeightRequest = thisAsIVisualElement.MinimumHeightRequest;
            if (NativeControl.MaximumWidthRequest != thisAsIVisualElement.MaximumWidthRequest) NativeControl.MaximumWidthRequest = thisAsIVisualElement.MaximumWidthRequest;
            if (NativeControl.MaximumHeightRequest != thisAsIVisualElement.MaximumHeightRequest) NativeControl.MaximumHeightRequest = thisAsIVisualElement.MaximumHeightRequest;
            if (NativeControl.FlowDirection != thisAsIVisualElement.FlowDirection) NativeControl.FlowDirection = thisAsIVisualElement.FlowDirection;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIVisualElement = (IVisualElement)this;
            if (thisAsIVisualElement.ChildrenReorderedAction != null || thisAsIVisualElement.ChildrenReorderedActionWithArgs != null)
            {
                NativeControl.ChildrenReordered += NativeControl_ChildrenReordered;
            }
            if (thisAsIVisualElement.FocusedAction != null || thisAsIVisualElement.FocusedActionWithArgs != null)
            {
                NativeControl.Focused += NativeControl_Focused;
            }
            if (thisAsIVisualElement.MeasureInvalidatedAction != null || thisAsIVisualElement.MeasureInvalidatedActionWithArgs != null)
            {
                NativeControl.MeasureInvalidated += NativeControl_MeasureInvalidated;
            }
            if (thisAsIVisualElement.SizeChangedAction != null || thisAsIVisualElement.SizeChangedActionWithArgs != null)
            {
                NativeControl.SizeChanged += NativeControl_SizeChanged;
            }
            if (thisAsIVisualElement.UnfocusedAction != null || thisAsIVisualElement.UnfocusedActionWithArgs != null)
            {
                NativeControl.Unfocused += NativeControl_Unfocused;
            }
            if (thisAsIVisualElement.ChildAddedAction != null || thisAsIVisualElement.ChildAddedActionWithArgs != null)
            {
                NativeControl.ChildAdded += NativeControl_ChildAdded;
            }
            if (thisAsIVisualElement.ChildRemovedAction != null || thisAsIVisualElement.ChildRemovedActionWithArgs != null)
            {
                NativeControl.ChildRemoved += NativeControl_ChildRemoved;
            }
            if (thisAsIVisualElement.DescendantAddedAction != null || thisAsIVisualElement.DescendantAddedActionWithArgs != null)
            {
                NativeControl.DescendantAdded += NativeControl_DescendantAdded;
            }
            if (thisAsIVisualElement.DescendantRemovedAction != null || thisAsIVisualElement.DescendantRemovedActionWithArgs != null)
            {
                NativeControl.DescendantRemoved += NativeControl_DescendantRemoved;
            }
            if (thisAsIVisualElement.ParentChangingAction != null || thisAsIVisualElement.ParentChangingActionWithArgs != null)
            {
                NativeControl.ParentChanging += NativeControl_ParentChanging;
            }
            if (thisAsIVisualElement.ParentChangedAction != null || thisAsIVisualElement.ParentChangedActionWithArgs != null)
            {
                NativeControl.ParentChanged += NativeControl_ParentChanged;
            }
            if (thisAsIVisualElement.HandlerChangingAction != null || thisAsIVisualElement.HandlerChangingActionWithArgs != null)
            {
                NativeControl.HandlerChanging += NativeControl_HandlerChanging;
            }
            if (thisAsIVisualElement.HandlerChangedAction != null || thisAsIVisualElement.HandlerChangedActionWithArgs != null)
            {
                NativeControl.HandlerChanged += NativeControl_HandlerChanged;
            }
            if (thisAsIVisualElement.PropertyChangedAction != null || thisAsIVisualElement.PropertyChangedActionWithArgs != null)
            {
                NativeControl.PropertyChanged += NativeControl_PropertyChanged;
            }
            if (thisAsIVisualElement.PropertyChangingAction != null || thisAsIVisualElement.PropertyChangingActionWithArgs != null)
            {
                NativeControl.PropertyChanging += NativeControl_PropertyChanging;
            }
            if (thisAsIVisualElement.BindingContextChangedAction != null || thisAsIVisualElement.BindingContextChangedActionWithArgs != null)
            {
                NativeControl.BindingContextChanged += NativeControl_BindingContextChanged;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_ChildrenReordered(object? sender, EventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.ChildrenReorderedAction?.Invoke();
            thisAsIVisualElement.ChildrenReorderedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_Focused(object? sender, FocusEventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.FocusedAction?.Invoke();
            thisAsIVisualElement.FocusedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_MeasureInvalidated(object? sender, EventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.MeasureInvalidatedAction?.Invoke();
            thisAsIVisualElement.MeasureInvalidatedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_SizeChanged(object? sender, EventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.SizeChangedAction?.Invoke();
            thisAsIVisualElement.SizeChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_Unfocused(object? sender, FocusEventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.UnfocusedAction?.Invoke();
            thisAsIVisualElement.UnfocusedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_ChildAdded(object? sender, ElementEventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.ChildAddedAction?.Invoke();
            thisAsIVisualElement.ChildAddedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_ChildRemoved(object? sender, ElementEventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.ChildRemovedAction?.Invoke();
            thisAsIVisualElement.ChildRemovedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_DescendantAdded(object? sender, ElementEventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.DescendantAddedAction?.Invoke();
            thisAsIVisualElement.DescendantAddedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_DescendantRemoved(object? sender, ElementEventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.DescendantRemovedAction?.Invoke();
            thisAsIVisualElement.DescendantRemovedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_ParentChanging(object? sender, ParentChangingEventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.ParentChangingAction?.Invoke();
            thisAsIVisualElement.ParentChangingActionWithArgs?.Invoke(e);
        }
        private void NativeControl_ParentChanged(object? sender, EventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.ParentChangedAction?.Invoke();
            thisAsIVisualElement.ParentChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_HandlerChanging(object? sender, HandlerChangingEventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.HandlerChangingAction?.Invoke();
            thisAsIVisualElement.HandlerChangingActionWithArgs?.Invoke(e);
        }
        private void NativeControl_HandlerChanged(object? sender, EventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.HandlerChangedAction?.Invoke();
            thisAsIVisualElement.HandlerChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PropertyChanged(object? sender, EventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.PropertyChangedAction?.Invoke();
            thisAsIVisualElement.PropertyChangedActionWithArgs?.Invoke(e);
        }
        private void NativeControl_PropertyChanging(object? sender, EventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.PropertyChangingAction?.Invoke();
            thisAsIVisualElement.PropertyChangingActionWithArgs?.Invoke(e);
        }
        private void NativeControl_BindingContextChanged(object? sender, EventArgs e)
        {
            var thisAsIVisualElement = (IVisualElement)this;
            thisAsIVisualElement.BindingContextChangedAction?.Invoke();
            thisAsIVisualElement.BindingContextChangedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.ChildrenReordered -= NativeControl_ChildrenReordered;
                NativeControl.Focused -= NativeControl_Focused;
                NativeControl.MeasureInvalidated -= NativeControl_MeasureInvalidated;
                NativeControl.SizeChanged -= NativeControl_SizeChanged;
                NativeControl.Unfocused -= NativeControl_Unfocused;
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


    public static partial class VisualElementExtensions
    {
        public static T Shadow<T>(this T visualelement, Microsoft.Maui.Controls.Shadow shadow) where T : IVisualElement
        {
            visualelement.Shadow = shadow;
            return visualelement;
        }

        public static T Style<T>(this T visualelement, Microsoft.Maui.Controls.Style style) where T : IVisualElement
        {
            visualelement.Style = style;
            return visualelement;
        }

        public static T InputTransparent<T>(this T visualelement, bool inputTransparent) where T : IVisualElement
        {
            visualelement.InputTransparent = inputTransparent;
            return visualelement;
        }

        public static T IsEnabled<T>(this T visualelement, bool isEnabled) where T : IVisualElement
        {
            visualelement.IsEnabled = isEnabled;
            return visualelement;
        }

        public static T AnchorX<T>(this T visualelement, double anchorX) where T : IVisualElement
        {
            visualelement.AnchorX = anchorX;
            return visualelement;
        }

        public static T AnchorY<T>(this T visualelement, double anchorY) where T : IVisualElement
        {
            visualelement.AnchorY = anchorY;
            return visualelement;
        }

        public static T TranslationX<T>(this T visualelement, double translationX) where T : IVisualElement
        {
            visualelement.TranslationX = translationX;
            return visualelement;
        }

        public static T TranslationY<T>(this T visualelement, double translationY) where T : IVisualElement
        {
            visualelement.TranslationY = translationY;
            return visualelement;
        }

        public static T Rotation<T>(this T visualelement, double rotation) where T : IVisualElement
        {
            visualelement.Rotation = rotation;
            return visualelement;
        }

        public static T RotationX<T>(this T visualelement, double rotationX) where T : IVisualElement
        {
            visualelement.RotationX = rotationX;
            return visualelement;
        }

        public static T RotationY<T>(this T visualelement, double rotationY) where T : IVisualElement
        {
            visualelement.RotationY = rotationY;
            return visualelement;
        }

        public static T Scale<T>(this T visualelement, double scale) where T : IVisualElement
        {
            visualelement.Scale = scale;
            return visualelement;
        }

        public static T ScaleX<T>(this T visualelement, double scaleX) where T : IVisualElement
        {
            visualelement.ScaleX = scaleX;
            return visualelement;
        }

        public static T ScaleY<T>(this T visualelement, double scaleY) where T : IVisualElement
        {
            visualelement.ScaleY = scaleY;
            return visualelement;
        }

        public static T Clip<T>(this T visualelement, Microsoft.Maui.Controls.Shapes.Geometry clip) where T : IVisualElement
        {
            visualelement.Clip = clip;
            return visualelement;
        }

        public static T Visual<T>(this T visualelement, Microsoft.Maui.Controls.IVisual visual) where T : IVisualElement
        {
            visualelement.Visual = visual;
            return visualelement;
        }

        public static T IsVisible<T>(this T visualelement, bool isVisible) where T : IVisualElement
        {
            visualelement.IsVisible = isVisible;
            return visualelement;
        }

        public static T Opacity<T>(this T visualelement, double opacity) where T : IVisualElement
        {
            visualelement.Opacity = opacity;
            return visualelement;
        }

        public static T BackgroundColor<T>(this T visualelement, Microsoft.Maui.Graphics.Color backgroundColor) where T : IVisualElement
        {
            visualelement.BackgroundColor = backgroundColor;
            return visualelement;
        }

        public static T Background<T>(this T visualelement, Microsoft.Maui.Controls.Brush background) where T : IVisualElement
        {
            visualelement.Background = background;
            return visualelement;
        }

        public static T WidthRequest<T>(this T visualelement, double widthRequest) where T : IVisualElement
        {
            visualelement.WidthRequest = widthRequest;
            return visualelement;
        }

        public static T HeightRequest<T>(this T visualelement, double heightRequest) where T : IVisualElement
        {
            visualelement.HeightRequest = heightRequest;
            return visualelement;
        }

        public static T MinimumWidthRequest<T>(this T visualelement, double minimumWidthRequest) where T : IVisualElement
        {
            visualelement.MinimumWidthRequest = minimumWidthRequest;
            return visualelement;
        }

        public static T MinimumHeightRequest<T>(this T visualelement, double minimumHeightRequest) where T : IVisualElement
        {
            visualelement.MinimumHeightRequest = minimumHeightRequest;
            return visualelement;
        }

        public static T MaximumWidthRequest<T>(this T visualelement, double maximumWidthRequest) where T : IVisualElement
        {
            visualelement.MaximumWidthRequest = maximumWidthRequest;
            return visualelement;
        }

        public static T MaximumHeightRequest<T>(this T visualelement, double maximumHeightRequest) where T : IVisualElement
        {
            visualelement.MaximumHeightRequest = maximumHeightRequest;
            return visualelement;
        }

        public static T FlowDirection<T>(this T visualelement, Microsoft.Maui.FlowDirection flowDirection) where T : IVisualElement
        {
            visualelement.FlowDirection = flowDirection;
            return visualelement;
        }


        public static T OnChildrenReordered<T>(this T visualelement, Action childrenreorderedAction) where T : IVisualElement
        {
            visualelement.ChildrenReorderedAction = childrenreorderedAction;
            return visualelement;
        }

        public static T OnChildrenReordered<T>(this T visualelement, Action<EventArgs> childrenreorderedActionWithArgs) where T : IVisualElement
        {
            visualelement.ChildrenReorderedActionWithArgs = childrenreorderedActionWithArgs;
            return visualelement;
        }
        public static T OnFocused<T>(this T visualelement, Action focusedAction) where T : IVisualElement
        {
            visualelement.FocusedAction = focusedAction;
            return visualelement;
        }

        public static T OnFocused<T>(this T visualelement, Action<FocusEventArgs> focusedActionWithArgs) where T : IVisualElement
        {
            visualelement.FocusedActionWithArgs = focusedActionWithArgs;
            return visualelement;
        }
        public static T OnMeasureInvalidated<T>(this T visualelement, Action measureinvalidatedAction) where T : IVisualElement
        {
            visualelement.MeasureInvalidatedAction = measureinvalidatedAction;
            return visualelement;
        }

        public static T OnMeasureInvalidated<T>(this T visualelement, Action<EventArgs> measureinvalidatedActionWithArgs) where T : IVisualElement
        {
            visualelement.MeasureInvalidatedActionWithArgs = measureinvalidatedActionWithArgs;
            return visualelement;
        }
        public static T OnSizeChanged<T>(this T visualelement, Action sizechangedAction) where T : IVisualElement
        {
            visualelement.SizeChangedAction = sizechangedAction;
            return visualelement;
        }

        public static T OnSizeChanged<T>(this T visualelement, Action<EventArgs> sizechangedActionWithArgs) where T : IVisualElement
        {
            visualelement.SizeChangedActionWithArgs = sizechangedActionWithArgs;
            return visualelement;
        }
        public static T OnUnfocused<T>(this T visualelement, Action unfocusedAction) where T : IVisualElement
        {
            visualelement.UnfocusedAction = unfocusedAction;
            return visualelement;
        }

        public static T OnUnfocused<T>(this T visualelement, Action<FocusEventArgs> unfocusedActionWithArgs) where T : IVisualElement
        {
            visualelement.UnfocusedActionWithArgs = unfocusedActionWithArgs;
            return visualelement;
        }
        public static T OnChildAdded<T>(this T visualelement, Action childaddedAction) where T : IVisualElement
        {
            visualelement.ChildAddedAction = childaddedAction;
            return visualelement;
        }

        public static T OnChildAdded<T>(this T visualelement, Action<ElementEventArgs> childaddedActionWithArgs) where T : IVisualElement
        {
            visualelement.ChildAddedActionWithArgs = childaddedActionWithArgs;
            return visualelement;
        }
        public static T OnChildRemoved<T>(this T visualelement, Action childremovedAction) where T : IVisualElement
        {
            visualelement.ChildRemovedAction = childremovedAction;
            return visualelement;
        }

        public static T OnChildRemoved<T>(this T visualelement, Action<ElementEventArgs> childremovedActionWithArgs) where T : IVisualElement
        {
            visualelement.ChildRemovedActionWithArgs = childremovedActionWithArgs;
            return visualelement;
        }
        public static T OnDescendantAdded<T>(this T visualelement, Action descendantaddedAction) where T : IVisualElement
        {
            visualelement.DescendantAddedAction = descendantaddedAction;
            return visualelement;
        }

        public static T OnDescendantAdded<T>(this T visualelement, Action<ElementEventArgs> descendantaddedActionWithArgs) where T : IVisualElement
        {
            visualelement.DescendantAddedActionWithArgs = descendantaddedActionWithArgs;
            return visualelement;
        }
        public static T OnDescendantRemoved<T>(this T visualelement, Action descendantremovedAction) where T : IVisualElement
        {
            visualelement.DescendantRemovedAction = descendantremovedAction;
            return visualelement;
        }

        public static T OnDescendantRemoved<T>(this T visualelement, Action<ElementEventArgs> descendantremovedActionWithArgs) where T : IVisualElement
        {
            visualelement.DescendantRemovedActionWithArgs = descendantremovedActionWithArgs;
            return visualelement;
        }
        public static T OnParentChanging<T>(this T visualelement, Action parentchangingAction) where T : IVisualElement
        {
            visualelement.ParentChangingAction = parentchangingAction;
            return visualelement;
        }

        public static T OnParentChanging<T>(this T visualelement, Action<ParentChangingEventArgs> parentchangingActionWithArgs) where T : IVisualElement
        {
            visualelement.ParentChangingActionWithArgs = parentchangingActionWithArgs;
            return visualelement;
        }
        public static T OnParentChanged<T>(this T visualelement, Action parentchangedAction) where T : IVisualElement
        {
            visualelement.ParentChangedAction = parentchangedAction;
            return visualelement;
        }

        public static T OnParentChanged<T>(this T visualelement, Action<EventArgs> parentchangedActionWithArgs) where T : IVisualElement
        {
            visualelement.ParentChangedActionWithArgs = parentchangedActionWithArgs;
            return visualelement;
        }
        public static T OnHandlerChanging<T>(this T visualelement, Action handlerchangingAction) where T : IVisualElement
        {
            visualelement.HandlerChangingAction = handlerchangingAction;
            return visualelement;
        }

        public static T OnHandlerChanging<T>(this T visualelement, Action<HandlerChangingEventArgs> handlerchangingActionWithArgs) where T : IVisualElement
        {
            visualelement.HandlerChangingActionWithArgs = handlerchangingActionWithArgs;
            return visualelement;
        }
        public static T OnHandlerChanged<T>(this T visualelement, Action handlerchangedAction) where T : IVisualElement
        {
            visualelement.HandlerChangedAction = handlerchangedAction;
            return visualelement;
        }

        public static T OnHandlerChanged<T>(this T visualelement, Action<EventArgs> handlerchangedActionWithArgs) where T : IVisualElement
        {
            visualelement.HandlerChangedActionWithArgs = handlerchangedActionWithArgs;
            return visualelement;
        }
        public static T OnPropertyChanged<T>(this T visualelement, Action propertychangedAction) where T : IVisualElement
        {
            visualelement.PropertyChangedAction = propertychangedAction;
            return visualelement;
        }

        public static T OnPropertyChanged<T>(this T visualelement, Action<EventArgs> propertychangedActionWithArgs) where T : IVisualElement
        {
            visualelement.PropertyChangedActionWithArgs = propertychangedActionWithArgs;
            return visualelement;
        }
        public static T OnPropertyChanging<T>(this T visualelement, Action propertychangingAction) where T : IVisualElement
        {
            visualelement.PropertyChangingAction = propertychangingAction;
            return visualelement;
        }

        public static T OnPropertyChanging<T>(this T visualelement, Action<EventArgs> propertychangingActionWithArgs) where T : IVisualElement
        {
            visualelement.PropertyChangingActionWithArgs = propertychangingActionWithArgs;
            return visualelement;
        }
        public static T OnBindingContextChanged<T>(this T visualelement, Action bindingcontextchangedAction) where T : IVisualElement
        {
            visualelement.BindingContextChangedAction = bindingcontextchangedAction;
            return visualelement;
        }

        public static T OnBindingContextChanged<T>(this T visualelement, Action<EventArgs> bindingcontextchangedActionWithArgs) where T : IVisualElement
        {
            visualelement.BindingContextChangedActionWithArgs = bindingcontextchangedActionWithArgs;
            return visualelement;
        }
    }
}
