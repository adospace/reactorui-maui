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
                
                RenderDisplayPanel(use, state),

                new KeyPad()
                    .OnKeyPressed(key => OnKeyPressed(state, setState, key))
                    .GridRow(2),
            }
        }
        .BackgroundColor(AppTheme.Background(use));
    }

    VisualNode RenderDisplayPanel(ICapsuleHandle use, MainPageState state)
    {
        return new VStack(spacing: 0)
        {
            AppTheme.Label(use, ()=> $"{state.Number1} {state.CurrentOperation} {state.Number2}{(state.Perc ? "%" : string.Empty)}{(state.Result != null ? " =" : string.Empty)}")
                .FontSize(40)
                .TextColor(AppTheme.Text(use).WithAlpha(0.4f))
                .HorizontalTextAlignment(TextAlignment.End),

            AppTheme.Label(use, ()=> state.Result != null ? state.Result.Value.ToString() : state.CurrentNumber.Length > 0 ? state.CurrentNumber : "0")
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
        var newState = new MainPageState
        {
            CurrentNumber = state.CurrentNumber,
            Number1 = state.Number1,
            Number2 = state.Number2,
            CurrentOperation = state.CurrentOperation,
            Result = state.Result,
            Perc = state.Perc,
        };

        if (newState.Result != null)
        {
            if (key == "÷" || key == "×" || key == "+" || key == "-")
            {
                newState.Number1 = newState.Result;
                newState.Number2 = newState.Result = null;
                newState.CurrentOperation = key;
                newState.CurrentNumber = string.Empty;
                newState.Perc = false;
            }
            else
            {
                newState.Number1 = newState.Number2 = newState.Result = null;
                newState.CurrentOperation = string.Empty;
                newState.CurrentNumber = string.Empty;
                newState.Perc = false;
            }
        };


        if (key == "back")
        {
            if (newState.CurrentNumber.Length > 0)
            {
                newState.CurrentNumber = newState.CurrentNumber.Substring(0, newState.CurrentNumber.Length - 1);
            }
        }
        else if (key == ".")
        {
            if (newState.CurrentNumber.Length > 0 && !newState.CurrentNumber.Contains("."))
            {
                newState.CurrentNumber += key;
            }
        }
        else if (key == "0")
        {
            if (newState.CurrentNumber.Length > 0)
            {
                newState.CurrentNumber += key;
            }
        }
        else if (key == "C")
        {
            newState.CurrentNumber = string.Empty;
        }
        else if (key == "=")
        {
            if (newState.CurrentOperation.Length > 0 && newState.Number1 != null)
            {
                newState.Number2 = newState.CurrentNumber.Length > 0 ? double.Parse(newState.CurrentNumber) : 0.0;
                switch (newState.CurrentOperation)
                {
                    case "÷":
                        newState.Result = newState.Number1!.Value / newState.Number2.Value;
                        break;
                    case "×":
                        newState.Result = newState.Number1!.Value * newState.Number2.Value;
                        break;
                    case "+":
                        newState.Result = newState.Number1!.Value + newState.Number2.Value;
                        break;
                    case "-":
                        newState.Result = newState.Number1!.Value - newState.Number2.Value;
                        break;
                }
            }
        }
        else if (key == "%")
        {
            if (newState.CurrentOperation.Length > 0 && newState.Number1 != null)
            {
                newState.Number2 = newState.CurrentNumber.Length > 0 ? double.Parse(newState.CurrentNumber) : 0.0;
                newState.Perc = true;
                switch (newState.CurrentOperation)
                {
                    case "÷":
                        newState.Result = newState.Number1!.Value / (newState.Number2.Value / 100.0) * newState.Number1!.Value;
                        break;
                    case "×":
                        newState.Result = newState.Number1!.Value * (newState.Number2.Value / 100.0) * newState.Number1!.Value;
                        break;
                    case "+":
                        newState.Result = newState.Number1!.Value + (newState.Number2.Value / 100.0) * newState.Number1!.Value;
                        break;
                    case "-":
                        newState.Result = newState.Number1!.Value - (newState.Number2.Value / 100.0) * newState.Number1!.Value;
                        break;
                }
            }
        }
        else if (key == "+-")
        {
            if (newState.CurrentNumber.Length > 0)
            {
                newState.CurrentNumber = newState.CurrentNumber.StartsWith("-") ? newState.CurrentNumber = newState.CurrentNumber.Substring(1) : "-" + newState.CurrentNumber;
            }
        }
        else if (key == "÷" || key == "×" || key == "+" || key == "-")
        {
            if (newState.CurrentOperation.Length == 0 && newState.CurrentNumber.Length > 0)
            {
                newState.CurrentOperation = key;
                newState.Number1 = double.Parse(newState.CurrentNumber);
                newState.CurrentNumber = string.Empty;
            }
        }
        else
        {
            newState.CurrentNumber += key;
        }

        setState(newState);
    }
}

