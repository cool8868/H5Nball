
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class GambleDetailMgr
    {
        #region  GetByManagerIdTop10

        public static List<GambleDetailEntity> GetByManagerIdTop10(System.Guid managerId, string zoneId = "")
        {
            var provider = new GambleDetailProvider(zoneId);
            return provider.GetByManagerIdTop10(managerId);
        }

        #endregion	
	}
}
