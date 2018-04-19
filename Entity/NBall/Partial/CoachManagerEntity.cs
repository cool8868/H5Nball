using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class CoachManagerEntity
	{
        public CoachManagerEntity(Guid managerId)
        {
            this.BodyAttr = 0;
            this.CoachString = new byte[0];
            this.Defense = 0;
            this.EnableCoachId = 0;
            this.Goalkeeping = 0;
            this.ManagerId = managerId;
            this.Offensive = 0;
            this.Organizational = 0;
            this.RowTime = DateTime.Now;
            this.Status = 0;
            this.UpdateTime = DateTime.Now;
            this.HaveExp = 0;
        }
 
	}
	
	
    public partial class CoachManagerResponse
    {

    }
}
