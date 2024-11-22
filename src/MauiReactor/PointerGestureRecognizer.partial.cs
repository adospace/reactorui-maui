using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public static partial class PointerGestureRecognizerExtensions
{
    public static T OnPointerEntered<T>(this T pointerGestureRecognizer, Action<Point?>? pointerEnteredAction)
        where T : IPointerGestureRecognizer
    {
        pointerGestureRecognizer.PointerEnteredEvent = new SyncEventCommand<PointerEventArgs>((sender, args) =>
        {
            if (pointerEnteredAction != null && sender is Microsoft.Maui.Controls.Element element)
            {
                pointerEnteredAction?.Invoke(args.GetPosition(element));
            }
        });

        return pointerGestureRecognizer;
    }

    public static T OnPointerExited<T>(this T pointerGestureRecognizer, Action<Point?>? pointerExitedAction)
        where T : IPointerGestureRecognizer
    {
        pointerGestureRecognizer.PointerExitedEvent = new SyncEventCommand<PointerEventArgs>((sender, args) =>
        {
            if (pointerExitedAction != null && sender is Microsoft.Maui.Controls.Element element)
            {
                pointerExitedAction?.Invoke(args.GetPosition(element));
            }
        });

        return pointerGestureRecognizer;
    }

    public static T OnPointerMoved<T>(this T pointerGestureRecognizer, Action<Point?>? pointerMovedAction)
        where T : IPointerGestureRecognizer
    {
        pointerGestureRecognizer.PointerMovedEvent = new SyncEventCommand<PointerEventArgs>((sender, args) =>
        {
            if (pointerMovedAction != null && sender is Microsoft.Maui.Controls.Element element)
            {
                pointerMovedAction?.Invoke(args.GetPosition(element));
            }
        });

        return pointerGestureRecognizer;
    }

    public static T OnPointerPressed<T>(this T pointerGestureRecognizer, Action<Point?>? pointerMovedAction)
        where T : IPointerGestureRecognizer
    {
        pointerGestureRecognizer.PointerPressedEvent = new SyncEventCommand<PointerEventArgs>((sender, args) =>
        {
            if (pointerMovedAction != null && sender is Microsoft.Maui.Controls.Element element)
            {
                pointerMovedAction?.Invoke(args.GetPosition(element));
            }
        });

        return pointerGestureRecognizer;
    }

    public static T OnPointerReleased<T>(this T pointerGestureRecognizer, Action<Point?>? pointerMovedAction)
        where T : IPointerGestureRecognizer
    {
        pointerGestureRecognizer.PointerReleasedEvent = new SyncEventCommand<PointerEventArgs>((sender, args) =>
        {
            if (pointerMovedAction != null && sender is Microsoft.Maui.Controls.Element element)
            {
                pointerMovedAction?.Invoke(args.GetPosition(element));
            }
        });

        return pointerGestureRecognizer;
    }
}
