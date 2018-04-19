using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Entity.Response.Ladder;
using ProtoBuf;

namespace Games.NBall.Entity
{    

	public partial class LadderHookEntity
	{
        public int Score { get; set; }

        public LadderManagerEntity LadderManager { get; set; }
	}
	
	
    public partial class LadderHookResponse
    {

    }
}
