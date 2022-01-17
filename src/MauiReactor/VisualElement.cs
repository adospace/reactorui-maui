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
    public partial interface IVisualElement : INavigableElement
    {
        PropertyValue<Microsoft.Maui.Controls.Shadow>? Shadow { get; set; }
        PropertyValue<Microsoft.Maui.Controls.Style>? Style { get; set; }
        PropertyValue<bool>? InputTransparent { get; set; }
        PropertyValue<bool>? IsEnabled { get; set; }
        PropertyValue<double>? AnchorX { get; set; }
        PropertyValue<double>? AnchorY { get; set; }
        PropertyValue<double>? TranslationX { get; set; }
        PropertyValue<double>? TranslationY { get; set; }
        PropertyValue<double>? Rotation { get; set; }
        PropertyValue<double>? RotationX { get; set; }
        PropertyValue<double>? RotationY { get; set; }
        PropertyValue<double>? Scale { get; set; }
        PropertyValue<double>? ScaleX { get; set; }
        PropertyValue<double>? ScaleY { get; set; }
        PropertyValue<Microsoft.Maui.Controls.IVisual>? Visual { get; set; }
        PropertyValue<bool>? IsVisible { get; set; }
        PropertyValue<double>? Opacity { get; set; }
        PropertyValue<Microsoft.Maui.Graphics.Color>? BackgroundColor { get; set; }
        PropertyValue<Microsoft.Maui.Controls.Brush>? Background { get; set; }
        PropertyValue<double>? WidthRequest { get; set; }
        PropertyValue<double>? HeightRequest { get; set; }
        PropertyValue<double>? MinimumWidthRequest { get; set; }
        PropertyValue<double>? MinimumHeightRequest { get; set; }
        PropertyValue<double>? MaximumWidthRequest { get; set; }
        PropertyValue<double>? MaximumHeightRequest { get; set; }
        PropertyValue<Microsoft.Maui.FlowDirection>? FlowDirection { get; set; }

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

        PropertyValue<Microsoft.Maui.Controls.Shadow>? IVisualElement.Shadow { get; set; }
        PropertyValue<Microsoft.Maui.Controls.Style>? IVisualElement.Style { get; set; }
        PropertyValue<bool>? IVisualElement.InputTransparent { get; set; }
        PropertyValue<bool>? IVisualElement.IsEnabled { get; set; }
        PropertyValue<double>? IVisualElement.AnchorX { get; set; }
        PropertyValue<double>? IVisualElement.AnchorY { get; set; }
        PropertyValue<double>? IVisualElement.TranslationX { get; set; }
        PropertyValue<double>? IVisualElement.TranslationY { get; set; }
        PropertyValue<double>? IVisualElement.Rotation { get; set; }
        PropertyValue<double>? IVisualElement.RotationX { get; set; }
        PropertyValue<double>? IVisualElement.RotationY { get; set; }
        PropertyValue<double>? IVisualElement.Scale { get; set; }
        PropertyValue<double>? IVisualElement.ScaleX { get; set; }
        PropertyValue<double>? IVisualElement.ScaleY { get; set; }
        PropertyValue<Microsoft.Maui.Controls.IVisual>? IVisualElement.Visual { get; set; }
        PropertyValue<bool>? IVisualElement.IsVisible { get; set; }
        PropertyValue<double>? IVisualElement.Opacity { get; set; }
        PropertyValue<Microsoft.Maui.Graphics.Color>? IVisualElement.BackgroundColor { get; set; }
        PropertyValue<Microsoft.Maui.Controls.Brush>? IVisualElement.Background { get; set; }
        PropertyValue<double>? IVisualElement.WidthRequest { get; set; }
        PropertyValue<double>? IVisualElement.HeightRequest { get; set; }
        PropertyValue<double>? IVisualElement.MinimumWidthRequest { get; set; }
        PropertyValue<double>? IVisualElement.MinimumHeightRequest { get; set; }
        PropertyValue<double>? IVisualElement.MaximumWidthRequest { get; set; }
        PropertyValue<double>? IVisualElement.MaximumHeightRequest { get; set; }
        PropertyValue<Microsoft.Maui.FlowDirection>? IVisualElement.FlowDirection { get; set; }

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

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIVisualElement = (IVisualElement)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.ShadowProperty, thisAsIVisualElement.Shadow);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.StyleProperty, thisAsIVisualElement.Style);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.InputTransparentProperty, thisAsIVisualElement.InputTransparent);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.IsEnabledProperty, thisAsIVisualElement.IsEnabled);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.AnchorXProperty, thisAsIVisualElement.AnchorX);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.AnchorYProperty, thisAsIVisualElement.AnchorY);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.TranslationXProperty, thisAsIVisualElement.TranslationX);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.TranslationYProperty, thisAsIVisualElement.TranslationY);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.RotationProperty, thisAsIVisualElement.Rotation);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.RotationXProperty, thisAsIVisualElement.RotationX);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.RotationYProperty, thisAsIVisualElement.RotationY);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.ScaleProperty, thisAsIVisualElement.Scale);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.ScaleXProperty, thisAsIVisualElement.ScaleX);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.ScaleYProperty, thisAsIVisualElement.ScaleY);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.VisualProperty, thisAsIVisualElement.Visual);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.IsVisibleProperty, thisAsIVisualElement.IsVisible);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.OpacityProperty, thisAsIVisualElement.Opacity);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.BackgroundColorProperty, thisAsIVisualElement.BackgroundColor);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.BackgroundProperty, thisAsIVisualElement.Background);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.WidthRequestProperty, thisAsIVisualElement.WidthRequest);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.HeightRequestProperty, thisAsIVisualElement.HeightRequest);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.MinimumWidthRequestProperty, thisAsIVisualElement.MinimumWidthRequest);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.MinimumHeightRequestProperty, thisAsIVisualElement.MinimumHeightRequest);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.MaximumWidthRequestProperty, thisAsIVisualElement.MaximumWidthRequest);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.MaximumHeightRequestProperty, thisAsIVisualElement.MaximumHeightRequest);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.VisualElement.FlowDirectionProperty, thisAsIVisualElement.FlowDirection);


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

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.ChildrenReordered -= NativeControl_ChildrenReordered;
                NativeControl.Focused -= NativeControl_Focused;
                NativeControl.MeasureInvalidated -= NativeControl_MeasureInvalidated;
                NativeControl.SizeChanged -= NativeControl_SizeChanged;
                NativeControl.Unfocused -= NativeControl_Unfocused;
            }

            base.OnDetachNativeEvents();
        }

    }


    public static partial class VisualElementExtensions
    {
        public static T Shadow<T>(this T visualelement, Microsoft.Maui.Controls.Shadow shadow) where T : IVisualElement
        {
            visualelement.Shadow = new PropertyValue<Microsoft.Maui.Controls.Shadow>(shadow);
            return visualelement;
        }
        public static T Shadow<T>(this T visualelement, Func<Microsoft.Maui.Controls.Shadow> shadowFunc) where T : IVisualElement
        {
            visualelement.Shadow = new PropertyValue<Microsoft.Maui.Controls.Shadow>(shadowFunc);
            return visualelement;
        }



        public static T Style<T>(this T visualelement, Microsoft.Maui.Controls.Style style) where T : IVisualElement
        {
            visualelement.Style = new PropertyValue<Microsoft.Maui.Controls.Style>(style);
            return visualelement;
        }
        public static T Style<T>(this T visualelement, Func<Microsoft.Maui.Controls.Style> styleFunc) where T : IVisualElement
        {
            visualelement.Style = new PropertyValue<Microsoft.Maui.Controls.Style>(styleFunc);
            return visualelement;
        }



        public static T InputTransparent<T>(this T visualelement, bool inputTransparent) where T : IVisualElement
        {
            visualelement.InputTransparent = new PropertyValue<bool>(inputTransparent);
            return visualelement;
        }
        public static T InputTransparent<T>(this T visualelement, Func<bool> inputTransparentFunc) where T : IVisualElement
        {
            visualelement.InputTransparent = new PropertyValue<bool>(inputTransparentFunc);
            return visualelement;
        }



        public static T IsEnabled<T>(this T visualelement, bool isEnabled) where T : IVisualElement
        {
            visualelement.IsEnabled = new PropertyValue<bool>(isEnabled);
            return visualelement;
        }
        public static T IsEnabled<T>(this T visualelement, Func<bool> isEnabledFunc) where T : IVisualElement
        {
            visualelement.IsEnabled = new PropertyValue<bool>(isEnabledFunc);
            return visualelement;
        }



        public static T AnchorX<T>(this T visualelement, double anchorX) where T : IVisualElement
        {
            visualelement.AnchorX = new PropertyValue<double>(anchorX);
            return visualelement;
        }
        public static T AnchorX<T>(this T visualelement, Func<double> anchorXFunc) where T : IVisualElement
        {
            visualelement.AnchorX = new PropertyValue<double>(anchorXFunc);
            return visualelement;
        }



        public static T AnchorY<T>(this T visualelement, double anchorY) where T : IVisualElement
        {
            visualelement.AnchorY = new PropertyValue<double>(anchorY);
            return visualelement;
        }
        public static T AnchorY<T>(this T visualelement, Func<double> anchorYFunc) where T : IVisualElement
        {
            visualelement.AnchorY = new PropertyValue<double>(anchorYFunc);
            return visualelement;
        }



        public static T TranslationX<T>(this T visualelement, double translationX) where T : IVisualElement
        {
            visualelement.TranslationX = new PropertyValue<double>(translationX);
            return visualelement;
        }
        public static T TranslationX<T>(this T visualelement, Func<double> translationXFunc) where T : IVisualElement
        {
            visualelement.TranslationX = new PropertyValue<double>(translationXFunc);
            return visualelement;
        }



        public static T TranslationY<T>(this T visualelement, double translationY) where T : IVisualElement
        {
            visualelement.TranslationY = new PropertyValue<double>(translationY);
            return visualelement;
        }
        public static T TranslationY<T>(this T visualelement, Func<double> translationYFunc) where T : IVisualElement
        {
            visualelement.TranslationY = new PropertyValue<double>(translationYFunc);
            return visualelement;
        }



        public static T Rotation<T>(this T visualelement, double rotation) where T : IVisualElement
        {
            visualelement.Rotation = new PropertyValue<double>(rotation);
            return visualelement;
        }
        public static T Rotation<T>(this T visualelement, Func<double> rotationFunc) where T : IVisualElement
        {
            visualelement.Rotation = new PropertyValue<double>(rotationFunc);
            return visualelement;
        }



        public static T RotationX<T>(this T visualelement, double rotationX) where T : IVisualElement
        {
            visualelement.RotationX = new PropertyValue<double>(rotationX);
            return visualelement;
        }
        public static T RotationX<T>(this T visualelement, Func<double> rotationXFunc) where T : IVisualElement
        {
            visualelement.RotationX = new PropertyValue<double>(rotationXFunc);
            return visualelement;
        }



        public static T RotationY<T>(this T visualelement, double rotationY) where T : IVisualElement
        {
            visualelement.RotationY = new PropertyValue<double>(rotationY);
            return visualelement;
        }
        public static T RotationY<T>(this T visualelement, Func<double> rotationYFunc) where T : IVisualElement
        {
            visualelement.RotationY = new PropertyValue<double>(rotationYFunc);
            return visualelement;
        }



        public static T Scale<T>(this T visualelement, double scale) where T : IVisualElement
        {
            visualelement.Scale = new PropertyValue<double>(scale);
            return visualelement;
        }
        public static T Scale<T>(this T visualelement, Func<double> scaleFunc) where T : IVisualElement
        {
            visualelement.Scale = new PropertyValue<double>(scaleFunc);
            return visualelement;
        }



        public static T ScaleX<T>(this T visualelement, double scaleX) where T : IVisualElement
        {
            visualelement.ScaleX = new PropertyValue<double>(scaleX);
            return visualelement;
        }
        public static T ScaleX<T>(this T visualelement, Func<double> scaleXFunc) where T : IVisualElement
        {
            visualelement.ScaleX = new PropertyValue<double>(scaleXFunc);
            return visualelement;
        }



        public static T ScaleY<T>(this T visualelement, double scaleY) where T : IVisualElement
        {
            visualelement.ScaleY = new PropertyValue<double>(scaleY);
            return visualelement;
        }
        public static T ScaleY<T>(this T visualelement, Func<double> scaleYFunc) where T : IVisualElement
        {
            visualelement.ScaleY = new PropertyValue<double>(scaleYFunc);
            return visualelement;
        }



        public static T Visual<T>(this T visualelement, Microsoft.Maui.Controls.IVisual visual) where T : IVisualElement
        {
            visualelement.Visual = new PropertyValue<Microsoft.Maui.Controls.IVisual>(visual);
            return visualelement;
        }
        public static T Visual<T>(this T visualelement, Func<Microsoft.Maui.Controls.IVisual> visualFunc) where T : IVisualElement
        {
            visualelement.Visual = new PropertyValue<Microsoft.Maui.Controls.IVisual>(visualFunc);
            return visualelement;
        }



        public static T IsVisible<T>(this T visualelement, bool isVisible) where T : IVisualElement
        {
            visualelement.IsVisible = new PropertyValue<bool>(isVisible);
            return visualelement;
        }
        public static T IsVisible<T>(this T visualelement, Func<bool> isVisibleFunc) where T : IVisualElement
        {
            visualelement.IsVisible = new PropertyValue<bool>(isVisibleFunc);
            return visualelement;
        }



        public static T Opacity<T>(this T visualelement, double opacity) where T : IVisualElement
        {
            visualelement.Opacity = new PropertyValue<double>(opacity);
            return visualelement;
        }
        public static T Opacity<T>(this T visualelement, Func<double> opacityFunc) where T : IVisualElement
        {
            visualelement.Opacity = new PropertyValue<double>(opacityFunc);
            return visualelement;
        }



        public static T BackgroundColor<T>(this T visualelement, Microsoft.Maui.Graphics.Color backgroundColor) where T : IVisualElement
        {
            visualelement.BackgroundColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(backgroundColor);
            return visualelement;
        }
        public static T BackgroundColor<T>(this T visualelement, Func<Microsoft.Maui.Graphics.Color> backgroundColorFunc) where T : IVisualElement
        {
            visualelement.BackgroundColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(backgroundColorFunc);
            return visualelement;
        }



        public static T Background<T>(this T visualelement, Microsoft.Maui.Controls.Brush background) where T : IVisualElement
        {
            visualelement.Background = new PropertyValue<Microsoft.Maui.Controls.Brush>(background);
            return visualelement;
        }
        public static T Background<T>(this T visualelement, Func<Microsoft.Maui.Controls.Brush> backgroundFunc) where T : IVisualElement
        {
            visualelement.Background = new PropertyValue<Microsoft.Maui.Controls.Brush>(backgroundFunc);
            return visualelement;
        }



        public static T WidthRequest<T>(this T visualelement, double widthRequest) where T : IVisualElement
        {
            visualelement.WidthRequest = new PropertyValue<double>(widthRequest);
            return visualelement;
        }
        public static T WidthRequest<T>(this T visualelement, Func<double> widthRequestFunc) where T : IVisualElement
        {
            visualelement.WidthRequest = new PropertyValue<double>(widthRequestFunc);
            return visualelement;
        }



        public static T HeightRequest<T>(this T visualelement, double heightRequest) where T : IVisualElement
        {
            visualelement.HeightRequest = new PropertyValue<double>(heightRequest);
            return visualelement;
        }
        public static T HeightRequest<T>(this T visualelement, Func<double> heightRequestFunc) where T : IVisualElement
        {
            visualelement.HeightRequest = new PropertyValue<double>(heightRequestFunc);
            return visualelement;
        }



        public static T MinimumWidthRequest<T>(this T visualelement, double minimumWidthRequest) where T : IVisualElement
        {
            visualelement.MinimumWidthRequest = new PropertyValue<double>(minimumWidthRequest);
            return visualelement;
        }
        public static T MinimumWidthRequest<T>(this T visualelement, Func<double> minimumWidthRequestFunc) where T : IVisualElement
        {
            visualelement.MinimumWidthRequest = new PropertyValue<double>(minimumWidthRequestFunc);
            return visualelement;
        }



        public static T MinimumHeightRequest<T>(this T visualelement, double minimumHeightRequest) where T : IVisualElement
        {
            visualelement.MinimumHeightRequest = new PropertyValue<double>(minimumHeightRequest);
            return visualelement;
        }
        public static T MinimumHeightRequest<T>(this T visualelement, Func<double> minimumHeightRequestFunc) where T : IVisualElement
        {
            visualelement.MinimumHeightRequest = new PropertyValue<double>(minimumHeightRequestFunc);
            return visualelement;
        }



        public static T MaximumWidthRequest<T>(this T visualelement, double maximumWidthRequest) where T : IVisualElement
        {
            visualelement.MaximumWidthRequest = new PropertyValue<double>(maximumWidthRequest);
            return visualelement;
        }
        public static T MaximumWidthRequest<T>(this T visualelement, Func<double> maximumWidthRequestFunc) where T : IVisualElement
        {
            visualelement.MaximumWidthRequest = new PropertyValue<double>(maximumWidthRequestFunc);
            return visualelement;
        }



        public static T MaximumHeightRequest<T>(this T visualelement, double maximumHeightRequest) where T : IVisualElement
        {
            visualelement.MaximumHeightRequest = new PropertyValue<double>(maximumHeightRequest);
            return visualelement;
        }
        public static T MaximumHeightRequest<T>(this T visualelement, Func<double> maximumHeightRequestFunc) where T : IVisualElement
        {
            visualelement.MaximumHeightRequest = new PropertyValue<double>(maximumHeightRequestFunc);
            return visualelement;
        }



        public static T FlowDirection<T>(this T visualelement, Microsoft.Maui.FlowDirection flowDirection) where T : IVisualElement
        {
            visualelement.FlowDirection = new PropertyValue<Microsoft.Maui.FlowDirection>(flowDirection);
            return visualelement;
        }
        public static T FlowDirection<T>(this T visualelement, Func<Microsoft.Maui.FlowDirection> flowDirectionFunc) where T : IVisualElement
        {
            visualelement.FlowDirection = new PropertyValue<Microsoft.Maui.FlowDirection>(flowDirectionFunc);
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
    }
}
