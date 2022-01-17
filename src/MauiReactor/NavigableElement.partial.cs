using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface INavigableElement
    { 
        List<string>? Class { get; set; }
    }

    public partial class NavigableElement<T>
    {
        List<string>? INavigableElement.Class { get; set; }

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsINavigableElement = (INavigableElement)this;
            if (!NativeControl.StyleClass.NullableSequenceEqual(thisAsINavigableElement.Class))
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"[{this}] Update 'StyleClass' to {(thisAsINavigableElement.Class == null ? "null" : string.Join(",", thisAsINavigableElement.Class))}");
#endif
                NativeControl.StyleClass = thisAsINavigableElement.Class;
            }

        }
    }

    public static partial class NavigableElementExtensions
    {
        public static T Class<T>(this T navigableelement, string className) where T : INavigableElement
        {
            navigableelement.Class ??= new List<string>();
            navigableelement.Class.Add(className);
            return navigableelement;
        }
    }

    
}
