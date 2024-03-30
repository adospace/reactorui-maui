using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Resources.Styles;
using MauiReactor;
using MauiReactor.Canvas;
using Rearch;
using Rearch.Reactor.Components;

namespace Calculator.Pages;

class MainPageState
{
    public string CurrentNumber { get; set; } = string.Empty;

    public double? Number1 { get; set; }

    public double? Number2 { get; set; }

    public string CurrentOperation { get; set; } = string.Empty;

    public double? Result { get; set; }

    public bool Perc { get; set; }
    
}

class MainPage : CapsuleConsumer
{
    protected override void OnMounted()
    {
        if (MauiControls.Application.Current != null)
        {
            MauiControls.Application.Current.RequestedThemeChanged += (sender, args) => Invalidate();
        }
        
        base.OnMounted();
    }

    public override VisualNode Render(ICapsuleHandle use)
    {
        var (state, setState) = use.State(new MainPageState());

        return new ContentPage
        {
            new Grid("48 * 420", "*")
            {
                new ThemeToggle(),
                
                RenderDisplayPanel(state),

                new KeyPad()
                    .OnKeyPressed(key => OnKeyPressed(state, setState, key))
                    .GridRow(2),
            }
        }
        .BackgroundColor(AppTheme.Background);
    }

    VisualNode RenderDisplayPanel(MainPageState state)
    {
        return new VStack(spacing: 0)
        {
            AppTheme.Label(()=> $"{state.Number1} {state.CurrentOperation} {state.Number2}{(state.Perc ? "%" : string.Empty)}{(state.Result != null ? " =" : string.Empty)}")
                .FontSize(40)
                .TextColor(AppTheme.Text.WithAlpha(0.4f))
                .HorizontalTextAlignment(TextAlignment.End),

            AppTheme.Label(()=> state.Result != null ? state.Result.Value.ToString() : state.CurrentNumber.Length > 0 ? state.CurrentNumber : "0")
                .FontSize(63)
                .HorizontalTextAlignment(TextAlignment.End)
        }
        .Margin(20,0)
        .GridRow(1)
        .HFill()
        .VEnd();
    }

    void OnKeyPressed(MainPageState state, Action<MainPageState> setState, string key)
    {
        if (state.Result != null)
        {
            if (key == "÷" || key == "×" || key == "+" || key == "-")
            {
                setState(new MainPageState
                {
                    CurrentNumber = string.Empty,
                    Number1 = state.Result,
                    Number2 = null,
                    CurrentOperation = key,
                    Result = null,
                    Perc = false,
                });
            }
            else
            {
                setState(new MainPageState
                {
                    CurrentNumber = string.Empty,
                    Number1 = null,
                    Number2 = null,
                    CurrentOperation = string.Empty,
                    Result = null,
                    Perc = false,
                });
            }
        };


        if (key == "back")
        {
            if (state.CurrentNumber.Length > 0)
            {
                setState(new MainPageState
                {
                    CurrentNumber = state.CurrentNumber.Substring(0, state.CurrentNumber.Length - 1),
                    Number1 = state.Number1,
                    Number2 = state.Number2,
                    CurrentOperation = state.CurrentOperation,
                    Result = state.Result,
                    Perc = state.Perc,
                });
            }
        }
        else if (key == ".")
        {
            if (state.CurrentNumber.Length > 0 && !state.CurrentNumber.Contains("."))
            {
                setState(new MainPageState
                {
                    CurrentNumber = state.CurrentNumber + key,
                    Number1 = state.Number1,
                    Number2 = state.Number2,
                    CurrentOperation = state.CurrentOperation,
                    Result = state.Result,
                    Perc = state.Perc,
                });
            }
        }
        else if (key == "0")
        {
            if (state.CurrentNumber.Length > 0)
            {
                setState(new MainPageState
                {
                    CurrentNumber = state.CurrentNumber + key,
                    Number1 = state.Number1,
                    Number2 = state.Number2,
                    CurrentOperation = state.CurrentOperation,
                    Result = state.Result,
                    Perc = state.Perc,
                });
            }
        }
        else if (key == "C")
        {
            setState(new MainPageState
            {
                CurrentNumber = string.Empty,
                Number1 = state.Number1,
                Number2 = state.Number2,
                CurrentOperation = state.CurrentOperation,
                Result = state.Result,
                Perc = state.Perc,
            });
        }
        else if (key == "=")
        {
            if (state.CurrentOperation.Length > 0 && state.Number1 != null)
            {
                var number2 = state.CurrentNumber.Length > 0 ? double.Parse(state.CurrentNumber) : 0.0;
                var result = state.CurrentOperation switch
                {
                    "÷" => state.Number1!.Value / number2,
                    "×" => state.Number1!.Value * number2,
                    "+" => state.Number1!.Value + number2,
                    "-" => state.Number1!.Value - number2,
                    _ => state.Result,
                };
                setState(new MainPageState
                {
                    CurrentNumber = state.CurrentNumber,
                    Number1 = state.Number1,
                    Number2 = number2,
                    CurrentOperation = state.CurrentOperation,
                    Result = result,
                    Perc = state.Perc,
                });
            }
        }
        else if (key == "%")
        {
            if (state.CurrentOperation.Length > 0 && state.Number1 != null)
            {
                var number2 = state.CurrentNumber.Length > 0 ? double.Parse(state.CurrentNumber) : 0.0;
                var result = state.CurrentOperation switch
                {
                    "÷" => state.Number1!.Value / (number2 / 100.0) * state.Number1!.Value,
                    "×" => state.Number1!.Value * (number2 / 100.0) * state.Number1!.Value,
                    "+" => state.Number1!.Value + (number2 / 100.0) * state.Number1!.Value,
                    "-" => state.Number1!.Value - (number2 / 100.0) * state.Number1!.Value,
                    _ => state.Result,
                };
                setState(new MainPageState
                {
                    CurrentNumber = state.CurrentNumber,
                    Number1 = state.Number1,
                    Number2 = number2,
                    CurrentOperation = state.CurrentOperation,
                    Result = result,
                    Perc = state.Perc,
                });
            }
        }
        else if (key == "+-")
        {
            if (state.CurrentNumber.Length > 0)
            {
                var currentNumber = state.CurrentNumber.StartsWith('-') ?
                    state.CurrentNumber[1..] :
                    "-" + state.CurrentNumber;
                setState(new MainPageState
                {
                    CurrentNumber = currentNumber,
                    Number1 = state.Number1,
                    Number2 = state.Number2,
                    CurrentOperation = state.CurrentOperation,
                    Result = state.Result,
                    Perc = state.Perc,
                });
            }
        }
        else if (key == "÷" || key == "×" || key == "+" || key == "-")
        {
            if (state.CurrentOperation.Length == 0 && state.CurrentNumber.Length > 0)
            {
                setState(new MainPageState
                {
                    CurrentNumber = string.Empty,
                    Number1 = double.Parse(state.CurrentNumber),
                    Number2 = state.Number2,
                    CurrentOperation = key,
                    Result = state.Result,
                    Perc = state.Perc,
                });
            }
        }
        else
        {
            setState(new MainPageState
            {
                CurrentNumber = state.CurrentNumber + key,
                Number1 = state.Number1,
                Number2 = state.Number2,
                CurrentOperation = state.CurrentOperation,
                Result = state.Result,
                Perc = state.Perc,
            });
        }
    }
}

