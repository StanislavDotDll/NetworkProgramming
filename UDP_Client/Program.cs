using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 2020;
            UdpClient client = new UdpClient();

            byte[] info = Encoding.UTF8.GetBytes(Console.ReadLine());
            client.Connect("192.168.0.103", port);
            int count = client.Send(info, info.Length);
            Console.WriteLine("{0} bytes sent", count);

            client.Close();
        }
    }
}
