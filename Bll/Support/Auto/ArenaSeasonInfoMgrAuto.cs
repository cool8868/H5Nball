
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
    /// ArenaSeasoninfo管理类
    /// </summary>
    public static partial class ArenaSeasoninfoMgr
    {
        
		#region  GetById
		
        public static ArenaSeasoninfoEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ArenaSeasoninfoProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetSeasonInfo
		
        public static ArenaSeasoninfoEntity GetSeasonInfo( System.Int32 seasonId, System.Int32 domainId,string zoneId="")
        {
            var provider = new ArenaSeasoninfoProvider(zoneId);
            return provider.GetSeasonInfo( seasonId, domainId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ArenaSeasoninfoEntity> GetAll(string zoneId="")
        {
            var provider = new ArenaSeasoninfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ArenaSeasoninfoProvider provider = new ArenaSeasoninfoProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ArenaSeasoninfoEntity arenaSeasoninfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaSeasoninfoProvider(zoneId);
            return provider.Insert(arenaSeasoninfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ArenaSeasoninfoEntity arenaSeasoninfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaSeasoninfoProvider(zoneId);
            return provider.Update(arenaSeasoninfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}
