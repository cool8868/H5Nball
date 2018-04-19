
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
    /// ConfigLottery管理类
    /// </summary>
    public static partial class ConfigLotteryMgr
    {
        
		#region  GetById
		
        public static ConfigLotteryEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigLotteryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigLotteryEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigLotteryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigLotteryProvider provider = new ConfigLotteryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigLotteryEntity configLotteryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLotteryProvider(zoneId);
            return provider.Insert(configLotteryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigLotteryEntity configLotteryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLotteryProvider(zoneId);
            return provider.Update(configLotteryEntity,trans);
        }
		
		#endregion	
		
		
	}
}

