
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
    /// ConfigEquipmentupgrade管理类
    /// </summary>
    public static partial class ConfigEquipmentupgradeMgr
    {
        
		#region  GetById
		
        public static ConfigEquipmentupgradeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigEquipmentupgradeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigEquipmentupgradeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigEquipmentupgradeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigEquipmentupgradeProvider provider = new ConfigEquipmentupgradeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Update
		
        public static bool Update ( System.Int32 idx, System.Int32 equipQuality, System.Int32 sourceLevel, System.Int32 targetLevel, System.Int32 failLevel, System.Int32 protectedLevel, System.Int32 protectedConsume, System.Int32 propertyNum, System.Int32 rate, System.Int32 coin, System.Int32 itemCode1, System.Int32 itemCount1, System.Int32 itemCode2, System.Int32 itemCount2, System.Int32 itemCode3, System.Int32 itemCount3,DbTransaction trans=null,string zoneId="")
        {
            ConfigEquipmentupgradeProvider provider = new ConfigEquipmentupgradeProvider(zoneId);

            return provider.Update( idx, equipQuality, sourceLevel, targetLevel, failLevel, protectedLevel, protectedConsume, propertyNum, rate, coin, itemCode1, itemCount1, itemCode2, itemCount2, itemCode3, itemCount3,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigEquipmentupgradeEntity configEquipmentupgradeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEquipmentupgradeProvider(zoneId);
            return provider.Insert(configEquipmentupgradeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigEquipmentupgradeEntity configEquipmentupgradeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEquipmentupgradeProvider(zoneId);
            return provider.Update(configEquipmentupgradeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

