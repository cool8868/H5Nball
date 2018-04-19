using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using ProtoBuf;

namespace Games.NBall.Entity
{
    /// <summary>
    /// 球员训练里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberTrainSpeedUpResponse : BaseResponse<TeammemberTrainSpeedUp>
    {
    }

    /// <summary>
    /// 球员训练里的球员列表
    /// </summary>
    [DataContract]
    [Serializable]
    public class TeammemberTrainSpeedUp
    {
        /// <summary>
        /// 训练球员信息
        /// </summary>
        [DataMember]
        public TeammemberTrainEntity TrainEntity { get; set; }

        /// <summary>
        /// 背包数据
        /// </summary>
        [DataMember]
        public ItemPackageData Package { get; set; }

        /// <summary>
        /// KPI
        /// </summary>
        [DataMember]
        public int Kpi { get; set; }
    }

}
