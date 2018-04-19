
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
    /// NbManagerskillmixrecord管理类
    /// </summary>
    public static partial class NbManagerskillmixrecordMgr
    {
        
		#region  GetById
		
        public static NbManagerskillmixrecordEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagerskillmixrecordProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagerskillmixrecordEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagerskillmixrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            NbManagerskillmixrecordProvider provider = new NbManagerskillmixrecordProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagerskillmixrecordEntity nbManagerskillmixrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerskillmixrecordProvider(zoneId);
            return provider.Insert(nbManagerskillmixrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagerskillmixrecordEntity nbManagerskillmixrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerskillmixrecordProvider(zoneId);
            return provider.Update(nbManagerskillmixrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

