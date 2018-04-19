
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
    /// AllDatabase管理类
    /// </summary>
    public static partial class AllDatabaseMgr
    {
        
		#region  GetById
		
        public static AllDatabaseEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new AllDatabaseProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<AllDatabaseEntity> GetAll(string zoneId="")
        {
            var provider = new AllDatabaseProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            AllDatabaseProvider provider = new AllDatabaseProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(AllDatabaseEntity allDatabaseEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllDatabaseProvider(zoneId);
            return provider.Insert(allDatabaseEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(AllDatabaseEntity allDatabaseEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllDatabaseProvider(zoneId);
            return provider.Update(allDatabaseEntity,trans);
        }
		
		#endregion	
		
		
	}
}

