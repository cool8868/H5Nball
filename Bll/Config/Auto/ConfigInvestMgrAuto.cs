
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
    /// ConfigInvest管理类
    /// </summary>
    public static partial class ConfigInvestMgr
    {
        
		#region  GetById
		
        public static ConfigInvestEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigInvestProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigInvestEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigInvestProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigInvestProvider provider = new ConfigInvestProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigInvestEntity configInvestEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigInvestProvider(zoneId);
            return provider.Insert(configInvestEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigInvestEntity configInvestEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigInvestProvider(zoneId);
            return provider.Update(configInvestEntity,trans);
        }
		
		#endregion	
		
		
	}
}

