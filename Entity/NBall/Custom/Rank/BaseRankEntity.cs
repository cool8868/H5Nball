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
    public class BaseRankEntity
    {
        /// <summary>
        /// 经理id
        /// </summary>
        [DataMember]
        public Guid ManagerId { get; set; }
        /// <summary>
        /// 经理名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 当前排名
        /// </summary>
        [DataMember]
        public int Rank { get; set; }
        /// <summary>
        /// 昨日排名
        /// </summary>
        [DataMember]
        public int YesterdayRank { get; set; }

        [DataMember]
        public int VipLevel { get; set; }

        public virtual int GetData()
        {
            return 0;
        }

        public virtual int GetExtraData()
        {
            return 0;
        }
    }
}
