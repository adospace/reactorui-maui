using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class HorizontalStackLayout
    {
        public HorizontalStackLayout(double spacing) => this.Spacing(spacing);
    }

    public class HStack : HorizontalStackLayout
    {
        public HStack(double spacing) => this.Spacing(spacing);
    }
}
