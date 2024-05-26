using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_part
{
    public class TcpClientWrapper : ITcpClient
    {
        private readonly TcpClient _tcpClient;

        public TcpClientWrapper(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        public void Connect(string hostname, int port)
        {
            _tcpClient.Connect(hostname, port);
        }

        public INetworkStream GetStream()
        {
            return new NetworkStreamWrapper(_tcpClient.GetStream());
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }

    public class NetworkStreamWrapper : INetworkStream
    {
        private readonly NetworkStream _networkStream;

        public NetworkStreamWrapper(NetworkStream networkStream)
        {
            _networkStream = networkStream;
        }

        public StreamWriter GetWriter()
        {
            return new StreamWriter(_networkStream);
        }

        public StreamReader GetReader()
        {
            return new StreamReader(_networkStream);
        }

        public void Dispose()
        {
            _networkStream.Dispose();
        }
    }
}