using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Udp_filetransfer_Client
{
    class Program
    {
        const int port = 2020;
           static UdpClient client = new UdpClient(port);
        static void Main(string[] args)
        {

            ReceiveInfo();
        }

        private static void ReceiveInfo()
        {
            IPEndPoint ip = null;
            var data = client.Receive(ref ip);
            string path = Encoding.UTF8.GetString(data);
            Receive(path);
        }

        private static void Receive(string path)
        {
            IPEndPoint ip = null;
            var data= client.Receive(ref ip);

            File.WriteAllBytes(path, data);
            Console.WriteLine("Ok");

        }
    }
}
