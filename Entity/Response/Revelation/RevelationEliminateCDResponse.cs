using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Revelation
{
    /// <summary>
    ///球星启示录查询是否通关输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class RevelationEliminateCDResponse : BaseResponse<RevelationEliminateCD>
    {
    }

    [Serializable]
    [DataContract]
    public class RevelationEliminateCD
    {
        /// <summary>
        /// 剩余点卷
        /// </summary>
        [DataMember]
        public int Point { get; set; }
    }
}
