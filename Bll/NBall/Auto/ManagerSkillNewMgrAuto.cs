
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
    /// ManagerskillNew管理类
    /// </summary>
    public static partial class ManagerskillNewMgr
    {
        
		#region  GetById
		
        public static ManagerskillNewEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new ManagerskillNewProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ManagerskillNewEntity> GetAll(string zoneId="")
        {
            var provider = new ManagerskillNewProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
        
		#region Insert

        public static bool Insert(ManagerskillNewEntity managerskillNewEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerskillNewProvider(zoneId);
            return provider.Insert(managerskillNewEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ManagerskillNewEntity managerskillNewEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerskillNewProvider(zoneId);
            return provider.Update(managerskillNewEntity,trans);
        }
		
		#endregion	
		
		
	}
}

