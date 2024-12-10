using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class HorizontalLinearItemsLayout : LinearItemsLayout<Internals.HorizontalLinearItemsLayout>
    {
        public HorizontalLinearItemsLayout()
        {

        }

        public HorizontalLinearItemsLayout(Action<Microsoft.Maui.Controls.LinearItemsLayout?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public partial class VerticalLinearItemsLayout : LinearItemsLayout<Internals.VerticalLinearItemsLayout>
    {
        public VerticalLinearItemsLayout()
        {

        }

        public VerticalLinearItemsLayout(Action<Microsoft.Maui.Controls.LinearItemsLayout?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }


    public partial class Component
    {
        public static HorizontalLinearItemsLayout HorizontalLinearItemsLayout() => new();
        public static VerticalLinearItemsLayout VerticalLinearItemsLayout() => new();

    }
}

namespace MauiReactor.Internals
{
    public class HorizontalLinearItemsLayout : Microsoft.Maui.Controls.LinearItemsLayout
    {
        public HorizontalLinearItemsLayout() : base(ItemsLayoutOrientation.Horizontal)
        {
        }
    }

    public class VerticalLinearItemsLayout : Microsoft.Maui.Controls.LinearItemsLayout
    {
        public VerticalLinearItemsLayout() : base(ItemsLayoutOrientation.Vertical)
        {
        }
    }
}
