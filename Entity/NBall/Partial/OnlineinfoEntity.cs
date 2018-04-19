using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class OnlineInfoEntity
    {
        public OnlineInfoEntity(Guid managerId, DateTime curTime)
        {
            ManagerId = managerId;
            LoginTime = curTime;
            ActiveTime = curTime;
            GuildInTime = curTime;
            RowTime = curTime;
        }

        public static int TIMEOffLineMinutes = 3;

        public bool IsOnline
        {
            get
            {
                int inactMinutes = (int)DateTime.Now.Subtract(ActiveTime).TotalMinutes;
                return inactMinutes < TIMEOffLineMinutes;
            }
        }

        public long RealTotalMinutes { get { return TotalOnlineMinutes + TodayMinutes; } }

        public int TodayMinutes { get { return CurOnlineMinutes + CntOnlineMinutes; } }

        public bool IsHandlerCrossDay { get; set; }
	}
	
	
    public partial class OnlineInfoResponse
    {

    }
}

