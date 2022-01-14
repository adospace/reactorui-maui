using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class Button
    {
        public Button(string text) => this.Text(text);

        public Button(string text, Action onClick) => this.Text(text).OnClicked(onClick);
    }
}
