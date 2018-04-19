
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class PayConsumehistoryMgr
    {
        public static List<PayConsumeManagerEntity> GetPointList(System.DateTime startTime, System.DateTime endTime)
        {
            var provider = new PayConsumehistoryProvider();
            return provider.GetPointList(startTime, endTime);
        }

        public static List<PayConsumeManagerEntity> GetEqLockPointList(System.DateTime startTime, System.DateTime endTime)
        {
            var provider = new PayConsumehistoryProvider();
            return provider.GetEqLockPointList(startTime, endTime);
        }


        public static bool GetEqLockPointForActivity(System.Guid managerId, System.DateTime startTime, System.DateTime endTime, ref  System.Int32 point)
        {
            var provider = new PayConsumehistoryProvider();
            return provider.GetEqLockPointForActivity(managerId, startTime, endTime, ref point);
        }
	}
}

