using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Resources.Styles;
using MauiReactor;
using MauiReactor.Canvas;

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

class MainPage : Component<MainPageState>
{
    protected override void OnMounted()
    {
        if (MauiControls.Application.Current != null)
        {
            MauiControls.Application.Current.RequestedThemeChanged += (sender, args) => Invalidate();
        }
        
        base.OnMounted();
    }

    public override VisualNode Render()
    {
        return new ContentPage
        {
            new Grid("48 * 420", "*")
            {
                new ThemeToggle(),
                
                RenderDisplayPanel(),

                new KeyPad()
                    .OnKeyPressed(OnKeyPressed)
                    .GridRow(2),
            }
        }
        .BackgroundColor(Theme.Background);
    }

    VisualNode RenderDisplayPanel()
    {
        return new VStack(spacing: 0)
        {
            Theme.Label(()=> $"{State.Number1} {State.CurrentOperation} {State.Number2}{(State.Perc ? "%" : string.Empty)}{(State.Result != null ? " =" : string.Empty)}")
                .FontSize(40)
                .TextColor(Theme.Text.WithAlpha(0.4f))
                .HorizontalTextAlignment(TextAlignment.End),

            Theme.Label(()=> State.Result != null ? State.Result.Value.ToString() : State.CurrentNumber.Length > 0 ? State.CurrentNumber : "0")
                .FontSize(63)
                .HorizontalTextAlignment(TextAlignment.End)
        }
        .Margin(20,0)
        .GridRow(1)
        .HFill()
        .VEnd();
    }

    void OnKeyPressed(string key)
    {
        if (State.Result != null)
        {
            if (key == "÷" || key == "×" || key == "+" || key == "-")
            {
                SetState(s =>
                {
                    s.Number1 = s.Result;
                    s.Number2 = s.Result = null;
                    s.CurrentOperation = key;
                    s.CurrentNumber = string.Empty;
                    s.Perc = false;
                }, false);
            }
            else
            {
                SetState(s =>
                {
                    s.Number1 = s.Number2 = s.Result = null;
                    s.CurrentOperation = string.Empty;
                    s.CurrentNumber = string.Empty;
                    s.Perc = false;
                }, false);
            }
        };


        if (key == "back")
        {
            if (State.CurrentNumber.Length > 0)
            {
                SetState(s => s.CurrentNumber = s.CurrentNumber.Substring(0, s.CurrentNumber.Length - 1), false);
            }
        }
        else if (key == ".")
        {
            if (State.CurrentNumber.Length > 0 && !State.CurrentNumber.Contains("."))
            {
                SetState(s => s.CurrentNumber += key, false);
            }
        }
        else if (key == "0")
        {
            if (State.CurrentNumber.Length > 0)
            {
                SetState(s => s.CurrentNumber += key, false);
            }
        }
        else if (key == "C")
        {
            SetState(s => s.CurrentNumber = string.Empty);
        }
        else if (key == "=")
        {
            if (State.CurrentOperation.Length > 0 && State.Number1 != null)
            {
                SetState(s =>
                {
                    s.Number2 = State.CurrentNumber.Length > 0 ? double.Parse(State.CurrentNumber) : 0.0;
                    switch (s.CurrentOperation)
                    {
                        case "÷":
                            s.Result = s.Number1!.Value / s.Number2.Value;
                            break;
                        case "×":
                            s.Result = s.Number1!.Value * s.Number2.Value;
                            break;
                        case "+":
                            s.Result = s.Number1!.Value + s.Number2.Value;
                            break;
                        case "-":
                            s.Result = s.Number1!.Value - s.Number2.Value;
                            break;
                    }
                }, false);
            }
        }
        else if (key == "%")
        {
            if (State.CurrentOperation.Length > 0 && State.Number1 != null)
            {
                SetState(s =>
                {
                    s.Number2 = State.CurrentNumber.Length > 0 ? double.Parse(State.CurrentNumber) : 0.0;
                    s.Perc = true;
                    switch (s.CurrentOperation)
                    {
                        case "÷":
                            s.Result = s.Number1!.Value / (s.Number2.Value / 100.0) * s.Number1!.Value;
                            break;
                        case "×":
                            s.Result = s.Number1!.Value * (s.Number2.Value / 100.0) * s.Number1!.Value;
                            break;
                        case "+":
                            s.Result = s.Number1!.Value + (s.Number2.Value / 100.0) * s.Number1!.Value;
                            break;
                        case "-":
                            s.Result = s.Number1!.Value - (s.Number2.Value / 100.0) * s.Number1!.Value;
                            break;
                    }
                }, false);
            }
        }
        else if (key == "+-")
        {
            if (State.CurrentNumber.Length > 0)
            {
                SetState(s => s.CurrentNumber = s.CurrentNumber.StartsWith("-") ? s.CurrentNumber = s.CurrentNumber.Substring(1) : "-" + s.CurrentNumber, false);
            }
        }
        else if (key == "÷" || key == "×" || key == "+" || key == "-")
        {
            if (State.CurrentOperation.Length == 0 && State.CurrentNumber.Length > 0)
            {
                SetState(s =>
                {
                    s.CurrentOperation = key;
                    s.Number1 = double.Parse(s.CurrentNumber);
                    s.CurrentNumber = string.Empty;
                }, false);
            }
        }
        else
        {
            SetState(s =>
            {
                s.CurrentNumber += key;
            }, false);
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
                            .FillColor(Theme.ButtonMediumEmphasisBackground)
                    }
                    .Height(24)
                    .Width(24)
                    .Margin(4)
                    .HorizontalAlignment(Theme.IsDarkTheme ? Microsoft.Maui.Primitives.LayoutAlignment.Start : Microsoft.Maui.Primitives.LayoutAlignment.End)
                    .VCenter(),
                
                    new Align
                    {
                        Theme.IsDarkTheme ?
                        new Picture("Calculator.Resources.Images.moon.png")
                        :
                        new Picture("Calculator.Resources.Images.sun.png"),
                    }
                    .Height(24)
                    .Width(24)
                    .Margin(8,4)
                    .HorizontalAlignment(Theme.IsDarkTheme ? Microsoft.Maui.Primitives.LayoutAlignment.End : Microsoft.Maui.Primitives.LayoutAlignment.Start)
                    .VCenter()
                }
            }
            .BackgroundColor(Theme.ButtonLowEmphasisBackground)
            .CornerRadius(16)
        }
        .OnTapped(Theme.ToggleCurrentAppTheme)
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
        .Margin(20, 0, 20, 20)
        .OnSizeChanged(Invalidate)
        .HeightRequest(400);
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