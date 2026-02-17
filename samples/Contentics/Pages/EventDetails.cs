using Contentics.Models;
using Contentics.Resources.Styles;
using MauiReactor;
using MauiReactor.Canvas;
using MauiReactor.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contentics.Pages;

class EventDetailsPageState
{
    public double ImageTransaleX { get; set; }
    public double ImageTransaleY { get; set; }
    public double ImageHeight { get; set; }
    public double ImageOpacity { get; set; }
    public double MainPanelX { get; set; }
}

class EventDetailsPageProps
{
    public EventModel? Model { get; set; }
    public Rect SourceRect { get; set; }
}

class EventDetails : Component<EventDetailsPageState, EventDetailsPageProps>
{
    protected override void OnMounted()
    {
        State.ImageTransaleX = Props.SourceRect.X;
        State.ImageTransaleY = Props.SourceRect.Y;
        State.ImageHeight = Props.SourceRect.Height;
        State.MainPanelX = 700;

        MauiControls.Application.Current?.Dispatcher.Dispatch(new Action(() => SetState(s =>
        {
            s.ImageTransaleX = 0;
            s.ImageTransaleY = 0;
            s.ImageHeight = 345;
            s.ImageOpacity = 1;
            s.MainPanelX = 313;
        })));

        base.OnMounted();
    }


    public override VisualNode Render()
    {
        return new ContentPage
        {
            new Grid("*, 104", "*")
            {
                RenderTopPanel(),

                RenderMainPanel(),

                RenderBottomPanel()
            }
        }
        .BackgroundColor(ThemeBrushes.Background)
        .Set(MauiControls.NavigationPage.HasNavigationBarProperty, false);
    }

    VisualNode[] RenderTopPanel()
        => new VisualNode[]{
            new CanvasView
                {
                    new ClipRectangle()
                    {
                        new Picture($"Contentics.Resources.Images.{Props.Model?.ImageSourceDetails}")
                            .Aspect(Aspect.Fill)
                    }
                    .CornerRadius(0,0,8,8),
                }
                .Margin(State.ImageTransaleX, State.ImageTransaleY, State.ImageTransaleX, 0)
                .HeightRequest(State.ImageHeight)
                .Opacity(State.ImageOpacity)
                .WithAnimation(duration: 300)
                .BackgroundColor(Colors.Transparent)
                .VStart(),

                new ImageButton("back_white.png")
                    .Margin(16, 38)
                    .HStart()
                    .VStart()
                    .HeightRequest(32)
                    .WidthRequest(32)
                    .OnClicked(OnBack),

                new ImageButton("share.png")
                    .Margin(64, 42)
                    .HEnd()
                    .VStart()
                    .HeightRequest(24)
                    .WidthRequest(24),

                new ImageButton("fav_white.png")
                    .Margin(16, 42)
                    .HEnd()
                    .VStart()
                    .HeightRequest(24)
                    .WidthRequest(24)
            };

