
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
    /// AllPlatmap管理类
    /// </summary>
    public static partial class AllPlatmapMgr
    {
        
		#region  GetById
		
        public static AllPlatmapEntity GetById( System.String platCode,string zoneId="")
        {
            var provider = new AllPlatmapProvider(zoneId);
            return provider.GetById( platCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<AllPlatmapEntity> GetAll(string zoneId="")
        {
            var provider = new AllPlatmapProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String platCode,DbTransaction trans=null,string zoneId="")
        {
            AllPlatmapProvider provider = new AllPlatmapProvider(zoneId);

            return provider.Delete( platCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(AllPlatmapEntity allPlatmapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllPlatmapProvider(zoneId);
            return provider.Insert(allPlatmapEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(AllPlatmapEntity allPlatmapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllPlatmapProvider(zoneId);
            return provider.Update(allPlatmapEntity,trans);
        }
		
		#endregion	
		
		
	}
}

