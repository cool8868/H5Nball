using System;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Games.NBall.Entity
{    

	public partial class GambleHostoptionrateEntity
	{
        ///<summary>
        ///OptionContent
        ///</summary>
        [DataMember]
        [ProtoMember(10)]
        public System.String OptionContent { get; set; }

        ///<summary>
        ///TeamIcon  标志
        ///</summary>
        [DataMember]
        [ProtoMember(11)]
        public int TeamIcon { get; set; }
	}
	
	
    public partial class GambleHostoptionrateResponse
    {

    }
}
