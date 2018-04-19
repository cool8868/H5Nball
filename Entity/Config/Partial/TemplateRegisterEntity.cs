using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class TemplateRegisterEntity
    {/// <summary>
        /// 阵型id
        /// </summary>
        [DataMember]
        public int FormationId { get; set; }

        public int OrangeCount { get; set; }
	}
	
	
    public partial class TemplateRegisterResponse
    {

    }
}