    VisualNode RenderMainPanel()
        => new CanvasView
        {
            new Align
            { 
                new PointInteractionHandler
                {
                }
                .OnTap(()=>SetState(s => s.MainPanelX = s.MainPanelX == 72 ? 313 : 72))
            }
            .Height(64)
            .VStart(),

            new Box
            {
                new Column("40, 48, 64, 88, 52, 186")
                {
                    new Align
                    {
                        new Box()
                            .BackgroundColor(ThemeBrushes.Grey20)
                    }
                    .Height(4)
                    .Width(64)
                    .HCenter()
                    .VStart(),

                    new Text("Sit amet odio nisi leo viverra sed a vel blandit adipiscing")
                        .FontSize(24)
                        .FontWeight(FontWeights.Bold)
                        .FontColor(ThemeBrushes.Dark),

                    new Row("32, *, 75")
                    {
                        new Picture($"Contentics.Resources.Images.photo5.png"),

                        new Column
                        {
                            new Text("Floys Miles")
                                .FontSize(14)
                                .FontColor(ThemeBrushes.Dark)
                                .FontWeight(FontWeights.Bold)
                                .VerticalAlignment(VerticalAlignment.Center),
                            new Text(DateTime.Today.ToShortDateString())
                                .FontColor(ThemeBrushes.Grey100)
                                .VerticalAlignment(VerticalAlignment.Center),
                        }
                        .Margin(8,4.5f,0,4.5f),

                        new Box
                        { 
                            new Text(DateTime.Now.ToShortTimeString())
                                .FontWeight(FontWeights.Bold)
                                .VerticalAlignment (VerticalAlignment.Center)
                                .HorizontalAlignment(HorizontalAlignment.Center),
                        }
                        .BackgroundColor(ThemeBrushes.Grey20)
                        .CornerRadius(8)
                        .Margin(0,4.5f),

                    }
                    .Margin(0,24,0,0),

                    new Box()
                    { 
                        new Row("48, *, 32")
                        {
                            new Box()
                            {
                                new Align
                                {
                                    new Picture($"Contentics.Resources.Images.event_calendar.png")
                                }
                                .Height(24)
                                .Width(24)
                                .VCenter()
                                .HCenter()
                            }
                            .BackgroundColor (ThemeBrushes.Grey)
                            .CornerRadius(16),

                            new Column
                            {
                                new Text(DateTime.Today.AddDays(2).ToShortDateString())
                                    .FontSize(14)
                                    .VerticalAlignment(VerticalAlignment.Center)
                                    .FontColor(ThemeBrushes.Grey100),
                                new Text("10:00 am - 11:30 am")
                                    .FontSize(14)
                                    .FontWeight (FontWeights.Bold)
                                    .VerticalAlignment(VerticalAlignment.Center)
                                    .FontColor(ThemeBrushes.Dark)
                            }
                            .Margin(16,6,0,6),

                            new Align
                            {
                                new Picture($"Contentics.Resources.Images.chevron_right.png")
                            }
                            .Width(16)
                            .Height(16)
                            .VCenter()
                            .HCenter()
                        }
                        .Margin(8)
                    }
                    .Margin(0, 24, 0, 0)
                    .BackgroundColor(ThemeBrushes.White)
                    .CornerRadius(16),


                    new Row("20, *, 140")
                    {
                        new Text("32")
                            .FontSize(16)
                            .FontColor(ThemeBrushes.Dark)
                            .VerticalAlignment(VerticalAlignment.Center),

                        new Text("people are going")
                            .FontColor(ThemeBrushes.Grey100)
                            .VerticalAlignment(VerticalAlignment.Center)
                            .FontSize(14),

                        new Align
                        {
                            new Group
                            {
                                new Align
                                {
                                    new Picture($"Contentics.Resources.Images.photo1_circle.png")
                                }
                                .HEnd()
                                .Width(32)
                                .Margin(0,0,22+22+22+22,0),

                                new Align
                                {
                                    new Picture($"Contentics.Resources.Images.photo2_circle.png")
                                }
                                .HEnd()
                                .Width(32)
                                .Margin(0,0,22+22+22,0),

                                new Align
                                {
                                    new Picture($"Contentics.Resources.Images.photo3_circle.png")
                                }
                                .HEnd()
                                .Width(32)
                                .Margin(0,0,22+22,0),

                                new Align
                                {
                                    new Picture($"Contentics.Resources.Images.photo4_circle.png")
                                }
                                .HEnd()
                                .Width(32)
                                .Margin(0,0,22,0),

                                new Align
                                {
                                    new Group
                                    {
                                        new Ellipse()
                                            .FillColor(ThemeBrushes.White),
                                        new Text("۰۰۰")
                                            .VerticalAlignment(VerticalAlignment.Center)
                                            .HorizontalAlignment(HorizontalAlignment.Center)
                                            .FontSize(20)
                                            .FontWeight(FontWeights.UltraBold)
                                            .FontColor(ThemeBrushes.Grey50)
                                    }
                                }
                                .HEnd()
                                .Width(32)
                                .Height(32)
                            }
                        }
                        .VCenter()
                        .Height(32)

                    }
                    .Margin(0,24,0,0),

                    new Column("20 *")
                    {
                        new Text("About event")    
                            .FontSize(20)
                            .FontWeight(FontWeights.Bold)
                            .FontColor(ThemeBrushes.Dark),

                        new Text("Nunc vitae pharetra bibendum ultrices. Ornare amet aliquam aenean viverra ut tellus.\r\n\r\nCras aliquam nisi, risus enim amet. Sed pellentesque mauris, eget urna id ut sed vitae. Erat facilisi purus ut id in pulvinar sed sit. Vestibulum convallis consectetur quis eget netus magna ultrices adipiscing. Ornare sit semper cras lorem nec. Metus consectetur nunc aliquam in non.")
                            .FontSize(12)
                            .FontColor(ThemeBrushes.Grey100)
                    }
                    .Margin(0,30,0,0)
                }
                .Margin(16)
            }
            .CornerRadius(24, 24, 0, 0)
            .BackgroundColor(ThemeBrushes.Background)
        }
        .Opacity(State.ImageOpacity)
        .Margin(0, State.MainPanelX, 0, 0)
        .WithAnimation(duration: 200)
        .BackgroundColor(Colors.Transparent);

    VisualNode RenderBottomPanel()
        => new CanvasView()
        {
            new Box()
            {
                new Row("56, *")
                {
                    new Box
                    {
                        new Align
                        {
                            new Picture($"Contentics.Resources.Images.fav_small.png")
                        }
                        .Height(24)
                        .Width(24)
                        .HCenter()
                        .VCenter()
                    }
                    .CornerRadius(12)
                    .BackgroundColor(ThemeBrushes.Purple50),

                    new Box
                    {
                        new Text("Join to event")
                            .FontColor(ThemeBrushes.White)
                            .FontSize(16)
                            .HorizontalAlignment(HorizontalAlignment.Center)
                            .VerticalAlignment(VerticalAlignment.Center)
                    }
                    .Margin(16,0,0,0)
                    .CornerRadius(12)
                    .BackgroundColor(ThemeBrushes.Purple10)
                }
                .Margin(16,24)
            }
            .CornerRadius(24, 24, 0, 0)
            .BackgroundColor(ThemeBrushes.White)
        }
        .BackgroundColor(Colors.Transparent)
        .HeightRequest(104)
        .GridRow(1);

    private async void OnBack()
    {
        if (Navigation != null && Navigation.NavigationStack.Count > 0)
        {
            await Navigation.PopAsync();
        }
    }
}
