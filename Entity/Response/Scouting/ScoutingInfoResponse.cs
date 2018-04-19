using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Scouting
{
    /// <summary>
    /// 球探信息响应
    /// </summary>
    public class ScoutingInfoResponse : BaseResponse<ScoutingInfoEntity>
    {
    }

    /// <summary>
    /// 球探信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ScoutingInfoEntity
    {
        /// <summary>
        /// 显示列表
        /// </summary>
        [DataMember]
        public List<ConfigScoutingDataEntity> ShowList { get; set; }

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
        /// 当前经理友情点
        /// </summary>
        [DataMember]
        public int FriendShipPoint { get; set; }

        /// <summary>
        /// 金条数量
        /// </summary>
        [DataMember]
        public int GoldBar { get; set; }

        /// <summary>
        /// 金币倒计时
        /// </summary>
        [DataMember]
        public long CoinFreeTimeTick { get; set; }

        /// <summary>
        /// 钻石倒计时
        /// </summary>
        [DataMember]
        public long PointFreeTimeTick { get; set; }

        /// <summary>
        /// 友情点倒计时
        /// </summary>
        [DataMember]
        public long FriendShipPointFreeTimeTick { get; set; }

        /// <summary>
        /// 金币10次还剩几次
        /// </summary>
        [DataMember]
        public int CoinNeedCount { get; set; }

        /// <summary>
        /// 点券10次还剩几次
        /// </summary>
        [DataMember]
        public int PointNeedCount { get; set; }

        /// <summary>
        /// 友情点10次还剩几次
        /// </summary>
        [DataMember]
        public int FriendShipPointNeedCount { get; set; }


        /// <summary>
        /// 金条10次还剩几次
        /// </summary>
        [DataMember]
        public int GoldBarNeedCount { get; set; }

    }




    /// <summary>
    /// 球探数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class ConfigScoutingDataEntity
    {
        /// <summary>
        /// 球探id
        /// </summary>
        [DataMember]
        public int Idx { get; set; }
        /// <summary>
        /// 货币类型，1：点券；2，金币，9友情点
        /// </summary>
        [DataMember]
        public int CurrencyType { get; set; }
        /// <summary>
        /// 货币数量
        /// </summary>
        [DataMember]
        public int CurrencyCount { get; set; }
    }
}
