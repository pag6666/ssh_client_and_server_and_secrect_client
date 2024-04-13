using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Secren_Client
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
            Write("1");
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
                checkRequest = Read(ref readText);
                //
                switch (readText) 
                {
                    case "close": writeText = "close_sh"; break;
                    case "1": writeText = "sp-1"; break;
                    case "2": writeText = "sp-2"; break;
                    case "3": writeText = "sp-3"; break;
                    case "ssh":
                        string ip = "";
                        string login = "";
                        string password = "";
                        //
                        checkRequest = Write("input ip");
                        checkRequest = Read(ref ip);
                        //
                        checkRequest = Write("input login");
                        checkRequest = Read(ref login);
                        //
                        checkRequest = Write("input password");
                        checkRequest = Read(ref password);
                        //
                        Console.WriteLine($" host = {ip} login = {login} password = {password}");
                        try
                        {
                            using (SshClient client = new SshClient(ip, login, password))
                            {client.Connect();
                                
                                //Console.WriteLine(client.RunCommand("df").Result);
                                Write("success connect ssh, Input command ssh: ");
                                while (true) 
                                {
                                    string read_text = "";
                                    string write_text = "";
                                    Read(ref readText);
                                    if (readText == "close") 
                                    {
                                        Write("close_sh");
                                        break;
                                    }
                                    writeText = client.RunCommand(readText).Result;
                                    if (writeText.Length == 0) 
                                    {
                                        writeText = "size of 0";
                                    }
                                    if (writeText == null) 
                                    {
                                        writeText = "null";
                                    }
                                    Write(writeText);

                                }
                            }
                            goto jumpcode;
                        }
                        catch 
                        {
                            writeText = "fail connect ssh";
                        }
                            
                        break;
                    default:  writeText = "command not found"; break;
                }
                //
                checkRequest = Write(writeText);
                jumpcode:
                isConnected = checkRequest;
            }
        }
    }
}
