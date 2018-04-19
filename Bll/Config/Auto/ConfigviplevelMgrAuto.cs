
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
    /// ConfigViplevel管理类
    /// </summary>
    public static partial class ConfigViplevelMgr
    {
        
		#region  GetById
		
        public static ConfigViplevelEntity GetById( System.Int32 effectId,string zoneId="")
        {
            var provider = new ConfigViplevelProvider(zoneId);
            return provider.GetById( effectId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigViplevelEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigViplevelProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<ConfigViplevelEntity> GetAllForCache(string zoneId="")
        {
            var provider = new ConfigViplevelProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 effectId,DbTransaction trans=null,string zoneId="")
        {
            ConfigViplevelProvider provider = new ConfigViplevelProvider(zoneId);

            return provider.Delete( effectId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigViplevelEntity configViplevelEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigViplevelProvider(zoneId);
            return provider.Insert(configViplevelEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigViplevelEntity configViplevelEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigViplevelProvider(zoneId);
            return provider.Update(configViplevelEntity,trans);
        }
		
		#endregion	
		
		
	}
}

