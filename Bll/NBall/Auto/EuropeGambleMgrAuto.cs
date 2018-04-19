
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
    /// EuropeGamble管理类
    /// </summary>
    public static partial class EuropeGambleMgr
    {
        
		#region  GetById
		
        public static EuropeGambleEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new EuropeGambleProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<EuropeGambleEntity> GetAll(string zoneId="")
        {
            var provider = new EuropeGambleProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            EuropeGambleProvider provider = new EuropeGambleProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(EuropeGambleEntity europeGambleEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EuropeGambleProvider(zoneId);
            return provider.Insert(europeGambleEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(EuropeGambleEntity europeGambleEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EuropeGambleProvider(zoneId);
            return provider.Update(europeGambleEntity,trans);
        }
		
		#endregion	
		
		
	}
}
