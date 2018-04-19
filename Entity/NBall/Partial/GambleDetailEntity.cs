using System;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Games.NBall.Entity
{    

	public partial class GambleDetailEntity
	{
        ///<summary>
        ///Version
        ///</summary>
        [DataMember]
        [ProtoMember(10)]
        public decimal WinRate { get; set; }

        ///<summary>
        ///Version
        ///</summary>
        [DataMember]
        [ProtoMember(11)]
        public System.String OptionContent { get; set; }
        ///<summary>
        ///Version
        ///</summary>
        [DataMember]
        [ProtoMember(12)]
        public System.String Title { get; set; }

        [DataMember]
        [ProtoMember(13)]
        public string RightOption { get; set; } 
	}
	
	
    public partial class GambleDetailResponse
    {

    }
}
