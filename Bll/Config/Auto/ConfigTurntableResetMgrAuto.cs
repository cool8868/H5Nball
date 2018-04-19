
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
    /// ConfigTurntablereset管理类
    /// </summary>
    public static partial class ConfigTurntableresetMgr
    {
        
		#region  GetById
		
        public static ConfigTurntableresetEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigTurntableresetProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigTurntableresetEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigTurntableresetProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigTurntableresetProvider provider = new ConfigTurntableresetProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigTurntableresetEntity configTurntableresetEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigTurntableresetProvider(zoneId);
            return provider.Insert(configTurntableresetEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigTurntableresetEntity configTurntableresetEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigTurntableresetProvider(zoneId);
            return provider.Update(configTurntableresetEntity,trans);
        }
		
		#endregion	
		
		
	}
}
