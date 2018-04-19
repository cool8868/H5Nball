
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class AllAppMgr
    {
        public static List<AllAppEntity> GetAllForFactory()
        {
            var provider = new AllAppProvider();
            return provider.GetAllForFactory();
        }
	}
}

