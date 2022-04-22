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

namespace MauiReactor
{
    public partial interface IProgressBar : IView
    {
        PropertyValue<Microsoft.Maui.Graphics.Color>? ProgressColor { get; set; }
        PropertyValue<double>? Progress { get; set; }


    }

    public partial class ProgressBar<T> : View<T>, IProgressBar where T : Microsoft.Maui.Controls.ProgressBar, new()
    {
        public ProgressBar()
        {

        }

        public ProgressBar(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Graphics.Color>? IProgressBar.ProgressColor { get; set; }
        PropertyValue<double>? IProgressBar.Progress { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIProgressBar = (IProgressBar)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.ProgressBar.ProgressColorProperty, thisAsIProgressBar.ProgressColor);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.ProgressBar.ProgressProperty, thisAsIProgressBar.Progress);


            base.OnUpdate();

            OnEndUpdate();
        }

        protected override void OnAnimate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIProgressBar = (IProgressBar)this;

            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.ProgressBar.ProgressProperty, thisAsIProgressBar.Progress);

            base.OnAnimate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class ProgressBar : ProgressBar<Microsoft.Maui.Controls.ProgressBar>
    {
        public ProgressBar()
        {

        }

        public ProgressBar(Action<Microsoft.Maui.Controls.ProgressBar?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ProgressBarExtensions
    {
        public static T ProgressColor<T>(this T progressBar, Microsoft.Maui.Graphics.Color progressColor) where T : IProgressBar
        {
            progressBar.ProgressColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(progressColor);
            return progressBar;
        }

        public static T ProgressColor<T>(this T progressBar, Func<Microsoft.Maui.Graphics.Color> progressColorFunc) where T : IProgressBar
        {
            progressBar.ProgressColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(progressColorFunc);
            return progressBar;
        }



        public static T Progress<T>(this T progressBar, double progress, RxDoubleAnimation? customAnimation = null) where T : IProgressBar
        {
            progressBar.Progress = new PropertyValue<double>(progress);
            progressBar.AppendAnimatable(Microsoft.Maui.Controls.ProgressBar.ProgressProperty, customAnimation ?? new RxDoubleAnimation(progress), v => progressBar.Progress = new PropertyValue<double>(v.CurrentValue()));
            return progressBar;
        }

        public static T Progress<T>(this T progressBar, Func<double> progressFunc) where T : IProgressBar
        {
            progressBar.Progress = new PropertyValue<double>(progressFunc);
            return progressBar;
        }




    }
}