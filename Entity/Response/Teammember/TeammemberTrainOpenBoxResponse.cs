using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;

namespace Games.NBall.Entity
{
    /// <summary>
    /// 球员训练里的开启宝箱
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberTrainOpenBoxResponse : BaseResponse<TeammemberTrainOpenBoxEntity>
    {
    }



    /// <summary>
    /// 开宝箱数据
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberTrainOpenBoxEntity
    {
        /// <summary>
        /// 奖励类型
        /// </summary>
        [DataMember]
        public int PrizeType { get; set; }

        /// <summary>
        /// 奖励物品
        /// </summary>
        [DataMember]
        public int PrizeItem { get; set; }

        /// <summary>
        /// 奖励数量
        /// </summary>
        [DataMember]
        public int PrizeCount { get; set; }

    }

}
