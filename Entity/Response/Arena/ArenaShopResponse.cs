using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    [Serializable]
    [DataContract]
    public class GetArenaShopResponse : BaseResponse<ArenaShop>
    {
    }

    [Serializable]
    [DataContract]
    public class ArenaShop
    {
        /// <summary>
        /// 物品串
        /// </summary>
        [DataMember]
        public string ItemString { get; set; }

        /// <summary>
        /// 已经兑换的物品id
        /// </summary>
        [DataMember]
        public string ExchangeString { get; set; }

        /// <summary>
        /// 竞技币
        /// </summary>
        [DataMember]
        public int ArenaCoin { get; set; }

        /// <summary>
        /// 下次刷新时间
        /// </summary>
        [DataMember]
        public long RefreshTick { get; set; }

        /// <summary>
        /// 下次刷新需要消耗点卷
        /// </summary>
        [DataMember]
        public int NextRefreshPoint { get; set; }

        /// <summary>
        /// 剩余钻石
        /// </summary>
        [DataMember]
        public int Point { get; set; }
    }
}
