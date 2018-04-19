
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class LadderHookMgr
    {
        #region  GetList

        public static List<LadderHookEntity> GetList(string zoneId = "")
        {
            var provider = new LadderHookProvider(zoneId);
            return provider.GetList();
        }

        #endregion	
	}
}
