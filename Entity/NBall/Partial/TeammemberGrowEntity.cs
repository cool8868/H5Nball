using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity
{    

	public partial class TeammemberGrowEntity
	{
        /// <summary>
        /// 免费的快速成长次数，如果有免费次数，则快速按钮显示免费
        /// </summary>
        [DataMember]
        public int FreeFastGrowCount { get; set; }

        /// <summary>
        /// 突破需要的成长值
        /// </summary>
        [DataMember]
        public int BreakGrowNum { get; set; }

        /// <summary>
        /// 突破成功率
        /// </summary>
        [DataMember]
        public int BreakRate { get; set; }

        /// <summary>
        /// 经理的灵气值
        /// </summary>
        [DataMember]
        public int ManagerReiki { get; set; }
        /// <summary>
        /// 普通成长消耗灵气值
        /// </summary>
        [DataMember]
        public int GrowCostReiki { get; set; }
        /// <summary>
        /// 快速成长消耗灵气值，为0表示本次快速成长免费
        /// </summary>
        [DataMember]
        public int FastGrowCostReiki { get; set; }
        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
	}
	
	
    public partial class TeammemberGrowResponse
    {

    }
}

