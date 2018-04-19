
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
    /// ConfigPlayerlevel管理类
    /// </summary>
    public static partial class ConfigPlayerlevelMgr
    {
        
		#region  GetById
		
        public static ConfigPlayerlevelEntity GetById( System.Int32 level,string zoneId="")
        {
            var provider = new ConfigPlayerlevelProvider(zoneId);
            return provider.GetById( level);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigPlayerlevelEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigPlayerlevelProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 level,DbTransaction trans=null,string zoneId="")
        {
            ConfigPlayerlevelProvider provider = new ConfigPlayerlevelProvider(zoneId);

            return provider.Delete( level,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigPlayerlevelEntity configPlayerlevelEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPlayerlevelProvider(zoneId);
            return provider.Insert(configPlayerlevelEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigPlayerlevelEntity configPlayerlevelEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPlayerlevelProvider(zoneId);
            return provider.Update(configPlayerlevelEntity,trans);
        }
		
		#endregion	
		
		
	}
}

