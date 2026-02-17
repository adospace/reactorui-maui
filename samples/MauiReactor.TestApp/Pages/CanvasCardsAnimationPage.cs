using MauiReactor.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;


class CanvasAnimationPageState
{
    public List<Card> Cards { get; set; } = Enumerable.Range(1, 10).Select(index => new Card { Index = index - 1, Position = index }).ToList();
}

class CanvasCardsAnimationPage : Component<CanvasAnimationPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage("Canvas Animation Sample")
        {
            new CanvasView
            {
                State.Cards
                    .Select(card => new CanvasCardPage()
                        .Index(card.Index)
                        .Position(card.Position)
                        .OnMovedBack(cardIndex =>
                        {
                            foreach (var card in State.Cards)
                            {
                                card.Position++;
                            }

                            State.Cards[cardIndex].Position = 1;
                            Invalidate();
                        })),
            }
            .BackgroundColor(Colors.Transparent)
        }
        .Background(MauiControls.Brush.Black);
    }
}

class CanvasCardState
{
    public float Rotation { get; set; } = (float)(Random.Shared.NextDouble() * 5 - 2.5);

    public bool MovingBack { get; set; }
}

class CanvasCardPage : Component<CanvasCardState>
{
    private static readonly Paint[] _cardBackgrounds = new[]
    {
        (From: "#36D1DC", To: "#5B86E5"),
        (From: "#CB356B", To: "#BD3F32"),
        (From: "#283c86", To: "#45a247"),
        (From: "#EF3B36", To: "#FFFFFF"),
        (From: "#c0392b", To: "#8e44ad"),
        (From: "#159957", To: "#155799"),
        (From: "#000046", To: "#1CB5E0"),
        (From: "#007991", To: "#78ffd6"),
        (From: "#56CCF2", To: "#2F80ED"),
        (From: "#F2994A", To: "#F2C94C"),
    }
    .Select(MakeBrush)
    .ToArray();

    public static Paint MakeBrush((string From, string To) fromTo)
    {
        var brush = new LinearGradientPaint();
        brush.AddOffset(0.0f, Color.FromArgb(fromTo.From));
        brush.AddOffset(1.0f, Color.FromArgb(fromTo.To));
        return brush;
    }

    private int _cardIndex;
    private int _zIndex;
    private Action<int>? _onMovedBackAction;

    public CanvasCardPage Index(int cardIndex)
    {
        _cardIndex = cardIndex;
        return this;
    }

    public CanvasCardPage Position(int zIndex)
    {
        _zIndex = zIndex;
        return this;
    }

    public CanvasCardPage OnMovedBack(Action<int> onMovedBackAction)
    {
        _onMovedBackAction = onMovedBackAction;
        return this;
    }

    public override VisualNode Render()
    {
        return new Align
        {
            new PointInteractionHandler
            {
                new Box()
                {
                    new Timer(100, ()=>
                    {
                        if (State.MovingBack)
                        {
                            State.MovingBack = false;
                            _onMovedBackAction?.Invoke(_cardIndex);
                        }
                    })
                    .IsEnabled(State.MovingBack)
                }
                .TranslationY(State.MovingBack ? -230 : 0)
                .Rotation(State.Rotation)
                .WithAnimation()
                .AnchorX(0.5f)
                .AnchorY(0.5f)
                .Background(_cardBackgrounds[_cardIndex])
                .CornerRadius(5)            
            }
            .IsEnabled(!State.MovingBack)
            .OnTap(() =>
            {
                SetState(s =>
                {
                    s.MovingBack = true;
                    s.Rotation += 360 * 2;
                });
            })
        }
        .Width(300)
        .Height(200)
        .ZIndex(_zIndex)
        .VEnd()
        .HCenter()
        .Margin(0, 40);
    }
}

