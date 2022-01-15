using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class VerticalStackLayout
    {
        public VerticalStackLayout(double spacing) => this.Spacing(spacing);
    }

    public class VStack : VerticalStackLayout
    {
        public VStack(double spacing) => this.Spacing(spacing);
    }
}
