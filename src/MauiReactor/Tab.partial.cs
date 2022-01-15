using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class Tab
    {
        public Tab(string title) => this.Title(title);

        public Tab(string title, string icon) => this.Title(title).Icon(icon);
    }
}
