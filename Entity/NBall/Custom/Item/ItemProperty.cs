using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom.Item;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom
{
    [DataContract]
    [Serializable]
    [ProtoContract]
    [ProtoInclude(30, typeof(EquipmentProperty))]
    [ProtoInclude(31, typeof(PlayerCardProperty))]
    [ProtoInclude(32, typeof(BallSoulProperty))]
    [ProtoInclude(33, typeof(MallItemProperty))]
    public class ItemProperty
    {
        public virtual ItemProperty Clone()
        {
            return new ItemProperty();
        }

    }
}
