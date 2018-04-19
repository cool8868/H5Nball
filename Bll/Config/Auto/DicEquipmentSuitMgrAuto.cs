
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
    /// DicEquipmentsuit管理类
    /// </summary>
    public static partial class DicEquipmentsuitMgr
    {
        
		#region  GetById
		
        public static DicEquipmentsuitEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicEquipmentsuitProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicEquipmentsuitEntity> GetAll(string zoneId="")
        {
            var provider = new DicEquipmentsuitProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicEquipmentsuitEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicEquipmentsuitProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicEquipmentsuitProvider provider = new DicEquipmentsuitProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicEquipmentsuitEntity dicEquipmentsuitEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicEquipmentsuitProvider(zoneId);
            return provider.Insert(dicEquipmentsuitEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicEquipmentsuitEntity dicEquipmentsuitEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicEquipmentsuitProvider(zoneId);
            return provider.Update(dicEquipmentsuitEntity,trans);
        }
		
		#endregion	
		
		
	}
}

