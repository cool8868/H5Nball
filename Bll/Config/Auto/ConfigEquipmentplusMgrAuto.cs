
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
    /// ConfigEquipmentplus管理类
    /// </summary>
    public static partial class ConfigEquipmentplusMgr
    {
        
		#region  GetById
		
        public static ConfigEquipmentplusEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigEquipmentplusProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigEquipmentplusEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigEquipmentplusProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigEquipmentplusProvider provider = new ConfigEquipmentplusProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigEquipmentplusEntity configEquipmentplusEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEquipmentplusProvider(zoneId);
            return provider.Insert(configEquipmentplusEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigEquipmentplusEntity configEquipmentplusEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEquipmentplusProvider(zoneId);
            return provider.Update(configEquipmentplusEntity,trans);
        }
		
		#endregion	
		
		
	}
}

