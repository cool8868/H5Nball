using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 巡回赛抽奖返回
    /// </summary>
    public class TourLotteryResponse : BaseResponse<TourLotteryEntity>
    {

    }

    /// <summary>
    /// 巡回赛抽奖
    /// </summary>
    [Serializable]
    [DataContract]
    public class TourLotteryEntity
    {
        /// <summary>
        /// 背包是否已满，为true时需提示code=170的消息
        /// </summary>
        [DataMember]
        public bool IsPackageFull { get; set; }

        /// <summary>
        /// 物品code
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// 物品数量
        /// </summary>
        [DataMember]
        public int ItemCount { get; set; }
        /// <summary>
        /// 装备属性
        /// </summary>
        [DataMember]
        public EquipmentProperty EquipmentProperty { get; set; }

        [DataMember]
        public bool IsBinding { get; set; }

        /// <summary>
        /// 奥运金牌ID
        /// </summary>
        [DataMember]
        public int OlympicTheGoldMedalId { get; set; }
    }

    /// <summary>
    /// 巡回赛抽奖返回
    /// </summary>
    public class TourMailLotteryResponse : BaseResponse<TourMailLotteryEntity>
    {

    }

    /// <summary>
    /// 巡回赛抽奖
    /// </summary>
    [Serializable]
    [DataContract]
    public class TourMailLotteryEntity
    {
        /// <summary>
        /// 物品code
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        ///<summary>
        ///抽奖数据，物品ID逗号分隔
        ///</summary>
        [DataMember]
        public string PrizeItemString { get; set; }
    }
}
