using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp
{
    internal class HomePage : Component
    {
        public override VisualNode Render()
        {
            return new ContentPage("Title")
            {
                new Label("Hello World!!!!!")
                    .VerticalOptions(LayoutOptions.Center)
            };
        }
    }
}
