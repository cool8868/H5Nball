using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class CrossladderHookEntity
	{
        public int Score { get; set; }

        public CrossladderManagerEntity LadderManager { get; set; }
	}
	
	
    public partial class CrossladderHookResponse
    {

    }
}
