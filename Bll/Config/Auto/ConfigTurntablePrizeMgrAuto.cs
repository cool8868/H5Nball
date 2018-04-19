
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
    /// ConfigTurntableprize管理类
    /// </summary>
    public static partial class ConfigTurntableprizeMgr
    {
        
		#region  GetById
		
        public static ConfigTurntableprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigTurntableprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigTurntableprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigTurntableprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigTurntableprizeProvider provider = new ConfigTurntableprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigTurntableprizeEntity configTurntableprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigTurntableprizeProvider(zoneId);
            return provider.Insert(configTurntableprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigTurntableprizeEntity configTurntableprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigTurntableprizeProvider(zoneId);
            return provider.Update(configTurntableprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}
