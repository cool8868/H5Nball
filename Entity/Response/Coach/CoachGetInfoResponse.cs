using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Coach
{
    [Serializable]
    [DataContract]
    public class CoachGetInfoResponse:BaseResponse<CoachGetInfoEntity>
    { 
    }

    [Serializable]
    [DataContract]
    public class CoachGetInfoEntity
    {
        /// <summary>
        /// 教练详情
        /// </summary>
        [DataMember]
        public CoachFrameEntity CoachInfo { get; set; }

        /// <summary>
        /// 启用的教练ID
        /// </summary>
        [DataMember]
        public int EnableCoachId { get; set; }

        /// <summary>
        /// 可分配的教练经验
        /// </summary>
        [DataMember]
        public int SumExp { get; set; }

        /// <summary>
        /// 综合实力
        /// </summary>
        [DataMember]
        public int Kpi { get; set; }
    }
}
