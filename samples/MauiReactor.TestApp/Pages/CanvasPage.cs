using MauiReactor.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class CanvasPageState
{
    public float Degrees { get; set; }

    public float ScaleX { get; set; } = 1.0f;

    public float ScaleY { get; set; } = 1.0f;

    public bool IsMouseHoverImage {  get; set; }
}

class CanvasPage : Component<CanvasPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage("Canvas Test Page")
        {
            new CanvasView
            {
                new Column("100,*")
                {
                    new Box()
                    { 
                        new ClipRectangle
                        {
                            new Text("This a Text element!")
                                .HorizontalAlignment(HorizontalAlignment.Center)
                                .VerticalAlignment(VerticalAlignment.Center)
                                .FontColor(Colors.Black)
                                .FontSize(14)
                        }
                    }
                    .Margin(10)
                    .BackgroundColor(Colors.Green)
                    .CornerRadius(10)
                    ,
                    new Box()
                    { 
                        new Column("*, 50")
                        {
                            new PointInterationHandler
                            {
                                new Picture("MauiReactor.TestApp.Resources.Images.Embedded.norway_1.jpeg"),
                            }
                            .OnHoverIn(()=> SetState(s=> s.IsMouseHoverImage = true))
                            .OnHoverOut(()=> SetState(s=> s.IsMouseHoverImage = false)),

                            new Text(State.IsMouseHoverImage ? "Mouse hovering" : "Awesome Norway!")
                                .HorizontalAlignment(HorizontalAlignment.Center)
                                .VerticalAlignment(VerticalAlignment.Center)
                                .FontColor(Colors.White)
                                .FontSize(24)


                        },
                    }
                    .Margin(10)
                    .BackgroundColor(Colors.Red)
                    .CornerRadius(10)
                }
                .TranslationX(20)
                .TranslationY(30)
            }
        };
    }
    //public override VisualNode Render()
    //{
    //    return new ContentPage("Canvas Test Page")
    //    {
    //        new Grid("Auto,Auto,Auto,*", "*")
    //        {
    //            new Slider()
    //                .Minimum(0)
    //                .Maximum(360)
    //                .Value(State.Degrees)
    //                .OnValueChanged((s, args)=>SetState(s => s.Degrees = (float)args.NewValue)),

    //            new Slider()
    //                .Minimum(0.5)
    //                .Maximum(2.0)
    //                .Value(State.ScaleX)
    //                .OnValueChanged((s, args)=>SetState(s => s.ScaleX = (float)args.NewValue))
    //                .GridRow(1),

    //            new Slider()
    //                .Minimum(0.5)
    //                .Maximum(2.0)
    //                .Value(State.ScaleY)
    //                .OnValueChanged((s, args)=>SetState(s => s.ScaleY = (float)args.NewValue))
    //                .GridRow(2),

    //            new CanvasView
    //            {
    //                new Row
    //                {
    //                    new Box()
    //                    {
    //                        new Picture("MauiReactor.TestApp.Resources.Images.Embedded.norway_1.jpeg")
    //                            .Aspect(Aspect.AspectFill)
    //                    }
    //                    .Margin(10)
    //                    .BackgroundColor(Colors.Green)
    //                    .CornerRadius(10)
    //                    ,

    //                    new Align
    //                    {
    //                        new PointIterationHandler
    //                        {
    //                            new Box()
    //                            {
    //                                new Text("Text")
    //                                    .FontSize(14f)
    //                                    .FontColor(Colors.Bisque)
    //                                    .VerticalAlignment(VerticalAlignment.Center)
    //                                    .HorizontalAlignment(HorizontalAlignment.Center)
    //                            }
    //                            .Margin(10)
    //                            .BackgroundColor(Colors.Red)
    //                            .CornerRadius(10)
    //                            .Anchor(0.5f, 0.5f)
    //                            .Rotate(State.Degrees)
    //                            .Scale(State.ScaleX, State.ScaleY)
    //                        }
    //                        .OnTap(OnClicked)
    //                    }
    //                    .Height(300)
    //                    .VerticalAlignment(Microsoft.Maui.Primitives.LayoutAlignment.Center)
    //                }
    //                .Columns("100, *")
    //            }
    //            .GridRow(3)
    //        }
    //    };
    //}

    //private async void OnClicked()
    //{
    //    if (ContainerPage == null)
    //    {
    //        return;
    //    }

    //    await ContainerPage.DisplayAlert("Test Message", "Tap event", "OK");
    //}
}
