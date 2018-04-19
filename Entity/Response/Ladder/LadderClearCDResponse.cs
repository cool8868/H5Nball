using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Ladder
{
    [Serializable]
    [DataContract]
    public class LadderClearCDResponse : BaseResponse<LadderClearCD>
    {
    }

    [Serializable]
    [DataContract]
    public class LadderClearCD
    {
        /// <summary>
        /// 剩余点卷
        /// </summary>
        [DataMember]
        public int Point { get; set; }

        /// <summary>
        /// CD
        /// </summary>
        [DataMember]
        public long CDTick { get; set; }
       
    }


    [Serializable]
    [DataContract]
    public class LadderClearCDParaResponse : BaseResponse<LadderClearCDPara>
    {

    }

    [Serializable]
    [DataContract]
    public class LadderClearCDPara
    {
        /// <summary>
        /// 花费点卷剩余点卷
        /// </summary>
        [DataMember]
        public int Point { get; set; }

    }

}
