using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class InputViewExtensions
    {
        public static T OnTextChanged<T>(this T inputView, Action<string>? textChangedActionWithText) where T : IInputView
        {
            inputView.TextChangedActionWithArgs = textChangedActionWithText == null ? null : new Action<object?, TextChangedEventArgs>((sender, args) => textChangedActionWithText?.Invoke(args.NewTextValue));
            return inputView;
        }

    }
}
