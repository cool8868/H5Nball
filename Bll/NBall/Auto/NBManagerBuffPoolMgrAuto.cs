
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
    /// NbManagerbuffpool管理类
    /// </summary>
    public static partial class NbManagerbuffpoolMgr
    {
        
		#region  GetById
		
        public static NbManagerbuffpoolEntity GetById( System.Int64 id,string zoneId="")
        {
            var provider = new NbManagerbuffpoolProvider(zoneId);
            return provider.GetById( id);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagerbuffpoolEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagerbuffpoolProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByMid
		
        public static List<NbManagerbuffpoolEntity> GetByMid( System.Guid managerId, System.Int32 managerHash,string zoneId="")
        {
            var provider = new NbManagerbuffpoolProvider(zoneId);
            return provider.GetByMid( managerId, managerHash);            
        }
		
		#endregion		  
		
		#region  Include
		
        public static bool Include ( System.Guid managerId, System.Int32 managerHash, System.String skillCode, System.Int32 skillLevel, System.Int32 buffSrcType, System.String buffSrcId, System.Int32 buffUnitType, System.Int32 liveFlag, System.Int32 buffNo, System.Int32 dstDir, System.Int32 dstMode, System.String dstKey, System.String buffMap, System.Decimal buffVal, System.Decimal buffPer, System.Int32 expiryMinutes, System.Int32 limitTimes, System.Int32 remainTimes, System.Boolean repeatBuffFlag, System.Boolean repeatTimeFlag, System.Boolean repeatTimesFlag,DbTransaction trans=null,string zoneId="")
        {
            NbManagerbuffpoolProvider provider = new NbManagerbuffpoolProvider(zoneId);

            return provider.Include( managerId, managerHash, skillCode, skillLevel, buffSrcType, buffSrcId, buffUnitType, liveFlag, buffNo, dstDir, dstMode, dstKey, buffMap, buffVal, buffPer, expiryMinutes, limitTimes, remainTimes, repeatBuffFlag, repeatTimeFlag, repeatTimesFlag,trans);
            
        }
		
		#endregion
        
		#region  Exclude
		
        public static bool Exclude ( System.Guid managerId, System.Int32 managerHash, System.Int32 buffSrcType, System.String buffSrcId, System.String skillCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerbuffpoolProvider provider = new NbManagerbuffpoolProvider(zoneId);

            return provider.Exclude( managerId, managerHash, buffSrcType, buffSrcId, skillCode,trans);
            
        }
		
		#endregion
        
		#region  ExcludeMulti
		
        public static bool ExcludeMulti ( System.Guid managerId, System.Int32 managerHash, System.String skillCode, System.Int32 buffNo, System.String skillCode2, System.Int32 buffNo2, System.String skillCode3, System.Int32 buffNo3, System.String skillCode4, System.Int32 buffNo4, System.String skillCode5, System.Int32 buffNo5,DbTransaction trans=null,string zoneId="")
        {
            NbManagerbuffpoolProvider provider = new NbManagerbuffpoolProvider(zoneId);

            return provider.ExcludeMulti( managerId, managerHash, skillCode, buffNo, skillCode2, buffNo2, skillCode3, buffNo3, skillCode4, buffNo4, skillCode5, buffNo5,trans);
            
        }
		
		#endregion
        
		#region  GetVersionByMid
		
        public static bool GetVersionByMid ( System.Guid managerId, System.Int32 managerHash,ref  System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            NbManagerbuffpoolProvider provider = new NbManagerbuffpoolProvider(zoneId);

            return provider.GetVersionByMid( managerId, managerHash,ref  rowVersion,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagerbuffpoolEntity nbManagerbuffpoolEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerbuffpoolProvider(zoneId);
            return provider.Insert(nbManagerbuffpoolEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagerbuffpoolEntity nbManagerbuffpoolEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerbuffpoolProvider(zoneId);
            return provider.Update(nbManagerbuffpoolEntity,trans);
        }
		
		#endregion	
		
		
	}
}

