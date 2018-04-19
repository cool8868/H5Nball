using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.Activity
{

    /// <summary>
    /// 签到奖励
    /// </summary>
    [Serializable]
    [DataContract]
    public class DailyAttendancePrizeResponse : BaseResponse<DailyAttendancePrize>
    {
    }

    /// <summary>
    /// 签到奖励
    /// </summary>
    [Serializable]
    [DataContract]
    public class DailyAttendancePrize
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public ConfigDaysattendprizeEntity Prize { get; set; }
    }
}
