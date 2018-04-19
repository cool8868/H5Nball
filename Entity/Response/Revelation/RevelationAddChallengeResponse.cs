using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Revelation
{
    /// <summary>
    ///球星启示录过关卡输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class RevelationAddChallengeResponse : BaseResponse<RevelationAddChallenge>
    {
    }
    [Serializable]
    [DataContract]
    public class RevelationAddChallenge
    {
        /// <summary>
        /// 剩余点卷
        /// </summary>
        [DataMember]
        public int Point { get; set; }

        /// <summary>
        /// 士气
        /// </summary>
        [DataMember]
        public int Morale { get; set; }

    }

}
