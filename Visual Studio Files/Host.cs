using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeardedManStudios.Forge.Networking;

namespace MasterServerUDP
{
    class Host
    {
        public string Name;
        public string Address;
        public ushort Port;
        public int PlayerCount;
        public int MaxPlayers;
        public string Comment;
        public string Id;
        public string Type;
        public string Mode;
        public string Protocol;
        public DateTime Created;
        public DateTime LastAlive;
        public NetworkingPlayer Player;

        public Host()
        {
            Created = DateTime.Now;
        }

        public void Alive()
        {
            LastAlive = DateTime.Now;
        }

    }
}
