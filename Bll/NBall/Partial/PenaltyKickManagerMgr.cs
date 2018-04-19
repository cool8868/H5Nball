
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;
using Games.NBall.Entity.Response.Ad;

namespace Games.NBall.Bll
{
    
    public partial class PenaltykickManagerMgr
    {

        #region  GetRank

        public static List<PenaltyKickRankEntity> GetRank(string zoneId = "")
        {
            var provider = new PenaltykickManagerProvider(zoneId);
            return provider.GetRank();
        }

        #endregion		  
		
	}
}
