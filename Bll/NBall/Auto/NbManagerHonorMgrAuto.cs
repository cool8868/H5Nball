
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
    /// NbManagerhonor管理类
    /// </summary>
    public static partial class NbManagerhonorMgr
    {
        
		#region  GetById
		
        public static NbManagerhonorEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new NbManagerhonorProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagerhonorEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagerhonorProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByManager
		
        public static List<NbManagerhonorEntity> GetByManager( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagerhonorProvider(zoneId);
            return provider.GetByManager( managerId);            
        }
		
		#endregion		  
		
		#region  GetByManagerTop
		
        public static List<NbManagerhonorEntity> GetByManagerTop( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagerhonorProvider(zoneId);
            return provider.GetByManagerTop( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            NbManagerhonorProvider provider = new NbManagerhonorProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Add
		
        public static bool Add ( System.Guid managerId, System.Int32 matchType, System.Int32 subType, System.Int32 periodId, System.Int32 rank,DbTransaction trans=null,string zoneId="")
        {
            NbManagerhonorProvider provider = new NbManagerhonorProvider(zoneId);

            return provider.Add( managerId, matchType, subType, periodId, rank,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagerhonorEntity nbManagerhonorEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerhonorProvider(zoneId);
            return provider.Insert(nbManagerhonorEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagerhonorEntity nbManagerhonorEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerhonorProvider(zoneId);
            return provider.Update(nbManagerhonorEntity,trans);
        }
		
		#endregion	
		
		
	}
}