public class ThemeToggle : CapsuleConsumer
{
    public override VisualNode Render(ICapsuleHandle use)
    {
        var (isDarkTheme, toggleCurrentAppTheme) = use.Invoke(AppTheme.ThemeCapsule);

        return new CanvasView
        {
            new Box
            {
                new Group
                {
                    new Align
                    {
                        new Ellipse()
                            .FillColor(AppTheme.ButtonMediumEmphasisBackground(use))
                    }
                    .Height(24)
                    .Width(24)
                    .Margin(4)
                    .HorizontalAlignment(isDarkTheme ? Microsoft.Maui.Primitives.LayoutAlignment.Start : Microsoft.Maui.Primitives.LayoutAlignment.End)
                    .VCenter(),
                
                    new Align
                    {
                        isDarkTheme ?
                        new Picture("Calculator.Resources.Images.moon.png")
                        :
                        new Picture("Calculator.Resources.Images.sun.png"),
                    }
                    .Height(24)
                    .Width(24)
                    .Margin(8,4)
                    .HorizontalAlignment(isDarkTheme ? Microsoft.Maui.Primitives.LayoutAlignment.End : Microsoft.Maui.Primitives.LayoutAlignment.Start)
                    .VCenter()
                }
            }
            .BackgroundColor(AppTheme.ButtonLowEmphasisBackground(use))
            .CornerRadius(16)
        }
        .OnTapped(toggleCurrentAppTheme)
        .Margin(16)
        .VCenter()
        .HCenter()
        .HeightRequest(32)
        .WidthRequest(72)
        .BackgroundColor(Colors.Transparent);
    }
}


public class KeyPad : CapsuleConsumer
{
    private Action<string>? _keyPressedAction;

    public KeyPad OnKeyPressed(Action<string> keyPressed)
    {
        _keyPressedAction = keyPressed;
        return this;
    }

    public override VisualNode Render(ICapsuleHandle use)
    {
        var (isDarkTheme, _) = AppTheme.ThemeCapsule(use);

        return new Grid()
        {
            RenderButtonMediumEmphasis(use, "C", 0, 0),
            RenderImageButtonMediumEmphasis(use, isDarkTheme ? "plus_minus_white.png" : "plus_minus.png", "+-", 0, 1),
            RenderButtonMediumEmphasis(use, "%", 0, 2),
            RenderButtonHighEmphasis(use, "÷", 0, 3),

            RenderButtonLowEmphasis(use, "7", 1, 0),
            RenderButtonLowEmphasis(use, "8", 1, 1),
            RenderButtonLowEmphasis(use, "9", 1, 2),
            RenderButtonHighEmphasis(use, "×", 1, 3),

            RenderButtonLowEmphasis(use, "4", 2, 0),
            RenderButtonLowEmphasis(use, "5", 2, 1),
            RenderButtonLowEmphasis(use, "6", 2, 2),
            RenderButtonHighEmphasis(use, "-", 2, 3),

            RenderButtonLowEmphasis(use, "1", 3, 0),
            RenderButtonLowEmphasis(use, "2", 3, 1),
            RenderButtonLowEmphasis(use, "3", 3, 2),
            RenderButtonHighEmphasis(use, "+", 3, 3),

            RenderButtonLowEmphasis(use, ".", 4, 0),
            RenderButtonLowEmphasis(use, "0", 4, 1),
            RenderImageButtonLowEmphasis(use, isDarkTheme ? "back_white.png" : "back.png", "back", 4, 2),
            RenderButtonHighEmphasis(use, "=", 4, 3),

        }
        .Rows("* * * * *")
        .Columns("* * * *")
        .ColumnSpacing(16)
        .RowSpacing(16)
        .Margin(20, 0, 20, 20)
        .OnSizeChanged(Invalidate)
        .HeightRequest(400);
    }

    Button RenderButtonLowEmphasis(ICapsuleHandle use, string text, int row, int column)
        => AppTheme.ButtonLowEmphasis(use, text)
        .GridRow(row)
        .GridColumn(column)
        .OnClicked(() => _keyPressedAction?.Invoke(text));

    Button RenderButtonMediumEmphasis(ICapsuleHandle use, string text, int row, int column)
        => AppTheme.ButtonMediumEmphasis(use, text)
        .GridRow(row)
        .GridColumn(column)
        .OnClicked(() => _keyPressedAction?.Invoke(text));

    Grid RenderImageButtonMediumEmphasis(ICapsuleHandle use, string imageSource, string text, int row, int column)
        => AppTheme.ImageButtonMediumEmphasis(use, imageSource, () => _keyPressedAction?.Invoke(text))        
        .GridRow(row)
        .GridColumn(column);

    Grid RenderImageButtonLowEmphasis(ICapsuleHandle use, string imageSource, string text, int row, int column)
        => AppTheme.ImageButtonLowEmphasis(use, imageSource, () => _keyPressedAction?.Invoke(text))
        .GridRow(row)
        .GridColumn(column);

    Button RenderButtonHighEmphasis(ICapsuleHandle use, string text, int row, int column)
        => AppTheme.ButtonHighEmphasis(use, text)
        .GridRow(row)
        .GridColumn(column)
        .OnClicked(() => _keyPressedAction?.Invoke(text));

}