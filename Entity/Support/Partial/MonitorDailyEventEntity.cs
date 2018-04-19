using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class MonitorDailyeventEntity
	{
        public DateTime NextInvokeTime { get; set; }

        [DataMember]
        public long OpenTimeTick { get; set; }
        [DataMember]
        public long StartTimeTick { get; set; }
        [DataMember]
        public long EndTimeTick { get; set; }
	}
	
	
    public partial class MonitorDailyeventResponse
    {

    }
}
