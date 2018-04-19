
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;
using Games.NBall.Entity.Response.Ladder;

namespace Games.NBall.Bll
{
    
    public partial class CrossladderMatchMgr
    {
        #region  GetMatchTop10

        public static List<LadderMatchMarqueeEntity> GetMatchTop10(int domainId)
        {
            var provider = new CrossladderMatchProvider();
            return provider.GetMatchTop10(domainId);
        }

        #endregion	
	}
}
