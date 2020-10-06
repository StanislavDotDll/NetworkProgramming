using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _02_AsyncClient
{
    // Структура для передачі між потоками
    struct TransferData
    {
        public Socket Socket { get; set; }
        public byte[] Buffer { get; set; }
        public static readonly int size = 1024;

    }
    class Program
    {
        const int port = 2020;
        static void Main(string[] args)
        {
            // Активні сокети (клієнт), пасивні (серверні)
            // синхронні, асинхронні

            StartClient();
        }

        private static void StartClient()
        {
            // Отримуємо інформацію про сервер (IP, port)
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHost.AddressList[0];

            // клієнтський сокет (абстракція мережевої передачі)
            Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // Намагаємось підключитись до сервера
                client.BeginConnect(new IPEndPoint(ipAddress, port), ConnectCallback, client);
                // як тільки метод завершиться в іншому потоці, спрацює колбек метод ConnectCallback
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            // клієнтський сокет можемо отримати із параметра AsyncState
            var client = (Socket)ar.AsyncState;
            // необхідний, бо потрібно зробити EndXXX(ar)
            client.EndConnect(ar);

            // переводимо рядок в масив байт
            byte[] data = Encoding.UTF8.GetBytes(String.Format("Hello, server {0}", client.LocalEndPoint));
            // передаємо на сервер
            client.BeginSend(data, 0, data.Length, SocketFlags.None, SendCallback, client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            var client = (Socket)ar.AsyncState;
            int countBytes = client.EndSend(ar);
            Console.WriteLine("Send to server {0} bytes", countBytes);

            Receive(client);
        }

        private static void Receive(Socket client)
        {
            TransferData data = new TransferData
            {
                Socket = client,
                Buffer = new byte[TransferData.size]
            };
            client.BeginReceive(data.Buffer, 0, TransferData.size,SocketFlags.None, ReceiveCallback, data);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            var data = (TransferData)ar.AsyncState;
            var client = (Socket)data.Socket;
            int countBytes = client.EndReceive(ar);

            string result = Encoding.UTF8.GetString(data.Buffer, 0, countBytes);

            Console.WriteLine("Got {0} from server {1}", result, client.RemoteEndPoint);
        }
    }
}
