
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
    /// RevelationChallengerevord管理类
    /// </summary>
    public static partial class RevelationChallengerevordMgr
    {
        
		#region  GetById
		
        public static RevelationChallengerevordEntity GetById( System.Guid gameId,string zoneId="")
        {
            var provider = new RevelationChallengerevordProvider(zoneId);
            return provider.GetById( gameId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RevelationChallengerevordEntity> GetAll(string zoneId="")
        {
            var provider = new RevelationChallengerevordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid gameId,DbTransaction trans=null,string zoneId="")
        {
            RevelationChallengerevordProvider provider = new RevelationChallengerevordProvider(zoneId);

            return provider.Delete( gameId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RevelationChallengerevordEntity revelationChallengerevordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationChallengerevordProvider(zoneId);
            return provider.Insert(revelationChallengerevordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RevelationChallengerevordEntity revelationChallengerevordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationChallengerevordProvider(zoneId);
            return provider.Update(revelationChallengerevordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

