
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class CrossladderHookMgr
    {
        #region  GetList

        public static List<CrossladderHookEntity> GetList(string zoneId = "")
        {
            var provider = new CrossladderHookProvider(zoneId);
            return provider.GetList();
        }

        #endregion	
	}
}
