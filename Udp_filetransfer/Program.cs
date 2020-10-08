using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Udp_filetransfer
{
    class Program
    {
        static UdpClient server = new UdpClient();
        const string ip = "92.52.138.128";
        const int port = 2020;
        static void Main(string[] args)
        {

            string filePath = "pic.jpg";

            SendFile(filePath);
        }

        private static void SendFile(string filePath)
        {
            SendInfo(filePath);
            var data = File.ReadAllBytes(filePath);
            server.Send(data, data.Length, ip, port);
            Console.WriteLine("Send...");
        }

        private static void SendInfo(string filePath)
        {
            string name = Path.GetFileName(filePath);

            var data = Encoding.UTF8.GetBytes(name);
            server.Send(data, data.Length, ip, port);
        }
    }
}
