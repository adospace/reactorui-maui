
namespace MauiReactor
{
    public class VerticalGridItemsLayout : GridItemsLayout<Internals.VerticalGridItemsLayout>
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
                _nativeControl ??= new Internals.VerticalGridItemsLayout(_span.Value);
            }

            base.OnMount();
        }
    }

    public class HorizontalGridItemsLayout : GridItemsLayout<Internals.HorizontalGridItemsLayout>
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
                _nativeControl ??= new Internals.HorizontalGridItemsLayout(_span.Value);
            }
            
            base.OnMount();
        }
    }
}

namespace MauiReactor.Internals
{
    public class VerticalGridItemsLayout : GridItemsLayout
    {
        public VerticalGridItemsLayout() : base(ItemsLayoutOrientation.Vertical)
        {
        }

        public VerticalGridItemsLayout(int span) : base(span, ItemsLayoutOrientation.Vertical)
        {
        }
    }

    public class HorizontalGridItemsLayout : GridItemsLayout
    {
        public HorizontalGridItemsLayout() : base(ItemsLayoutOrientation.Horizontal)
        {
        }

        public HorizontalGridItemsLayout(int span) : base(span, ItemsLayoutOrientation.Horizontal)
        {
        }
    }
}
