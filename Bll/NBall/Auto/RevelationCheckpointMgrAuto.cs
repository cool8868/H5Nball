
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
    /// RevelationCheckpoint管理类
    /// </summary>
    public static partial class RevelationCheckpointMgr
    {
        
		#region  GetById
		
        public static RevelationCheckpointEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new RevelationCheckpointProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  C_RevelationGetCheckoint
		
        public static RevelationCheckpointEntity C_RevelationGetCheckoint( System.Guid managerid, System.Int32 mark,string zoneId="")
        {
            var provider = new RevelationCheckpointProvider(zoneId);
            return provider.C_RevelationGetCheckoint( managerid, mark);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RevelationCheckpointEntity> GetAll(string zoneId="")
        {
            var provider = new RevelationCheckpointProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  C_RevelationCountGenerl
		
        public static List<RevelationCheckpointEntity> C_RevelationCountGenerl( System.Guid managerid,string zoneId="")
        {
            var provider = new RevelationCheckpointProvider(zoneId);
            return provider.C_RevelationCountGenerl( managerid);            
        }
		
		#endregion		  
		
		#region  C_RevelationIsOpenVeteran
		
		public static Int32 C_RevelationIsOpenVeteran (string zoneId="")
        {
            var provider = new RevelationCheckpointProvider(zoneId);
            return provider.C_RevelationIsOpenVeteran();
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            RevelationCheckpointProvider provider = new RevelationCheckpointProvider(zoneId);

            return provider.Delete( idx, rowVersion,trans);
            
        }
		
		#endregion
        
		#region  C_RevelationTheGame
		
        public static bool C_RevelationTheGame ( System.Guid managerId, System.Int32 mark, System.Int32 littleLevels, System.Int32 goals, System.Int32 toConcede, System.Boolean isGeneral, System.Int32 courage, System.Boolean isVictory,DbTransaction trans=null,string zoneId="")
        {
            RevelationCheckpointProvider provider = new RevelationCheckpointProvider(zoneId);

            return provider.C_RevelationTheGame( managerId, mark, littleLevels, goals, toConcede, isGeneral, courage, isVictory,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RevelationCheckpointEntity revelationCheckpointEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationCheckpointProvider(zoneId);
            return provider.Insert(revelationCheckpointEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RevelationCheckpointEntity revelationCheckpointEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationCheckpointProvider(zoneId);
            return provider.Update(revelationCheckpointEntity,trans);
        }
		
		#endregion	
		
		
	}
}

