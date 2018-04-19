using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.Scouting
{
    public class ScoutingLotteryResponse:BaseResponse<ScoutingLotteryEntity>
    {
    }

    /// <summary>
    /// 球探抽奖
    /// </summary>
    [Serializable]
    [DataContract]
    public class ScoutingLotteryEntity
    {
        /// <summary>
        /// 背包是否已满，如果已满显示消息(170)
        /// </summary>
        [DataMember]
        public bool PackageIsFull { get; set; }
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
        /// 所得物品编码,为0表示是10连抽，读itemstring显示列表
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// 所得物品强化等级
        /// </summary>
        [DataMember]
        public int Strength { get; set; }
        /// <summary>
        /// 所得物品是否绑定
        /// </summary>
        [DataMember]
        public bool IsBinding { get; set; }
        /// <summary>
        /// 物品串
        /// </summary>
        [DataMember]
        public string ItemString { get; set; }

        /// <summary>
        /// 可出售的物品串
        /// </summary>
        [DataMember]
        public string DealItemString { get; set; }

        ///<summary>
        ///球探抽卡免费次数
        ///</summary>
        [DataMember]
        public System.Int32 PointScouting { get; set; }
        /// <summary>
        /// 金币免费次数
        /// </summary>
        [DataMember]
        public int CoinScouting { get; set; }

        /// <summary>
        /// 友情点免费次数
        /// </summary>
        [DataMember]
        public int FriendScouting { get; set; }

        /// <summary>
        /// 下次球探抽卡免费截止时间轴
        /// </summary>
        [DataMember]
        public long NextPointScoutingFreeTick { get; set; }

        /// <summary>
        /// 下次金币球探抽卡免费截止时间轴
        /// </summary>
        [DataMember]
        public long NextCoinScoutingFreeTick { get; set; }
        /// <summary>
        /// 下次友情点球探抽卡免费截止时间轴
        /// </summary>
        [DataMember]
        public long NextFriendScoutingFreeTick { get; set; }

        /// <summary>
        /// 还需多少次必得
        /// </summary>
        [DataMember]
        public int NextPointScouting { get; set; }

        /// <summary>
        /// 还需多少次必得
        /// </summary>
        [DataMember]
        public int NextCoinScouting { get; set; }
        /// <summary>
        /// 还需多少次必得
        /// </summary>
        [DataMember]
        public int NextFriendScouting { get; set; }


        /// <summary>
        /// 还需多少次必得
        /// </summary>
        [DataMember]
        public int NextGoldBarScouting { get; set; }
        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
        [DataMember]
        public int AddReiki { get; set; }

        /// <summary>
        /// 幸运币数量
        /// </summary>
        [DataMember]
        public int LuckyCoinNumber { get; set; }

        /// <summary>
        /// 游戏币数量
        /// </summary>
        [DataMember]
        public int GameCurrency { get; set; }

        /// <summary>
        /// 奥运金牌ID
        /// </summary>
        [DataMember]
        public int OlympicTheGoldMedalId { get; set; }
    }

    [Serializable]
    [DataContract]
    public class NLotteryResponse : BaseResponse<NNLotteryResponseEntity>
    {
        
    }

    [Serializable]
    [DataContract]
    public class NNLotteryResponseEntity
    {
        [DataMember]
        public int ItemCode { get; set; }

        [DataMember]
        public int LotteryPoint { get; set; }

        [DataMember]
        public bool IsBinding { get; set; }

        [DataMember]
        public int LotteryCount { get; set; }

        [DataMember]
        public int ManagerPoint { get; set; }

        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }

        [DataMember]
        public int AddReiki { get; set; }
    }
}
