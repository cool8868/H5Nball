
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
    /// DicActivity管理类
    /// </summary>
    public static partial class DicActivityMgr
    {
        
		#region  GetById
		
        public static DicActivityEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicActivityProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicActivityEntity> GetAll(string zoneId="")
        {
            var provider = new DicActivityProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicActivityEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicActivityProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicActivityProvider provider = new DicActivityProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicActivityEntity dicActivityEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicActivityProvider(zoneId);
            return provider.Insert(dicActivityEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicActivityEntity dicActivityEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicActivityProvider(zoneId);
            return provider.Update(dicActivityEntity,trans);
        }
		
		#endregion	
		
		
	}
}

