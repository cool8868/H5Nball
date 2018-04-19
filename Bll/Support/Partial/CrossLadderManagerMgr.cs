
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class CrossladderManagerMgr
    {
        #region  GetAll

        public static List<CrossladderDailyHonor> GetDailyHonor(string zoneId = "")
        {
            var provider = new CrossladderManagerProvider(zoneId);
            return provider.GetDailyHonor();
        }

        #endregion		  
	}
}
