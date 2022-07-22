using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages
{
    class Card
    {
        public int Index { get; init; }

        public int Position { get; set; }

    }

    class AnimationPageState : IState
    {
        public List<Card> Cards { get; set; } = Enumerable.Range(1, 10).Select(index => new Card { Index = index - 1, Position = index }).ToList();

        public int MovingBackCardIndex { get; set; } = -1;
    }

    class AnimationPage : Component<AnimationPageState>
    {
        public override VisualNode Render()
        {
            return new ContentPage("Animation Sample")
            {
                new Grid
                {
                    State.Cards
                        .Select(card => new CardPage()
                            .Index(card.Index)
                            .ZIndex(card.Position)
                            .TopPosition(card.Position == State.Cards.Count)
                            .MovingBackUp(card.Index == State.MovingBackCardIndex)
                            .OnTapped(cardIndex =>
                            {
                                SetState(s => s.MovingBackCardIndex = cardIndex);                            
                            })),

                    new Timer(600, ()=>
                    {
                        SetState(s =>
                        {
                            foreach(var card in State.Cards)
                            {
                                card.Position++;
                            }
                            
                            State.Cards[s.MovingBackCardIndex].Position = 1;
                            
                            s.MovingBackCardIndex = -1;
                        });
                    })
                    .IsEnabled(State.MovingBackCardIndex != -1)
                }
                .Background(Brush.Black)
            };
        }
    }

    class CardState : IState
    {
        public double Rotation { get; set; } = Random.Shared.NextDouble() * 5 - 2.5;
    }

    class CardPage : Component<CardState>
    {
        private static readonly Brush[] _cardBackgrounds = new[]
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

        public static Brush MakeBrush((string From, string To) fromTo)
        {
            var brush = new LinearGradientBrush();
            brush.GradientStops.Add(new GradientStop(Color.FromArgb(fromTo.From), 0.0f));
            brush.GradientStops.Add(new GradientStop(Color.FromArgb(fromTo.To), 0.0f));
            return brush;
        }

        private int _cardIndex;
        private int _zIndex;
        private Action<int>? _onTappedAction;
        private bool _topPosition;
        private bool _movingBackUp;

        public CardPage Index(int cardIndex)
        {
            _cardIndex = cardIndex;
            return this;
        }

        public CardPage ZIndex(int zIndex)
        {
            _zIndex = zIndex;
            return this;
        }

        public CardPage TopPosition(bool topPosition)
        {
            _topPosition = topPosition;
            return this;
        }

        public CardPage MovingBackUp(bool movingBackUp)
        {
            _movingBackUp = movingBackUp;
            return this;
        }

        public CardPage OnTapped(Action<int> onTappedAction)
        {
            _onTappedAction = onTappedAction;
            return this;
        }        

        public override VisualNode Render()
        {
            return new Frame()
                .ZIndex(_zIndex)
                .TranslationY(_movingBackUp ? -250 : 0)
                .Rotation(State.Rotation + (_movingBackUp ? 360 : 0))
                .WithAnimation()
                .Background(_cardBackgrounds[_cardIndex])
                .WidthRequest(300)
                .HeightRequest(200)
                .CornerRadius(5)
                .VEnd()
                .HCenter()
                .Margin(0, 40)
                .OnTapped(()=>
                { 
                    if (_topPosition && !_movingBackUp)
                    {
                        _onTappedAction?.Invoke(_cardIndex);
                    }
                })
            ;

        }


    }
}
