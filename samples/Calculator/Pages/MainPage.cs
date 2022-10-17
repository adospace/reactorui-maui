using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Resources.Styles;
using MauiReactor;

namespace Calculator.Pages;

class MainPageState : IState
{
    public string CurrentNumber { get; set; } = string.Empty;

    public string PreviousNumber { get; set;} = string.Empty;

    public string CurrentOperation { get; set; } = string.Empty;

    
    
}

class MainPage : Component<MainPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage
        {
            new Grid("* Auto", "*")
            {
                new KeyPad()
                    .GridRow(1)
            }
        }
        .BackgroundColor(Theme.Background);
    }
}


public class KeyPad : Component
{
    private Action<string>? _keyPressedAction;
    private MauiControls.Grid? _grid;

    public KeyPad OnKeyPressed(Action<string> keyPressed)
    {
        _keyPressedAction = keyPressed;
        return this;
    }

    public override VisualNode Render()
    {
        return new Grid(refToGrid => _grid = refToGrid)
        {
            RenderButtonMediumEmphasis("C", 0, 0),
            RenderImageButtonMediumEmphasis(Theme.IsDarkTheme ? "plus_minus_white.png" : "plus_minus.png", "+-", 0, 1),
            RenderButtonMediumEmphasis("%", 0, 2),
            RenderButtonHighEmphasis("÷", 0, 3),

            RenderButtonLowEmphasis("7", 1, 0),
            RenderButtonLowEmphasis("8", 1, 1),
            RenderButtonLowEmphasis("9", 1, 2),
            RenderButtonHighEmphasis("×", 1, 3),

            RenderButtonLowEmphasis("4", 2, 0),
            RenderButtonLowEmphasis("5", 2, 1),
            RenderButtonLowEmphasis("6", 2, 2),
            RenderButtonHighEmphasis("-", 2, 3),

            RenderButtonLowEmphasis("1", 3, 0),
            RenderButtonLowEmphasis("2", 3, 1),
            RenderButtonLowEmphasis("3", 3, 2),
            RenderButtonHighEmphasis("+", 3, 3),

            RenderButtonLowEmphasis(".", 4, 0),
            RenderButtonLowEmphasis("0", 4, 1),
            RenderImageButtonLowEmphasis(Theme.IsDarkTheme ? "back_white.png" : "back.png", "back", 4, 2),
            RenderButtonHighEmphasis("=", 4, 3),

        }
        .Rows("* * * * *")
        .Columns("* * * *")
        .ColumnSpacing(16)
        .RowSpacing(16)
        .Margin(20, 0, 20, 66)
        .OnSizeChanged(Invalidate)
        .HeightRequest(Math.Max(72, (_grid?.Width * 1.265671).GetValueOrDefault()));
    }

    Button RenderButtonLowEmphasis(string text, int row, int column)
        => Theme.ButtonLowEmphasis(text)
        .GridRow(row)
        .GridColumn(column)
        .OnClicked(() => _keyPressedAction?.Invoke(text));

    Button RenderButtonMediumEmphasis(string text, int row, int column)
        => Theme.ButtonMediumEmphasis(text)
        .GridRow(row)
        .GridColumn(column)
        .OnClicked(() => _keyPressedAction?.Invoke(text));

    Grid RenderImageButtonMediumEmphasis(string imageSource, string text, int row, int column)
        => Theme.ImageButtonMediumEmphasis(imageSource, () => _keyPressedAction?.Invoke(text))        
        .GridRow(row)
        .GridColumn(column);

    Grid RenderImageButtonLowEmphasis(string imageSource, string text, int row, int column)
        => Theme.ImageButtonLowEmphasis(imageSource, () => _keyPressedAction?.Invoke(text))
        .GridRow(row)
        .GridColumn(column);

    Button RenderButtonHighEmphasis(string text, int row, int column)
        => Theme.ButtonHighEmphasis(text)
        .GridRow(row)
        .GridColumn(column)
        .OnClicked(() => _keyPressedAction?.Invoke(text));

}