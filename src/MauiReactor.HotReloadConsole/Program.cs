// See https://aka.ms/new-console-template for more information
using CommandLine;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace MauiReactor.HotReloadConsole
{
    class Program
    {
        public class Options
        {
            [Option('a', "assembly", Required = true, HelpText = "Assembly file name to monitor and send to xamarin-reactorui hot reload server.")]
            public string AssemblyPath { get; set; } = null!;

            [Option('p', "port", Required = false, HelpText = "xamarin-reactorui hot reload server port mapped to emulator port.")]
            public int Port { get; set; } = 45820;

            [Option('m', "monitor", Required = false, HelpText = "Monitor assembly.")]
            public bool Monitor { get; set; } = true;
        }

        private static async Task<int> Main(string[] args)
        {
            //C:\Program Files (x86)\Android\android-sdk>adb forward tcp:45820 tcp:45821
            if (!ExecutePortForwardCommmand())
            {
                return -1;
            }

            var optionsParsed = Parser.Default.ParseArguments<Options>(args);

            await optionsParsed.WithParsedAsync(RunMonitorAndConnectionClient);

            return 0;
        }

        private static async Task RunMonitorAndConnectionClient(Options options)
        {
            var hotReloadClient = new HotReloadClient(options.AssemblyPath);
            
            Console.WriteLine("Press Ctrl+C or Ctrl+Break to quit");

            var tsc = new CancellationTokenSource();
            Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs e) => tsc.Cancel();

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
