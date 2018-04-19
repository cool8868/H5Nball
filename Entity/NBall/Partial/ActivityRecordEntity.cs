using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Common;

namespace Games.NBall.Entity
{    

	public partial class ActivityRecordEntity
	{

        /// <summary>
        /// 倒计时/充值点券数量
        /// </summary>
        [DataMember]
        public int Countdown { get; set; }

        private int managerCoin = -1;
        /// <summary>
        /// 经理金币，-1不更新
        /// </summary>
        [DataMember]
        public int ManagerCoin { get { return managerCoin; } set { managerCoin = value; } }

        /// <summary>
        /// 是否需要同步
        /// </summary>
        public bool NeedSync { get; set; }

        #region Online

        public int OnlinePrevSeconds { get { return ConvertHelper.ConvertToInt(StepRecord); } set { StepRecord = value.ToString(); } }

        public int CurOnlineSeconds { get; set; }
        #endregion
        /// <summary>
        /// 奖励列表
        /// </summary>
        [DataMember]
        public List<ActivityPrizeEntity> PrizeList { get; set; }

        /// <summary>
        /// 竞技场类型
        /// </summary>
        [DataMember]
        public int ArenaType { get; set; }

        /// <summary>
        /// 传奇卡CODE
        /// </summary>
        [DataMember]
        public int LegendCode { get; set; }

        /// <summary>
        /// 传奇碎片code
        /// </summary>
        [DataMember]
        public int LegendDebrisCode { get; set; }
	}
	
	
    public partial class ActivityRecordResponse
    {

    }

    [Serializable]
    [DataContract]
    public class ActivityPrizeEntity
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        [DataMember]
        public int ActivityId { get; set; }
        /// <summary>
        /// 活动分组
        /// </summary>
        [DataMember]
        public int ActivityStep { get; set; }
        ///<summary>
        ///奖励类型，1：金币；2：物品；3：点券  6：动态合同页  7动态球员卡 8 绑定点券
        ///</summary>
        [DataMember]
        public System.Int32 Type { get; set; }

        ///<summary>
        ///ItemCode
        ///</summary>
        [DataMember]
        public System.Int32 ItemCode { get; set; }

        ///<summary>
        ///Strength
        ///</summary>
        [DataMember]
        public System.Int32 Strength { get; set; }

        ///<summary>
        ///Count
        ///</summary>
        [DataMember]
        public System.Int32 Count { get; set; }

        ///<summary>
        ///IsBinding
        ///</summary>
        [DataMember]
        public System.Boolean IsBinding { get; set; }

        /// <summary>
        /// 彩孔数量
        /// </summary>
        public int SlotColorCount { get; set; }
    }

}

