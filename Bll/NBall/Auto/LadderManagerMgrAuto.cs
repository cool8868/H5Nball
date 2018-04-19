
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
    /// LadderManager管理类
    /// </summary>
    public static partial class LadderManagerMgr
    {
        
		#region  GetById
		
        public static LadderManagerEntity GetById( System.Guid managerId, System.Int32 initLadderScore,string zoneId="")
        {
            var provider = new LadderManagerProvider(zoneId);
            return provider.GetById( managerId, initLadderScore);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LadderManagerEntity> GetAll(string zoneId="")
        {
            var provider = new LadderManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetBot
		
        public static List<LadderManagerEntity> GetBot( System.Int32 botNumber, System.Int32 minScore, System.Int32 maxScore,string zoneId="")
        {
            var provider = new LadderManagerProvider(zoneId);
            return provider.GetBot( botNumber, minScore, maxScore);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            LadderManagerProvider provider = new LadderManagerProvider(zoneId);

            return provider.Delete( managerId, rowVersion,trans);
            
        }
		
		#endregion
        
		#region  AddHonor
		
        public static bool AddHonor ( System.Guid managerId, System.Int32 honor,DbTransaction trans=null,string zoneId="")
        {
            LadderManagerProvider provider = new LadderManagerProvider(zoneId);

            return provider.AddHonor( managerId, honor,trans);
            
        }
		
		#endregion
        
		#region  AddDailyHonor
		
        public static bool AddDailyHonor ( System.Guid managerId, System.Int32 newlyHonor, System.Int32 newlyLadderCoin,DbTransaction trans=null,string zoneId="")
        {
            LadderManagerProvider provider = new LadderManagerProvider(zoneId);

            return provider.AddDailyHonor( managerId, newlyHonor, newlyLadderCoin,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LadderManagerEntity ladderManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderManagerProvider(zoneId);
            return provider.Insert(ladderManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LadderManagerEntity ladderManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderManagerProvider(zoneId);
            return provider.Update(ladderManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}
