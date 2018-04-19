
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
    /// GambleRank管理类
    /// </summary>
    public static partial class GambleRankMgr
    {
        
		#region  GetById
		
        public static GambleRankEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new GambleRankProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetRank
		
        public static List<GambleRankEntity> GetRank( System.Int32 topNum,string zoneId="")
        {
            var provider = new GambleRankProvider(zoneId);
            return provider.GetRank( topNum);            
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<GambleRankEntity> GetAll(string zoneId="")
        {
            var provider = new GambleRankProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  MoveToHistory
		
		public static Int32 MoveToHistory ( System.Int32 seasonId,string zoneId="")
        {
            var provider = new GambleRankProvider(zoneId);
            return provider.MoveToHistory( seasonId);
        }
		
		#endregion		  
		
		#region  UpdateRank
		
        public static bool UpdateRank (DbTransaction trans=null,string zoneId="")
        {
            GambleRankProvider provider = new GambleRankProvider(zoneId);

            return provider.UpdateRank(trans);
            
        }
		
		#endregion
        
		#region  UpdateData
		
        public static bool UpdateData ( System.Guid managerId, System.String managerName, System.Int32 money,DbTransaction trans=null,string zoneId="")
        {
            GambleRankProvider provider = new GambleRankProvider(zoneId);

            return provider.UpdateData( managerId, managerName, money,trans);
            
        }
		
		#endregion
        
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            GambleRankProvider provider = new GambleRankProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(GambleRankEntity gambleRankEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleRankProvider(zoneId);
            return provider.Insert(gambleRankEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(GambleRankEntity gambleRankEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleRankProvider(zoneId);
            return provider.Update(gambleRankEntity,trans);
        }
		
		#endregion	
		
		
	}
}
