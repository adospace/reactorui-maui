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
            if (pointerEnteredAction != null && sender is Element element)
            {
                pointerEnteredAction.Invoke(args.GetPosition(element));
            }
        });

        return pointerGestureRecognizer;
    }

    public static T OnPointerEntered<T>(this T pointerGestureRecognizer, Func<Point?, Task>? pointerEnteredAction)
    where T : IPointerGestureRecognizer
    {
        pointerGestureRecognizer.PointerEnteredEvent = new AsyncEventCommand<PointerEventArgs>(async (sender, args) =>
        {
            if (pointerEnteredAction != null && sender is Element element)
            {
                await pointerEnteredAction.Invoke(args.GetPosition(element));
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
                pointerExitedAction.Invoke(args.GetPosition(element));
            }
        });

        return pointerGestureRecognizer;
    }

    public static T OnPointerExited<T>(this T pointerGestureRecognizer, Func<Point?, Task>? pointerExitedAction)
        where T : IPointerGestureRecognizer
    {
        pointerGestureRecognizer.PointerExitedEvent = new AsyncEventCommand<PointerEventArgs>(async (sender, args) =>
        {
            if (pointerExitedAction != null && sender is Microsoft.Maui.Controls.Element element)
            {
                await pointerExitedAction.Invoke(args.GetPosition(element));
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

    public static T OnPointerMoved<T>(this T pointerGestureRecognizer, Func<Point?, Task>? pointerMovedAction)
        where T : IPointerGestureRecognizer
    {
        pointerGestureRecognizer.PointerMovedEvent = new AsyncEventCommand<PointerEventArgs>(async (sender, args) =>
        {
            if (pointerMovedAction != null && sender is Microsoft.Maui.Controls.Element element)
            {
                await pointerMovedAction.Invoke(args.GetPosition(element));
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
                pointerMovedAction.Invoke(args.GetPosition(element));
            }
        });

        return pointerGestureRecognizer;
    }
    public static T OnPointerPressed<T>(this T pointerGestureRecognizer, Func<Point?, Task>? pointerMovedAction)
        where T : IPointerGestureRecognizer
    {
        pointerGestureRecognizer.PointerPressedEvent = new AsyncEventCommand<PointerEventArgs>(async (sender, args) =>
        {
            if (pointerMovedAction != null && sender is Microsoft.Maui.Controls.Element element)
            {
                await pointerMovedAction.Invoke(args.GetPosition(element));
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
                pointerMovedAction.Invoke(args.GetPosition(element));
            }
        });

        return pointerGestureRecognizer;
    }

    public static T OnPointerReleased<T>(this T pointerGestureRecognizer, Func<Point?, Task>? pointerReleasedAction)
        where T : IPointerGestureRecognizer
    {
        pointerGestureRecognizer.PointerReleasedEvent = new AsyncEventCommand<PointerEventArgs>(async (sender, args) =>
        {
            if (pointerReleasedAction != null && sender is Microsoft.Maui.Controls.Element element)
            {
                await pointerReleasedAction.Invoke(args.GetPosition(element));
            }
        });

        return pointerGestureRecognizer;
    }
}
