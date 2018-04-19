
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
    /// NbManagerskillask管理类
    /// </summary>
    public static partial class NbManagerskillaskMgr
    {
        
		#region  GetById
		
        public static NbManagerskillaskEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagerskillaskProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagerskillaskEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagerskillaskProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            NbManagerskillaskProvider provider = new NbManagerskillaskProvider(zoneId);

            return provider.Delete( managerId, rowVersion,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagerskillaskEntity nbManagerskillaskEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerskillaskProvider(zoneId);
            return provider.Insert(nbManagerskillaskEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagerskillaskEntity nbManagerskillaskEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerskillaskProvider(zoneId);
            return provider.Update(nbManagerskillaskEntity,trans);
        }
		
		#endregion	
		
		
	}
}

