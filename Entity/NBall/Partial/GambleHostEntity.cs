using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Games.NBall.Entity
{    

	public partial class GambleHostEntity
	{
        [DataMember]
        [ProtoMember(12)]
        public List<GambleHostoptionrateEntity> RateList { get; set; }

        [DataMember]
        [ProtoMember(13)]
        public string Title { get; set; }

        [DataMember]
        [ProtoMember(14)]
        public long StartedTime { get; set; }

        [DataMember]
        [ProtoMember(15)]
        public long StopedTime { get; set; }

        [DataMember]
        [ProtoMember(16)]
        public string RightOption { get; set; } 
	}
	
	
    public partial class GambleHostResponse
    {

    }
}
