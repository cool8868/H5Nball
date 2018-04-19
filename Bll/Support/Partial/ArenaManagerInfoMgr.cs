
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class ArenaManagerinfoMgr
    {
         /// <summary>
        /// GetRank
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="domainId"></param>
        /// <param name="zoneId"></param>
        /// <returns>ArenaManagerinfoEntity列表</returns>
        /// <remarks>2016/8/23 17:49:26</remarks>
        public static List<ArenaManagerinfoEntity> GetRank(int pageIndex,int domainId, string zoneId = "")
        {
            var provider = new ArenaManagerinfoProvider(zoneId);
            return provider.GetRank(pageIndex, domainId);
        }


        #region  RefreshOpponent

        public static List<ArenaManagerinfoEntity> RefreshOpponent(System.Int32 danGrading, System.Int32 domainId, string zoneId = "")
        {
            var provider = new ArenaManagerinfoProvider(zoneId);
            return provider.RefreshOpponent(danGrading, domainId);
        }

        #endregion		  
		
	}
}
