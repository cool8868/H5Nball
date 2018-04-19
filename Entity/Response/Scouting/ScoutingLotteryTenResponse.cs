using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Scouting
{
    public class ScoutingLotteryTenResponse : BaseResponse<ScoutingLotteryTenEntity>
    {
    }

    /// <summary>
    /// 球探十连抽
    /// </summary>
    [Serializable]
    [DataContract]
    public class ScoutingLotteryTenEntity
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
        /// 所得物品编码，逗号分隔
        /// </summary>
        [DataMember]
        public string ItemCodes { get; set; }
    }
}
