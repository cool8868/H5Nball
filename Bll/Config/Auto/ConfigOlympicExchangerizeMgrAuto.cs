
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
    /// ConfigOlympicexchangerize管理类
    /// </summary>
    public static partial class ConfigOlympicexchangerizeMgr
    {
        
		#region  GetById
		
        public static ConfigOlympicexchangerizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigOlympicexchangerizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigOlympicexchangerizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigOlympicexchangerizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigOlympicexchangerizeProvider provider = new ConfigOlympicexchangerizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigOlympicexchangerizeEntity configOlympicexchangerizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigOlympicexchangerizeProvider(zoneId);
            return provider.Insert(configOlympicexchangerizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigOlympicexchangerizeEntity configOlympicexchangerizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigOlympicexchangerizeProvider(zoneId);
            return provider.Update(configOlympicexchangerizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}
