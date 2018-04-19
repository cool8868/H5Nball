
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
    /// ConfigPlayerkillpoint管理类
    /// </summary>
    public static partial class ConfigPlayerkillpointMgr
    {
        
		#region  GetById
		
        public static ConfigPlayerkillpointEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigPlayerkillpointProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigPlayerkillpointEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigPlayerkillpointProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigPlayerkillpointProvider provider = new ConfigPlayerkillpointProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigPlayerkillpointEntity configPlayerkillpointEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPlayerkillpointProvider(zoneId);
            return provider.Insert(configPlayerkillpointEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigPlayerkillpointEntity configPlayerkillpointEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPlayerkillpointProvider(zoneId);
            return provider.Update(configPlayerkillpointEntity,trans);
        }
		
		#endregion	
		
		
	}
}
