
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
    /// ConfigLeaguewincountprize管理类
    /// </summary>
    public static partial class ConfigLeaguewincountprizeMgr
    {
        
		#region  GetById
		
        public static ConfigLeaguewincountprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigLeaguewincountprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigLeaguewincountprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigLeaguewincountprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigLeaguewincountprizeProvider provider = new ConfigLeaguewincountprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigLeaguewincountprizeEntity configLeaguewincountprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeaguewincountprizeProvider(zoneId);
            return provider.Insert(configLeaguewincountprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigLeaguewincountprizeEntity configLeaguewincountprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeaguewincountprizeProvider(zoneId);
            return provider.Update(configLeaguewincountprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

