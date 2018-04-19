
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
    /// AllZoneinfo管理类
    /// </summary>
    public static partial class AllZoneinfoMgr
    {
        
		#region  GetById
		
        public static AllZoneinfoEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new AllZoneinfoProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<AllZoneinfoEntity> GetAll(string zoneId="")
        {
            var provider = new AllZoneinfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            AllZoneinfoProvider provider = new AllZoneinfoProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(AllZoneinfoEntity allZoneinfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllZoneinfoProvider(zoneId);
            return provider.Insert(allZoneinfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(AllZoneinfoEntity allZoneinfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllZoneinfoProvider(zoneId);
            return provider.Update(allZoneinfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}

