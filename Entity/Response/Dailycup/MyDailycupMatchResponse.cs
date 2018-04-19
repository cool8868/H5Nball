using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Dailycup
{
    /// <summary>
    /// 我的杯赛历程响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class MyDailycupMatchResponse : BaseResponse<MyDailycupMatchEntity>
    {
    }

    /// <summary>
    ///  我的杯赛历程
    /// </summary>
    [Serializable]
    [DataContract]
    public class MyDailycupMatchEntity
    {
        /// <summary>
        /// 比赛列表
        /// </summary>
        [DataMember]
        public List<DailycupMatchEntity> Matchs { get; set; }
    }
}
