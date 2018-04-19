
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
    /// InvestManager管理类
    /// </summary>
    public static partial class InvestManagerMgr
    {
        
		#region  GetById
		
        public static InvestManagerEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new InvestManagerProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<InvestManagerEntity> GetAll(string zoneId="")
        {
            var provider = new InvestManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            InvestManagerProvider provider = new InvestManagerProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(InvestManagerEntity investManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new InvestManagerProvider(zoneId);
            return provider.Insert(investManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(InvestManagerEntity investManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new InvestManagerProvider(zoneId);
            return provider.Update(investManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}

