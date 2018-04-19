
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
    /// LadderDayprize管理类
    /// </summary>
    public static partial class LadderDayprizeMgr
    {
        
		#region  GetById
		
        public static LadderDayprizeEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new LadderDayprizeProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LadderDayprizeEntity> GetAll(string zoneId="")
        {
            var provider = new LadderDayprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            LadderDayprizeProvider provider = new LadderDayprizeProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LadderDayprizeEntity ladderDayprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderDayprizeProvider(zoneId);
            return provider.Insert(ladderDayprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LadderDayprizeEntity ladderDayprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderDayprizeProvider(zoneId);
            return provider.Update(ladderDayprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

