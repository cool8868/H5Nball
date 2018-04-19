
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
    /// ConfigLotteryrelation管理类
    /// </summary>
    public static partial class ConfigLotteryrelationMgr
    {
        
		#region  GetById
		
        public static ConfigLotteryrelationEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigLotteryrelationProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigLotteryrelationEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigLotteryrelationProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigLotteryrelationProvider provider = new ConfigLotteryrelationProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigLotteryrelationEntity configLotteryrelationEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLotteryrelationProvider(zoneId);
            return provider.Insert(configLotteryrelationEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigLotteryrelationEntity configLotteryrelationEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLotteryrelationProvider(zoneId);
            return provider.Update(configLotteryrelationEntity,trans);
        }
		
		#endregion	
		
		
	}
}

