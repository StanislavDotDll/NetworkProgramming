using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTP
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // HTTP - hypertext transfer protocol
            // 
            //  await UsingwebClientAsync();

            //  UsingHttpWebRequest();

            await UsingHttpClient();
        }

        private static async Task UsingHttpClient()
        {
            string url = "https://robohash.org/anton.png?set=set4";
            HttpClient client = new HttpClient();
            var res = await client.GetByteArrayAsync(url);

            File.WriteAllBytes("robot.png", res);
        }

        private static void UsingHttpWebRequest()
        {
            string url = "https://randomuser.me/api/";
            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse responce = (HttpWebResponse)request.GetResponse();


            Console.WriteLine(responce.StatusCode);
            if (responce.StatusCode == HttpStatusCode.OK)
                Console.WriteLine("response OK");

            Stream stream = responce.GetResponseStream();

            StreamReader reader = new StreamReader(stream);
            var res = reader.ReadToEnd();

            RandomMe me = JsonConvert.DeserializeObject<RandomMe>(res);
            Console.WriteLine(me.results[0].email);
        }

        private static async Task UsingwebClientAsync()
        {
            using (WebClient client = new WebClient())
            {
                const string url = "https://www.gutenberg.org/files/1342/1342-0.txt";
                const string img = "https://d2bgjx2gb489de.cloudfront.net/gbb-blogs/wp-content/uploads/2016/11/28163410/shutterstock_217744243.jpg";
                await client.DownloadFileTaskAsync(url, "res.txt");
                var res = img.Split("/".ToCharArray());
                client.DownloadFile(new Uri(img), res[res.Count() - 1]);
                //   File.WriteAllText("index.txt", res);
            }
        }
    }
}
