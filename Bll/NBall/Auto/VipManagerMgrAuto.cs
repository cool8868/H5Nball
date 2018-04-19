
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
    /// VipManager管理类
    /// </summary>
    public static partial class VipManagerMgr
    {
        
		#region  GetById
		
        public static VipManagerEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new VipManagerProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<VipManagerEntity> GetAll(string zoneId="")
        {
            var provider = new VipManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            VipManagerProvider provider = new VipManagerProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(VipManagerEntity vipManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new VipManagerProvider(zoneId);
            return provider.Insert(vipManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(VipManagerEntity vipManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new VipManagerProvider(zoneId);
            return provider.Update(vipManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}

