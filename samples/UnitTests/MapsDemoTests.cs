using MauiReactor;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

public class MapsDemoTests
{
    [Test]
    public void Adding_A_Pin_To_Map_Works_Correctly()
    {
        var mainPageNode = new TemplateHost<MauiControls.ContentPage>(
            new MauiReactor.MapsDemo.Pages.MainPage());

        // Check that the map has no pins
        mainPageNode.Find<MauiControls.Maps.Map>("Map")
            .Pins.Count.ShouldBe(0);

        // Click on the map
        mainPageNode.Find<MauiControls.Maps.Map>("Map")
            .SendClicked();

        // Check that the map has one pin
        mainPageNode.Find<MauiControls.Maps.Map>("Map")
            .Pins[0]
            .Location.ShouldBe(new Microsoft.Maui.Devices.Sensors.Location(100.0, 100.0));
    }    
}

public static class MapExtensions
{
    public static void SendClicked(this MauiControls.Maps.Map map)
    {
        var mapController = (Microsoft.Maui.Maps.IMap)map;
        mapController.Clicked(new Microsoft.Maui.Devices.Sensors.Location(100.0, 100.0));
    }
}
