using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Gamble
{
    /// <summary>
    /// 获取欧洲杯竞猜输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class EuropeGambleMatchResponse : BaseResponse<EuropeGambleMatch>
    {
    }


    [Serializable]
    [DataContract]
    public class EuropeGambleMatch
    {
        /// <summary>
        /// 剩余钻石
        /// </summary>
        [DataMember]
        public int Point { get; set; }
    }


    public class GamblePointConfig
    {
        /// <summary>
        /// 竞猜点卷数量类型
        /// </summary>
        public int GambleType { get; set; }
        /// <summary>
        /// 点卷数量
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 需要VIP等级
        /// </summary>
        public int VipLevel { get; set; }
    }
}
