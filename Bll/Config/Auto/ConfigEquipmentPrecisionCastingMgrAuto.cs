
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
    /// ConfigEquipmentprecisioncasting管理类
    /// </summary>
    public static partial class ConfigEquipmentprecisioncastingMgr
    {
        
		#region  GetById
		
        public static ConfigEquipmentprecisioncastingEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigEquipmentprecisioncastingProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigEquipmentprecisioncastingEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigEquipmentprecisioncastingProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigEquipmentprecisioncastingProvider provider = new ConfigEquipmentprecisioncastingProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Update
		
        public static bool Update ( System.Int32 idx, System.Int32 equipmentQuality, System.Int32 propertyQuality, System.Int32 propertyType, System.Int32 rateMin, System.Int32 rateMax,DbTransaction trans=null,string zoneId="")
        {
            ConfigEquipmentprecisioncastingProvider provider = new ConfigEquipmentprecisioncastingProvider(zoneId);

            return provider.Update( idx, equipmentQuality, propertyQuality, propertyType, rateMin, rateMax,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigEquipmentprecisioncastingEntity configEquipmentprecisioncastingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEquipmentprecisioncastingProvider(zoneId);
            return provider.Insert(configEquipmentprecisioncastingEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigEquipmentprecisioncastingEntity configEquipmentprecisioncastingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEquipmentprecisioncastingProvider(zoneId);
            return provider.Update(configEquipmentprecisioncastingEntity,trans);
        }
		
		#endregion	
		
		
	}
}

