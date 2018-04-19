using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class EuropeGambleEntity
	{
        /// <summary>
        /// 活动重置时间
        /// </summary>
        [DataMember]
        public long ActivityEndTime { get; set; }
	}
	
	
    public partial class EuropeGambleResponse
    {

    }
}

