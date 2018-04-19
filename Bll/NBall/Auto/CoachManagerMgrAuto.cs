
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
    /// CoachManager管理类
    /// </summary>
    public static partial class CoachManagerMgr
    {
        
		#region  GetById
		
        public static CoachManagerEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new CoachManagerProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CoachManagerEntity> GetAll(string zoneId="")
        {
            var provider = new CoachManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            CoachManagerProvider provider = new CoachManagerProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
		#region  AddExp
		
        public static bool AddExp ( System.Guid managerId, System.Int32 number,DbTransaction trans=null,string zoneId="")
        {
            CoachManagerProvider provider = new CoachManagerProvider(zoneId);

            return provider.AddExp( managerId, number,trans);
            
        }
		
		#endregion
        
		#region  CostExpRecord
		
        public static bool CostExpRecord ( System.Guid managerId, System.Int32 number,DbTransaction trans=null,string zoneId="")
        {
            CoachManagerProvider provider = new CoachManagerProvider(zoneId);

            return provider.CostExpRecord( managerId, number,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CoachManagerEntity coachManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CoachManagerProvider(zoneId);
            return provider.Insert(coachManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CoachManagerEntity coachManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CoachManagerProvider(zoneId);
            return provider.Update(coachManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}
