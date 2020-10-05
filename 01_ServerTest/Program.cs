using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _01_ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1) new Socket
            // 2) Bind(EP)  Ep = IP:port
            // 3) Listen(queue)
            // 4) Accept 
            // 5) work with client
            // 6) send responce to client
            // 7) shutdown, close

            // 1) Створили сокет
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 2) 
            
            
            const int PORT = 2020;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
            try
            {
            server.Bind(ep);
                // 3
                const int backlog = 10;
                const int size = 5;
                server.Listen(backlog); // переходимо в режим прослуховування
                Console.Title = "Server: " + ep;
                while (true)
                {
                    // 4
                    Socket client = server.Accept(); // Wait for client

                    //5
                    byte[] buffer = new byte[size];
                    int count = 0;
                    string data = "";
                    do
                    {
                       int tempCount = client.Receive(buffer);
                        data += Encoding.UTF8.GetString(buffer, 0, tempCount);
                        count += tempCount;
                    }
                    while (client.Available > 0);
                    // Перетворюємо масив байтів у рядок
                     
                    Console.WriteLine("Got: {0}, count bytes = {1}", data, count);

                    //6
                    string responce = String.Format("Got {0}", DateTime.Now.ToShortTimeString());
                    client.Send(Encoding.UTF8.GetBytes(responce));

                    //7
                    client.Shutdown(SocketShutdown.Both);
                    client.Close(); // звільняє ресурси
                }
            }
            catch(SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.Close();
            }
        }
    }
}
