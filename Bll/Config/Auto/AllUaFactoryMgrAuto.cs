
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    /// <summary>
    /// AllUafactory管理类
    /// </summary>
    public static partial class AllUafactoryMgr
    {
        
		#region  GetById
		
        public static AllUafactoryEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new AllUafactoryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<AllUafactoryEntity> GetAll(string zoneId="")
        {
            var provider = new AllUafactoryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            AllUafactoryProvider provider = new AllUafactoryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(AllUafactoryEntity allUafactoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllUafactoryProvider(zoneId);
            return provider.Insert(allUafactoryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(AllUafactoryEntity allUafactoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllUafactoryProvider(zoneId);
            return provider.Update(allUafactoryEntity,trans);
        }
		
		#endregion	
		
		
	}
}

