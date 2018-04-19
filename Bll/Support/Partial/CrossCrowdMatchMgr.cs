
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class CrosscrowdMatchMgr
    {
        public static bool Save(CrosscrowdMatchEntity crowdMatchEntity, DateTime resurrectionTime, DateTime nextMatchTime, DbTransaction trans = null, string zoneId = "")
        {
            var provider = new CrosscrowdMatchProvider(zoneId);
            return provider.Save(crowdMatchEntity, resurrectionTime, nextMatchTime, trans);
        }
	}
}
