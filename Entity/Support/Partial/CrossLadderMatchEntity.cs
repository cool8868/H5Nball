using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class CrossladderMatchEntity
	{
        public CrossladderMatchEntity(CrossladderManagerEntity home, CrossladderManagerEntity away, Guid idx, Guid ladderid, int groupindex)
        {
            this.Idx = idx;
            this.LadderId = ladderid;
            this.HomeId = home.ManagerId;
            this.AwayId = away.ManagerId;
            this.HomeName = home.ShowName;
            this.AwayName = away.ShowName;
            this.HomeLadderScore = home.Score;
            this.AwayLadderScore = away.Score;
            this.HomeSiteId = home.SiteId;
            this.AwaySiteId = away.SiteId;
            this.HomeLogo = home.Logo;
            this.AwayLogo = away.Logo;
            this.HomeScore = 0;
            this.AwayScore = 0;
            this.HomeIsBot = home.IsBot;
            this.AwayIsBot = away.IsBot;
            this.GroupIndex = groupindex;
            this.PrizeHomeScore = 0;
            this.PrizeAwayScore = 0;
            this.Status = -1;
            this.RowTime = DateTime.Now;

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

        public bool HomeIsHook { get; set; }

        public bool AwayIsHook { get; set; }
	}
	
	
    public partial class CrossladderMatchResponse
    {

    }
}
