using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial interface IInputView
{
    EventCommand<string>? AfterTextChangedEvent { get; set; }
}

public partial class InputView<T>
{
    EventCommand<string>? IInputView.AfterTextChangedEvent { get; set; }

    private EventCommand<string>? _executingAfterTextChangedEvent;

    partial void OnAttachingNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsIInputView = (IInputView)this;

        if (thisAsIInputView.AfterTextChangedEvent != null)
        {
            NativeControl.Unfocused += NativeControl_Unfocused;
        }
    }

    private void NativeControl_Unfocused(object? sender, FocusEventArgs e)
    {
        if (NativeControl != null)
        {
            var thisAsIInputView = (IInputView)this;
            if (_executingAfterTextChangedEvent == null || _executingAfterTextChangedEvent.IsCompleted)
            {
                _executingAfterTextChangedEvent = thisAsIInputView.AfterTextChangedEvent;
                _executingAfterTextChangedEvent?.Execute(sender, NativeControl.Text);
            }
        }
    }

    partial void OnDetachingNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.Unfocused -= NativeControl_Unfocused;
        }
    }

    partial void Migrated(VisualNode newNode)
    {
        if (newNode is InputView<T> @inputview)
        {
            if (_executingAfterTextChangedEvent != null && !_executingAfterTextChangedEvent.IsCompleted)
            {
                @inputview._executingAfterTextChangedEvent = _executingAfterTextChangedEvent;
            }
        }
    }
}

public partial class InputViewExtensions
{

    public static T OnTextChanged<T>(this T inputView, Action<string>? textChangedActionWithText) where T : IInputView
    {
        if (textChangedActionWithText != null)
        {
            inputView.TextChangedEvent = new SyncEventCommand<TextChangedEventArgs>((sender, args) => textChangedActionWithText?.Invoke(args.NewTextValue));
        }
        return inputView;
    }
    public static T OnTextChanged<T>(this T inputView, Func<string, Task>? textChangedActionWithText) where T : IInputView
    {
        if (textChangedActionWithText != null)
        {
            inputView.TextChangedEvent = new AsyncEventCommand<TextChangedEventArgs>((sender, args) => textChangedActionWithText.Invoke(args.NewTextValue));
        }
        return inputView;
    }

    public static T OnAfterTextChanged<T>(this T inputView, Action<string>? textChangedAction)
        where T : IInputView
    {
        inputView.AfterTextChangedEvent = new SyncEventCommand<string>(executeWithArgs: textChangedAction);
        return inputView;
    }

    public static T OnAfterTextChanged<T>(this T inputView, Func<string, Task>? textChangedAction, bool runInBackground = false)
        where T : IInputView
    {
        inputView.AfterTextChangedEvent = new AsyncEventCommand<string>(executeWithArgs: textChangedAction, runInBackground);
        return inputView;
    }


}
