using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Activity
{
    [DataContract]
    [Serializable]
    public class OlympicGetInfoResponse : BaseResponse<OlympicGetInfo>
    {
      
    }

    [Serializable]
    [DataContract]

    public class OlympicGetInfo
    {
        /// <summary>
        /// 奥运金牌字典
        /// </summary>
        [DataMember]
        public Dictionary<int, int> TheGoldMedalDic { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public long StartTimeTick { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public long EndTimeTick { get; set; }
    }
}


