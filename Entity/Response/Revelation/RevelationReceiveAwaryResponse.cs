using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Revelation
{
    /// <summary>
    ///球星启示录领取通过奖励输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class RevelationReceiveAwaryResponse : BaseResponse<RevelationReceiveAwary>
    {
    }

    [Serializable]
    [DataContract]
    public class RevelationReceiveAwary
    {
        /// <summary>
        /// 是否领取成功
        /// </summary>
        [DataMember]
        public bool GetTheSuccess { get; set; }

        /// <summary>
        /// 奖励物品
        /// </summary>
        [DataMember]
        public int AwaryItem { get; set; }
    }
}
