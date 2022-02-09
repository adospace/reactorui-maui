using System.Net.Sockets;

namespace MauiReactor.HotReload
{
    internal class BufferedStream
    {
        private readonly NetworkStream _networkStream;

        public BufferedStream(NetworkStream networkStream)
        {
            _networkStream = networkStream;
        }

        public async Task<int> ReadInt32Async(CancellationToken cancellationToken)
        {
            var buffer = await ReadAsync(4, cancellationToken);
            return BitConverter.ToInt32(buffer);
        }

        public async Task<byte[]> ReadAsync(int count, CancellationToken cancellationToken)
        {
            var buffer = new byte[count];

            int offset = 0;
            while (offset < count)
            {
                offset += await _networkStream.ReadAsync(buffer.AsMemory(offset, count - offset), cancellationToken);
            }

            return buffer;
        }
    }
}
