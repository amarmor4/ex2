using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex1
{
    class Player
    {
        public TcpClient Client { get; set; }
        public bool IsConnected { get; set; }

        public Player(TcpClient client)
        {
            this.Client = client;
            this.IsConnected = true;
        }
    }
}
