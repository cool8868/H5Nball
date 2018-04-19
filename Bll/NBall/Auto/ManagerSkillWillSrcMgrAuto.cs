
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
    /// ManagerskillWillsrc管理类
    /// </summary>
    public static partial class ManagerskillWillsrcMgr
    {
        
		#region  GetById
		
        public static ManagerskillWillsrcEntity GetById( System.Int64 id,string zoneId="")
        {
            var provider = new ManagerskillWillsrcProvider(zoneId);
            return provider.GetById( id);
        }
		
		#endregion		  
		
		#region  GetWillSrc
		
        public static ManagerskillWillsrcEntity GetWillSrc( System.Guid managerId, System.String willCode,string zoneId="")
        {
            var provider = new ManagerskillWillsrcProvider(zoneId);
            return provider.GetWillSrc( managerId, willCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ManagerskillWillsrcEntity> GetAll(string zoneId="")
        {
            var provider = new ManagerskillWillsrcProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetWillSrcList
		
        public static List<ManagerskillWillsrcEntity> GetWillSrcList( System.Guid managerId,string zoneId="")
        {
            var provider = new ManagerskillWillsrcProvider(zoneId);
            return provider.GetWillSrcList( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int64 id, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            ManagerskillWillsrcProvider provider = new ManagerskillWillsrcProvider(zoneId);

            return provider.Delete( id, rowVersion,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ManagerskillWillsrcEntity managerskillWillsrcEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerskillWillsrcProvider(zoneId);
            return provider.Insert(managerskillWillsrcEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ManagerskillWillsrcEntity managerskillWillsrcEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerskillWillsrcProvider(zoneId);
            return provider.Update(managerskillWillsrcEntity,trans);
        }
		
		#endregion	
		
		
	}
}

