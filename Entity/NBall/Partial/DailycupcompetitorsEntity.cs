using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class DailycupCompetitorsEntity
	{

        ///<summary>
        ///等级
        ///</summary>
        [DataMember]
        public System.Int32 Level { get; set; }

        ///<summary>
        ///综合实力
        ///</summary>
        [DataMember]
        public System.Int32 kpi { get; set; }

        ///<summary>
        ///世界杯积分
        ///</summary>
        [DataMember]
        public System.Int32 WorldScore { get; set; }

	}
	
	
    public partial class DailycupCompetitorsResponse
    {

    }
}

