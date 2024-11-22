using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals;

public abstract class EventCommand<TArgs>
{
    public abstract void Execute(object? sender, TArgs args);

    public abstract bool IsCompleted { get; }
}

public class SyncEventCommand<TArgs> : EventCommand<TArgs>
{
    private readonly Action? _execute;
    private readonly Action<TArgs>? _executeWithArgs;
    private readonly Action<object?, TArgs>? _executeWithFullArgs;

    public SyncEventCommand(
        Action? execute
    )
    {
        _execute = execute;
    }

    public SyncEventCommand(
        Action<TArgs>? executeWithArgs
    )
    {
        _executeWithArgs = executeWithArgs;
    }

    public SyncEventCommand(
        Action<object?, TArgs>? executeWithFullArgs
    )
    {
        _executeWithFullArgs = executeWithFullArgs;
    }

    public override bool IsCompleted { get; } = true;

    public override void Execute(object? sender, TArgs args)
    {
        _execute?.Invoke();
        _executeWithArgs?.Invoke(args);
        _executeWithFullArgs?.Invoke(sender, args);
    }
}

public class AsyncEventCommand<TArgs> : EventCommand<TArgs>
{
    private readonly Func<Task>? _execute;
    private readonly Func<TArgs, Task>? _executeWithArgs;
    private readonly Func<object?, TArgs, Task>? _executeWithFullArgs;
    private Task? _excutingTask;

    public AsyncEventCommand(
        Func<Task>? execute)
    {
        _execute = execute;
    }

    public AsyncEventCommand(
        Func<TArgs, Task>? executeWithArgs)
    {
        _executeWithArgs = executeWithArgs;
    }

    public AsyncEventCommand(
        Func<object?, TArgs, Task>? executeWithFullArgs)
    {
        _executeWithFullArgs = executeWithFullArgs;
    }

    public override bool IsCompleted => _excutingTask?.IsCompleted ?? true;

    public override void Execute(object? sender, TArgs args)
    {
        if (_execute is not null)
        {
            AwaitAndThrowIfFailed(_excutingTask = _execute());
        }
        if (_executeWithArgs is not null)
        {
            AwaitAndThrowIfFailed(_excutingTask = _executeWithArgs(args));
        }
        if (_executeWithFullArgs is not null)
        {
            AwaitAndThrowIfFailed(_excutingTask = _executeWithFullArgs(sender, args));
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Awaits an input <see cref="Task"/> and throws an exception on the calling context, if the task fails.
    /// </summary>
    /// <param name="executionTask">The input <see cref="Task"/> instance to await.</param>
    internal static async void AwaitAndThrowIfFailed(Task executionTask)
    {
        // Note: this method is purposefully an async void method awaiting the input task. This is done so that
        // if an async relay command is invoked synchronously (ie. when Execute is called, eg. from a binding),
        // exceptions in the wrapped delegate will not be ignored or just become visible through the ExecutionTask
        // property, but will be rethrown in the original synchronization context by default. This makes the behavior
        // more consistent with how normal commands work (where exceptions are also just normally propagated to the
        // caller context), and avoids getting an app into an inconsistent state in case a method faults without
        // other components being notified. It is also possible to not await this task and to instead ignore exceptions
        // and then inspect them manually from the ExecutionTask property, by constructing an async command instance
        // using the AsyncRelayCommandOptions.FlowExceptionsToTaskScheduler option. That will cause this call to
        // be skipped, and exceptions will just either normally be available through that property, or will otherwise
        // flow to the static TaskScheduler.UnobservedTaskException event if otherwise unobserved (eg. for logging).
        await executionTask;
    }

}
