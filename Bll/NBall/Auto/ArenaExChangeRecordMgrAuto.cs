
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
    /// ArenaExchangerecord管理类
    /// </summary>
    public static partial class ArenaExchangerecordMgr
    {
        
		#region  GetById
		
        public static ArenaExchangerecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ArenaExchangerecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ArenaExchangerecordEntity> GetAll(string zoneId="")
        {
            var provider = new ArenaExchangerecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ArenaExchangerecordProvider provider = new ArenaExchangerecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ArenaExchangerecordEntity arenaExchangerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaExchangerecordProvider(zoneId);
            return provider.Insert(arenaExchangerecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ArenaExchangerecordEntity arenaExchangerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaExchangerecordProvider(zoneId);
            return provider.Update(arenaExchangerecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}
