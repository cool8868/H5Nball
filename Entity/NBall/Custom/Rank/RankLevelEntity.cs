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
    public class RankLevelEntity:BaseRankEntity
    {
        /// <summary>
        /// 等级
        /// </summary>
        [DataMember]
        public int Level { get; set; }
        /// <summary>
        /// 经验值
        /// </summary>
        [DataMember]
        public int Exp { get; set; }

        public override int GetData()
        {
            return Level;
        }

        [DataMember]
        public string Logo { get; set; }
    }
}
