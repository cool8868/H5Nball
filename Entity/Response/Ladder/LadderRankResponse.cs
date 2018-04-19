using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Ladder
{
    [Serializable]
    [DataContract]
    public class LadderRankResponse : BaseResponse<LadderRankDataEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class LadderRankDataEntity
    {
        /// <summary>
        /// 我的排名，为-1时显示code=13的消息，即排名太靠后
        /// </summary>
        [DataMember]
        public int MyRank { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        [DataMember]
        public int TotalPage { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public List<LadderRankEntity> Ranks { get; set; }
    }

    [Serializable]
    [DataContract]
    public class LadderRankEntity
    {
        [DataMember]
        public Guid ManagerId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Score { get; set; }

        [DataMember]
        public int Rank { get; set; }
    }
}
