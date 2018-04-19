
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
    /// AllSitemap管理类
    /// </summary>
    public static partial class AllSitemapMgr
    {
        
		#region  GetById
		
        public static AllSitemapEntity GetById( System.Int32 id,string zoneId="")
        {
            var provider = new AllSitemapProvider(zoneId);
            return provider.GetById( id);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<AllSitemapEntity> GetAll(string zoneId="")
        {
            var provider = new AllSitemapProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 id,DbTransaction trans=null,string zoneId="")
        {
            AllSitemapProvider provider = new AllSitemapProvider(zoneId);

            return provider.Delete( id,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(AllSitemapEntity allSitemapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllSitemapProvider(zoneId);
            return provider.Insert(allSitemapEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(AllSitemapEntity allSitemapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllSitemapProvider(zoneId);
            return provider.Update(allSitemapEntity,trans);
        }
		
		#endregion	
		
		
	}
}

