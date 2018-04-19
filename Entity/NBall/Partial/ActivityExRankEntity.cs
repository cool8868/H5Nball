using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class ActivityexRankEntity
	{
        /// <summary>
        /// 名次
        /// </summary>
        [DataMember]
        public int ExStep { get; set; }
	}
	
	
    public partial class ActivityexRankResponse
    {

    }
}

