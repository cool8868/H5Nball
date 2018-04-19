using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Games.NBall.Entity.NBall.Custom.Rank
{
    /// <summary>
    /// 竞技场排行
    /// </summary>
    [DataContract]
    [Serializable]
    public class RankArenaEntity : BaseRankEntity
    {
        /// <summary>
        /// 声望
        /// </summary>
        [DataMember]
        public int Integral { get; set; }
        /// <summary>
        /// 区名
        /// </summary>
        [DataMember]
        public string ZoneName { get; set; }
        /// <summary>
        /// 区名
        /// </summary>
        [DataMember]
        public string SiteId { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        [DataMember]
        public string Logo { get; set; }

        public override int GetData()
        {
            return Integral;
        }
    }
}
