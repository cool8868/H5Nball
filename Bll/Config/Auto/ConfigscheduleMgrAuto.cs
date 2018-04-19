
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
    /// ConfigSchedule管理类
    /// </summary>
    public static partial class ConfigScheduleMgr
    {
        
		#region  GetById
		
        public static ConfigScheduleEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigScheduleProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigScheduleEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigScheduleProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<ConfigScheduleEntity> GetAllForCache(string zoneId="")
        {
            var provider = new ConfigScheduleProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigScheduleProvider provider = new ConfigScheduleProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigScheduleEntity configScheduleEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigScheduleProvider(zoneId);
            return provider.Insert(configScheduleEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigScheduleEntity configScheduleEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigScheduleProvider(zoneId);
            return provider.Update(configScheduleEntity,trans);
        }
		
		#endregion	
		
		
	}
}

