using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;
using Microsoft.Maui.Graphics;
using SlidingPuzzle.Resources.Styles;

namespace SlidingPuzzle.Pages
{
    class GameState
    {
        public bool IsStarted { get; set; }

        public DateTime Started { get; set; }

        public TimeSpan Elapsed { get; set; }

        public DateTime StartTimeStamp { get; set; }
    
        public int[,] CellPositions { get; set; } = new int[4, 4] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 }, { 12, 13, 14, -1 } };

        public double GameBoardWidth { get; set; }
    }

    class MainPage : Component<GameState>
    {
        protected override void OnMounted()
        {
            base.OnMounted();
        }

        public override VisualNode Render()
        {
            return new ContentPage
            {
                new Grid("Auto,*", "*")
                {
                    new GameTimer(State),

                    new GameBoard(State)
                        .GridRow(1)
                }
                .Background(ThemeColors.Warning)
            };
        }
    }

    class GameTimer : Component<GameState>
    {
        public GameTimer(GameState state) : base(state)
        { }

        public override VisualNode Render()
        {
            return new HorizontalStackLayout
            {

                new Timer(100, () => SetState(s => s.Elapsed = (DateTime.Now - State.Started), invalidateComponent: false))
                    .DueTime(TimeSpan.Zero)
                    .IsEnabled(()=>State.IsStarted),

                new Label()
                    .Text(() => State.Elapsed.ToString("hh\\:mm\\:ss"))
                    .FontSize(28)
                    .FontAttributes(Microsoft.Maui.Controls.FontAttributes.Bold)
                    .VCenter(),

                new ImageButton()
                    .Background(ThemeColors.Transparent)
                    .Source("play_svgrepo_com.png")
                    .OnClicked(()=>SetState(s =>
                    {
                        s.IsStarted = true;
                        s.Started = DateTime.Now;
                        s.Elapsed = TimeSpan.Zero;

                    }, invalidateComponent: false))
                    .IsVisible(()=>!State.IsStarted),

                new ImageButton()
                    .Background(ThemeColors.Transparent)
                    .Source("stop_pause_svgrepo_com.png")
                    .OnClicked(()=>SetState(s => s.IsStarted = false, invalidateComponent: false))
                    .IsVisible(()=>State.IsStarted),
            }
            .Spacing(20)
            .HCenter()
            .VCenter()
            .Padding(0,40);
        }
    }

    class GameBoard : Component<GameState>
    {
        public GameBoard(GameState state) : base(state)
        { }

        public override VisualNode Render()
        {
            return new ContentView
            {
                new AbsoluteLayout()
                {
                    State.GameBoardWidth == 0 ? null :
                    Enumerable.Range(0, 4).SelectMany(row =>
                        Enumerable.Range(0, 4).Select(column =>
                        {
                            var index = State.CellPositions[row, column];
                            if (index == -1)
                            {
                                return (-1,-1,-1);
                            }


                            return (index, row, column);
                        }))
                    .Where(_=>_.Item1 != -1)
                    .OrderBy(_=>_.Item1)
                    .Select(_=> new PuzzleCell()
                        .Row(_.Item2)
                        .Column(_.Item3)
                        .Index(State.CellPositions[_.Item2, _.Item3])
                        .Size(State.GameBoardWidth / 4.0)
                        .OnTap(OnCellTap))
                    .ToArray()
                }
                .MinimumWidthRequest(400.0)
                .MaximumWidthRequest(800.0)
                .HeightRequest(State.GameBoardWidth)
                .HCenter()
                .VCenter()
            }
            .OnSizeChanged(OnContainerSizeChanged)
            .VFill()
            .HFill()            
            ;
        }

        private void OnCellTap(int cellIndex, int cellRow, int cellColumn)
        {
            for (int row = cellRow + 1; row < 4; row++)
            {
                if (State.CellPositions[row, cellColumn] == -1)
                {
                    while (row > cellRow)
                    {
                        State.CellPositions[row, cellColumn] = State.CellPositions[row - 1, cellColumn];
                        row--;
                    }
                    State.CellPositions[row, cellColumn] = -1;
                    Invalidate();
                    return;
                }
            }

            for (int row = 0; row < cellRow; row++)
            {
                if (State.CellPositions[row, cellColumn] == -1)
                {
                    while (row < cellRow)
                    {
                        State.CellPositions[row, cellColumn] = State.CellPositions[row + 1, cellColumn];
                        row++;
                    }
                    State.CellPositions[row, cellColumn] = -1;
                    Invalidate();
                    return;
                }
            }

            for (int column = cellColumn + 1; column < 4; column++)
            {
                if (State.CellPositions[cellRow, column] == -1)
                {
                    while (column > cellColumn)
                    {
                        State.CellPositions[cellRow, column] = State.CellPositions[cellRow, column - 1];
                        column--;
                    }

                    State.CellPositions[cellRow, column] = -1;
                    Invalidate();
                    return;
                }
            }

            for (int column = 0; column < cellColumn; column++)
            {
                if (State.CellPositions[cellRow, column] == -1)
                {
                    while (column < cellColumn)
                    {
                        State.CellPositions[cellRow, column] = State.CellPositions[cellRow, column + 1];
                        column++;
                    }

                    State.CellPositions[cellRow, column] = -1;
                    Invalidate();
                    return;
                }
            }
        }

        void OnContainerSizeChanged(object? sender, EventArgs args)
        {
            var visualElement = (Microsoft.Maui.Controls.VisualElement?)sender;
            if (visualElement == null)
            {
                return;
            }
            SetState(s => s.GameBoardWidth = Math.Min(800.0, Math.Max(400, visualElement.Width)));
        }

    }

    class CellState 
    {
        public double X { get; set; }

        public double Y { get; set; }
    }

    class PuzzleCell : Component<CellState>
    {
        private int _index;
        private int _row;
        private int _column;
        private double _size;
        private Action<int, int, int>? _onCellTapped;

        public PuzzleCell Index(int index)
        {
            _index = index;
            return this;
        }

        public PuzzleCell Row(int row)
        {
            _row = row;
            return this;
        }

        public PuzzleCell Column(int column)
        {
            _column = column;
            return this;
        }

        public PuzzleCell Size(double size)
        {
            _size = size;
            return this;
        }

        public PuzzleCell OnTap(Action<int, int, int> onCellTapped)
        {
            _onCellTapped = onCellTapped;
            return this;
        }

        protected override void OnMounted()
        {
            State.X = _column * _size;
            State.Y = _row * _size;

            base.OnMounted();
        }

        protected override void OnPropsChanged()
        {
            State.X = _column * _size;
            State.Y = _row * _size;

            base.OnPropsChanged();
        }

        public override VisualNode Render()
        {
            return new Grid
            {
                new ImageButton()
                    .Source("red_square_svgrepo_com.png")
                    .CornerRadius(18)
                    .Margin(4)
                    .OnClicked(() => _onCellTapped?.Invoke(_index, _row, _column)),

                    new Label((_index + 1).ToString())
                        .VCenter()
                        .HCenter()
                        .TextColor(ThemeColors.White)
                        .FontSize(36)
                        .FontAttributes(Microsoft.Maui.Controls.FontAttributes.Bold)
            }
            .AbsoluteLayoutBounds(new Rect(State.X, State.Y, _size, _size))
            .WithAnimation(easing: Easing.BounceOut)
            .AbsoluteLayoutFlags(Microsoft.Maui.Layouts.AbsoluteLayoutFlags.None)
            ;
        }
    }
}


