using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity
{

    /// <summary>
    /// 对Table dbo.LadderManagerEntity 的数据映射.
    /// </summary>	
    public partial class LadderManagerEntity
    {
        public string Name { get; set; }

        public bool IsBot { get; set; }
        /// <summary>
        /// 赛季数据
        /// </summary>
        [DataMember]
        public LadderSeasonEntity Season { get; set; }
        /// <summary>
        /// 荣誉兑换刷新时间
        /// </summary>
        [DataMember]
        public long ExchangeRefreshTick { get; set; }
        /// <summary>
        /// 当前排名
        /// </summary>
        [DataMember]
        public int MyRank { get; set; }
        /// <summary>
        /// 昨日排名
        /// </summary>
        [DataMember]
        public int YesterdayRank { get; set; }
        /// <summary>
        /// 胜率
        /// </summary>
        [DataMember]
        public double WinRate { get; set; }


        public bool HasTask { get; set; }
        /// <summary>
        /// 刷新需要点券
        /// </summary>
        [DataMember]
        public int RefreshPoint { get; set; }

        /// <summary>
        /// 装备属性
        /// </summary>
        [DataMember]
        public List<EquipmentProperty> AllEquipmentProperties { get; set; }

        public bool IsHook { get; set; }

        /// <summary>
        /// 比赛CD结束时间
        /// </summary>
        [DataMember]
        public long CDTick { get; set; }

    }


    /// <summary>
    /// 对Table dbo.LadderManagerEntity 的输出映射.
    /// </summary>
    public partial class LadderManagerResponse
    {

    }

    [DataContract]
    [Serializable]
    public class LadderRefreshExchangeResponse : BaseResponse<LadderRefreshExchangeEntity>
    {
    }

    [DataContract]
    [Serializable]
    public class LadderRefreshExchangeEntity
    {
        [DataMember]
        public System.String ExchangeIds { get; set; }

        [DataMember]
        public int ManagerPoint { get; set; }
        /// <summary>
        /// 刷新需要点券
        /// </summary>
        [DataMember]
        public int RefreshPoint { get; set; }
        /// <summary>
        /// 天梯积分
        /// </summary>
        [DataMember]
        public int Honor { get; set; }

        /// <summary>
        /// 荣誉兑换刷新时间
        /// </summary>
        [DataMember]
        public long ExchangeRefreshTick { get; set; }

        /// <summary>
        /// 装备属性
        /// </summary>
        [DataMember]
        public List<EquipmentProperty> AllEquipmentProperties { get; set; }

        /// <summary>
        /// 天梯币
        /// </summary>
        [DataMember]
        public int LadderCoin { get; set; }
    }
}

