using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiReactor.Internals;

internal interface IAsyncCommand : ICommand
{
    Task ExecuteAsync(object? parameter);
}

internal class AsyncCommand : IAsyncCommand
{
    public event EventHandler? CanExecuteChanged;

    private bool _isExecuting;
    private readonly Func<Task> _execute;
    private readonly Func<bool>? _canExecute;

    public AsyncCommand(Func<Task> execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return !_isExecuting && (_canExecute?.Invoke() ?? true);
    }

    public async Task ExecuteAsync(object? parameter)
    {
        if (CanExecute(parameter))
        {
            try
            {
                _isExecuting = true;
                await _execute();
            }
            finally
            {
                _isExecuting = false;
            }
        }

        RaiseCanExecuteChanged();
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    //#region Explicit implementations
    bool ICommand.CanExecute(object? parameter)
    {
        return CanExecute(parameter);
    }

    void ICommand.Execute(object? parameter)
    {
        FireAndForgetSafeAsync(ExecuteAsync(parameter));
    }
    //#endregion

    private static async void FireAndForgetSafeAsync(Task task)
    {
        try
        {
            await task;
        }
        catch (Exception ex)
        {
            var logger = ServiceCollectionProvider.ServiceProvider?.GetService<ILogger>();
            logger?.LogError(ex, "Error in executing async command");
        }
    }
}