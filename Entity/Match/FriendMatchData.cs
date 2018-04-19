using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;
using System.Runtime.Serialization;

namespace Games.NBall.Entity.Match
{
    [Serializable]
    [DataContract]
    public class FriendMatchData : BaseMatchData
    {
        public FriendMatchData(Guid matchId, Guid homeId, Guid npcId)
            : base(EnumMatchType.Friend, matchId, homeId, npcId)
        {
            
        }
        public FriendManagerEntity FriendRecord { get; set; }
    }
}
