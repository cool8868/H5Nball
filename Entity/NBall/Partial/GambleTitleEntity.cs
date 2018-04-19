using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Games.NBall.Entity
{    

	public partial class GambleTitleEntity
	{
        [DataMember]
        [ProtoMember(12)]
        public List<GambleOptionEntity> OptionList { get; set; }

        [DataMember]
        [ProtoMember(13)]
        public long StartedTime { get; set; }

        [DataMember]
        [ProtoMember(14)]
        public long StopedTime { get; set; }
	}
	
	
    public partial class GambleTitleResponse
    {

    }
}
