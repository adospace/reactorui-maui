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
    public partial interface ITapGestureRecognizer
    {
        System.Windows.Input.ICommand Command { get; set; }
        object CommandParameter { get; set; }
        int NumberOfTapsRequired { get; set; }

        Action? TappedAction { get; set; }
        Action<EventArgs>? TappedActionWithArgs { get; set; }

    }
    public sealed partial class TapGestureRecognizer : GestureRecognizer<Microsoft.Maui.Controls.TapGestureRecognizer>, ITapGestureRecognizer
    {
        public TapGestureRecognizer()
        {

        }

        public TapGestureRecognizer(Action<Microsoft.Maui.Controls.TapGestureRecognizer?> componentRefAction)
            : base(componentRefAction)
        {

        }

        System.Windows.Input.ICommand ITapGestureRecognizer.Command { get; set; } = (System.Windows.Input.ICommand)Microsoft.Maui.Controls.TapGestureRecognizer.CommandProperty.DefaultValue;
        object ITapGestureRecognizer.CommandParameter { get; set; } = (object)Microsoft.Maui.Controls.TapGestureRecognizer.CommandParameterProperty.DefaultValue;
        int ITapGestureRecognizer.NumberOfTapsRequired { get; set; } = (int)Microsoft.Maui.Controls.TapGestureRecognizer.NumberOfTapsRequiredProperty.DefaultValue;

        Action? ITapGestureRecognizer.TappedAction { get; set; }
        Action<EventArgs>? ITapGestureRecognizer.TappedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsITapGestureRecognizer = (ITapGestureRecognizer)this;
            if (NativeControl.Command != thisAsITapGestureRecognizer.Command) NativeControl.Command = thisAsITapGestureRecognizer.Command;
            if (NativeControl.CommandParameter != thisAsITapGestureRecognizer.CommandParameter) NativeControl.CommandParameter = thisAsITapGestureRecognizer.CommandParameter;
            if (NativeControl.NumberOfTapsRequired != thisAsITapGestureRecognizer.NumberOfTapsRequired) NativeControl.NumberOfTapsRequired = thisAsITapGestureRecognizer.NumberOfTapsRequired;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsITapGestureRecognizer = (ITapGestureRecognizer)this;
            if (thisAsITapGestureRecognizer.TappedAction != null || thisAsITapGestureRecognizer.TappedActionWithArgs != null)
            {
                NativeControl.Tapped += NativeControl_Tapped;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Tapped(object? sender, EventArgs e)
        {
            var thisAsITapGestureRecognizer = (ITapGestureRecognizer)this;
            thisAsITapGestureRecognizer.TappedAction?.Invoke();
            thisAsITapGestureRecognizer.TappedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.Tapped -= NativeControl_Tapped;
            }

            base.OnDetachNativeEvents();
        }

    }


    public static partial class TapGestureRecognizerExtensions
    {
        public static T Command<T>(this T tapgesturerecognizer, System.Windows.Input.ICommand command) where T : ITapGestureRecognizer
        {
            tapgesturerecognizer.Command = command;
            return tapgesturerecognizer;
        }

        public static T CommandParameter<T>(this T tapgesturerecognizer, object commandParameter) where T : ITapGestureRecognizer
        {
            tapgesturerecognizer.CommandParameter = commandParameter;
            return tapgesturerecognizer;
        }

        public static T NumberOfTapsRequired<T>(this T tapgesturerecognizer, int numberOfTapsRequired) where T : ITapGestureRecognizer
        {
            tapgesturerecognizer.NumberOfTapsRequired = numberOfTapsRequired;
            return tapgesturerecognizer;
        }


        public static T OnTapped<T>(this T tapgesturerecognizer, Action tappedAction) where T : ITapGestureRecognizer
        {
            tapgesturerecognizer.TappedAction = tappedAction;
            return tapgesturerecognizer;
        }

        public static T OnTapped<T>(this T tapgesturerecognizer, Action<EventArgs> tappedActionWithArgs) where T : ITapGestureRecognizer
        {
            tapgesturerecognizer.TappedActionWithArgs = tappedActionWithArgs;
            return tapgesturerecognizer;
        }
    }
}
