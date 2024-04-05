using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SystemLife
{
    class Program
    {
        static Dictionary<int,ItemElement> users;
        static void StartThreadClient(object obj) 
        {
            //add user
           
            //get my id;
            int myId = index_list_users;
            //++ index;
           
            //start chat;
            
                ClientObject client = new ClientObject(obj);
            if (client.GetUsername() == "1")
            {
                users.Add(index_list_users, new ItemElement(user_index.ToString(), (TcpClient)obj, index_list_users));
                index_list_users++;
                user_index++;
            }
            //while -> true if isconnected == true
            client.Start(ref users, myId);
            Console.WriteLine("Disconnected");
            //rm user;
            if (client.GetUsername() == "1") {
                users.Remove(myId);
                //-- index;
                index_list_users--;
                user_index--;
            }
            
        }
        static int user_index = 0;
        static int index_list_users = 0;
        static void Main(string[] args)
        {
            users = new Dictionary<int,ItemElement>();
            
            Console.WriteLine("Start Server");
            TcpListener server = new TcpListener(System.Net.IPAddress.Any, 25565);
            server.Start();
            while (true) 
            {  
                TcpClient client = server.AcceptTcpClient(); 
                new Thread(StartThreadClient).Start(client);
                Console.WriteLine("Connected");
            }
        }
    }
}
