using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class EuropeMatchEntity
	{
        /// <summary>
        /// 是否已经竞猜过
        /// </summary>
        [DataMember]
        public bool IsAlreadyGamble { get; set; }

        /// <summary>
        /// 主队Logo
        /// </summary>
        [DataMember]
        public int HomeLogo { get; set; }

        /// <summary>
        /// 客队Logo
        /// </summary>
        [DataMember]
        public int AwayLogo { get; set; }

        /// <summary>
        /// 比赛时间
        /// </summary>
        [DataMember]
        public long MatchTimeTick { get; set; }

        /// <summary>
        /// 比赛结果类型
        /// </summary>
        public string ResultTypeString { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusString { get; set; }
	}
	
	
    public partial class EuropeMatchResponse
    {

    }
}

