using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity
{    

	public partial class PlayerkillMatchEntity
	{
        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }

        [DataMember]
        public int VipExp { get; set; }
	}
	
	
    public partial class PlayerkillMatchResponse
    {

    }
}

