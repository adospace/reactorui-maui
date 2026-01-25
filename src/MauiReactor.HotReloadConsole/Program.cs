// See https://aka.ms/new-console-template for more information
using CommandLine;
using Microsoft.Build.Locator;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MauiReactor.HotReloadConsole
{
    class Program
    {

        private static async Task<int> Main(string[] args)
        {
            Console.WriteLine($"MauiReactor Hot-Reload CLI");
            Console.WriteLine($"Version {Assembly.GetExecutingAssembly().GetName().Version}");

            var optionsParsed = Parser.Default.ParseArguments<Options>(args);

            await optionsParsed.WithParsedAsync(RunMonitorAndConnectionClient);

            return 0;
        }

        private static async Task RunMonitorAndConnectionClient(Options options)
        {
            // MSBuildLocator must be registered before using MSBuildWorkspace
            // On macOS/Linux this is required to find MSBuild assemblies
            if (!MSBuildLocator.IsRegistered)
            {
                MSBuildLocator.RegisterDefaults();
            }

            IHotReloadClient hotReloadClient = options.CompilationMode == CompilationMode.Slim ?
                new HotReloadClientEmit(options)
                :
                new HotReloadClientFull(options);

            Console.WriteLine("Press Ctrl+C or Ctrl+Break to quit");

            var tsc = new CancellationTokenSource();
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) => tsc.Cancel();
            }

            await hotReloadClient.Startup(tsc.Token);

            await hotReloadClient.Run(tsc.Token);            
        }

    }
}
