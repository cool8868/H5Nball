using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Ad
{
    /// <summary>
    /// 获取点球活动输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class PenaltyKickGetActivityInfoResponse : BaseResponse<PenaltyKickGetActivityInfo>
    {
    }
    [Serializable]
    [DataContract]
    public class PenaltyKickGetActivityInfo
    {
        /// <summary>
        /// 是否有活动
        /// </summary>
        [DataMember]
        public bool IsActivity { get; set; }

        /// <summary>
        /// 是否有提示
        /// </summary>
        [DataMember]
        public bool IsPrompt { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public long StartTimeTick { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public long EndTimeTick { get; set; }
    }
}
