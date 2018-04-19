
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
    /// ConfigGambleicon管理类
    /// </summary>
    public static partial class ConfigGambleiconMgr
    {
        
		#region  GetById
		
        public static ConfigGambleiconEntity GetById( System.String name,string zoneId="")
        {
            var provider = new ConfigGambleiconProvider(zoneId);
            return provider.GetById( name);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigGambleiconEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigGambleiconProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
        
		#region Insert

        public static bool Insert(ConfigGambleiconEntity configGambleiconEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigGambleiconProvider(zoneId);
            return provider.Insert(configGambleiconEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigGambleiconEntity configGambleiconEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigGambleiconProvider(zoneId);
            return provider.Update(configGambleiconEntity,trans);
        }
		
		#endregion	
		
		
	}
}
