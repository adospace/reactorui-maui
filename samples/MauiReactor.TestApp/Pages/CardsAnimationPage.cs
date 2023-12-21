using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.Shapes;

namespace MauiReactor.TestApp.Pages
{
    class Card
    {
        public int Index { get; init; }

        public int Position { get; set; }

    }

    class AnimationPageState
    {
        public List<Card> Cards { get; set; } = Enumerable.Range(1, 10).Select(index => new Card { Index = index - 1, Position = index }).ToList();
    }

    class CardsAnimationPage : Component<AnimationPageState>
    {
        public override VisualNode Render()
        {
            return ContentPage(
            [
                Grid(
                    State.Cards
                        .Select(card => new CardPage()
                            .Index(card.Index)
                            .Position(card.Position)
                            .OnMovedBack(cardIndex =>
                            {
                                SetState(s => 
                                {
                                    foreach (var card in s.Cards)
                                    {
                                        card.Position++;
                                    }

                                    s.Cards[cardIndex].Position = 1;
                                });
                            })
                            )
                        .ToArray()
                )
                .Background(MauiControls.Brush.Black)
            ])
            .Title("Animation Sample");
        }
    }

    class CardState
    {
        public double Rotation { get; set; } = Random.Shared.NextDouble() * 5 - 2.5;

        public bool MovingBack { get; set; }
    }

    class CardPage : Component<CardState>
    {
        private static readonly MauiControls.Brush[] _cardBackgrounds = new[]
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

        public static MauiControls.Brush MakeBrush((string From, string To) fromTo)
        {
            var brush = new MauiControls.LinearGradientBrush();
            brush.GradientStops.Add(new MauiControls.GradientStop(Color.FromArgb(fromTo.From), 0.0f));
            brush.GradientStops.Add(new MauiControls.GradientStop(Color.FromArgb(fromTo.To), 1.0f));
            return brush;
        }

        private int _cardIndex;
        private int _zIndex;
        private Action<int>? _onMovedBackAction;

        public CardPage Index(int cardIndex)
        {
            _cardIndex = cardIndex;
            return this;
        }

        public CardPage Position(int zIndex)
        {
            _zIndex = zIndex;
            return this;
        }

        public CardPage OnMovedBack(Action<int> onMovedBackAction)
        {
            _onMovedBackAction = onMovedBackAction;
            return this;
        }

        public override VisualNode Render()
        {
            return Border(
            [
                Timer()
                    .Interval(300)
                    .OnTick(()=>
                    {
                        if (State.MovingBack)
                        {
                            State.MovingBack = false;
                            _onMovedBackAction?.Invoke(_cardIndex);
                        }
                    })
                    .IsEnabled(State.MovingBack)
            ])
            .ZIndex(_zIndex)
            .TranslationY(State.MovingBack ? -230 : 0)
            .Rotation(State.Rotation)
            .WithAnimation()
            .Background(_cardBackgrounds[_cardIndex])
            .WidthRequest(300)
            .HeightRequest(200)
            .StrokeCornerRadius(5)
            .VEnd()
            .HCenter()
            .Margin(0, 40)
            .OnTapped(()=>
            { 
                SetState(s =>
                {
                    s.MovingBack = true;
                    s.Rotation += 360 * 2;
                });
            })
            ;

        }


    }
}
