using MauiReactor;
using MauiReactor.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackizer.Styles;
class CustomEntryState : IState
{

}

class CustomEntry : Component<CustomEntryState>
{
    public override VisualNode Render()
    {
        return new Frame
        {
            new Entry()
                .BackgroundColor(Colors.Transparent)
                .TextColor(Theme.Current.White)
                .FontSize(18)
                .VCenter()
        }
        .Padding(10,0)
        .HeightRequest(48)
        .CornerRadius(16)
        .BorderColor(Theme.Current.Gray70)
        .BackgroundColor(Theme.Current.Gray60.WithAlpha(0.2f))
        ;
    }
}
