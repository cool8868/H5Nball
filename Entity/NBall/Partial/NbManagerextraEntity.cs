using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class NbManagerextraEntity
	{
        /// <summary>
        /// 恢复体力时间刻度
        /// </summary>
        [DataMember]
        public long ResumeStaminaTimeTick { get; set; }
        /// <summary>
        /// 体力是否满
        /// </summary>
        public bool IsStaminaFull { get; set; }

        [DataMember]
        public bool CanReceiveGuidePrize { get; set; }
	}
	
	
    public partial class NbManagerextraResponse
    {

    }
}

