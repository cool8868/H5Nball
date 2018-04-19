
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
    /// DicBuff管理类
    /// </summary>
    public static partial class DicBuffMgr
    {
        
		#region  GetById
		
        public static DicBuffEntity GetById( System.Int32 buffId,string zoneId="")
        {
            var provider = new DicBuffProvider(zoneId);
            return provider.GetById( buffId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicBuffEntity> GetAll(string zoneId="")
        {
            var provider = new DicBuffProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicBuffEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicBuffProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 buffId,DbTransaction trans=null,string zoneId="")
        {
            DicBuffProvider provider = new DicBuffProvider(zoneId);

            return provider.Delete( buffId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicBuffEntity dicBuffEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicBuffProvider(zoneId);
            return provider.Insert(dicBuffEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicBuffEntity dicBuffEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicBuffProvider(zoneId);
            return provider.Update(dicBuffEntity,trans);
        }
		
		#endregion	
		
		
	}
}

