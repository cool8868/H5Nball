using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity
{    

	public partial class LadderMatchEntity
    {
        public LadderMatchEntity(LadderManagerEntity home, LadderManagerEntity away, Guid idx, Guid ladderid, int groupindex)
        {
            this.Idx = idx;
            this.LadderId = ladderid;
            this.HomeId = home.ManagerId;
            this.AwayId = away.ManagerId;
            this.HomeName = home.Name;
            this.AwayName = away.Name;
            this.HomeLadderScore = home.Score;
            this.AwayLadderScore = away.Score;
            this.HomeScore = 0;
            this.AwayScore = 0;
            this.HomeIsBot = home.IsBot;
            this.AwayIsBot = away.IsBot;
            this.GroupIndex = groupindex;
            this.PrizeHomeScore = 0;
            this.PrizeAwayScore = 0;
            this.Status = -1;
            this.RowTime = DateTime.Now;

            this.HomeHasTask = home.HasTask;
            this.AwayHasTask = away.HasTask;
            this.HomeIsHook = home.IsHook;
            this.AwayIsHook = away.IsHook;
        }

        /// <summary>
        /// 主队胜率
        /// </summary>
        public int HomeWinPercent { get; set; }
        /// <summary>
        /// 客队胜率
        /// </summary>
        public int AwayWinPercent { get; set; }

        public bool HomeHasTask { get; set; }

        public bool AwayHasTask { get; set; }

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }

        public bool HomeIsHook { get; set; }

        public bool AwayIsHook { get; set; }
	}
	
	
    public partial class LadderMatchResponse
    {

    }
}

