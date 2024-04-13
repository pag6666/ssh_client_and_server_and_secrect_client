using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_NS
{
    class Program
    {
        static void Main(string[] args)
        {
            reconnected:
            try
            {
                TcpClient tempclient = new TcpClient("185.228.232.214", 25565);
                ClientObject client = new ClientObject(tempclient);
                client.Start();
                if (client.IsConnected() == false)
                {
                    Console.WriteLine("client Disconnected");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Reconnected");
                goto reconnected;
            }
            Console.WriteLine("press button");
            Console.ReadKey();
            goto reconnected;
        }
    }
}
