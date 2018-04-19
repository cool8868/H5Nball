
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class GambleHostMgr
    {
        #region  GetStartList

        public static List<GambleHostEntity> GetStartList(string zoneId = "")
        {
            var provider = new GambleHostProvider(zoneId);
            return provider.GetStartList();
        }

        #endregion

        #region  InsertOnce

        public static bool InsertOnce(GambleHostEntity e, DbTransaction trans = null, string zoneId = "")
        {
            GambleHostProvider provider = new GambleHostProvider(zoneId);

            return provider.InsertOnce2(e, trans);

        }

        #endregion

        #region  GetByManagerIdTop10

        public static List<GambleHostEntity> GetByManagerIdTop10(System.Guid managerId, string zoneId = "")
        {
            var provider = new GambleHostProvider(zoneId);
            return provider.GetByManagerIdTop10(managerId);
        }

        #endregion		  
	}
}
