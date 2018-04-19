
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
    /// ArenaManagerinfo管理类
    /// </summary>
    public static partial class ArenaManagerinfoMgr
    {
        
		#region  GetById
		
        public static ArenaManagerinfoEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new ArenaManagerinfoProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetChampionMax
		
        public static ArenaManagerinfoEntity GetChampionMax( System.Int32 domainId,string zoneId="")
        {
            var provider = new ArenaManagerinfoProvider(zoneId);
            return provider.GetChampionMax( domainId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ArenaManagerinfoEntity> GetAll(string zoneId="")
        {
            var provider = new ArenaManagerinfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            ArenaManagerinfoProvider provider = new ArenaManagerinfoProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
		#region  ImportRecord
		
        public static bool ImportRecord ( System.Int32 seasonId, System.Int32 arenaType, System.Int32 domainId,DbTransaction trans=null,string zoneId="")
        {
            ArenaManagerinfoProvider provider = new ArenaManagerinfoProvider(zoneId);

            return provider.ImportRecord( seasonId, arenaType, domainId,trans);
            
        }
		
		#endregion
        
		#region  AddArenaCoin
		
        public static bool AddArenaCoin ( System.Guid managerId, System.Int32 arenaCoin,DbTransaction trans=null,string zoneId="")
        {
            ArenaManagerinfoProvider provider = new ArenaManagerinfoProvider(zoneId);

            return provider.AddArenaCoin( managerId, arenaCoin,trans);
            
        }
		
		#endregion
        
		#region  SetChampion
		
        public static bool SetChampion ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            ArenaManagerinfoProvider provider = new ArenaManagerinfoProvider(zoneId);

            return provider.SetChampion( managerId,trans);
            
        }
		
		#endregion
        
		#region  ClearRecord
		
        public static bool ClearRecord ( System.Int32 arenaType, System.Int32 domainId,DbTransaction trans=null,string zoneId="")
        {
            ArenaManagerinfoProvider provider = new ArenaManagerinfoProvider(zoneId);

            return provider.ClearRecord( arenaType, domainId,trans);
            
        }
		
		#endregion
        
		#region  SetRank
		
        public static bool SetRank ( System.Int32 domainId,DbTransaction trans=null,string zoneId="")
        {
            ArenaManagerinfoProvider provider = new ArenaManagerinfoProvider(zoneId);

            return provider.SetRank( domainId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ArenaManagerinfoEntity arenaManagerinfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaManagerinfoProvider(zoneId);
            return provider.Insert(arenaManagerinfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ArenaManagerinfoEntity arenaManagerinfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaManagerinfoProvider(zoneId);
            return provider.Update(arenaManagerinfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}
