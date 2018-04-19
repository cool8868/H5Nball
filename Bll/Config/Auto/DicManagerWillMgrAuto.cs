
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
    /// DicManagerwill管理类
    /// </summary>
    public static partial class DicManagerwillMgr
    {
        
		#region  GetById
		
        public static DicManagerwillEntity GetById( System.String skillCode,string zoneId="")
        {
            var provider = new DicManagerwillProvider(zoneId);
            return provider.GetById( skillCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicManagerwillEntity> GetAll(string zoneId="")
        {
            var provider = new DicManagerwillProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicManagerwillEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicManagerwillProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String skillCode,DbTransaction trans=null,string zoneId="")
        {
            DicManagerwillProvider provider = new DicManagerwillProvider(zoneId);

            return provider.Delete( skillCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicManagerwillEntity dicManagerwillEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicManagerwillProvider(zoneId);
            return provider.Insert(dicManagerwillEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicManagerwillEntity dicManagerwillEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicManagerwillProvider(zoneId);
            return provider.Update(dicManagerwillEntity,trans);
        }
		
		#endregion	
		
		
	}
}

