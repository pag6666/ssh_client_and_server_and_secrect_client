using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SystemLife
{
    class ClientObject
    {
        private string _username = "";
        private bool isConnected;
        private uint max_read_pack = 1024u;
        private TcpClient _client;
        private Stream _stream;
        public ClientObject(object obj) 
        {
            _client = (TcpClient)obj;
            _stream = _client.GetStream();
            //get_username
            Read(ref _username);
            Write($"Succees get username: {_username}");
        }
        ////////////////////////////////////////start
        private bool Write(string text)
        {
            bool checkRequest;
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                if (_stream.CanWrite)
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
                if (_stream.CanRead)
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
        //
        private bool WriteStream(string text, Stream stream)
        {
            bool checkRequest;
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                if (stream.CanWrite)
                {
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Flush();

                }
                checkRequest = true;
            }
            catch
            {
                checkRequest = false;
            }
            return checkRequest;
        }
        private bool ReadStream(ref string answer, Stream stream)
        {
            bool checkRequest;
            try
            {
                if (stream.CanRead)
                {
                    byte[] buffer = new byte[max_read_pack];
                    int size = stream.Read(buffer, 0, buffer.Length);
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
        //
        private bool Write(byte[] byteArray)
        {
            bool checkRequest;
            try
            {
                byte[] buffer = byteArray;
                if (_stream.CanWrite)
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
        private bool Read(ref byte[] byteArray)
        {
            bool checkRequest;
            try
            {
                if (_stream.CanRead)
                {
                    byte[] buffer = new byte[max_read_pack];
                    int size = _stream.Read(buffer, 0, buffer.Length);
                    byteArray = buffer.Take(size).ToArray();
                }
                checkRequest = true;
            }
            catch
            {
                checkRequest = false;
            }
            return checkRequest;
        }
        ///////////////////////////////////////////////////////////read and write
        public bool IsConnected() { return isConnected; }
        public string GetUsername() { return _username; }
        private string GetUsers(Dictionary<int,ItemElement> users) 
        {
            string users_string = "";
            foreach (var user in users) 
            {
                users_string += $"{user.Value.GetUsername()},";
            }
            if (users_string.Length > 0) {
                users_string = users_string.Substring(0, users_string.Length - 1);
            }
            return users_string;
        }
        static bool FoundUserIndex(Dictionary<int,ItemElement> users, string name) 
        {
            bool found = false, nfound = false;
            foreach (var user in users) 
            {
                if (user.Value.GetUsername() == name)
                {
                    found = true;
                }
                else 
                {
                    nfound = false;
                }
            }
            return found || nfound;
        }
        static string standart_name = "1";
        public void Start(ref Dictionary<int, ItemElement> users, int myid)
        {
            if (GetUsername() != standart_name) {
                bool checkRequest = true;
                string readText = "";
                string writeText = "";
                while (checkRequest)
                {

                    checkRequest = Read(ref readText);
                    readText = readText.Trim();
                    Console.WriteLine($"|{readText}|");
                    switch (readText)
                    {
                        case "get_my_index": writeText = myid.ToString(); break;
                        case "get_users": writeText = GetUsers(users); break;
                        case "0": writeText = "server: Zero"; break;
                        case "connect": writeText = "input_index";
                            //check
                            foreach (var user in users) 
                            {
                                try
                                {
                                    Stream stream = user.Value.GetTcpClient().GetStream();
                                    WriteStream("1", stream);
                                    string temp_text = "";
                                    ReadStream(ref temp_text, stream);
                                    Console.WriteLine(temp_text);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("error user: "+ex.Message);
                                    users.Remove(user.Key);
                                }
                            }
                            //
                            checkRequest = Write(writeText);
                            string get_index = "";
                            checkRequest = Read(ref get_index);

                            if (FoundUserIndex(users, get_index))
                            {
                                bool tr = true;
                                writeText = "found";
                                checkRequest = Write(writeText);
                                ItemElement item = users[int.Parse(get_index)];
                                try
                                {
                                    while (tr)
                                    {
                                        checkRequest = Read(ref readText);
                                        tr = WriteStream(readText, item.GetTcpClient().GetStream());
                                        tr = ReadStream(ref readText, item.GetTcpClient().GetStream());
                                        checkRequest = Write(readText);
                                        if (readText == "close_sh")
                                        {
                                            break;
                                        }
                                    }
                                }
                                catch 
                                {
                                    users.Remove(item.GetId());
                                    Console.WriteLine("Ds_SH");
                                }
                                goto jump_code;
                                
                            }
                            else
                            {
                                writeText = "not found";
                            }
                            /*goto jump_code;*/
                            break;
                        case "2": writeText = "server: 2"; break;
                        case "3": writeText = "server: 3"; break;
                        default: writeText = "command not found"; break;
                    }
                    if (writeText == "" || writeText == string.Empty)
                    {
                        writeText = "zero";
                    }
                    checkRequest = Write(writeText);
                    jump_code:
                    isConnected = checkRequest;
                }
            }
            else 
            {
                int timer_chek_connect = 10_000;
                bool checkRequest = true;
                string readText = "";
                string writeText = "1";
                while (checkRequest) 
                {
                   /* checkRequest = Write(writeText);
                    checkRequest = Read(ref readText);
                    Thread.Sleep(timer_chek_connect);*/
                }
            }
        }
    }
}
