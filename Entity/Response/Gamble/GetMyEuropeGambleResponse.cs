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
    public class GetMyEuropeGambleResponse : BaseResponse<GetMyEuropeGamble>
    {
    }


    [Serializable]
    [DataContract]
    public class GetMyEuropeGamble
    {
        /// <summary>
        /// 竞猜列表
        /// </summary>
        [DataMember]
        public List<EuropeGamblerecordEntity> GambleList { get; set; }
    }

}
