using System;
using System.Net.Sockets;
using System.Text;

namespace _04_TcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 2020;
            TcpClient client = new TcpClient();
            NetworkStream stream = null;
            const int size = 512;
            try
            {
                bool flag = true;
                //    client.Connect("127.0.0.1", port);
                while (flag)
                {
                    client = new TcpClient("92.52.138.128", port);
                    Console.WriteLine("Connected to server {0}", client.Client.RemoteEndPoint);
                    stream = client.GetStream();
                    Console.WriteLine("Enter message to send:");
                    string msg = Console.ReadLine();

                    stream.Write(Encoding.UTF8.GetBytes(msg), 0, msg.Length);
                    if (msg.Equals("Bye"))
                        flag = false;

                    byte[] responce = new byte[size];
                    int count = 0;
                    do
                    {
                         count = stream.Read(responce, 0, responce.Length);
                    } while (stream.DataAvailable);
                    Console.WriteLine("Received: {0}", Encoding.UTF8.GetString(responce, 0, count));
                }
                stream.Close();
                client.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
          
        }
    }
}
