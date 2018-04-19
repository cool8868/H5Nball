
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
    /// ConfigSynthesiscard管理类
    /// </summary>
    public static partial class ConfigSynthesiscardMgr
    {
        
		#region  GetById
		
        public static ConfigSynthesiscardEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigSynthesiscardProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigSynthesiscardEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigSynthesiscardProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigSynthesiscardProvider provider = new ConfigSynthesiscardProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigSynthesiscardEntity configSynthesiscardEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSynthesiscardProvider(zoneId);
            return provider.Insert(configSynthesiscardEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigSynthesiscardEntity configSynthesiscardEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSynthesiscardProvider(zoneId);
            return provider.Update(configSynthesiscardEntity,trans);
        }
		
		#endregion	
		
		
	}
}

