using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.TestApp.Models;

namespace MauiReactor.TestApp.Pages;

//public class TestBug187 : Component
//{
//    private readonly IEnumerable<Monkey> _allMonkeys = Monkey.GetList();

//    public override VisualNode Render() 
//        => ContentPage([
//            Grid("*", "*", [
//                new ListView(MauiControls.ListViewCachingStrategy.RecycleElementAndDataTemplate)
//                    .HasUnevenRows(true)
//                    .ItemsSource(_allMonkeys, RenderMonkeyTemplate),

//                new Internals.FrameRateIndicator()
//                    .VStart()
//                    .HEnd()
//                    .BackgroundColor(Colors.White)
//            ]),
//        ]);

//    private ViewCell RenderMonkeyTemplate(Monkey monkey) => monkey.Name switch
//    {
//        "Capuchin Monkey" or "Howler Monkey" or "Red-shanked Douc" => RenderSpecialMonkeyTemplate2(monkey),
//        _ => RenderFullMonkeyTemplate2(monkey)
//    };

//    private ViewCell RenderSpecialMonkeyTemplate(Monkey monkey) => new ViewCell
//    {
//        new HorizontalStackLayout
//        {
//            new Label(monkey.Name)
//                .FontSize(12.0)
//                .Margin(5)
//        }
//        .BackgroundColor(Colors.AliceBlue)
//    };

//    private ViewCell RenderSpecialMonkeyTemplate2(Monkey monkey) => ViewCell([
//        HorizontalStackLayout(
//        [
//            Component.Label(monkey.Name)
//                .FontSize(12.0)
//                .Margin(5)
//        ])
//        .BackgroundColor(Colors.AliceBlue)
//    ]);

//    private ViewCell RenderFullMonkeyTemplate(Monkey monkey) => new ViewCell
//    {
//        new HorizontalStackLayout
//        {
//            new Image()
//                .Source(new Uri(monkey.ImageUrl))
//                .HeightRequest(100)
//                .Margin(4),

//            new StackLayout
//            {
//                new Label(monkey.Name)
//                    .FontSize(12.0)
//                    .Margin(5),

//                new Label(monkey.Location)
//                    .FontSize(12.0)
//                    .Margin(5)
//            }
//        }
//        .Padding(10)
//    };

//    private ViewCell RenderFullMonkeyTemplate2(Monkey monkey) => ViewCell([
//        HorizontalStackLayout(
//        [
//            Image()
//                .Source(new Uri(monkey.ImageUrl))
//                .HeightRequest(100)
//                .Margin(4),

//            StackLayout(
//            [
//                Component.Label(monkey.Name)
//                    .FontSize(12.0)
//                    .Margin(5),

//                Component.Label(monkey.Location)
//                    .FontSize(12.0)
//                    .Margin(5)
//            ])
//        ])
//        .Padding(10)
//    ]);

//}
