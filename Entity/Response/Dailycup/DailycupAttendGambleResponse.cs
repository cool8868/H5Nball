using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.Dailycup
{
    /// <summary>
    /// 杯赛竞猜响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class DailycupAttendGambleResponse : BaseResponse<DailycupAttendGambleEntity>
    {
    }

    /// <summary>
    /// 杯赛竞猜
    /// </summary>
    [Serializable]
    [DataContract]
    public class DailycupAttendGambleEntity
    {
        /// <summary>
        /// 最大竞猜数量，返回code=456时，用该值replace 参数{0}
        /// </summary>
        [DataMember]
        public int MaxGambleCount { get; set; }

        /// <summary>
        /// 最大竞猜强化等级，返回code=458时，用该值replace 参数{0}
        /// </summary>
        [DataMember]
        public int MaxGambleStrength { get; set; }

        /// <summary>
        /// 竞猜成功后返回竞猜数据
        /// </summary>
        [DataMember]
        public DailycupGambleEntity GambleData { get; set; }

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }
}
