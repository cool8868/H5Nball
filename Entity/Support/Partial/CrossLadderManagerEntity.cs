using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class CrossladderManagerEntity
	{
        public string ShowName { get; set; }
        public bool IsBot { get; set; }
        /// <summary>
        /// 赛季数据
        /// </summary>
        [DataMember]
        public CrossladderSeasonEntity Season { get; set; }
        /// <summary>
        /// 当前排名
        /// </summary>
        [DataMember]
        public int MyRank { get; set; }
        /// <summary>
        /// 昨日排名
        /// </summary>
        [DataMember]
        public int YesterdayRank { get; set; }
        /// <summary>
        /// 胜率
        /// </summary>
        [DataMember]
        public double WinRate { get; set; }

        public bool IsHook { get; set; }
	}
	
	
    public partial class CrossladderManagerResponse
    {

    }

    public class CrossladderDailyHonor
    {
        public Guid ManagerId { get; set; }
        public string SiteId { get; set; }
        public int NewlyHonor { get; set; }
        public int NewlyLadderCoin { get; set; }
    }

}
