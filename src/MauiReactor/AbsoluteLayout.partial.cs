using MauiReactor.Animations;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public static partial class AbsoluteLayoutExtensions
    {
        //static object? SetAbsoluteLayoutBounds(object visualNodeWithAttachedProperties, RxAnimation animation) 
        //{
        //    var valueToSet = ((RxRectAnimation)animation).CurrentValue();
        //    ((IVisualNodeWithAttachedProperties)visualNodeWithAttachedProperties).SetProperty(Microsoft.Maui.Controls.AbsoluteLayout.LayoutBoundsProperty, valueToSet);
        //    return valueToSet;
        //} 

        public static T AbsoluteLayoutBounds<T>(this T visualNodeWithAttachedProperties, Rect value, RxRectAnimation? customAnimation = null) where T : IVisualNodeWithAttachedProperties
        {
            visualNodeWithAttachedProperties.SetProperty(Microsoft.Maui.Controls.AbsoluteLayout.LayoutBoundsProperty, value);
            visualNodeWithAttachedProperties.AppendAnimatable(
                Microsoft.Maui.Controls.AbsoluteLayout.LayoutBoundsProperty, 
                customAnimation ?? new RxSimpleRectAnimation(value));

            return visualNodeWithAttachedProperties;
        }

        public static T AbsoluteLayoutBounds<T>(this T visualNodeWithAttachedProperties, double x, double y, double width, double height) where T : IVisualNodeWithAttachedProperties
        {
            visualNodeWithAttachedProperties.SetProperty(Microsoft.Maui.Controls.AbsoluteLayout.LayoutBoundsProperty, new Rect(x, y, width, height));

            return visualNodeWithAttachedProperties;
        }

        public static T AbsoluteLayoutBounds<T>(this T visualNodeWithAttachedProperties, Point loc, Size sz) where T : IVisualNodeWithAttachedProperties
        {
            visualNodeWithAttachedProperties.SetProperty(Microsoft.Maui.Controls.AbsoluteLayout.LayoutBoundsProperty, new Rect(loc, sz));

            return visualNodeWithAttachedProperties;
        }

        public static T AbsoluteLayoutFlags<T>(this T visualNodeWithAttachedProperties, AbsoluteLayoutFlags value) where T : IVisualNodeWithAttachedProperties
        {
            visualNodeWithAttachedProperties.SetProperty(Microsoft.Maui.Controls.AbsoluteLayout.LayoutFlagsProperty, value);

            return visualNodeWithAttachedProperties;
        }
    }
}
