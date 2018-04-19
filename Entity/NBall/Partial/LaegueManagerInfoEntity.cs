using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity
{    

	public partial class LaegueManagerinfoEntity
	{
        /// <summary>
        /// 刷新需要点券
        /// </summary>
        [DataMember]
        public int RefreshPoint { get; set; }

        /// <summary>
        /// 下次刷新时间 
        /// </summary>
        [DataMember]
        public long NextRefreshTick { get; set; }

        /// <summary>
        /// 装备属性
        /// </summary>
        [DataMember]
        public List<EquipmentProperty> AllEquipmentProperties { get; set; }
	}
	
	
    public partial class LaegueManagerinfoResponse
    {

    }


    [DataContract]
    [Serializable]
    public class LaegueRefreshExchangeResponse : BaseResponse<LeagueRefreshExchangeEntity>
    {
    }

    [DataContract]
    [Serializable]
    public class LeagueRefreshExchangeEntity
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
        /// 当前积分
        /// </summary>
        [DataMember]
        public int SumScore { get; set; }

        /// <summary>
        /// 下次刷新时间
        /// </summary>
        [DataMember]
        public long NextRefreshTick { get; set; }

        /// <summary>
        /// 装备属性
        /// </summary>
        [DataMember]
        public List<EquipmentProperty> AllEquipmentProperties { get; set; }

    }

}

