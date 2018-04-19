
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
    /// DailycupCompetitors管理类
    /// </summary>
    public static partial class DailycupCompetitorsMgr
    {
        
		#region  GetById
		
        public static DailycupCompetitorsEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DailycupCompetitorsProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DailycupCompetitorsEntity> GetAll(string zoneId="")
        {
            var provider = new DailycupCompetitorsProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByDailycupId
		
        public static List<DailycupCompetitorsEntity> GetByDailycupId( System.Int32 dailyCupId,string zoneId="")
        {
            var provider = new DailycupCompetitorsProvider(zoneId);
            return provider.GetByDailycupId( dailyCupId);            
        }
		
		#endregion		  
		
		#region  GetForFight
		
        public static List<DailycupCompetitorsEntity> GetForFight( System.Int32 dailyCupId,string zoneId="")
        {
            var provider = new DailycupCompetitorsProvider(zoneId);
            return provider.GetForFight( dailyCupId);            
        }
		
		#endregion		  
		
		#region  Attend
		
        public static bool Attend ( System.Int32 dailyCupId, System.Guid managerId, System.Int32 attendRepeatCode,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            DailycupCompetitorsProvider provider = new DailycupCompetitorsProvider(zoneId);

            return provider.Attend( dailyCupId, managerId, attendRepeatCode,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  ExistsByManager
		
        public static bool ExistsByManager ( System.Int32 dailyCupId, System.Guid managerId,ref  System.Boolean isExists,DbTransaction trans=null,string zoneId="")
        {
            DailycupCompetitorsProvider provider = new DailycupCompetitorsProvider(zoneId);

            return provider.ExistsByManager( dailyCupId, managerId,ref  isExists,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DailycupCompetitorsEntity dailycupCompetitorsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DailycupCompetitorsProvider(zoneId);
            return provider.Insert(dailycupCompetitorsEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DailycupCompetitorsEntity dailycupCompetitorsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DailycupCompetitorsProvider(zoneId);
            return provider.Update(dailycupCompetitorsEntity,trans);
        }
		
		#endregion	
		
		
	}
}

