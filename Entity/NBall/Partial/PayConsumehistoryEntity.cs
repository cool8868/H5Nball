using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class PayConsumehistoryEntity
	{
	}
	
	
    public partial class PayConsumehistoryResponse
    {

    }
    public class PayConsumeManagerEntity
    {
        public Guid ManagerId { get; set; }

        public int Point { get; set; }
    }
}

