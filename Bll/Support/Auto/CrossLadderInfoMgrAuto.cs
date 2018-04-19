
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
    /// CrossladderInfo管理类
    /// </summary>
    public static partial class CrossladderInfoMgr
    {
        
		#region  GetById
		
        public static CrossladderInfoEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new CrossladderInfoProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrossladderInfoEntity> GetAll(string zoneId="")
        {
            var provider = new CrossladderInfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            CrossladderInfoProvider provider = new CrossladderInfoProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  ScoreToHonor
		
        public static bool ScoreToHonor ( System.DateTime curDate, System.Int32 curSeasonId, System.Int32 isNewSeason, System.Int32 domainId,DbTransaction trans=null,string zoneId="")
        {
            CrossladderInfoProvider provider = new CrossladderInfoProvider(zoneId);

            return provider.ScoreToHonor( curDate, curSeasonId, isNewSeason, domainId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrossladderInfoEntity crossladderInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderInfoProvider(zoneId);
            return provider.Insert(crossladderInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrossladderInfoEntity crossladderInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderInfoProvider(zoneId);
            return provider.Update(crossladderInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}
