using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom.Rank;
using Games.NBall.Entity.Response.Ladder;

namespace Games.NBall.Entity.Response.Rank
{
    public class RankResponse:BaseResponse<RankDataEntity>
    {
    }

        [Serializable]
        [DataContract]
        [KnownType(typeof(RankLadderEntity))]
        [KnownType(typeof(RankLevelEntity))]
        [KnownType(typeof(RankKpiEntity))]
        [KnownType(typeof(RankScoreEntity))]
        [KnownType(typeof(RankArenaEntity))]
        [KnownType(typeof(RankCrowdEntity))]
        [KnownType(typeof(RankCrossCrowdEntity))]
        [KnownType(typeof(RankCrossLadderEntity))]
        [KnownType(typeof(RankDialEntity))]
        public class RankDataEntity
        {
            /// <summary>
            /// 排名类型，H5端响应慢时需标记显示
            /// </summary>
            [DataMember]
            public int RankType { get; set; }

            /// <summary>
            /// 我的排名，为-1时显示code=13的消息，即排名太靠后
            /// </summary>
            [DataMember]
            public int MyRank { get; set; }
            /// <summary>
            /// 我的数据，按不同排行榜显示不同
            /// </summary>
            [DataMember]
            public int MyData { get; set; }
            /// <summary>
            /// 我的额外数据
            /// </summary>
            [DataMember]
            public int MyExtra { get; set; }
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
            /// <summary>
            /// 下次更新时间
            /// </summary>
            [DataMember]
            public long NextTimeTick { get; set; }
            /// <summary>
            /// 排名列表
            /// </summary>
            [DataMember]
            public List<BaseRankEntity> Ranks { get; set; }

            /// <summary>
            /// 我的区名  跨服用
            /// </summary>
            [DataMember]
            public string MyZoneName { get; set; }
        }
}
