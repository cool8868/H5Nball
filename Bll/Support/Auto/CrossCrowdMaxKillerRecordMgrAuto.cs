
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
    /// CrosscrowdMaxkillerrecord管理类
    /// </summary>
    public static partial class CrosscrowdMaxkillerrecordMgr
    {
        
		#region  GetById
		
        public static CrosscrowdMaxkillerrecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new CrosscrowdMaxkillerrecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetMaxKiller
		
        public static CrosscrowdMaxkillerrecordEntity GetMaxKiller( System.Int32 crowId,string zoneId="")
        {
            var provider = new CrosscrowdMaxkillerrecordProvider(zoneId);
            return provider.GetMaxKiller( crowId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrosscrowdMaxkillerrecordEntity> GetAll(string zoneId="")
        {
            var provider = new CrosscrowdMaxkillerrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            CrosscrowdMaxkillerrecordProvider provider = new CrosscrowdMaxkillerrecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrosscrowdMaxkillerrecordEntity crosscrowdMaxkillerrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdMaxkillerrecordProvider(zoneId);
            return provider.Insert(crosscrowdMaxkillerrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrosscrowdMaxkillerrecordEntity crosscrowdMaxkillerrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdMaxkillerrecordProvider(zoneId);
            return provider.Update(crosscrowdMaxkillerrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}
