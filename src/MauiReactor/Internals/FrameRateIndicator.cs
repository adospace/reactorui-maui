using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals;

public sealed class FrameRateIndicator : Label
{
    private static Stopwatch? _stopwatch;
    private static readonly Stack<FrameRateIndicator> _instances = new();
    private static bool _started;
    private static double _lastfps;
    private static double? _minfps = null;

    public FrameRateIndicator()
    {
    }

    protected override void OnMount()
    {
        //if (_instances.Peek() != this)
        {
            _instances.Push(this);
        }
        base.OnMount();
    }

    protected override void OnMigrated(VisualNode newNode)
    {
        //if (_instances.Peek() == this)
        {
            _instances.Pop();
            _instances.Push((FrameRateIndicator)newNode);
        }
        base.OnMigrated(newNode);
    }

    protected override void OnUnmount()
    {
        //if (_instances.Peek() == this)
        {
            _instances.Pop();
        }
        base.OnUnmount();
    }

    public static void Start()
    {
        _started = true;
        Task.Run(UpdateFrameRate);
    }

    public static void Stop()
    {
        _started = false;
    }

    internal static async Task UpdateFrameRate()
    {
        bool startingUp = true;
        while (_started)
        {
            if (Application.Current == null)
            {
                return;
            }

            if (startingUp)
            {
                await Task.Delay(1000);
            }

            _stopwatch ??= new();
            _stopwatch.Restart();

            await Application.Current.Dispatcher.DispatchAsync(() =>
            {
                var elapsedMs = _stopwatch.ElapsedMilliseconds;
                double fps = Math.Round(elapsedMs > 0 ? 1000.0 / elapsedMs : 60.0);

                if (_lastfps != fps)
                {
                    _lastfps = fps;
                    _minfps = _minfps == null ? fps : Math.Min(fps, _minfps.Value);
                    if (_instances.TryPeek(out var instance) &&
                        instance.NativeControl != null)
                    {
                        instance.NativeControl.Text = $"FPS: {Math.Clamp(fps, 0, 60)} MIN: {_minfps}";
                    }
                }
            });
        }
    }
}
