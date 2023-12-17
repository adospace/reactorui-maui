using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial interface IInputView
{
    Action<string>? AfterTextChangedAction { get; set; }
}

public partial class InputView<T>
{
    Action<string>? IInputView.AfterTextChangedAction { get; set; }

    partial void OnReset()
    {
        var thisAsIInputView = (IInputView)this;
        thisAsIInputView.AfterTextChangedAction = null;
    }

    partial void OnAttachingNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsIInputView = (IInputView)this;

        if (thisAsIInputView.AfterTextChangedAction != null)
        {
            NativeControl.Unfocused += NativeControl_Unfocused;
        }
    }

    private void NativeControl_Unfocused(object? sender, FocusEventArgs e)
    {
        if (NativeControl != null)
        {
            var thisAsIInputView = (IInputView)this;
            thisAsIInputView.AfterTextChangedAction?.Invoke(NativeControl.Text);
        }
    }

    partial void OnDetachingNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.Unfocused -= NativeControl_Unfocused;
        }

    }
}

public partial class InputViewExtensions
{

    public static T OnTextChanged<T>(this T inputView, Action<string>? textChangedActionWithText) where T : IInputView
    {
        inputView.TextChangedActionWithArgs = textChangedActionWithText == null ? null : new Action<object?, TextChangedEventArgs>((sender, args) => textChangedActionWithText?.Invoke(args.NewTextValue));
        return inputView;
    }

    public static T OnAfterTextChanged<T>(this T inputView, Action<string>? action) where T : IInputView
    {
        inputView.AfterTextChangedAction = action;
        return inputView;
    }

}
