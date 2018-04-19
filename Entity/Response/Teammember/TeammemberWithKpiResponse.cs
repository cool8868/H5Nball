using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;
using log4net.Core;

namespace Games.NBall.Entity
{
    /// <summary>
    /// 球员训练里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberWithKpiResponse : BaseResponse<TeammemberWithKpiEntity>
    {
    }

    /// <summary>
    /// 球员训练里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberWithKpiEntity
    {
        /// <summary>
        /// 综合实力
        /// </summary>
        [DataMember]
        public int Kpi { get; set; }

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
      
    }

}
