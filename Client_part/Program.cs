using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client_part
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";

            string server = "127.0.0.1";
            int port = 12000; // Match the server port

            using (IClient client = new Client(new TcpClientWrapper(new TcpClient()), server, port))
            {
                string message;
                bool running = true;

                while (running)
                {
                    message = Console.ReadLine();
                    if (message == "exit")
                    {
                        running = false;
                        break;
                    }

                    client.SendMessage(message);
                    Console.WriteLine("Sent: " + message);
                }

                client.Close();
            }
        }
    }
}

