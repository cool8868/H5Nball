
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
    /// NbManagertree管理类
    /// </summary>
    public static partial class NbManagertreeMgr
    {
        
		#region  GetById
		
        public static NbManagertreeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new NbManagertreeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagertreeEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagertreeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByManagerId
		
        public static List<NbManagertreeEntity> GetByManagerId( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagertreeProvider(zoneId);
            return provider.GetByManagerId( managerId);            
        }
		
		#endregion		  
		
		#region  InsertList
		
        public static List<NbManagertreeEntity> InsertList( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagertreeProvider(zoneId);
            return provider.InsertList( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            NbManagertreeProvider provider = new NbManagertreeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  DeleteManagerTree
		
        public static bool DeleteManagerTree ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            NbManagertreeProvider provider = new NbManagertreeProvider(zoneId);

            return provider.DeleteManagerTree( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagertreeEntity nbManagertreeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagertreeProvider(zoneId);
            return provider.Insert(nbManagertreeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagertreeEntity nbManagertreeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagertreeProvider(zoneId);
            return provider.Update(nbManagertreeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

