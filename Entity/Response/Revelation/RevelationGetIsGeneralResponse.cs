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
    public class RevelationGetIsGeneralResponse : BaseResponse<RevelationGetIsGeneral>
    {
    }

    [Serializable]
    [DataContract]
    public class RevelationGetIsGeneral
    {
        /// <summary>
        /// 关卡ID
        /// </summary>
        [DataMember]
        public int MarkId { get; set; }
        ///<summary>
        ///进度
        ///</summary>
        [DataMember]
        public System.Int32 Schedule { get; set; }

        ///<summary>
        ///是否通关
        ///</summary>
        [DataMember]
        public System.Boolean IsGeneral { get; set; }

        /// <summary>
        /// 是否可以挑战
        /// </summary>
        [DataMember]
        public bool IsCanChallenge { get; set; }

        /// <summary>
        /// 是否挂机
        /// </summary>
        [DataMember]
        public bool IsHook { get; set; }

        /// <summary>
        /// 挂机ID
        /// </summary>
        [DataMember]
        public Guid HookId { get; set; }

        /// <summary>
        /// 是否有翻牌机会
        /// </summary>
        [DataMember]
        public bool IsDraw { get; set; }

        /// <summary>
        /// 翻牌ID
        /// </summary>
        [DataMember]
        public Guid DrawId { get; set; }

        /// <summary>
        /// 士气
        /// </summary>
        [DataMember]
        public int Morale { get; set; }

        /// <summary>
        /// 关卡霸主
        /// </summary>
        [DataMember]
        public string MarkDominateName { get; set; }

        /// <summary>
        /// 霸主比分
        /// </summary>
        [DataMember]
        public string MarkDominateGoals { get; set; }

        /// <summary>
        /// 我的比分记录
        /// </summary>
        [DataMember]
        public string MyGoalsLog { get; set; }
    }

    
}
