using System;
using System.Collections.Concurrent;
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
    public class RevelationGetManagerResponse : BaseResponse<RevelationGetManager>
    {
    }


    [Serializable]
    [DataContract]
    public class RevelationGetManager
    {
        /// <summary>
        /// 是否有正在比赛的关卡
        /// </summary>
        [DataMember]
        public bool IsMatchMark { get; set; }

        /// <summary>
        /// 比赛关卡ID
        /// </summary>
        [DataMember]
        public int MarchMarkId { get; set; }

        /// <summary>
        /// 解锁到了哪个关卡
        /// </summary>
        [DataMember]
        public int LockMark { get; set; }

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
        /// 是否有挂机
        /// </summary>
        [DataMember]
        public bool IsHook { get; set; }

        /// <summary>
        /// 挂机ID
        /// </summary>
        [DataMember]
        public Guid HookId { get; set; }

        /// <summary>
        /// 关卡次数信息
        /// </summary>
        [DataMember]
        public List<RevelationMarkNumberEntity> MarkInfo { get; set; }
    }



}
