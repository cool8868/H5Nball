
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
    /// LadderSeason管理类
    /// </summary>
    public static partial class LadderSeasonMgr
    {
        
		#region  GetById
		
        public static LadderSeasonEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new LadderSeasonProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LadderSeasonEntity> GetAll(string zoneId="")
        {
            var provider = new LadderSeasonProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            LadderSeasonProvider provider = new LadderSeasonProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LadderSeasonEntity ladderSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderSeasonProvider(zoneId);
            return provider.Insert(ladderSeasonEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LadderSeasonEntity ladderSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderSeasonProvider(zoneId);
            return provider.Update(ladderSeasonEntity,trans);
        }
		
		#endregion	
		
		
	}
}

