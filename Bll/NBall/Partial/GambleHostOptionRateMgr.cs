
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class GambleHostoptionrateMgr
    {

        #region  GetByHostId

        public static List<GambleHostoptionrateEntity> GetByHostId2(System.Int32 hostId, string zoneId = "")
        {
            var provider = new GambleHostoptionrateProvider(zoneId);
            return provider.GetByHostId2(hostId);
        }

        #endregion		
	}
}
