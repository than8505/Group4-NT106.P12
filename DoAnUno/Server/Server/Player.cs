using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Player
    {
        public string idGame { get; set; }
        public int numOfCard { get; set; }
        public int turn { get; set; }
        public bool isHost { get; set; }
        public Socket pSocket { get; set; }

    }
}
