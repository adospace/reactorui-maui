using MauiReactor.Animations;
using MauiReactor.Shapes;
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class AnimationLoopPageState 
{
    public Point[] Positions { get; set; } = new Point[5];

    public bool IsPaused { get; set; }
}


class AnimationLoopPage : Component<AnimationLoopPageState>
{
    private readonly static Color _baseColor = Color.Parse("#512BD4");

    public override VisualNode Render()
    {
        return new ContentPage("Animation Loop")
        {
            new Grid("128 *", "*")
            {
                new PlayControls()
                    .IsPaused(State.IsPaused)
                    .OnTap(()=> SetState(s => s.IsPaused =! s.IsPaused))
                ,

                Enumerable.Range(0, 5)
                    .Select(index=> new Ellipse()
                            .HeightRequest(20)
                            .WidthRequest(20)
                            .HCenter()
                            .VCenter()
                            .Fill(_baseColor.WithAlpha(1.0f-index*0.15f))
                            .TranslationX(()=>State.Positions[index].X)
                            .TranslationY(()=>State.Positions[index].Y)
                            .GridRow(1)
                    )
                    .ToArray(),

                new AnimationController
                {
                    Enumerable.Range(0, 5)
                        .Select(index => new ParallelAnimation
                        {
                            new SequenceAnimation
                            {
                                new CubicBezierPathAnimation()
                                    .StartPoint(new Point(-180,0))
                                    .EndPoint(new Point(180,0))
                                    .ControlPoint1(new Point(-150,200))
                                    .ControlPoint2(new Point(150,-200))
                                    .Duration(1000)
                                    .OnTick(v => SetState(s => s.Positions[index] = v, false)),

                                new CubicBezierPathAnimation()
                                    .StartPoint(new Point(180,0))
                                    .EndPoint(new Point(-180,0))
                                    .ControlPoint1(new Point(150,200))
                                    .ControlPoint2(new Point(-150,-200))
                                    .Duration(1000)
                                    .OnTick(v => SetState(s => s.Positions[index] = v, false)),
                            }
                            .Loop(true)
                        }
                        .RepeatForever()
                        .InitialDelay(40 * index))
                        .ToArray()
                }
                .IsEnabled(true)
                .IsPaused(()=>State.IsPaused)
            }
        }
        .BackgroundColor(Colors.Black);
    }
}

class PlayControlsState
{
    public double PauseIconRotationAngle { get; set; }
}

class PlayControls : Component<PlayControlsState>
{
    static readonly PathF _pausePath = new PathF()
        .MoveTo(5, 2)
        .LineTo(5, 28)
        .MoveTo(18, 2)
        .LineTo(18, 28);

    static readonly PathF _playPath = new PathF()
        .MoveTo(0, 0)
        .LineTo(0, 30)
        .LineTo(22, 15);

    private bool _isPaused;
    private Action? _tapAction;

    public PlayControls IsPaused(bool isPaused)
    {
        _isPaused = isPaused;
        return this;
    }

    public PlayControls OnTap(Action tapAction)
    {
        _tapAction = tapAction;
        return this;
    }

    public override VisualNode Render()
    {
        return new Grid("72", "72 *")
        {
            new GraphicsView()
                .Margin(24)
                .OnDraw(OnDrawPlayPauseIcon)
                .OnStartInteraction(_tapAction),

            new Label(_isPaused ? "Paused" : "Running")
                .GridColumn(1)
                .VCenter()
                .FontSize(24)
                .TextColor(Color.Parse("#DFD8F7"))
                .Margin(10)
                ,

            new AnimationController
            { 
                new SequenceAnimation
                {
                    new DoubleAnimation()
                        .StartValue(3*4)
                        .TargetValue(0)
                        .OnTick(v => SetState(s => s.PauseIconRotationAngle = v))                        
                }
            }
            .IsEnabled(!_isPaused)
        }
        .OnTapped(_tapAction)
        .HCenter()
        .VCenter();
    }

    void OnDrawPlayPauseIcon(ICanvas canvas, RectF dirtyRect)
    {
        if (_isPaused)
        {
            canvas.FillColor = Color.Parse("#DFD8F7");
            canvas.StrokeLineCap = LineCap.Round;
            canvas.FillPath(_playPath);
        }
        else
        {
            canvas.StrokeSize = 3;
            canvas.StrokeColor = Color.Parse("#DFD8F7");
            canvas.StrokeDashPattern = new[] { 1.0f, 2.0f };
            canvas.StrokeDashOffset = (float)State.PauseIconRotationAngle;
            canvas.DrawCircle(11f, 15, 30);

            canvas.StrokeSize = 6;
            canvas.StrokeDashPattern = null;
            canvas.StrokeLineCap = LineCap.Round;
            canvas.DrawPath(_pausePath);
        }

    }
}