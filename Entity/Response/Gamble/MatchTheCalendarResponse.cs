using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Gamble
{
    /// <summary>
    /// 获取欧洲杯竞猜输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class MatchTheCalendarResponse : BaseResponse<MatchTheCalendar>
    {
    }


    [Serializable]
    [DataContract]
    public class MatchTheCalendar
    {
        /// <summary>
        /// 比赛日历
        /// </summary>
        [DataMember]
        public Dictionary<long, bool> TheCalendar;
    }


}
