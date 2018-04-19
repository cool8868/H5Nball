using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Games.NBall.Entity.Response.Revelation
{
    /// <summary>
    ///球星启示录过关卡输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class RevelationTheGameResponse : BaseResponse<RevelationTheGameEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class RevelationTheGameEntity
    {
        /// <summary>
        /// 比赛ID
        /// </summary>
        [DataMember]
        public Guid MatchId { get; set; }
        /// <summary>
        /// 进球数
        /// </summary>
        [DataMember]
        public int Goals { get; set; }

        /// <summary>
        /// 失球数
        /// </summary>
        [DataMember]
        public int ToConcede { get; set; }

        /// <summary>
        /// 是否通关
        /// </summary>
        [DataMember]
        public bool IsGeneral { get; set; }

        /// <summary>
        /// 获得勇气值
        /// </summary>
        [DataMember]
        public int Courage { get; set; }

        /// <summary>
        /// 是否有抽奖
        /// </summary>
        [DataMember]
        public bool IsDraw { get; set; }

        /// <summary>
        /// 抽奖ID
        /// </summary>
        [DataMember]
        public Guid DrawId { get; set; }

        /// <summary>
        /// 进度
        /// </summary>
        [DataMember]
        public int Schedule { get; set; }

        /// <summary>
        /// 剩余士气
        /// </summary>
        [DataMember]
        public int Moare { get; set; }

        /// <summary>
        /// 获得的物品
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// 得到的物品数量
        /// </summary>
        [DataMember]
        public int ItemCount { get; set; }
    }
}
