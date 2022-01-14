using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public class VStack : StackLayout
    {
        public VStack()
        {
            ((IStackLayout)this).Orientation = StackOrientation.Vertical;
        }

        public VStack(double spacing)
            :this()
        {
            ((IStackBase)this).Spacing = spacing;
        }
    }

    public class HStack : StackLayout
    {
        public HStack()
        {
            ((IStackLayout)this).Orientation = StackOrientation.Vertical;
        }
        public HStack(double spacing)
            : this()
        {
            ((IStackBase)this).Spacing = spacing;
        }
    }
}
