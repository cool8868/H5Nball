
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
    /// DicEquipment管理类
    /// </summary>
    public static partial class DicEquipmentMgr
    {
        
		#region  GetById
		
        public static DicEquipmentEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicEquipmentProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicEquipmentEntity> GetAll(string zoneId="")
        {
            var provider = new DicEquipmentProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicEquipmentEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicEquipmentProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicEquipmentProvider provider = new DicEquipmentProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicEquipmentEntity dicEquipmentEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicEquipmentProvider(zoneId);
            return provider.Insert(dicEquipmentEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicEquipmentEntity dicEquipmentEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicEquipmentProvider(zoneId);
            return provider.Update(dicEquipmentEntity,trans);
        }
		
		#endregion	
		
		
	}
}

