
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
    /// DicItem管理类
    /// </summary>
    public static partial class DicItemMgr
    {
        
		#region  GetById
		
        public static DicItemEntity GetById( System.Int32 itemCode,string zoneId="")
        {
            var provider = new DicItemProvider(zoneId);
            return provider.GetById( itemCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicItemEntity> GetAll(string zoneId="")
        {
            var provider = new DicItemProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicItemEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicItemProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  GetContractItem
		
        public static List<DicItemEntity> GetContractItem(string zoneId="")
        {
            var provider = new DicItemProvider(zoneId);
            return provider.GetContractItem();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 itemCode,DbTransaction trans=null,string zoneId="")
        {
            DicItemProvider provider = new DicItemProvider(zoneId);

            return provider.Delete( itemCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicItemEntity dicItemEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicItemProvider(zoneId);
            return provider.Insert(dicItemEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicItemEntity dicItemEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicItemProvider(zoneId);
            return provider.Update(dicItemEntity,trans);
        }
		
		#endregion	
		
		
	}
}

