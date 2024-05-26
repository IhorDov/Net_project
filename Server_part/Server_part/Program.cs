using Server_part;
using System.Net;
using System.Net.Sockets;

Server server = new Server("127.0.0.1", 12000);
server.Start();
server.Stop();
