
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
    /// ConfigCoachinfo管理类
    /// </summary>
    public static partial class ConfigCoachinfoMgr
    {
        
		#region  GetById
		
        public static ConfigCoachinfoEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigCoachinfoProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigCoachinfoEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigCoachinfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigCoachinfoProvider provider = new ConfigCoachinfoProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigCoachinfoEntity configCoachinfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCoachinfoProvider(zoneId);
            return provider.Insert(configCoachinfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigCoachinfoEntity configCoachinfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCoachinfoProvider(zoneId);
            return provider.Update(configCoachinfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}
