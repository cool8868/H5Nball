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
    public class RankCrowdEntity : BaseRankEntity
    {
        /// <summary>
        /// 积分
        /// </summary>
        [DataMember]
        public int Score { get; set; }
        /// <summary>
        /// 击杀数量
        /// </summary>
        [DataMember]
        public int KillCount { get; set; }

        public override int GetData()
        {
            return Score;
        }

        public override int GetExtraData()
        {
            return KillCount;
        }
    }

    [DataContract]
    [Serializable]
    public class RankCrossCrowdEntity : BaseRankEntity
    {
        [DataMember]
        public int Kpi { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        [DataMember]
        public int Score { get; set; }
        /// <summary>
        /// 击杀数量
        /// </summary>
        [DataMember]
        public int KillCount { get; set; }
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
        public override int GetData()
        {
            return Score;
        }

        public override int GetExtraData()
        {
            return KillCount;
        }
    }
}
