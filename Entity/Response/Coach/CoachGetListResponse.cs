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
    public class CoachGetListResponse : BaseResponse<CoachGetListEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class CoachGetListEntity
    {
        /// <summary>
        /// 教练列表
        /// </summary>
        [DataMember]
        public List<CoachFrameEntity> CoachList { get; set; }

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
    }
}
