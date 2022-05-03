using MauiReactor.Graphics;
using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class GraphicsView<T>
    {
        partial void OnEndUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            
            if (Children.Count > 0 && Children[0] is Canvas canvas)
            {
                var drawable = canvas.GetDrawable();
                if (NativeControl.Drawable != drawable)
                {
                    NativeControl.Drawable = drawable;
                }
                else
                {
                    NativeControl.Invalidate();
                }
            }
        }
    }
}
