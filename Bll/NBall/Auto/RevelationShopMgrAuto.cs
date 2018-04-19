
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
    /// RevelationShop管理类
    /// </summary>
    public static partial class RevelationShopMgr
    {
        
		#region  GetById
		
        public static RevelationShopEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new RevelationShopProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RevelationShopEntity> GetAll(string zoneId="")
        {
            var provider = new RevelationShopProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            RevelationShopProvider provider = new RevelationShopProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RevelationShopEntity revelationShopEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationShopProvider(zoneId);
            return provider.Insert(revelationShopEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RevelationShopEntity revelationShopEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationShopProvider(zoneId);
            return provider.Update(revelationShopEntity,trans);
        }
		
		#endregion	
		
		
	}
}
