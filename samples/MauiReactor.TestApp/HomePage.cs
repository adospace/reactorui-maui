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
            return new Shell
            {
                new FlyoutItem("Counter")
                {
                    new Pages.CounterPage()
                },
                new FlyoutItem("Parameters")
                {
                    new Pages.ParametersPage()
                },
                new FlyoutItem("CollectionView")
                {
                    new Pages.CollectionViewPage()
                },
                new FlyoutItem("ListView")
                {
                    new Pages.ListViewPage()
                },

            }
            .ItemTemplate(RenderItemTemplate);
        }

        static VisualNode RenderItemTemplate(Microsoft.Maui.Controls.BaseShellItem item)
            => new Grid("68", "*")
            {
                new Label(item.Title)
                    .VCenter()
                    .Margin(10,0)
            };

    }
}
