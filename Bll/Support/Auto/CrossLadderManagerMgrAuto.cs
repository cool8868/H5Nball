
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
    /// CrossladderManager管理类
    /// </summary>
    public static partial class CrossladderManagerMgr
    {
        
		#region  GetById
		
        public static CrossladderManagerEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new CrossladderManagerProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrossladderManagerEntity> GetAll(string zoneId="")
        {
            var provider = new CrossladderManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetBot
		
        public static List<CrossladderManagerEntity> GetBot( System.Int32 botNumber, System.Int32 minScore, System.Int32 maxScore,string zoneId="")
        {
            var provider = new CrossladderManagerProvider(zoneId);
            return provider.GetBot( botNumber, minScore, maxScore);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            CrossladderManagerProvider provider = new CrossladderManagerProvider(zoneId);

            return provider.Delete( managerId, rowVersion,trans);
            
        }
		
		#endregion
        
		#region  GetOldHonor
		
        public static bool GetOldHonor ( System.Guid managerId,ref  System.Int32 honor,DbTransaction trans=null,string zoneId="")
        {
            CrossladderManagerProvider provider = new CrossladderManagerProvider(zoneId);

            return provider.GetOldHonor( managerId,ref  honor,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrossladderManagerEntity crossladderManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderManagerProvider(zoneId);
            return provider.Insert(crossladderManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrossladderManagerEntity crossladderManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderManagerProvider(zoneId);
            return provider.Update(crossladderManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}
