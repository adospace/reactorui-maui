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
    public class VerticalGridItemsLayout : GridItemsLayout<MauiReactor.Internals.VerticalGridItemsLayout>
    {
        private readonly int? _span;

        public VerticalGridItemsLayout()
        {
            
        }

        public VerticalGridItemsLayout(int span)
        {
            _span = span;
        }

        protected override void OnMount()
        {
            if (_span != null)
            {
                _nativeControl ??= new MauiReactor.Internals.VerticalGridItemsLayout(_span.Value);
            }

            base.OnMount();
        }
    }

    public class HorizontalGridItemsLayout : GridItemsLayout<MauiReactor.Internals.HorizontalGridItemsLayout>
    {
        private readonly int? _span;

        public HorizontalGridItemsLayout()
        {

        }

        public HorizontalGridItemsLayout(int span)
        {
            _span = span;
        }

        protected override void OnMount()
        {
            if (_span != null)
            {
                _nativeControl ??= new MauiReactor.Internals.HorizontalGridItemsLayout(_span.Value);
            }
            
            base.OnMount();
        }
    }
}

namespace MauiReactor.Internals
{
    public class VerticalGridItemsLayout : Microsoft.Maui.Controls.GridItemsLayout
    {
        public VerticalGridItemsLayout() : base(ItemsLayoutOrientation.Vertical)
        {
        }

        public VerticalGridItemsLayout(int span) : base(span, ItemsLayoutOrientation.Vertical)
        {
        }
    }

    public class HorizontalGridItemsLayout : Microsoft.Maui.Controls.GridItemsLayout
    {
        public HorizontalGridItemsLayout() : base(ItemsLayoutOrientation.Horizontal)
        {
        }

        public HorizontalGridItemsLayout(int span) : base(span, ItemsLayoutOrientation.Horizontal)
        {
        }
    }
}
