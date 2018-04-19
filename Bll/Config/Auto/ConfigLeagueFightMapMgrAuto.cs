
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
    /// ConfigLeaguefightmap管理类
    /// </summary>
    public static partial class ConfigLeaguefightmapMgr
    {
        
		#region  GetById
		
        public static ConfigLeaguefightmapEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigLeaguefightmapProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigLeaguefightmapEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigLeaguefightmapProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigLeaguefightmapProvider provider = new ConfigLeaguefightmapProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigLeaguefightmapEntity configLeaguefightmapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeaguefightmapProvider(zoneId);
            return provider.Insert(configLeaguefightmapEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigLeaguefightmapEntity configLeaguefightmapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeaguefightmapProvider(zoneId);
            return provider.Update(configLeaguefightmapEntity,trans);
        }
		
		#endregion	
		
		
	}
}

