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
    /// 商城购买物品响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class MallBuyItemResponse : BaseResponse<MallBuyItemEntity>
    {

    }

    /// <summary>
    /// 商城购买物品
    /// </summary>
    [Serializable]
    [DataContract]
    public class MallBuyItemEntity
    {
        /// <summary>
        /// 货币类型，1：点券；2，金币；
        /// </summary>
        [DataMember]
        public int CurrencyType { get; set; }

        /// <summary>
        /// 当前货币数量，对应货币类型更新
        /// </summary>
        [DataMember]
        public int CurCurrency { get; set; }

        /// <summary>
        /// 1,扩展背包;2,重置精英巡回赛;3,加速训练;4,增加训练位;5,增加替补席;6,增加体力;
        /// </summary>
        [DataMember]
        public int ExtraType { get; set; }

        /// <summary>
        /// 购买后该项的值，如扩展背包，则为最新的背包格数
        /// </summary>
        [DataMember]
        public int ExtraResultValue { get; set; }
    }

    /// <summary>
    /// 商城购买物品响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class MallBuyPointResponse : BaseResponse<MallBuyPoint>
    {

    }

    [Serializable]
    [DataContract]
    public class MallBuyPoint
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public string OrderId { get; set; }

        /// <summary>
        /// 购买的物品code
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// SessionId
        /// </summary>
        [DataMember]
        public string SessionId { get; set; }

        /// <summary>
        /// 钱
        /// </summary>
        [DataMember]
        public int Cash { get; set; }
        /// <summary>
        /// 物品名字
        /// </summary>
        [DataMember]
        public string ItemName { get; set; }
        /// <summary>
        /// 用户最后登录的ip
        /// </summary>
        [DataMember]
        public string IP { get; set; }

        /// <summary>
        /// ext
        /// </summary>
        [DataMember]
        public string Ext { get; set; }
        /// <summary>
        /// channelAlias= pf渠道简称
        /// </summary>
        [DataMember]
        public string ChannelAlias { get; set; }
        /// <summary>
        /// 服务器id
        /// </summary>
       [DataMember]
        public string ServerId { get; set; }

        [DataMember]
       public string AccountId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int itemCount { get; set; }

        [DataMember]
        public string ServerName{get; set; }
        /// <summary>
        /// 公用字段
        /// </summary>
        [DataMember]
        public string StrCommon { get; set; }
        /// <summary>
        /// 是否余额不足，需要跳转tx付款接口
        /// </summary>
        [DataMember]
        public bool IsJump { get; set; }

        [DataMember]
        public bool Is_vip { get; set; }
        [DataMember]
        public string OpenKey { get; set; }
        [DataMember]
        public string OpenId { get; set; }
        [DataMember]
        public string AppId { get; set; }
        /// <summary>
        /// 玩吧传递系统zoneid，安卓1，ios2
        /// </summary>
        [DataMember]
        public string ZoneId { get; set; }
        [DataMember]
        public int Score { get; set; }
        [DataMember]
        public string DefaultScore { get; set; }

        [DataMember]
        public NbManagerEntity DateEntity { get; set; }
    }


    /// <summary>
    /// 商城购买物品响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class MallTxBuyPointResponse : BaseResponse<MallTxBuyPoint>
    {

    }

    [Serializable]
    [DataContract]
    public class MallTxBuyPoint
    {
        /// <summary>
        /// 物品名字
        /// </summary>
        [DataMember]
        public string ItemName { get; set; }

        /// <summary>
        /// 商城物品ID
        /// </summary>
        [DataMember]
        public int MallCode { get; set; }

        /// <summary>
        /// 价格  分
        /// </summary>
        [DataMember]
        public int Cash { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public string OpenId { get; set; }

        /// <summary>
        /// 1 Android、2 IOS
        /// </summary>
        [DataMember]
        public string ZoneId { get; set; }

        /// <summary>
        /// 游戏道具ID
        /// </summary>
        [DataMember]
        public string ItemId { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        [DataMember]
        public int Count { get; set; }

        
        [DataMember]
        public string Openkey { get; set; }

        [DataMember]
        public string Pf { get; set; }

    }


    /// <summary>
    /// 商城购买物品响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class MallTxBuyPointResultResponse : BaseResponse<MallTxBuyPointResult>
    {

    }

    [Serializable]
    [DataContract]
    public class MallTxBuyPointResult
    {
        /// <summary>
        /// 总点卷数量
        /// </summary>
        [DataMember]
        public int AddPoint { get; set; }
    }


}
