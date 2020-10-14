using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FTP
{
    class Program
    {
        static void Main(string[] args)
        {
            // FTP - file transfer protocol
            // 
            const string url = "ftp://92.52.138.128/";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            #region ListDirectoryDetails
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string content = reader.ReadToEnd();
            Console.WriteLine(content);
            Console.WriteLine("Status: " + response.StatusDescription);
            reader.Close();
            response.Close();
            #endregion

            #region MakeDirectory
            string foldername = "wednesday";

            //request = (FtpWebRequest)WebRequest.Create(Path.Combine(url, foldername));
            //request.Method = WebRequestMethods.Ftp.MakeDirectory;
            //request.GetResponse();
            #endregion

            #region RemoveDirectory
            //string foldername = "Roma";

            //request = (FtpWebRequest)WebRequest.Create(Path.Combine(url, foldername));
            //request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            //request.GetResponse(); 
            #endregion

            #region UploadFile
            //request = (FtpWebRequest)WebRequest.Create(Path.Combine(url, foldername, "index.html"));
            //request.Method = WebRequestMethods.Ftp.UploadFile;

            //using (WebClient client = new WebClient())
            //{
            //    string text = client.DownloadString("https://google.com");
            //    //File.WriteAllText("index.html", text);

            //    StreamWriter writer = new StreamWriter(request.GetRequestStream());
            //    writer.Write(text);
            //    writer.Close();
            //}

            //request.GetResponse();
            #endregion

            #region DownloadFile
            request = (FtpWebRequest)WebRequest.Create(Path.Combine(url, "index.html"));
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            var responce = (FtpWebResponse)request.GetResponse();

            Stream readerStream = responce.GetResponseStream();
            const int size = 9026;
            var buffer = new byte[size];
            readerStream.Read(buffer, 0, buffer.Length);

            File.WriteAllBytes("roma.txt", buffer);
            #endregion

            request = (FtpWebRequest)WebRequest.Create(Path.Combine(url, "roma.txt"));
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.GetResponse();

        
        }
    }
}
