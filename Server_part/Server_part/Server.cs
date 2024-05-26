using Client_part;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server_part
{

    public class Server
    {
        private TcpListener _listener;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        public Server(string ipAddress, int port)
        {
            IPAddress localAddr = IPAddress.Parse(ipAddress);
            _listener = new TcpListener(localAddr, port);
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine("Server started... listening on port " + ((IPEndPoint)_listener.LocalEndpoint).Port);

            try
            {
                while (true)
                {
                    if (_cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    TcpClient client = _listener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.Interrupted)
            {
                // Handle the expected exception when stopping the listener
                Console.WriteLine("Listener stopped.");
            }
            finally
            {
                _listener.Stop();
            }
        }

        private void HandleClient(TcpClient client)
        {
            Guid clientId = Guid.NewGuid();
            Console.WriteLine($"Client {clientId} connected.");

            using (StreamWriter writer = new StreamWriter(client.GetStream()))
            using (StreamReader reader = new StreamReader(client.GetStream()))
            {
                writer.Flush();

                try
                {
                    while (client.Connected)
                    {
                        string message = reader.ReadLine();
                        if (message != null)
                        {
                            Console.WriteLine($"Received message from {clientId}: {message}");
                            writer.WriteLine(message + " From server");
                            writer.Flush();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred for client {clientId}: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine($"Client disconnected: {clientId}");
                }
            }
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _listener.Stop();
        }
    }
}

//public class Server
//{
//    private readonly ITcpListener _listener;
//    private ITcpClient _client;
//    private NetworkStream _stream;
//    private Thread _sendingThread;
//    private bool _running;

//    public Server(ITcpListener tcpListener)
//    {
//        _listener = tcpListener;
//        _running = true;
//    }

//    public void Start()
//    {
//        Console.Title = "Server";

//        _sendingThread = new Thread(() =>
//        {
//            while (_running)
//            {
//                if (_stream != null)
//                {
//                    var a = Console.ReadLine();
//                    if (a == "exit")
//                    {
//                        Stop();
//                        break;
//                    }
//                    byte[] msg = Encoding.ASCII.GetBytes(a);
//                    _stream.Write(msg, 0, msg.Length);
//                }
//            }
//        });

//        _sendingThread.Start();

//        try
//        {
//            _listener.Start();

//            byte[] bytes = new byte[256];
//            string data = null;

//            while (_running)
//            {
//                Console.WriteLine("Waiting for a connection...");
//                _client = (ITcpClient)_listener.AcceptTcpClient();
//                Console.WriteLine("Connected!");

//                data = null;

//                _stream = (NetworkStream)_client.GetStream();

//                int i;

//                while ((i = _stream.Read(bytes, 0, bytes.Length)) != 0)
//                {
//                    data = Encoding.ASCII.GetString(bytes, 0, i);
//                    Console.WriteLine($"Received: {data}");
//                }
//            }

//            _client?.Close();
//        }
//        catch (SocketException e)
//        {
//            Console.WriteLine($"SocketException: {e}");
//        }
//    }

//    public void Stop()
//    {
//        _running = false;
//        _client?.Close();
//        _listener?.Stop();
//        _sendingThread?.Join();
//    }
//}


//public class Server
//{
//    private readonly ITcpListener server;
//    //private readonly TcpListener server;
//    private TcpClient client;
//    private NetworkStream stream;
//    private Thread sendingThread;
//    private bool running;

//    public Server(ITcpListener tcpListener)
//    {
//        this.server = tcpListener;
//        running = true;
//    }

//    public void Start()
//    {
//        Console.Title = "Server";

//        sendingThread = new Thread(() =>
//        {
//            while (running)
//            {
//                if (stream != null)
//                {
//                    var a = Console.ReadLine();
//                    if (a == "exit")
//                    {
//                        Stop();
//                        break;
//                    }
//                    byte[] msg = Encoding.ASCII.GetBytes(a);
//                    stream.Write(msg, 0, msg.Length);
//                }
//            }
//        });

//        sendingThread.Start();

//        try
//        {
//            server.Start();

//            byte[] bytes = new byte[256];
//            string data = null;

//            while(running)
//            {
//                Console.WriteLine("Waiting for a connection...");
//                client = server.AcceptTcpClient();
//                Console.WriteLine("Connected!");

//                data = null;

//                stream = client.GetStream();

//                int i;

//                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
//                {
//                    data = Encoding.ASCII.GetString(bytes, 0, i);
//                    Console.WriteLine($"Received: {data}");
//                }
//            }

//             client?.Close();
//        }
//        catch(SocketException e)
//        {
//            Console.WriteLine($"SocketException: {e}");
//        }


//        //while (_running)
//        //{
//        //    try
//        //    {
//        //        TcpClient client = _listener.AcceptTcpClient();
//        //        Thread clientThread = new Thread(() => HandleClient(client));
//        //        clientThread.Start();
//        //    }
//        //    catch (SocketException)
//        //    {
//        //        if (!_running)
//        //        {
//        //            // Server was stopped, so break out of the loop
//        //            break;
//        //        }

//        //        throw; // Rethrow the exception if it was not caused by stopping the server
//        //    }
//        //}
//    }

//    public void Stop()
//    {
//        running = false;
//        client?.Close();
//        server?.Stop();
//        sendingThread?.Join();
//    }

//    //private void HandleClient(TcpClient client)
//    //{
//    //    Guid clientId = Guid.NewGuid();
//    //    Console.WriteLine($"Client {clientId} connected.");

//    //    StreamWriter writer = null;
//    //    StreamReader reader = null;

//    //    try
//    //    {
//    //        writer = new StreamWriter(client.GetStream());
//    //        reader = new StreamReader(client.GetStream());
//    //        writer.Flush();
//    //    }
//    //    catch (Exception ex)
//    //    {
//    //        Console.WriteLine($"Exception occurred while setting up streams for client {clientId}: {ex.Message}");
//    //        client.Dispose();
//    //        return;
//    //    }

//    //    try
//    //    {
//    //        while (client.Connected)
//    //        {
//    //            string message = reader.ReadLine();
//    //            if (message != null)
//    //            {
//    //                Console.WriteLine($"Received message from {clientId}: {message}");
//    //                writer.WriteLine(message + " From server");
//    //                writer.Flush();
//    //            }
//    //        }
//    //    }
//    //    catch (Exception ex)
//    //    {
//    //        Console.WriteLine($"Exception occurred for client {clientId}: {ex.Message}");
//    //    }
//    //    finally
//    //    {
//    //        Console.WriteLine($"Client disconnected: {clientId}");
//    //        client.Dispose();
//    //    }
//    //}
//}

