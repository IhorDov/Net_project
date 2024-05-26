using Server_part;
using System.Net.Sockets;
using System.Net;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using Client_part;

namespace Test_Server
{
    [TestClass]
    public class ServerTests
    {
        private Server _server;

        [TestInitialize]
        public void Setup()
        {
            _server = new Server("127.0.0.1", 12000);
        }

        [TestMethod]
        public void TestServerStartAndStop()
        {
            // Start the server in a separate thread
            Thread serverThread = new Thread(() => _server.Start());
            serverThread.Start();

            // Give some time for the server to start
            Thread.Sleep(1000);

            // Stop the server
            _server.Stop();
            serverThread.Join(); // Ensure the thread has finished

            Assert.IsFalse(serverThread.IsAlive);
        }
    }
}


//using Server_part;
//using System.Net.Sockets;
//using System.Net;
//using Moq;


//namespace Test_Server
//{
//    [TestClass]
//    public class ServerTests
//    {
//        private Server? server;
//        private Mock<ITcpListener>? mockTcpListener;
//        private Mock<TcpClient>? mockTcpClient;
//        private Mock<NetworkStream>? mockStream;

//        [TestInitialize]
//        public void Setup()
//        {
//            mockTcpListener = new Mock<ITcpListener>();
//            mockTcpClient = new Mock<TcpClient>();
//            mockStream = new Mock<NetworkStream>();

//            mockTcpClient.Setup(client => client.GetStream()).Returns(mockStream.Object);
//            mockTcpListener.Setup(listener => listener.AcceptTcpClient()).Returns(mockTcpClient.Object);

//            server = new Server(mockTcpListener.Object);

//            //mockTcpListener = new Mock<ITcpListener>(IPAddress.Parse("127.0.0.1"), 12000);
//            //mockTcpClient = new Mock<TcpClient>();
//            //mockStream = new Mock<NetworkStream>();
//            //server = new Server(mockTcpListener.Object);
//        }

//        [TestMethod]
//        public void TestServerStartAndStop()
//        {
//            mockTcpListener.Setup(listener => listener.Start());
//            mockTcpListener.Setup(listener => listener.Stop());

//            Thread serverThread = new Thread(() => server.Start());
//            serverThread.Start();

//            Thread.Sleep(1000); // Give some time for the server to start

//            server.Stop();
//            serverThread.Join(); // Ensure the thread has finished

//            Assert.IsFalse(serverThread.IsAlive);

//            //Thread serverThread = new Thread(() => server.Start());
//            //serverThread.Start();

//            //Thread.Sleep(1000); // Give some time for the server to start

//            //server.Stop();
//            //serverThread.Join(); // Ensure the thread has finished

//            //Assert.IsFalse(serverThread.IsAlive);
//        }
//    }
//}