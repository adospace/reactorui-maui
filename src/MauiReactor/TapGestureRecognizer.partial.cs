using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class TapGestureRecognizer
    {
        public TapGestureRecognizer(Action onTap) => this.OnTapped(onTap);
        public TapGestureRecognizer(Action onTap, int numberOfTapsRequired) => this.OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);
    }
}
