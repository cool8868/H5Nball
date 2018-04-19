using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 球员成长里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberListForGrowResponse : BaseResponse<TeammemberListForGrowEntity>
    {
    }

    /// <summary>
    /// 球员成长里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberListForGrowEntity
    {
        /// <summary>
        /// 球员列表
        /// </summary>
        [DataMember]
        public List<TeammemberForGrowEntity> Teammembers { get; set; }
    }

    /// <summary>
    /// 球员成长里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberForGrowEntity
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        [DataMember]
        public Guid TeammemberId { get; set; }

        /// <summary>
        /// 球员的字典id
        /// </summary>
        [DataMember]
        public int PlayerId { get; set; }

        /// <summary>
        /// 能力值
        /// </summary>
        [DataMember]
        public double Kpi { get; set; }

        ///<summary>
        ///球员训练等级
        ///</summary>
        [DataMember]
        public System.Int32 Level { get; set; }

        ///<summary>
        ///球员成长阶段
        ///</summary>
        [DataMember]
        public System.Int32 GrowLevel { get; set; }
    }
}
