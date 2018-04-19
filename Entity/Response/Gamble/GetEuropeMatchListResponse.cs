using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Gamble
{
    /// <summary>
    /// 获取欧洲杯比赛列表输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class GetEuropeMatchListResponse:BaseResponse<GetEuropeMatchList>
    {
    }


    [Serializable]
    [DataContract]
    public class GetEuropeMatchList
    {
        /// <summary>
        /// 比赛列表
        /// </summary>
        [DataMember]
        public List<EuropeMatchEntity> MatchList { get; set; }

        /// <summary>
        /// 比赛日期时间轴
        /// </summary>
        [DataMember]
        public long MatchDateTick { get; set; }
    }
}
