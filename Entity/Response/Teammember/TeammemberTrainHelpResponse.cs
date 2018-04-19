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
    public class TeammemberTrainHelpResponse : BaseResponse<TeammemberTrainHelpEntity>
    {
    }



    /// <summary>
    /// 球员训练里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberTrainHelpEntity
    {
        /// <summary>
        /// 帮助训练-球员信息
        /// </summary>
        [DataMember]
        public TeammemberTrainListEntity HelpTrainInfo { get; set; }

        /// <summary>
        /// 是否可以开宝箱
        /// </summary>
        [DataMember]
        public bool HasBox { get; set; }

    }

}
