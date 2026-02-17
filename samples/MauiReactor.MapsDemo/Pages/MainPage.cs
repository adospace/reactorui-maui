using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;
using MauiReactor.Maps;
using Microsoft.Maui.Devices.Sensors;

namespace MauiReactor.MapsDemo.Pages;

class MainPageState
{
    public List<Location> Positions { get; set; } = new();
}

class MainPage : Component<MainPageState>
{
    public override VisualNode Render()
    {
        return ContentPage(
            new Map()
            {
                State.Positions.Select((location, index)=> new Pin()
                    .Location(location)
                    .Label($"Pin{index+1}")
                    .OnMarkerClicked(()=> SetState(s => s.Positions.Remove(location)))
                    )
            }
            .AutomationId("Map")
            .GridRow(1)
            .OnMapClicked(args => SetState(s => s.Positions.Add(args.Location)))
        );
    }
}
