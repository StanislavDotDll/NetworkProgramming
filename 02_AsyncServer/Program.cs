using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace _02_AsyncServer
{
    struct TransferData
    {
        public Socket Socket { get; set; }
        public byte[] Buffer { get; set; }
        public static readonly int size = 1024;

    }
    class Program
    {
        static AutoResetEvent done = new AutoResetEvent(false);
        const int port = 2020;
        const int backlog = 100;
        static void Main(string[] args)
        {
            Console.Title = "Server";
            StartServer();
        }

        private static void StartServer()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHost.AddressList[0];

            Socket server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Bind(new IPEndPoint(ipAddress, port));
                server.Listen(backlog);

                while (true)
                {
                    Console.WriteLine("Wait for connection...");
                    server.BeginAccept(AcceptCallback, server);
                    done.WaitOne();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.Close();
            }
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            done.Set();
            Socket server = (Socket)ar.AsyncState;
            Socket client = server.EndAccept(ar);


            TransferData data = new TransferData
            {
                Socket = client,
                Buffer = new byte[TransferData.size]
            };
            client.BeginReceive(data.Buffer, 0, TransferData.size, SocketFlags.None, ReceiveCallback, data);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            var data = (TransferData)ar.AsyncState;
            var client = (Socket)data.Socket;
            int countBytes = client.EndReceive(ar);

            string result = Encoding.UTF8.GetString(data.Buffer, 0, countBytes);

            Console.WriteLine("Got {0} from {1}", result, client.RemoteEndPoint);
            Send(client);
        }

        private static void Send(Socket client)
        {
            byte[] responce = Encoding.UTF8.GetBytes("Server got " + DateTime.Now.ToLongTimeString());
            client.BeginSend(responce, 0, responce.Length, SocketFlags.None, SendCallback, client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                var client = (Socket)ar.AsyncState;
                int countBytes = client.EndSend(ar);

                Console.WriteLine("Sent {0} bytes to client", countBytes);
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
