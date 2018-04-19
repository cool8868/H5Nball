using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Games.NBall.Entity.Response.Gamble
{
    [Serializable]
    [DataContract]
    public class GambleRankListResponse : BaseResponse<List<GambleRankEntity>>
    {
        ///<summary>
        ///我的排名
        ///</summary>
        [DataMember]
        public int MyRank { get; set; }
        ///<summary>
        ///我的盈利数
        ///</summary>
        [DataMember]
        public int MyWinPoints { get; set; }
    }
    [Serializable]
    [DataContract]
    public class GambleDetailListResponse : BaseResponse<List<GambleDetailEntity>>
    {

    }
    [Serializable]
    [DataContract]
    public class GambleHostListResponse : BaseResponse<List<GambleHostEntity>>
    {
        ///<summary>
        ///标识
        ///</summary>
        [DataMember]
        public long RankRewardDate { get; set; }
    }
    [Serializable]
    [DataContract]
    public class GambleTitleListResponse : BaseResponse<List<GambleTitleEntity>>
    {
        /// <summary>
        /// 竞猜列表
        /// </summary>
        [DataMember]
        public List<GambleTitleEntity> List { get; set; }
    }
}
