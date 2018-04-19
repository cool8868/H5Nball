
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
    /// TeammemberTrain管理类
    /// </summary>
    public static partial class TeammemberTrainMgr
    {
        
		#region  GetById
		
        public static TeammemberTrainEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new TeammemberTrainProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetForStart
		
        public static TeammemberTrainEntity GetForStart( System.Guid teammemberId, System.Int32 trainStamina, System.Int32 mod,string zoneId="")
        {
            var provider = new TeammemberTrainProvider(zoneId);
            return provider.GetForStart( teammemberId, trainStamina, mod);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TeammemberTrainEntity> GetAll(string zoneId="")
        {
            var provider = new TeammemberTrainProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByManagerId
		
        public static List<TeammemberTrainEntity> GetByManagerId( System.Guid managerId,string zoneId="")
        {
            var provider = new TeammemberTrainProvider(zoneId);
            return provider.GetByManagerId( managerId);            
        }
		
		#endregion		  
		
		#region  GetTrainList
		
        public static List<TeammemberTrainEntity> GetTrainList(string zoneId="")
        {
            var provider = new TeammemberTrainProvider(zoneId);
            return provider.GetTrainList();            
        }
		
		#endregion		  
		
		#region  GetRestList
		
        public static List<TeammemberTrainEntity> GetRestList(string zoneId="")
        {
            var provider = new TeammemberTrainProvider(zoneId);
            return provider.GetRestList();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            TeammemberTrainProvider provider = new TeammemberTrainProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Start
		
        public static bool Start ( System.Guid managerId, System.Guid teammemberId, System.Int32 trainSeatOverCode, System.Int32 trainStamina, System.DateTime startTime,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            TeammemberTrainProvider provider = new TeammemberTrainProvider(zoneId);

            return provider.Start( managerId, teammemberId, trainSeatOverCode, trainStamina, startTime,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  UpdateData
		
        public static bool UpdateData ( System.Guid idx, System.Int32 level, System.Int32 eXP, System.Int32 trainStamina, System.Int32 trainState, System.DateTime startTime, System.DateTime settleTime, System.Int32 status, System.Int32 mod,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            TeammemberTrainProvider provider = new TeammemberTrainProvider(zoneId);

            return provider.UpdateData( idx, level, eXP, trainStamina, trainState, startTime, settleTime, status, mod,ref  returnCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TeammemberTrainEntity teammemberTrainEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TeammemberTrainProvider(zoneId);
            return provider.Insert(teammemberTrainEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TeammemberTrainEntity teammemberTrainEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TeammemberTrainProvider(zoneId);
            return provider.Update(teammemberTrainEntity,trans);
        }
		
		#endregion	
		
		
	}
}

