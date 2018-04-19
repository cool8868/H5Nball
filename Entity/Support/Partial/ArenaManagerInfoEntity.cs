using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Entity.Response;

namespace Games.NBall.Entity
{    

	public partial class ArenaManagerinfoEntity
	{
        public bool IsNpc { get; set; }

        /// <summary>
        /// 对手列表
        /// </summary>
        [DataMember]
        public ArenaOpponent OpponentList { get; set; }

        /// <summary>
        /// 综合实力
        /// </summary>
        [DataMember]
        public int Kpi { get; set; }
	}
	
	
    public partial class ArenaManagerinfoResponse
    {

    }
}
