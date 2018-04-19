
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
    /// ConfigLadderdayprize管理类
    /// </summary>
    public static partial class ConfigLadderdayprizeMgr
    {
        
		#region  GetById
		
        public static ConfigLadderdayprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigLadderdayprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigLadderdayprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigLadderdayprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigLadderdayprizeProvider provider = new ConfigLadderdayprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigLadderdayprizeEntity configLadderdayprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLadderdayprizeProvider(zoneId);
            return provider.Insert(configLadderdayprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigLadderdayprizeEntity configLadderdayprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLadderdayprizeProvider(zoneId);
            return provider.Update(configLadderdayprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

