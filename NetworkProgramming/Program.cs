using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net; // IpAddress, IpEndPoint
using System.Net.Sockets; // Sockets

namespace NetworkProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            // TCP/IP - 1983
            // OSI

            // Client-server app ?
            // client - 
            // server - america

            // Devices? PC, router, modem, smartphone, printers - network nodes
            //  OSI

            string hostName = Dns.GetHostName();
            Console.WriteLine("Name: {0}", hostName);

            PrintHostInfo("31.13.81.36"); // localhost
        }

        private static void PrintHostInfo(string hostName)
        {
            IPHostEntry entry = Dns.GetHostEntry(IPAddress.Parse(hostName));
            Console.WriteLine("Host name: " + entry.HostName);

            Console.WriteLine("Addresses:");
            foreach (var item in entry.AddressList)
            {
                // IP?    [0..255].[0..255].[0..255].[0..255]  Ipv4  Ipv6
                Console.WriteLine("\t" + item);
            }

            Console.WriteLine("\nAliases:");
            foreach (var item in entry.Aliases)
            {
                Console.WriteLine("\t" + item);
            }
        }
    }
}
