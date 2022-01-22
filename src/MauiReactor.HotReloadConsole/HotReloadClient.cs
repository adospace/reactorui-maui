using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.HotReloadConsole
{
    internal class HotReloadClient
    {
        private int _serverPort;

        private readonly Options _options;
        private readonly string _workingDirectory;
        private readonly string _projFileName;

        public HotReloadClient(Options options)
        {
            _options = options;
            _workingDirectory = _options.WorkingDirectory ?? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException();
            _serverPort = (options.Framework == "net6.0-android") ? 45820 : 45821;

            if (_options.ProjectFileName == null)
            {
                var projFiles = Directory.GetFiles(_workingDirectory, "*.csproj");
                if (projFiles.Length == 1)
                {
                    _projFileName = projFiles[0];
                }
                else
                {
                    throw new InvalidOperationException($"Directory {_workingDirectory} contains {projFiles.Length} projects");
                }
            }
            else
            {
                if (Path.IsPathFullyQualified(_options.ProjectFileName))
                {
                    _projFileName = _options.ProjectFileName;
                }
                else
                {
                    _projFileName = Path.Combine(_workingDirectory, _options.ProjectFileName);
                }
            }

            if (Path.GetExtension(_projFileName) == ".csproj")
            {
                _projFileName = Path.GetFileNameWithoutExtension(_projFileName);
            }
            else
            {
                _projFileName = Path.GetFileName(_projFileName);
            }
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            await ConnectionLoop(cancellationToken);        
        }

        private async Task ConnectionLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await WaitFileChanged(cancellationToken);

                    if (!await RunBuild(cancellationToken))
                    {
                        continue;
                    }

                    await SendAssemblyFileToServer(cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to hot reload the application:{Environment.NewLine}{ex}");
                    continue;
                }
            }
        }

        private Task WaitFileChanged(CancellationToken cancellationToken)
        {
            FileSystemWatcher watcher = new(_workingDirectory + Path.DirectorySeparatorChar, "*.cs")
            { 
                IncludeSubdirectories = true,
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.LastAccess | NotifyFilters.FileName,
            };

            var tcs = new TaskCompletionSource<bool>();

            void changedHandler(object s, FileSystemEventArgs e)
            {
                Console.WriteLine($"Detected changes to '{e.FullPath}'");
                tcs?.TrySetResult(true);
                watcher.Created -= changedHandler;
                watcher.Dispose();
            }

            watcher.Changed += changedHandler;
            watcher.Created += changedHandler;
            watcher.Renamed += changedHandler;
            watcher.Deleted += changedHandler;

            cancellationToken.Register(() => tcs.SetCanceled());

            Console.WriteLine($"Monitoring folder '{_workingDirectory}'...");

            return tcs.Task;
        }

        private async Task<bool> RunBuild(CancellationToken cancellationToken)
        {
            //dotnet build -f net6.0-android --no-restore
            var process = new System.Diagnostics.Process();

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = _workingDirectory ?? ".";
            process.StartInfo.Arguments = $"build -f {_options.Framework} --no-restore --no-dependencies";
            process.StartInfo.FileName = "dotnet";

            try
            {
                process.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
                process.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);
                process.Start();

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await process.WaitForExitAsync(cancellationToken);
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

        private async Task SendAssemblyFileToServer(CancellationToken cancellationToken)
        {
            using var tcpClient = new TcpClient();

            Console.Write($"Connecting to Hot-Reload server (Port: {_serverPort})...");
            await tcpClient.ConnectAsync(IPAddress.Loopback, _serverPort, cancellationToken);

            Console.WriteLine($"connected");

            using NetworkStream networkStream = tcpClient.GetStream();

            var assemblyFilePath = Path.Combine(_workingDirectory, "bin", "debug", _options.Framework, _projFileName + ".dll");

            var assemblyRaw = await ReadAllFileAsync(assemblyFilePath);

            var lengthBytes = BitConverter.GetBytes(assemblyRaw.Length);

            await networkStream.WriteAsync(lengthBytes, cancellationToken);

            await networkStream.WriteAsync(assemblyRaw, cancellationToken);

            await networkStream.FlushAsync(cancellationToken);

            var assemblySymbolStorePath = Path.Combine(
                Path.GetDirectoryName(assemblyFilePath) ?? throw new InvalidOperationException(), 
                Path.GetFileNameWithoutExtension(assemblyFilePath) + ".pdb");

            if (File.Exists(assemblySymbolStorePath))
            {
                var assemblySynmbolStoreRaw = await ReadAllFileAsync(assemblySymbolStorePath);

                lengthBytes = BitConverter.GetBytes(assemblySynmbolStoreRaw.Length);

                await networkStream.WriteAsync(lengthBytes, cancellationToken);

                await networkStream.WriteAsync(assemblySynmbolStoreRaw, cancellationToken);

                await networkStream.FlushAsync(cancellationToken);
            }
            else
            {
                lengthBytes = BitConverter.GetBytes(0);

                await networkStream.WriteAsync(lengthBytes, cancellationToken);

                await networkStream.FlushAsync(cancellationToken);
            }

            var booleanBuffer = new byte[1];
            if (await networkStream.ReadAsync(booleanBuffer.AsMemory(0, 1), cancellationToken) == 0)
                throw new SocketException();

            Console.WriteLine($"Hot-Reload completed");
        }

        private static async Task<byte[]> ReadAllFileAsync(string filename)
        {
            using var file = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
            var buff = new byte[file.Length];
            await file.ReadAsync(buff.AsMemory(0));
            return buff;
        }
    }
}
