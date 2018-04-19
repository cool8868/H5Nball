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
    public class OlympicExchangeResponse : BaseResponse<OlympicExchange>
    {
      
    }

    [Serializable]
    [DataContract]

    public class OlympicExchange
    {
        /// <summary>
        /// 奥运金牌字典
        /// </summary>
        [DataMember]
        public Dictionary<int, int> TheGoldMedalDic { get; set; }

        /// <summary>
        /// 得到的物品code
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }
    }
}


