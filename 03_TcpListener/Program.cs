using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _03_TcpListener
{
    class Program
    {
        static void Main(string[] args)
        {
            // TcpClient   - active
            // TcpListener - passive
            const int port = 2020;
            var ip = IPAddress.Parse("127.0.0.1");
            const int size = 512;
            TcpListener server = new TcpListener(IPAddress.Any, port);
            try
            {
                server.Start();

                while (true)
                {
                    Console.WriteLine("Wait for connection...");
                    try
                    {
                        TcpClient client = server.AcceptTcpClient();
                        Console.WriteLine("Connected: {0}", client.Client.RemoteEndPoint);
                        NetworkStream stream = client.GetStream();

                        byte[] buffer = new byte[size];

                        int count = 0;
                        do
                        {
                            count += stream.Read(buffer, 0, buffer.Length);
                        } while (stream.DataAvailable);

                        string data = Encoding.UTF8.GetString(buffer, 0, count);
                        Console.WriteLine("Got: {0} bytes, data:{1}", count, data);
                        data = String.Format("Responce: got {0} bytes", count);
                        stream.Write(Encoding.UTF8.GetBytes(data), 0, data.Length);
                       // stream.Close();
                        client.Close(); // Clean resources
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine(ex.Message);
                        // throw;
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
