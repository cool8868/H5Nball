using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom
{
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class PopMessageEntity
    {
        [DataMember]
        [ProtoMember(1)]
        public int PopType { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public string MessageText { get; set; }
    }
}
