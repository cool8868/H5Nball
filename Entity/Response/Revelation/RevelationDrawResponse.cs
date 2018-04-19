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
    public class RevelationDrawResultResponse : BaseResponse<RevelationDrawResult>
    {
    }


    [Serializable]
    [DataContract]
    public class RevelationDrawResult
    {

        /// <summary>
        /// 所有物品
        /// </summary>
        [DataMember]
        public string AllItemString { get; set; }

        /// <summary>
        /// 已经得到的物品
        /// </summary>
        [DataMember]
        public string PrizeItemString { get; set; }

        /// <summary>
        /// 得到我抽卡ID
        /// </summary>
        [DataMember]
        public int DrawId { get; set; }

        /// <summary>
        /// 下次翻牌需要的金条
        /// </summary>
        [DataMember]
        public int NextDrawGoldBar { get; set; }

        /// <summary>
        /// 得到的教练碎片code
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }
    }



}
