
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
    /// NbManagermatchachievement管理类
    /// </summary>
    public static partial class NbManagermatchachievementMgr
    {
        
		#region  GetByManagerIdAndTypeId
		
        public static NbManagermatchachievementEntity GetByManagerIdAndTypeId( System.Guid managerId, System.Int32 typeId,string zoneId="")
        {
            var provider = new NbManagermatchachievementProvider(zoneId);
            return provider.GetByManagerIdAndTypeId( managerId, typeId);
        }
		
		#endregion		  
		
		#region  GetById
		
        public static NbManagermatchachievementEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new NbManagermatchachievementProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagermatchachievementEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagermatchachievementProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetCountByMatchTypeId
		
		public static Int32 GetCountByMatchTypeId ( System.Int32 matchTypeId,string zoneId="")
        {
            var provider = new NbManagermatchachievementProvider(zoneId);
            return provider.GetCountByMatchTypeId( matchTypeId);
        }
		
		#endregion		  
		
		#region  Record
		
        public static bool Record ( System.Guid managerId, System.Int32 matchType, System.Int32 matchTypeId, System.Int32 rankIndex, System.Int32 status, System.DateTime rowtime,DbTransaction trans=null,string zoneId="")
        {
            NbManagermatchachievementProvider provider = new NbManagermatchachievementProvider(zoneId);

            return provider.Record( managerId, matchType, matchTypeId, rankIndex, status, rowtime,trans);
            
        }
		
		#endregion
        
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            NbManagermatchachievementProvider provider = new NbManagermatchachievementProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagermatchachievementEntity nbManagermatchachievementEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagermatchachievementProvider(zoneId);
            return provider.Insert(nbManagermatchachievementEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagermatchachievementEntity nbManagermatchachievementEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagermatchachievementProvider(zoneId);
            return provider.Update(nbManagermatchachievementEntity,trans);
        }
		
		#endregion	
		
		
	}
}

