
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
    /// DicStarskills管理类
    /// </summary>
    public static partial class DicStarskillsMgr
    {
        
		#region  GetById
		
        public static DicStarskillsEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicStarskillsProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicStarskillsEntity> GetAll(string zoneId="")
        {
            var provider = new DicStarskillsProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicStarskillsEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicStarskillsProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicStarskillsProvider provider = new DicStarskillsProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicStarskillsEntity dicStarskillsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicStarskillsProvider(zoneId);
            return provider.Insert(dicStarskillsEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicStarskillsEntity dicStarskillsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicStarskillsProvider(zoneId);
            return provider.Update(dicStarskillsEntity,trans);
        }
		
		#endregion	
		
		
	}
}

