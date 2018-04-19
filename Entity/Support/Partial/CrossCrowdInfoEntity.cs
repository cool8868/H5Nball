using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class CrosscrowdInfoEntity
	{
        /// <summary>
        /// 活动开始时间
        /// </summary>
        [DataMember]
        public long StartTick { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        [DataMember]
        public long EndTick { get; set; }
	}
	
	
    public partial class CrosscrowdInfoResponse
    {

    }


    public class CrosscrowdSendRankPrizeEntity
    {
        public int Idx { get; set; }
        public Guid ManagerId { get; set; }
        public int Rank { get; set; }
        public int Score { get; set; }
        public int Status { get; set; }
        public string SiteId { get; set; }
    }

    public class CrosscrowdSendKillPrizeEntity
    {
        public Guid Idx { get; set; }
        public Guid HomeId { get; set; }
        public Guid AwayId { get; set; }
        public string HomeName { get; set; }
        public string AwayName { get; set; }
        public int HomeMorale { get; set; }
        public int AwayMorale { get; set; }
        public int Status { get; set; }
        public string HomeSiteId { get; set; }
        public string AwaySiteId { get; set; }
    }
}
