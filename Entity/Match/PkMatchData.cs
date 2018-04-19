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
    public class PkMatchData : BaseMatchData
    {
        public PkMatchData(Guid matchId, Guid homeId, Guid awayId, long revengeRecordId, int dayWinTimes)
            : base(EnumMatchType.PlayerKill, matchId, homeId, awayId)
        {
            RevengeRecordId = revengeRecordId;
            DayWinTimes = dayWinTimes;
        }


        public int DayWinTimes { get; set; }

        public long RevengeRecordId { get; set; }
    }
}
