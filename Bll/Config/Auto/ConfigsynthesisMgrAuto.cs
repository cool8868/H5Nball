
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
    /// ConfigSynthesis管理类
    /// </summary>
    public static partial class ConfigSynthesisMgr
    {
        
		#region  GetById
		
        public static ConfigSynthesisEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigSynthesisProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigSynthesisEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigSynthesisProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigSynthesisProvider provider = new ConfigSynthesisProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigSynthesisEntity configSynthesisEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSynthesisProvider(zoneId);
            return provider.Insert(configSynthesisEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigSynthesisEntity configSynthesisEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSynthesisProvider(zoneId);
            return provider.Update(configSynthesisEntity,trans);
        }
		
		#endregion	
		
		
	}
}

