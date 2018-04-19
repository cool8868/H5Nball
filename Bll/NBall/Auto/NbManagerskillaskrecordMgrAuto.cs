
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
    /// NbManagerskillaskrecord管理类
    /// </summary>
    public static partial class NbManagerskillaskrecordMgr
    {
        
		#region  GetById
		
        public static NbManagerskillaskrecordEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagerskillaskrecordProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagerskillaskrecordEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagerskillaskrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            NbManagerskillaskrecordProvider provider = new NbManagerskillaskrecordProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagerskillaskrecordEntity nbManagerskillaskrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerskillaskrecordProvider(zoneId);
            return provider.Insert(nbManagerskillaskrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagerskillaskrecordEntity nbManagerskillaskrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerskillaskrecordProvider(zoneId);
            return provider.Update(nbManagerskillaskrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

