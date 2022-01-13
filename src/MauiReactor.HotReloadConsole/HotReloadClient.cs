using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.HotReloadConsole
{
    internal class HotReloadClient
    {
        private const int _serverPort = 45820;

        private readonly string _assemblyFilePath;

        public HotReloadClient(string assemblyFilePath)
        {
            _assemblyFilePath = assemblyFilePath;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            await ConnectionLoop(cancellationToken);        
        }

        private async Task ConnectionLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var tcpClient = new TcpClient();

                try
                {
                    Console.Write($"Connecting to HotReloadServer (Port: {_serverPort})...");
                    await tcpClient.ConnectAsync(IPAddress.Loopback, _serverPort, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception)
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await Task.Delay(10000, cancellationToken);
                    }

                    continue;
                }

                Console.WriteLine($"connected");

                try
                {
                    using var networkStream = tcpClient.GetStream();

                    //await SendAssemblyFileToServer(networkStream, cancellationToken);

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        await WaitFileChanged(cancellationToken);

                        await Task.Delay(1000, cancellationToken);

                        await SendAssemblyFileToServer(networkStream, cancellationToken);
                    }
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
            FileSystemWatcher watcher = new(Path.GetDirectoryName(_assemblyFilePath) ?? throw new InvalidOperationException());

            var tcs = new TaskCompletionSource<bool>();

            void changedHandler(object s, FileSystemEventArgs e)
            {
                if (e.Name == Path.GetFileName(_assemblyFilePath))
                {
                    tcs?.TrySetResult(true);
                    watcher.Created -= changedHandler;
                    watcher.Dispose();
                }
            }

            watcher.Changed += changedHandler;

            watcher.EnableRaisingEvents = true;

            cancellationToken.Register(() => tcs.SetCanceled());

            Console.WriteLine($"Monitoring assembly file '{_assemblyFilePath}'...");

            return tcs.Task;
        }

        private async Task SendAssemblyFileToServer(NetworkStream networkStream, CancellationToken cancellationToken)
        {
            var assemblyRaw = await ReadAllFileAsync(_assemblyFilePath);

            var lengthBytes = BitConverter.GetBytes(assemblyRaw.Length);

            await networkStream.WriteAsync(lengthBytes, cancellationToken);

            await networkStream.WriteAsync(assemblyRaw, cancellationToken);

            await networkStream.FlushAsync(cancellationToken);

            var assemblySymbolStorePath = Path.Combine(
                Path.GetDirectoryName(_assemblyFilePath) ?? throw new InvalidOperationException(), 
                Path.GetFileNameWithoutExtension(_assemblyFilePath) + ".pdb");

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

            Console.WriteLine($"Sent new assembly ({assemblyRaw.Length} bytes) to emulator");
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
