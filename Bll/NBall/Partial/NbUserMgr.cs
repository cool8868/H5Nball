
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class NbUserMgr
    {
        public static List<PayManagerEntity> GetPayList(string zoneId = "")
        {
            var provider = new PayUserProvider(zoneId);
            return provider.GetPayList();
        }
	}
}

