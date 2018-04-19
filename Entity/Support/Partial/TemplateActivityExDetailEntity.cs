using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class TemplateActivityexdetailEntity
	{
        ///<summary>
        ///StartTime
        ///</summary>
        public System.DateTime StartTime { get; set; }

        ///<summary>
        ///EndTime
        ///</summary>
        public System.DateTime EndTime { get; set; }

        public int ZoneActivityId { get; set; }
	}
	
	
    public partial class TemplateActivityexdetailResponse
    {

    }
}

