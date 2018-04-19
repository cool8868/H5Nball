using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom
{
    /// <summary>
    /// 提升球星技能
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class StarskillPlusEntity
    {
        /// <summary>
        /// 球星技能id
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int StarskillId { get; set; }

        /// <summary>
        /// 加成
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public PropertyPlusEntity PlusEntity { get; set; }

        public StarskillPlusEntity Clone()
        {
            StarskillPlusEntity entity = new StarskillPlusEntity();
            entity.StarskillId = this.StarskillId;
            if (PlusEntity != null)
            {
                entity.PlusEntity = this.PlusEntity.Clone();
            }
            return entity;
        }
    }
}
