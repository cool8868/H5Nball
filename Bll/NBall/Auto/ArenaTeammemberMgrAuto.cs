
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
    /// ArenaTeammember管理类
    /// </summary>
    public static partial class ArenaTeammemberMgr
    {
        
		#region  GetById
		
        public static ArenaTeammemberEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ArenaTeammemberProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByManagerId
		
        public static ArenaTeammemberEntity GetByManagerId( System.Guid managerId, System.Int32 arenaType,string zoneId="")
        {
            var provider = new ArenaTeammemberProvider(zoneId);
            return provider.GetByManagerId( managerId, arenaType);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ArenaTeammemberEntity> GetAll(string zoneId="")
        {
            var provider = new ArenaTeammemberProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ArenaTeammemberProvider provider = new ArenaTeammemberProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ArenaTeammemberEntity arenaTeammemberEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaTeammemberProvider(zoneId);
            return provider.Insert(arenaTeammemberEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ArenaTeammemberEntity arenaTeammemberEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaTeammemberProvider(zoneId);
            return provider.Update(arenaTeammemberEntity,trans);
        }
		
		#endregion	
		
		
	}
}
