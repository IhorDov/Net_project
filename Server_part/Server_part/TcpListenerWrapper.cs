using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server_part
{
    public class TcpListenerWrapper :ITcpListener
    {
        private readonly TcpListener _tcpListener;

        public TcpListenerWrapper(IPAddress localAddr, int port)
        {
            _tcpListener = new TcpListener(localAddr, port);
        }

        public void Start()
        {
            _tcpListener.Start();
        }

        public void Stop()
        {
            _tcpListener.Stop();
        }

        public TcpClient AcceptTcpClient()
        {
            return _tcpListener.AcceptTcpClient();
        }
    }
}
