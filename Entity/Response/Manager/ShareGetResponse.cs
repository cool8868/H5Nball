using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    [Serializable]
    [DataContract]
    public class ShareGetResponse : BaseResponse<ShareGet>
    {
    }

    [Serializable]
    [DataContract]
    public class ShareGet
    {
        /// <summary>
        /// 是否是首次分享
        /// </summary>
        [DataMember]
        public bool IsFirstShare { get; set; }

        /// <summary>
        /// 是否还可以分享
        /// </summary>
        [DataMember]
        public bool IsHaveShare { get; set; }

        /// <summary>
        /// 分享次数
        /// </summary>
        [DataMember]
        public int ShareNumber { get; set; }

        /// <summary>
        /// 下次分享可得到奖励时间
        /// </summary>
        [DataMember]
        public int NextShareEnd { get; set; }
    }
}
