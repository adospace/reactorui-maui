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

        mainPageNode.ShouldNotBeNull();

        mainPageNode.NativeElement.ShouldNotBeNull();

        mainPageNode.NativeElement.FindByAutomationId<MauiControls.Maps.Map>("Map")
            .ShouldNotBeNull()
            .SendClicked()
            ;

        mainPageNode.NativeElement.FindByAutomationId<MauiControls.Maps.Map>("Map")
            .ShouldNotBeNull()
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
