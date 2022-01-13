using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.HotReload
{
    internal class HotReloadServer
    {
        private CancellationTokenSource? _cancellationTokenSource;
        private const int _listenPort = 45820;
        private readonly List<HotReloadConnectedClient> _connectedClients = new();
        private readonly Action<Assembly> _newAssemblyReceived;

        public HotReloadServer(Action<Assembly> newAssemblyReceived)
        {
            _newAssemblyReceived = newAssemblyReceived;
        }

        public async void Run()
        {
            Stop();

            _cancellationTokenSource = new CancellationTokenSource();
            
            await ConnectionLoop(_cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        private async Task ConnectionLoop(CancellationToken cancellationToken)
        {
            TcpListener? tcpListener = null;

            while (tcpListener == null && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    tcpListener = new TcpListener(IPAddress.Any, _listenPort);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[MauiReactor] Unable to bind hot-reload server to local address {_listenPort}, waiting 60000ms before try again:{Environment.NewLine}{ex}");

                    await Task.Delay(60000, cancellationToken);
                }
            }

            if (tcpListener == null)
            {
                return;
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine($"[MauiReactor] Hot-Reload server started listening on {_listenPort}");
                    tcpListener.Start();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[MauiReactor] Hot-Reload server is unable to list on port {_listenPort}:{Environment.NewLine}{ex}");
                    await Task.Delay(60000, cancellationToken);
                    continue;
                }

                while (!cancellationToken.IsCancellationRequested)
                {
                    Socket socketConnectedToClient;
                    try
                    {
                        socketConnectedToClient = await tcpListener.AcceptSocketAsync(cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }

                    System.Diagnostics.Debug.WriteLine($"[MauiReactor] Hot-Reload connection accepted from {socketConnectedToClient.RemoteEndPoint}, begin connection loop");

                    //socketConnectedToClient.ReceiveTimeout = _options.SocketReceiveTimeout;
                    //socketConnectedToClient.SendTimeout = _options.SocketSendTimeout;

                    var newClient = new HotReloadConnectedClient(_newAssemblyReceived, client =>
                    {
                        _connectedClients.Remove(client);
                    });

                    newClient.StartConnectionLoop(socketConnectedToClient, cancellationToken);
                }
            }

            tcpListener.Stop();
        }
    
    
    }
}
