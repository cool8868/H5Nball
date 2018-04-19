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
    ///球星启示录开始一个关卡输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class RevelationStartMarkResponse : BaseResponse<RevelationStartMark>
    {
    }


    [Serializable]
    [DataContract]
    public class RevelationStartMark
    {
        /// <summary>
        /// 关卡信息
        /// </summary>
        [DataMember]
        public RevelationGetIsGeneral MarkInfo { get; set; }
    }

    /// <summary>
    /// 球星启示录关卡通关记录
    /// </summary>
    [Serializable]
    [DataContract]
    public class RevelationMarkNumberEntity
    {
        /// <summary>
        /// 关卡ID
        /// </summary>
        [DataMember]
        public int MarkId { get; set; }

        /// <summary>
        /// 总次数
        /// </summary>
        [DataMember]
        public int SumNumber { get; set; }

        /// <summary>
        /// 用掉了多少次
        /// </summary>
        [DataMember]
        public int UseNumber { get; set; }
    }

}
