
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
    /// ManagerChargenumber管理类
    /// </summary>
    public static partial class ManagerChargenumberMgr
    {
        
		#region  GetById
		
        public static ManagerChargenumberEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ManagerChargenumberProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByManagerId
		
        public static ManagerChargenumberEntity GetByManagerId( System.Guid managerId, System.Int32 mallCode,string zoneId="")
        {
            var provider = new ManagerChargenumberProvider(zoneId);
            return provider.GetByManagerId( managerId, mallCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ManagerChargenumberEntity> GetAll(string zoneId="")
        {
            var provider = new ManagerChargenumberProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetManagerIdList
		
        public static List<ManagerChargenumberEntity> GetManagerIdList( System.Guid managerId,string zoneId="")
        {
            var provider = new ManagerChargenumberProvider(zoneId);
            return provider.GetManagerIdList( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ManagerChargenumberProvider provider = new ManagerChargenumberProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ManagerChargenumberEntity managerChargenumberEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerChargenumberProvider(zoneId);
            return provider.Insert(managerChargenumberEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ManagerChargenumberEntity managerChargenumberEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerChargenumberProvider(zoneId);
            return provider.Update(managerChargenumberEntity,trans);
        }
		
		#endregion	
		
		
	}
}

