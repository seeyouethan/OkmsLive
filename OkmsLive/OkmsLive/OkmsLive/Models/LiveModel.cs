using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkmsLive.Model
{
   public class LiveModel
    {
        public IEnumerable<LiveDiscuss> GetDiscuss(string lid, string userName = "")
        {
            IEnumerable<LiveDiscuss> list = RedisHelper.Hash_GetAll<LiveDiscuss>("znyktLiveDiscuss" + lid.ToString());
            if (list == null) return null;
            if (userName != "")
            {
                list = list.Where(p => p.STrueName == userName || p.ATrueName == userName);
            }
            var query = list.OrderByDescending(p => p.SendDate).Take(1000).OrderBy(p=>p.SendDate);
            return query;
        }

        public IEnumerable<LiveUserOnLine> GetAllUser(string lid)
        {
            IEnumerable<LiveUserOnLine> list = RedisHelper.Hash_GetAll<LiveUserOnLine>("znyktLiveUserOnLine" + lid.ToString());
            list = list?.Where(p=>p.IsOnLine==1).OrderByDescending(p => p.IsOnLine).OrderByDescending(p => p.IsHost).OrderByDescending(p => p.IsOnLine).Take(1000);
            return list;

        }
    }
}
