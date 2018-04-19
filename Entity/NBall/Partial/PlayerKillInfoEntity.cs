using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Entity.Response;

namespace Games.NBall.Entity
{    

	public partial class PlayerkillInfoEntity
	{
        /// <summary>
        /// 对手列表
        /// </summary>
        [DataMember]
        public List<PlayerKillOpponentEntity> Opponents { get; set; }

        /// <summary>
        /// 有未抽卡的比赛
        /// </summary>
        [DataMember]
        public bool HasLottery { get; set; }

        /// <summary>
        /// 恢复体力时间刻度
        /// </summary>
        [DataMember]
        public long ResumeStaminaTimeTick { get; set; }

        /// <summary>
        /// 对手刷新时间
        /// </summary>
        [DataMember]
	    public long OpponentRefreshTimeTick { get; set; }

        [DataMember]
        public int TotalPoint { get; set; }

	}
	
	
    public partial class PlayerkillInfoResponse
    {

    }
}

