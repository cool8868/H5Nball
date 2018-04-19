
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
    /// NbManagershare管理类
    /// </summary>
    public static partial class NbManagershareMgr
    {
        
		#region  GetById
		
        public static NbManagershareEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagershareProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  Select
		
        public static NbManagershareEntity Select( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagershareProvider(zoneId);
            return provider.Select( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagershareEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagershareProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            NbManagershareProvider provider = new NbManagershareProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagershareEntity nbManagershareEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagershareProvider(zoneId);
            return provider.Insert(nbManagershareEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagershareEntity nbManagershareEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagershareProvider(zoneId);
            return provider.Update(nbManagershareEntity,trans);
        }
		
		#endregion	
		
		
	}
}
