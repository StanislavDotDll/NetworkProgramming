using System.Diagnostics;
using System.Threading;

namespace Helper
{
    class Program
    {
        static void Main(string[] args)
        {
            Process.Start("server.exe");
            Thread.Sleep(2000);

            for (int i = 0; i < 5; i++)
            {
                Process.Start("client.exe");
            }
        }
    }
}
