using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Config.Custom;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取商城购买点卷
    /// </summary>
    [Serializable]
    [DataContract]
    public class MallGetBuyPointResponse : BaseResponse<MallGetBuyPoint>
    {

    }

    /// <summary>
    /// 商城购买物品
    /// </summary>
    [Serializable]
    [DataContract]
    public class MallGetBuyPoint
    {
        /// <summary>
        /// 钻石列表
        /// </summary>
        [DataMember]
        public List<MallBuyPointInfo> List { get; set; }

        /// <summary>
        /// 礼包列表
        /// </summary>
        [DataMember]
        public List<MallBuyGiftBagInfo> GuftBagList { get; set; }
        /// <summary>
        /// 限时礼包开始时间
        /// </summary>
        [DataMember]
        public long StartDateTime { get; set; }
        /// <summary>
        /// 限时礼包结束时间
        /// </summary>
        [DataMember]
        public long EndDateTime { get; set; }
        /// <summary>
        /// 限时礼包mallcode
        /// </summary>
        [DataMember]
        public int LimitMallCode { get; set; }

        /// <summary>
        /// 节日限时礼包开始时间
        /// </summary>
        [DataMember]
        public long SevenStartDateTime { get; set; }
        /// <summary>
        /// 节日限时礼包结束时间
        /// </summary>
        [DataMember]
        public long SevenEndDateTime { get; set; }
        /// <summary>
        /// 节日礼包时间戳
        /// </summary>
        [DataMember]
        public int SevenMallCode { get; set; }
    }

    [Serializable]
    [DataContract]
    public class MallBuyPointInfo
    {
        /// <summary>
        /// 商城物品code
        /// </summary>
        [DataMember]
        public int MallCode { get; set; }

        /// <summary>
        /// 是否双倍
        /// </summary>
        [DataMember]
        public bool IsDouble { get; set; }
    }
    
    [Serializable]
    [DataContract]
    public class MallBuyGiftBagInfo
    {
        /// <summary>
        /// code
        /// </summary>
        [DataMember]
        public int MallCode{get;set;}

        /// <summary>
        /// 是否可以购买
        /// </summary>
        [DataMember]
        public bool IsHaveBuy{get;set;}

        /// <summary>
        /// 下次可购买时间   周末礼包专用
        /// </summary>
        [DataMember]
        public long NextBuyTick{get;set;}
    }
}
