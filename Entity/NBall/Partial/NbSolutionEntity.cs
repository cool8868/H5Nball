using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Entity.NBall.Custom.Teammember;
using ProtoBuf;

namespace Games.NBall.Entity
{    

	public partial class NbSolutionEntity
	{
        [ProtoMember(31)]
        public int FormationLevel { get; set; }
        /// <summary>
        /// playerId->entity
        /// </summary>
        [ProtoMember(32)]
        public Dictionary<int, SolutionPlayerEntity> PlayerDic { get; set; }
	}
	
	
    public partial class NbSolutionResponse
    {

    }
}

