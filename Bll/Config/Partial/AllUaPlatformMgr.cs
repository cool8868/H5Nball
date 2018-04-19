
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class AllUaplatformMgr
    {
        #region  GetByFactory

        public static List<AllUaplatformEntity> GetByFactory(System.String factoryCode, string zoneId = "")
        {
            var provider = new AllUaplatformProvider();
            return provider.GetByFactory(factoryCode);
        }
        #endregion


        #region  GetById

        public static AllUaplatformEntity GetByCode(string platformCode)
        {
            var provider = new AllUaplatformProvider();
            return provider.GetByCode(platformCode);
        }

        #endregion		  
	}
}

