
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
    /// ConfigEverydayactivitylegend管理类
    /// </summary>
    public static partial class ConfigEverydayactivitylegendMgr
    {
        
		#region  GetById
		
        public static ConfigEverydayactivitylegendEntity GetById( System.DateTime refreshDate,string zoneId="")
        {
            var provider = new ConfigEverydayactivitylegendProvider(zoneId);
            return provider.GetById( refreshDate);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigEverydayactivitylegendEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigEverydayactivitylegendProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetTop5
		
        public static List<ConfigEverydayactivitylegendEntity> GetTop5( System.DateTime startDate, System.DateTime endDate,string zoneId="")
        {
            var provider = new ConfigEverydayactivitylegendProvider(zoneId);
            return provider.GetTop5( startDate, endDate);            
        }
		
		#endregion		  
		
		#region  GetByTime
		
		public static Int32 GetByTime ( System.DateTime dateTime,string zoneId="")
        {
            var provider = new ConfigEverydayactivitylegendProvider(zoneId);
            return provider.GetByTime( dateTime);
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.DateTime refreshDate,DbTransaction trans=null,string zoneId="")
        {
            ConfigEverydayactivitylegendProvider provider = new ConfigEverydayactivitylegendProvider(zoneId);

            return provider.Delete( refreshDate,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigEverydayactivitylegendEntity configEverydayactivitylegendEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEverydayactivitylegendProvider(zoneId);
            return provider.Insert(configEverydayactivitylegendEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigEverydayactivitylegendEntity configEverydayactivitylegendEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEverydayactivitylegendProvider(zoneId);
            return provider.Update(configEverydayactivitylegendEntity,trans);
        }
		
		#endregion	
		
		
	}
}
