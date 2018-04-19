
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class ConfigLeaguefightmapMgr
    {
        #region  GetAll

        public static Dictionary<int, List<int>> GetAllForCache(string zoneId = "")
        {
            var provider = new ConfigLeaguefightmapProvider(zoneId);
            return provider.GetAllForCache();
        }

        #endregion		
	}
}

