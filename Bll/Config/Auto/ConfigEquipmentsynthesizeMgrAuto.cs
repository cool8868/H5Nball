
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
    /// ConfigEquipmentsynthesize管理类
    /// </summary>
    public static partial class ConfigEquipmentsynthesizeMgr
    {
        
		#region  GetById
		
        public static ConfigEquipmentsynthesizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigEquipmentsynthesizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigEquipmentsynthesizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigEquipmentsynthesizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigEquipmentsynthesizeProvider provider = new ConfigEquipmentsynthesizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigEquipmentsynthesizeEntity configEquipmentsynthesizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEquipmentsynthesizeProvider(zoneId);
            return provider.Insert(configEquipmentsynthesizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigEquipmentsynthesizeEntity configEquipmentsynthesizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEquipmentsynthesizeProvider(zoneId);
            return provider.Update(configEquipmentsynthesizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

