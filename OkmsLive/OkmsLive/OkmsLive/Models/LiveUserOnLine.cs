using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkmsLive.Model
{
   public class LiveUserOnLine
    {
        public int lid { get; set; }

        public string UserName { get; set; }

        public string TrueName { get; set; }

        public int IsOnLine { get; set; }

        public string ConnectionID { get; set; }

        public string SteamID { get; set; }

        public int IsHost { get; set; }

        public string HeadPhoto { get; set; }
    }

    public class LiveUserConnection
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string lid { get; set; }
    }
}
