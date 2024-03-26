using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using MauiReactor;

namespace ChartApp.Pages;

[Scaffold(typeof(LiveChartsCore.SkiaSharpView.Maui.PieChart))]
partial class PieChart { }

[Scaffold(typeof(LiveChartsCore.SkiaSharpView.Maui.CartesianChart))]
partial class CartesianChart { }

[Scaffold(typeof(LiveChartsCore.SkiaSharpView.Maui.PolarChart))]
partial class PolarChart { }



class ChartPageState
{
    public double[] Values { get; set; } = [2, 1, 2, 3, 2, 3, 3];
}


class ChartPage : Component<ChartPageState>
{
    static readonly Random _rnd = new();

    public override VisualNode Render()
    {
        return new ContentPage("Chart Sample")
        {
            new Grid("* * Auto", "*")
            {
                new PolarChart()
                    .Series(() => new ISeries[]
                    {
                        new PolarLineSeries<double>
                        {
                            Values = State.Values,
                            Fill = null,
                            IsClosed = false
                        }
                    }),

                new CartesianChart()
                    .Series(() => new ISeries[]
                    {
                        new LineSeries<double>
                        {
                            Values = State.Values,
                            Fill = null
                        }
                    })
                    .GridRow(1),


                new Slider()
                    .GridRow(2)
                    .Minimum(2)
                    .Maximum(10)
                    .Margin(5)
                    .Value(()=>State.Values.Length)
                    .OnValueChanged((s, args)=>
                    {
                        SetState(s => s.Values =
                            Enumerable.Range(1, (int)args.NewValue)
                            .Select(_=>_rnd.NextDouble()*20.0)
                            .ToArray(), false);
                    })


            }
        }
        .BackgroundColor(Colors.Black);
    }
}
