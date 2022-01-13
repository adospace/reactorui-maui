using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class TemplatedPage<T> : Page<T>
    {
        public TemplatedPage(string title)
            : base(title)
        {

        }
    }
}
