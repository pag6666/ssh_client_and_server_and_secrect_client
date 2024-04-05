using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Secren_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            reconnect:
            try
            {

                ClientObject client = new ClientObject((object)new TcpClient("127.0.0.1", 25565));
                client.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: "+ex.Message);
                
            }
            goto reconnect;
        }
    }
}
