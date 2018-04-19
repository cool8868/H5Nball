using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response.Scouting
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    public class ScoutingShowResponse : BaseResponse<ScoutingShowList>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class ScoutingShowList
    {
        /// <summary>
        /// 物品串
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public List<NScoutingEntity> ItemList { get; set; }

        /// <summary>
        /// 当前经理点券
        /// </summary>
        [DataMember]
        public int Point { get; set; }

        /// <summary>
        /// 当前经理游戏币
        /// </summary>
        [DataMember]
        public int Coin { get; set; }

        /// <summary>
        /// 下次自动刷新时间
        /// </summary>
        [DataMember]
        public long NextRefreshTick { get; set; }

        /// <summary>
        /// 抽卡点券
        /// </summary>
        [DataMember]
        public int LotteryPoint { get; set; }

        /// <summary>
        /// 当前第几次抽卡 >4需要自动刷新
        /// </summary>
        [DataMember]
        public int LotteryCount { get; set; }
    }

    [Serializable]
    [DataContract]
    [ProtoContract]
    public class NScoutingEntity
    {
        [DataMember]
        [ProtoMember(1)]
        public int ItemCode { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public bool LotteryFlag { get; set; }

        [DataMember]
        [ProtoMember(3)]
        public bool IsBinding { get; set; }

        [ProtoMember(4)]
        public int Kpi { get; set; }
        [ProtoMember(5)]
        public int Rate { get; set; }
        [ProtoMember(6)]
        public int LotteryIndex { get; set; }
    }
}
