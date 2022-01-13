using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class Page<T>
    {
        public Page(string title)
        {
            ((IPage)this).Title = title;
        }
    }
}
