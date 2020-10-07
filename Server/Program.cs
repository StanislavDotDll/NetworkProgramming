using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Server
{
   public class Program
    {
        static void Main(string[] args)
        {
            DbHelper helper = new DbHelper();
            const int port = 2020;
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);

            server.Start();
            while (true)
            {
                Console.WriteLine("Wait for connection:");
                var client = server.AcceptTcpClient();
                Console.WriteLine("Connected");
               
                helper.GetContacts();
                //if (helper.GetContacts().Count == 0)
                //    continue;

                using (NetworkStream stream = client.GetStream())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ContactDTO));
                    var contact = (ContactDTO)serializer.Deserialize(stream);

                    helper.AddContact(new Contact { Name = contact.Name, Phone = contact.Phone });
                    Console.WriteLine("OK");
                }
                client.Close();
            }
        }

        public class ContactDTO
        {
            public string Name { get; set; }
            public string Phone { get; set; }
        }
    }
}
