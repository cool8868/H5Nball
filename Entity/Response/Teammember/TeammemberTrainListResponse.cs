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
    /// 球员训练里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberTrainListResponse : BaseResponse<TeammemberTrainListEntity>
    {
    }

    /// <summary>
    /// 球员训练里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberTrainListEntity
    {
        /// <summary>
        /// 球员训练席位数
        /// </summary>
        [DataMember]
        public int TrainSeatCount { get; set; }

        /// <summary>
        /// 球员列表
        /// </summary>
        [DataMember]
        public List<TeammemberTrainEntity> Teammembers { get; set; }
    }

}