public class ThemeToggle : Component
{
    public override VisualNode Render()
    {
        return new CanvasView
        {
            new Box
            {
                new Group
                {
                    new Align
                    {
                        new Ellipse()
                            .FillColor(AppTheme.ButtonMediumEmphasisBackground)
                    }
                    .Height(24)
                    .Width(24)
                    .Margin(4)
                    .HorizontalAlignment(AppTheme.IsDarkTheme ? Microsoft.Maui.Primitives.LayoutAlignment.Start : Microsoft.Maui.Primitives.LayoutAlignment.End)
                    .VCenter(),
                
                    new Align
                    {
                        AppTheme.IsDarkTheme ?
                        new Picture("Calculator.Resources.Images.moon.png")
                        :
                        new Picture("Calculator.Resources.Images.sun.png"),
                    }
                    .Height(24)
                    .Width(24)
                    .Margin(8,4)
                    .HorizontalAlignment(AppTheme.IsDarkTheme ? Microsoft.Maui.Primitives.LayoutAlignment.End : Microsoft.Maui.Primitives.LayoutAlignment.Start)
                    .VCenter()
                }
            }
            .BackgroundColor(AppTheme.ButtonLowEmphasisBackground)
            .CornerRadius(16)
        }
        .OnTapped(AppTheme.ToggleCurrentAppTheme)
        .Margin(16)
        .VCenter()
        .HCenter()
        .HeightRequest(32)
        .WidthRequest(72)
        .BackgroundColor(Colors.Transparent);
    }
}


public class KeyPad : Component
{
    private Action<string>? _keyPressedAction;

    public KeyPad OnKeyPressed(Action<string> keyPressed)
    {
        _keyPressedAction = keyPressed;
        return this;
    }

    public override VisualNode Render()
    {
        return new Grid()
        {
            RenderButtonMediumEmphasis("C", 0, 0),
            RenderImageButtonMediumEmphasis(AppTheme.IsDarkTheme ? "plus_minus_white.png" : "plus_minus.png", "+-", 0, 1),
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
            RenderImageButtonLowEmphasis(AppTheme.IsDarkTheme ? "back_white.png" : "back.png", "back", 4, 2),
            RenderButtonHighEmphasis("=", 4, 3),

        }
        .Rows("* * * * *")
        .Columns("* * * *")
        .ColumnSpacing(16)
        .RowSpacing(16)
        .Margin(20, 0, 20, 20)
        .OnSizeChanged(Invalidate)
        .HeightRequest(400);
    }

    Button RenderButtonLowEmphasis(string text, int row, int column)
        => AppTheme.ButtonLowEmphasis(text)
        .GridRow(row)
        .GridColumn(column)
        .OnClicked(() => _keyPressedAction?.Invoke(text));

    Button RenderButtonMediumEmphasis(string text, int row, int column)
        => AppTheme.ButtonMediumEmphasis(text)
        .GridRow(row)
        .GridColumn(column)
        .OnClicked(() => _keyPressedAction?.Invoke(text));

    Grid RenderImageButtonMediumEmphasis(string imageSource, string text, int row, int column)
        => AppTheme.ImageButtonMediumEmphasis(imageSource, () => _keyPressedAction?.Invoke(text))        
        .GridRow(row)
        .GridColumn(column);

    Grid RenderImageButtonLowEmphasis(string imageSource, string text, int row, int column)
        => AppTheme.ImageButtonLowEmphasis(imageSource, () => _keyPressedAction?.Invoke(text))
        .GridRow(row)
        .GridColumn(column);

    Button RenderButtonHighEmphasis(string text, int row, int column)
        => AppTheme.ButtonHighEmphasis(text)
        .GridRow(row)
        .GridColumn(column)
        .OnClicked(() => _keyPressedAction?.Invoke(text));

}