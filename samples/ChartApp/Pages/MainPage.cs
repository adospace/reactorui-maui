using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using MauiReactor;
using Rearch;
using Rearch.Reactor.Components;

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


class ChartPage : CapsuleConsumer
{
    static readonly Random _rnd = new();

    public override VisualNode Render(ICapsuleHandle use)
    {
        var (values, setValues) = use.State<double[]>([2, 1, 2, 3, 2, 3, 3]);

        return new ContentPage("Chart Sample")
        {
            new Grid("* * Auto", "*")
            {
                new PolarChart()
                    .Series(() => new ISeries[]
                    {
                        new PolarLineSeries<double>
                        {
                            Values = values,
                            Fill = null,
                            IsClosed = false
                        }
                    }),

                new CartesianChart()
                    .Series(() => new ISeries[]
                    {
                        new LineSeries<double>
                        {
                            Values = values,
                            Fill = null
                        }
                    })
                    .GridRow(1),


                new Slider()
                    .GridRow(2)
                    .Minimum(2)
                    .Maximum(10)
                    .Margin(5)
                    .Value(()=>values.Length)
                    .OnValueChanged((s, args)=>
                    {
                        setValues(
                            Enumerable.Range(1, (int)args.NewValue)
                            .Select(_=>_rnd.NextDouble()*20.0)
                            .ToArray());
                    })


            }
        }
        .BackgroundColor(Colors.Black);
    }
}
