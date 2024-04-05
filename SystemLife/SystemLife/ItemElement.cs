using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SystemLife
{
    class ItemElement
    {
        private int _Id;
        private TcpClient _client;
        private string _username;
        public ItemElement(string username, TcpClient client, int id) 
        {
            this._Id = id;
            this._client = client;
            this._username = username;
        }
        public int GetId() { return this._Id; }
        public string GetUsername() { return this._username; }
        public TcpClient GetTcpClient() { return this._client; }
    }
}
