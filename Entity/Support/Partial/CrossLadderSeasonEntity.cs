using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class CrossladderSeasonEntity
	{
        /// <summary>
        /// 开始时间刻度
        /// </summary>
        [DataMember]
        public long StartTick { get; set; }

        /// <summary>
        /// 结束时间刻度
        /// </summary>
        [DataMember]
        public long EndTick { get; set; }
	}
	
	
    public partial class CrossladderSeasonResponse
    {

    }
}
