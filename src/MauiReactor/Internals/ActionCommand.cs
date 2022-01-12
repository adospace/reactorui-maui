using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MauiReactor.Internals
{
    internal class ActionCommand : ICommand
    {
        private readonly Action _action;

        public ActionCommand(Action action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _action != null;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }
    }
}
