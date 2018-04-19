using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 转盘获取经理信息输出类
    /// </summary>
    [DataContract]
    [Serializable]
    public class GetTurntableInfoResponse : BaseResponse<TurntableInfo>
    {
    }

    [DataContract]
    [Serializable]
    public class TurntableInfo 
    {
        /// <summary>
        /// 免费次数
        /// </summary>
        [DataMember]
        public int FreeOfChargeNumber { get; set; }

        /// <summary>
        /// 幸运币数量
        /// </summary>
        [DataMember]
        public int LuckyCoin { get; set; }

        /// <summary>
        /// 剩余可获得幸运币数量
        /// </summary>
        [DataMember]
        public int DayProduceLuckyCoin { get; set; }

        /// <summary>
        /// 转盘数量
        /// </summary>
        [DataMember]
        public int TurntableType { get; set; }

        /// <summary>
        /// 转盘物品
        /// </summary>
        [DataMember]
        public List<TurntableItem> ItemList { get; set; }

        /// <summary>
        /// 下次重置需要钻石数量
        /// </summary>
        [DataMember]
        public int NextResetPoint { get; set; }

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
