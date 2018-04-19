
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
    /// ConfigStrength管理类
    /// </summary>
    public static partial class ConfigStrengthMgr
    {
        
		#region  GetById
		
        public static ConfigStrengthEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigStrengthProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigStrengthEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigStrengthProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigStrengthProvider provider = new ConfigStrengthProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigStrengthEntity configStrengthEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigStrengthProvider(zoneId);
            return provider.Insert(configStrengthEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigStrengthEntity configStrengthEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigStrengthProvider(zoneId);
            return provider.Update(configStrengthEntity,trans);
        }
		
		#endregion	
		
		
	}
}

