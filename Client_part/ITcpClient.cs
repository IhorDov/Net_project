using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_part
{
    public interface ITcpClient : IDisposable
    {
        void Close();
        void Connect(string hostname, int port);
        INetworkStream GetStream();
    }

    public interface INetworkStream : IDisposable
    {
        StreamWriter GetWriter();
        StreamReader GetReader();
    }

    //public interface IClient : IDisposable
    //{
    //    void SendMessage(string message);
    //    void Close();
    //}
}
