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
    public class RankKpiEntity:BaseRankEntity
    {
        /// <summary>
        /// 等级
        /// </summary>
        [DataMember]
        public int Level { get; set; }
        /// <summary>
        /// 综合实力
        /// </summary>
        [DataMember]
        public int Kpi { get; set; }

        public override int GetData()
        {
            return Kpi;
        }

        [DataMember]
        public string Logo { get; set; }

    }
}
