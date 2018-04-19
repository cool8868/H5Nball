
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
    /// ManagerskillLib管理类
    /// </summary>
    public static partial class ManagerskillLibMgr
    {
        
		#region  GetById
		
        public static ManagerskillLibEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new ManagerskillLibProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ManagerskillLibEntity> GetAll(string zoneId="")
        {
            var provider = new ManagerskillLibProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            ManagerskillLibProvider provider = new ManagerskillLibProvider(zoneId);

            return provider.Delete( managerId, rowVersion,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ManagerskillLibEntity managerskillLibEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerskillLibProvider(zoneId);
            return provider.Insert(managerskillLibEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ManagerskillLibEntity managerskillLibEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerskillLibProvider(zoneId);
            return provider.Update(managerskillLibEntity,trans);
        }
		
		#endregion	
		
		
	}
}

