
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
    /// LadderHonorrecord管理类
    /// </summary>
    public static partial class LadderHonorrecordMgr
    {
        
		#region  GetById
		
        public static LadderHonorrecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new LadderHonorrecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LadderHonorrecordEntity> GetAll(string zoneId="")
        {
            var provider = new LadderHonorrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            LadderHonorrecordProvider provider = new LadderHonorrecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  ScoreToHonor
		
        public static bool ScoreToHonor ( System.DateTime curDate, System.Int32 curSeasonId, System.Int32 isNewSeason,DbTransaction trans=null,string zoneId="")
        {
            LadderHonorrecordProvider provider = new LadderHonorrecordProvider(zoneId);

            return provider.ScoreToHonor( curDate, curSeasonId, isNewSeason,trans);
            
        }
		
		#endregion
        
		#region  ScoreToHonorMergeZone
		
        public static bool ScoreToHonorMergeZone ( System.DateTime curDate, System.Int32 curSeasonId,DbTransaction trans=null,string zoneId="")
        {
            LadderHonorrecordProvider provider = new LadderHonorrecordProvider(zoneId);

            return provider.ScoreToHonorMergeZone( curDate, curSeasonId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LadderHonorrecordEntity ladderHonorrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderHonorrecordProvider(zoneId);
            return provider.Insert(ladderHonorrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LadderHonorrecordEntity ladderHonorrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderHonorrecordProvider(zoneId);
            return provider.Update(ladderHonorrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

