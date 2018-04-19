
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
    /// ConfigPlayerthestar管理类
    /// </summary>
    public static partial class ConfigPlayerthestarMgr
    {
        
		#region  GetById
		
        public static ConfigPlayerthestarEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigPlayerthestarProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigPlayerthestarEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigPlayerthestarProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigPlayerthestarProvider provider = new ConfigPlayerthestarProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigPlayerthestarEntity configPlayerthestarEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPlayerthestarProvider(zoneId);
            return provider.Insert(configPlayerthestarEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigPlayerthestarEntity configPlayerthestarEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPlayerthestarProvider(zoneId);
            return provider.Update(configPlayerthestarEntity,trans);
        }
		
		#endregion	
		
		
	}
}
