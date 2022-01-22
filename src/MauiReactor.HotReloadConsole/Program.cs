// See https://aka.ms/new-console-template for more information
using CommandLine;
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
            if (options.Framework == "net6.0-android")
            {
                if (!ExecutePortForwardCommmand())
                {
                    return;
                }
            }

            var hotReloadClient = new HotReloadClient(options);

            Console.WriteLine("Press Ctrl+C or Ctrl+Break to quit");

            var tsc = new CancellationTokenSource();
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) => tsc.Cancel();
            }

            await hotReloadClient.Run(tsc.Token);            
        }

        private static bool ExecutePortForwardCommmand()
        {
            var adbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), 
                "Android", "android-sdk", "platform-tools", "adb.exe");

            var process = new System.Diagnostics.Process();

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = "forward tcp:45820 tcp:45820";
            process.StartInfo.FileName = adbPath;

            try
            {
                process.Start();

                var adb_output = process.StandardOutput.ReadToEnd();

                if (adb_output.Length > 0 && adb_output != "45820" + Environment.NewLine)
                    throw new InvalidOperationException($"Unable to forward tcp port from emulator (executing '{adbPath} forward tcp:45820 tcp:45820' adb tool returned '{adb_output}')");
            }
            catch (Exception ex)
            {
                Console.WriteLine(process.StandardOutput.ReadToEnd());
                Console.WriteLine(process.StandardError.ReadToEnd());
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }
    }
}
