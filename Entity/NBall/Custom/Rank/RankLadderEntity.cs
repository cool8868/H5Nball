using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.NBall.Custom.Rank
{
    [DataContract]
    [Serializable]
    public class RankLadderEntity:BaseRankEntity
    {
        /// <summary>
        /// 天梯积分
        /// </summary>
        [DataMember]
        public int Score { get; set; }
        /// <summary>
        /// 综合实力
        /// </summary>
        [DataMember]
        public int Kpi { get; set; }

        public override int GetData()
        {
            return Score;
        }

        [DataMember]
        public string Logo { get; set; }

    }

    [DataContract]
    [Serializable]
    public class RankCrossLadderEntity : BaseRankEntity
    {
        /// <summary>
        /// 天梯积分
        /// </summary>
        [DataMember]
        public int Score { get; set; }
        /// <summary>
        /// 区名
        /// </summary>
        [DataMember]
        public string SiteName { get; set; }
        /// <summary>
        /// 区名
        /// </summary>
        [DataMember]
        public string SiteId { get; set; }
        [DataMember]
        public int Kpi { get; set; }
        [DataMember]
        public string Logo { get; set; }

        public override int GetData()
        {
            return Score;
        }
    }
}
