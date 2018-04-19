
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
    /// ArenaShop管理类
    /// </summary>
    public static partial class ArenaShopMgr
    {
        
		#region  GetById
		
        public static ArenaShopEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new ArenaShopProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ArenaShopEntity> GetAll(string zoneId="")
        {
            var provider = new ArenaShopProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            ArenaShopProvider provider = new ArenaShopProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ArenaShopEntity arenaShopEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaShopProvider(zoneId);
            return provider.Insert(arenaShopEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ArenaShopEntity arenaShopEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaShopProvider(zoneId);
            return provider.Update(arenaShopEntity,trans);
        }
		
		#endregion	
		
		
	}
}
