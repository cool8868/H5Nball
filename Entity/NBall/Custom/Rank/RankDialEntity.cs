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
    public class RankDialEntity : BaseRankEntity
    {
        /// <summary>
        /// 盈亏
        /// </summary>
        [DataMember]
        public long Score { get; set; }
        /// <summary>
        /// 区名
        /// </summary>
        [DataMember]
        public string SiteName { get; set; }
        /// <summary>
        /// 区号
        /// </summary>
        [DataMember]
        public string SiteId { get; set; }

        public override int GetData()
        {
            return (int)Score;
        }
    }
}
