using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class DailycupMatchEntity
	{
        /// <summary>
        /// 回合类型：1,8强;2,4强;3,决赛
        /// </summary>
        [DataMember]
        public int RoundType { get; set; }
	}
	
	
    public partial class DailycupMatchResponse
    {

    }
}

