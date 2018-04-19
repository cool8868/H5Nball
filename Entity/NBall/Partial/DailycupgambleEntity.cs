using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class DailycupGambleEntity
	{
        /// <summary>
        /// 竞猜时间
        /// </summary>
        [DataMember]
        public long TimeTick { get; set; }
	}
	
	
    public partial class DailycupGambleResponse
    {

    }
}

