using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages
{
    class ImagePage : Component
    {
        public override VisualNode Render()
        {
            return new ContentPage("Image")
            {
                new Image("dotnet_bot.png")
                    .Shadow(new Shadow().Brush(Microsoft.Maui.Controls.Brush.Gold).Offset(20,20).Radius(40).Opacity(0.8f))
                    .VCenter()
                    .HCenter()


            }
            .Background(Microsoft.Maui.Controls.Brush.White);
        }
    }
}
