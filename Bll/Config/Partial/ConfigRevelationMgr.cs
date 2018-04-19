
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class ConfigRevelationMgr
    {

        #region  GetAllFor

        public static List<ConfigRevelationEntity> GetAllFor(string zoneId = "")
        {
            var provider = new ConfigRevelationProvider(zoneId);
            return provider.GetAllFor();
        }

        #endregion		  
		
	}
}
