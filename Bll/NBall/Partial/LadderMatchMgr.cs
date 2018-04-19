
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;
using Games.NBall.Entity.Response.Ladder;

namespace Games.NBall.Bll
{
    
    public partial class LadderMatchMgr
    {
        #region  GetMatchTop10

        public static List<LadderMatchMarqueeEntity> GetMatchTop10()
        {
            var provider = new LadderMatchProvider();
            return provider.GetMatchTop10();
        }

        #endregion		
	}
}

