
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
    /// AllApp管理类
    /// </summary>
    public static partial class AllAppMgr
    {
        
		#region  GetById
		
        public static AllAppEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new AllAppProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<AllAppEntity> GetAll(string zoneId="")
        {
            var provider = new AllAppProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            AllAppProvider provider = new AllAppProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(AllAppEntity allAppEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllAppProvider(zoneId);
            return provider.Insert(allAppEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(AllAppEntity allAppEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllAppProvider(zoneId);
            return provider.Update(allAppEntity,trans);
        }
		
		#endregion	
		
		
	}
}

