
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
    /// DicGrow管理类
    /// </summary>
    public static partial class DicGrowMgr
    {
        
		#region  GetById
		
        public static DicGrowEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicGrowProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicGrowEntity> GetAll(string zoneId="")
        {
            var provider = new DicGrowProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicGrowEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicGrowProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicGrowProvider provider = new DicGrowProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicGrowEntity dicGrowEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicGrowProvider(zoneId);
            return provider.Insert(dicGrowEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicGrowEntity dicGrowEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicGrowProvider(zoneId);
            return provider.Update(dicGrowEntity,trans);
        }
		
		#endregion	
		
		
	}
}

