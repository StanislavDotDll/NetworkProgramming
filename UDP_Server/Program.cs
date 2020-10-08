using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 2020;

            UdpClient server = new UdpClient(port);

            Console.WriteLine("Wait...");
            IPEndPoint remoteEP = null;
            byte [] info = server.Receive(ref remoteEP);
            Console.WriteLine("Received: {0} from {1}", Encoding.UTF8.GetString(info), remoteEP);

            server.Close();

            Console.ReadLine();
        }
    }
}
