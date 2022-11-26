using MauiReactor.FigmaPlugin.Resources.Styles;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.FigmaPlugin.Pages.Components;

class ResizableContainerState
{
    public double StartSize { get; set; } = 200;
    public double FixedSize { get; set; } = 200;
}

class ResizableContainer : Component<ResizableContainerState>
{
    private MauiControls.StackOrientation _orientation;

    public ResizableContainer Orientation(MauiControls.StackOrientation orientation)
    {
        _orientation = orientation;
        return this;
    }

    public override VisualNode Render()
    {
        var children = Children();
        var leftElement = children.Count > 0 ? children[0] : null;
        var rightElement = children.Count > 1 ? children[1] : null;
        return new Grid(
            _orientation == StackOrientation.Horizontal ? "*" : $"{State.FixedSize.ToString(CultureInfo.InvariantCulture)},18,*",
            _orientation == StackOrientation.Horizontal ? $"{State.FixedSize.ToString(CultureInfo.InvariantCulture)},18,*" : "*")
        {
            leftElement,

            new Shapes.Rectangle()
                .GridRow(1)
                .GridColumn(1)
                .BackgroundColor(ThemeColors.Gray600)
                .StrokeThickness(0)
                .OnPanUpdated(OnResize),

            new Border
            {
                rightElement
            }
            .GridRow(2)
            .GridColumn(2)
        };
    }

    private void OnResize(object? sender, MauiControls.PanUpdatedEventArgs e)
    {
        //System.Diagnostics.Debug.WriteLine($"{e.StatusType} {e.TotalX} {e.TotalY}");

        switch (e.StatusType)
        {
            case GestureStatus.Started:
            case GestureStatus.Completed:
                {
                    State.StartSize = State.FixedSize;
                }
                break;
            case GestureStatus.Running:
                if (_orientation == StackOrientation.Horizontal)
                {
                    SetState(s =>
                    {
                        s.FixedSize = Math.Clamp(State.StartSize + e.TotalX, 100, 400);
                    });
                }
                else
                {
                    SetState(s =>
                    {
                        s.FixedSize = Math.Clamp(State.StartSize + e.TotalY, 100, 400);
                    });
                }
                // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                //Content.TranslationX = Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(Content.Width - DeviceDisplay.MainDisplayInfo.Width));
                //Content.TranslationY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(Content.Height - DeviceDisplay.MainDisplayInfo.Height));
                break;

                //case GestureStatus.Completed:
                //    // Store the translation applied during the pan
                //    SetState(s =>
                //    {
                //        s.StartDragPositionX = e.TotalX;
                //        s.StartDragPositionY = e.TotalY;
                //    });

                //    //x = Content.TranslationX;
                //    //y = Content.TranslationY;
                //    break;
        }
    }
}
