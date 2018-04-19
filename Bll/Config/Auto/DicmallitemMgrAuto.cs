
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
    /// DicMallitem管理类
    /// </summary>
    public static partial class DicMallitemMgr
    {
        
		#region  GetById
		
        public static DicMallitemEntity GetById( System.Int32 mallCode,string zoneId="")
        {
            var provider = new DicMallitemProvider(zoneId);
            return provider.GetById( mallCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicMallitemEntity> GetAll(string zoneId="")
        {
            var provider = new DicMallitemProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicMallitemEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicMallitemProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 mallCode,DbTransaction trans=null,string zoneId="")
        {
            DicMallitemProvider provider = new DicMallitemProvider(zoneId);

            return provider.Delete( mallCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicMallitemEntity dicMallitemEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicMallitemProvider(zoneId);
            return provider.Insert(dicMallitemEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicMallitemEntity dicMallitemEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicMallitemProvider(zoneId);
            return provider.Update(dicMallitemEntity,trans);
        }
		
		#endregion	
		
		
	}
}

