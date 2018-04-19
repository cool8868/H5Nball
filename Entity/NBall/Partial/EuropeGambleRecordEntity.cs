using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class EuropeGamblerecordEntity
	{
        /// <summary>
        /// 主队名
        /// </summary>
        [DataMember]
        public string HomeName { get; set; }

        /// <summary>
        /// 客队名
        /// </summary>
        [DataMember]
        public string AwayName { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [DataMember]
        public long TimeTick { get; set; }
	}
	
	
    public partial class EuropeGamblerecordResponse
    {

    }
}

