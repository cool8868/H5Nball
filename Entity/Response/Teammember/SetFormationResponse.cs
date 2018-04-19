using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Response;
using log4net.Core;

namespace Games.NBall.Entity
{
    /// <summary>
    /// 球员训练里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class SetFormationResponse : BaseResponse<SetFormation>
    {
    }



    /// <summary>
    /// 球员训练里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class SetFormation
    {
        /// <summary>
        /// 阵型和球员
        /// </summary>
        [DataMember]
        public NBSolutionInfo SolutionInfo { get; set; }

        /// <summary>
        /// 背包
        /// </summary>
        [DataMember]
        public ItemPackageEntity Package { get; set; }

    }

}
