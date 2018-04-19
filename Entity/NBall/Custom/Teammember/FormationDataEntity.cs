using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom.Teammember
{
    /// <summary>
    /// 阵型数据列表
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class FormationDataListEntity
    {
        public FormationDataListEntity()
        {
            Items = new List<FormationDataEntity>();
        }

        public FormationDataListEntity(byte[] formationData)
        {
            if (formationData.Length == 0)
            {
                Items = new List<FormationDataEntity>();
            }
            else
            {
                var entity = SerializationHelper.FromByte<FormationDataListEntity>(formationData);
                Items = entity.Items;
            }
        }
        /// <summary>
        /// 阵型列表
        /// </summary>
        [ProtoMember(1)]
        public List<FormationDataEntity> Items { get; set; }

        /// <summary>
        /// 获取等级
        /// </summary>
        /// <param name="formationId"></param>
        /// <returns></returns>
        public int GetLevel(int formationId)
        {
            var item = Items.Find(d => d.Idx == formationId);
            if (item == null)
                return 1;
            else
            {
                return item.Level;
            }
        }

        /// <summary>
        /// 阵型升级
        /// </summary>
        /// <param name="formationId"></param>
        public void Levelup(int formationId)
        {
            if (Items == null)
                Items = new List<FormationDataEntity>();
            var entity = Items.Find(d => d.Idx == formationId);
            if (entity == null)
            {
                Items.Add(new FormationDataEntity() { Idx = formationId, Level = 2 });
            }
            else
            {
                entity.Level++;
            }
        }
    }

    /// <summary>
    /// 阵型数据
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class FormationDataEntity
    {
        /// <summary>
        /// 阵型id
        /// </summary>
        [ProtoMember(1)]
        public int Idx { get; set; }
        /// <summary>
        /// 阵型等级
        /// </summary>
        [ProtoMember(2)]
        public int Level { get; set; }
    }
}
