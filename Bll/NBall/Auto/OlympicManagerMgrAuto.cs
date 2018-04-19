
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
    /// OlympicManager管理类
    /// </summary>
    public static partial class OlympicManagerMgr
    {
        
		#region  GetById
		
        public static OlympicManagerEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new OlympicManagerProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<OlympicManagerEntity> GetAll(string zoneId="")
        {
            var provider = new OlympicManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            OlympicManagerProvider provider = new OlympicManagerProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(OlympicManagerEntity olympicManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new OlympicManagerProvider(zoneId);
            return provider.Insert(olympicManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(OlympicManagerEntity olympicManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new OlympicManagerProvider(zoneId);
            return provider.Update(olympicManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}
