
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
    /// ArenaSeason管理类
    /// </summary>
    public static partial class ArenaSeasonMgr
    {
        
		#region  GetById
		
        public static ArenaSeasonEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ArenaSeasonProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetSeason
		
        public static ArenaSeasonEntity GetSeason( System.DateTime dates,string zoneId="")
        {
            var provider = new ArenaSeasonProvider(zoneId);
            return provider.GetSeason( dates);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ArenaSeasonEntity> GetAll(string zoneId="")
        {
            var provider = new ArenaSeasonProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ArenaSeasonProvider provider = new ArenaSeasonProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ArenaSeasonEntity arenaSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaSeasonProvider(zoneId);
            return provider.Insert(arenaSeasonEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ArenaSeasonEntity arenaSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaSeasonProvider(zoneId);
            return provider.Update(arenaSeasonEntity,trans);
        }
		
		#endregion	
		
		
	}
}
