
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
    /// DicLeagueexchange管理类
    /// </summary>
    public static partial class DicLeagueexchangeMgr
    {
        
		#region  GetById
		
        public static DicLeagueexchangeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicLeagueexchangeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicLeagueexchangeEntity> GetAll(string zoneId="")
        {
            var provider = new DicLeagueexchangeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicLeagueexchangeProvider provider = new DicLeagueexchangeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicLeagueexchangeEntity dicLeagueexchangeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicLeagueexchangeProvider(zoneId);
            return provider.Insert(dicLeagueexchangeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicLeagueexchangeEntity dicLeagueexchangeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicLeagueexchangeProvider(zoneId);
            return provider.Update(dicLeagueexchangeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

