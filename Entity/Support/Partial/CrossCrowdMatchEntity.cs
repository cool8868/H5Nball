using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class CrosscrowdMatchEntity
	{
        public int HomeKpi { get; set; }
        public int AwayKpi { get; set; }
        public CrosscrowdMatchEntity(CrosscrowdManagerEntity home, CrosscrowdManagerEntity away, Guid idx, int crowdId, int pairIndex)
        {
            this.Idx = idx;
            this.CrossCrowdId = crowdId;
            this.HomeId = home.ManagerId;
            this.AwayId = away.ManagerId;
            HomeKpi = home.Kpi;
            AwayKpi = away.Kpi;
            this.HomeSiteId = home.SiteId;
            this.AwaySiteId = away.SiteId;
            this.HomeName = home.ShowName;
            this.AwayName = away.ShowName;
            this.HomeScore = 0;
            this.AwayScore = 0;
            this.PairIndex = pairIndex;
            this.Status = -1;
            this.RowTime = DateTime.Now;
        }
        [DataMember]
        public string Pop { get; set; }
	}
	
	
    public partial class CrosscrowdMatchResponse
    {

    }
}
