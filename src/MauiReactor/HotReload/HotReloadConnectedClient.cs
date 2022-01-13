using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.HotReload
{
    internal class HotReloadConnectedClient
    {
        private readonly Action<Assembly> _onNewAssemblyReceived;
        private readonly Action<HotReloadConnectedClient> _onConnectionClosed;

        public HotReloadConnectedClient(Action<Assembly> onNewAssemblyReceived, Action<HotReloadConnectedClient> onConnectionClosed)
        {
            this._onNewAssemblyReceived = onNewAssemblyReceived;
            _onConnectionClosed = onConnectionClosed;
        }

        internal async void StartConnectionLoop(Socket socketConnectedToClient, CancellationToken cancellationToken)
        {
            using var socketStream = new NetworkStream(socketConnectedToClient);

            var bufferedStream = new BufferedStream(socketStream);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    int length = await bufferedStream.ReadInt32Async(cancellationToken);
                    if (length == -1)
                        return;
                    
                    var assemblyRaw = await bufferedStream.ReadAsync(length, cancellationToken);

                    length = await bufferedStream.ReadInt32Async(cancellationToken);
                    if (length > 0)
                    {
                        var assemblySymbolStoreRaw = await bufferedStream.ReadAsync(length, cancellationToken);

                        _onNewAssemblyReceived?.Invoke(Assembly.Load(assemblyRaw, assemblySymbolStoreRaw));
                    }
                    else
                    {
                        _onNewAssemblyReceived?.Invoke(Assembly.Load(assemblyRaw));
                    }


                    await socketStream.WriteAsync(new byte[] { 0x1 }, cancellationToken);

                    await socketStream.FlushAsync(cancellationToken);

                    //System.Runtime.Loader.AssemblyLoadContext.Default.
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception)
                {
                    break;
                }
            }

            _onConnectionClosed?.Invoke(this);
        }
    }
}
