
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class AllZoneinfoMgr
    {
        public static List<AllZoneinfoEntity> GetAllForFactory(string zoneId = "")
        {
            var provider = new AllZoneinfoProvider();
            return provider.GetAllForFactory();
        }

        #region  GetByPlatform

        public static List<AllZoneinfoEntity> GetByPlatform(System.String platformCode, string zoneId = "")
        {
            var provider = new AllZoneinfoProvider();
            return provider.GetByPlatform(platformCode);
        }

        #endregion	
	}
}

