
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
    /// DicManagertalent管理类
    /// </summary>
    public static partial class DicManagertalentMgr
    {
        
		#region  GetById
		
        public static DicManagertalentEntity GetById( System.String skillCode,string zoneId="")
        {
            var provider = new DicManagertalentProvider(zoneId);
            return provider.GetById( skillCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicManagertalentEntity> GetAll(string zoneId="")
        {
            var provider = new DicManagertalentProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicManagertalentEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicManagertalentProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String skillCode,DbTransaction trans=null,string zoneId="")
        {
            DicManagertalentProvider provider = new DicManagertalentProvider(zoneId);

            return provider.Delete( skillCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicManagertalentEntity dicManagertalentEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicManagertalentProvider(zoneId);
            return provider.Insert(dicManagertalentEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicManagertalentEntity dicManagertalentEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicManagertalentProvider(zoneId);
            return provider.Update(dicManagertalentEntity,trans);
        }
		
		#endregion	
		
		
	}
}

