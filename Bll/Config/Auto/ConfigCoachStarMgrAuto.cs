
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
    /// ConfigCoachstar管理类
    /// </summary>
    public static partial class ConfigCoachstarMgr
    {
        
		#region  GetById
		
        public static ConfigCoachstarEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigCoachstarProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigCoachstarEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigCoachstarProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigCoachstarProvider provider = new ConfigCoachstarProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigCoachstarEntity configCoachstarEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCoachstarProvider(zoneId);
            return provider.Insert(configCoachstarEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigCoachstarEntity configCoachstarEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCoachstarProvider(zoneId);
            return provider.Update(configCoachstarEntity,trans);
        }
		
		#endregion	
		
		
	}
}
