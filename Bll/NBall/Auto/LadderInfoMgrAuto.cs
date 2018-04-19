
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
    /// LadderInfo管理类
    /// </summary>
    public static partial class LadderInfoMgr
    {
        
		#region  GetById
		
        public static LadderInfoEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new LadderInfoProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LadderInfoEntity> GetAll(string zoneId="")
        {
            var provider = new LadderInfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            LadderInfoProvider provider = new LadderInfoProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  ScoreToHonor
		
        public static bool ScoreToHonor ( System.DateTime curDate, System.Int32 curSeasonId, System.Int32 isNewSeason,DbTransaction trans=null,string zoneId="")
        {
            LadderInfoProvider provider = new LadderInfoProvider(zoneId);

            return provider.ScoreToHonor( curDate, curSeasonId, isNewSeason,trans);
            
        }
		
		#endregion
        
		#region  ScoreToHonorMergeZone
		
        public static bool ScoreToHonorMergeZone ( System.DateTime curDate, System.Int32 curSeasonId,DbTransaction trans=null,string zoneId="")
        {
            LadderInfoProvider provider = new LadderInfoProvider(zoneId);

            return provider.ScoreToHonorMergeZone( curDate, curSeasonId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LadderInfoEntity ladderInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderInfoProvider(zoneId);
            return provider.Insert(ladderInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LadderInfoEntity ladderInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderInfoProvider(zoneId);
            return provider.Update(ladderInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}

