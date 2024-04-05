using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_NS
{
    class ClientObject
    {
        private bool isConnected;
        private uint max_read_pack = 1024u;
        private TcpClient _client;
        private Stream _stream;
        public ClientObject(object obj)
        {
            _client = (TcpClient)obj;
            _stream = _client.GetStream();
            string answer_get_username = "";
            Write("0");
            Read(ref answer_get_username);
            Console.WriteLine(answer_get_username);
        }
        private bool Write(string text)
        {
            bool checkRequest;
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                if (_stream.CanRead)
                {
                    _stream.Write(buffer, 0, buffer.Length);
                    _stream.Flush();

                }
                checkRequest = true;
            }
            catch
            {
                checkRequest = false;
            }
            return checkRequest;
        }
        private bool Read(ref string answer)
        {
            bool checkRequest;
            try
            {
                if (_stream.CanWrite)
                {
                    byte[] buffer = new byte[max_read_pack];
                    int size = _stream.Read(buffer, 0, buffer.Length);
                    answer = Encoding.UTF8.GetString(buffer, 0, size);
                }
                checkRequest = true;
            }
            catch
            {
                checkRequest = false;
            }
            return checkRequest;
        }
        public bool IsConnected() { return isConnected; }
        public void Start()
        {
            bool checkRequest = true;
            while (checkRequest)
            {
                string readText = "";
                string writeText = "";
                Console.WriteLine("Input command");
                writeText = Console.ReadLine();
                if (writeText == "" || writeText == string.Empty)
                {
                    writeText = "zero";
                }
                checkRequest = Write(writeText);
                checkRequest = Read(ref readText);
                Console.WriteLine("Server: "+readText);
                isConnected = checkRequest;
            }
        }
    }
}
