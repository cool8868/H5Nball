
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
    /// MallExtrarecord管理类
    /// </summary>
    public static partial class MallExtrarecordMgr
    {
        
		#region  GetById
		
        public static MallExtrarecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new MallExtrarecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetExtra
		
        public static MallExtrarecordEntity GetExtra( System.Guid managerId, System.Int32 extraType,string zoneId="")
        {
            var provider = new MallExtrarecordProvider(zoneId);
            return provider.GetExtra( managerId, extraType);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<MallExtrarecordEntity> GetAll(string zoneId="")
        {
            var provider = new MallExtrarecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  ExtraAddPkCount
		
        public static bool ExtraAddPkCount ( System.Guid managerId,ref  System.Int32 curPKCount,DbTransaction trans=null,string zoneId="")
        {
            MallExtrarecordProvider provider = new MallExtrarecordProvider(zoneId);

            return provider.ExtraAddPkCount( managerId,ref  curPKCount,trans);
            
        }
		
		#endregion
        
		#region  ExtraAddStamina
		
        public static bool ExtraAddStamina ( System.Guid managerId, System.DateTime resumeTime, System.Int32 curStamina,DbTransaction trans=null,string zoneId="")
        {
            MallExtrarecordProvider provider = new MallExtrarecordProvider(zoneId);

            return provider.ExtraAddStamina( managerId, resumeTime, curStamina,trans);
            
        }
		
		#endregion
        
		#region  ExtraAddTrainseat
		
        public static bool ExtraAddTrainseat ( System.Guid managerId,ref  System.Int32 curTrainSeat,DbTransaction trans=null,string zoneId="")
        {
            MallExtrarecordProvider provider = new MallExtrarecordProvider(zoneId);

            return provider.ExtraAddTrainseat( managerId,ref  curTrainSeat,trans);
            
        }
		
		#endregion
        
		#region  ExtraExpandPackage
		
        public static bool ExtraExpandPackage ( System.Guid managerId, System.Int32 addSize,ref  System.Int32 resultSize,DbTransaction trans=null,string zoneId="")
        {
            MallExtrarecordProvider provider = new MallExtrarecordProvider(zoneId);

            return provider.ExtraExpandPackage( managerId, addSize,ref  resultSize,trans);
            
        }
		
		#endregion
        
		#region  ExtraResetElite
		
        public static bool ExtraResetElite ( System.Guid managerId, System.DateTime recordDate,DbTransaction trans=null,string zoneId="")
        {
            MallExtrarecordProvider provider = new MallExtrarecordProvider(zoneId);

            return provider.ExtraResetElite( managerId, recordDate,trans);
            
        }
		
		#endregion
        
		#region  ExtraSave
		
        public static bool ExtraSave ( System.Int32 idx, System.Guid managerId, System.Int32 extraType, System.Int32 usedCount, System.DateTime recordDate, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            MallExtrarecordProvider provider = new MallExtrarecordProvider(zoneId);

            return provider.ExtraSave( idx, managerId, extraType, usedCount, recordDate, rowVersion,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  ExtraAddSubstitute
		
        public static bool ExtraAddSubstitute ( System.Guid managerId,ref  System.Int32 curSubstitute,DbTransaction trans=null,string zoneId="")
        {
            MallExtrarecordProvider provider = new MallExtrarecordProvider(zoneId);

            return provider.ExtraAddSubstitute( managerId,ref  curSubstitute,trans);
            
        }
		
		#endregion
        
		#region  UpdateUsedCount
		
        public static bool UpdateUsedCount ( System.Int32 extraType,DbTransaction trans=null,string zoneId="")
        {
            MallExtrarecordProvider provider = new MallExtrarecordProvider(zoneId);

            return provider.UpdateUsedCount( extraType,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(MallExtrarecordEntity mallExtrarecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MallExtrarecordProvider(zoneId);
            return provider.Insert(mallExtrarecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(MallExtrarecordEntity mallExtrarecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MallExtrarecordProvider(zoneId);
            return provider.Update(mallExtrarecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

