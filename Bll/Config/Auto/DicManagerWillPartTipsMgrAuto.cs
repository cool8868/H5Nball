
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
    /// DicManagerwillparttips管理类
    /// </summary>
    public static partial class DicManagerwillparttipsMgr
    {
        
		#region  GetById
		
        public static DicManagerwillparttipsEntity GetById( System.Int32 id,string zoneId="")
        {
            var provider = new DicManagerwillparttipsProvider(zoneId);
            return provider.GetById( id);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicManagerwillparttipsEntity> GetAll(string zoneId="")
        {
            var provider = new DicManagerwillparttipsProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 id,DbTransaction trans=null,string zoneId="")
        {
            DicManagerwillparttipsProvider provider = new DicManagerwillparttipsProvider(zoneId);

            return provider.Delete( id,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicManagerwillparttipsEntity dicManagerwillparttipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicManagerwillparttipsProvider(zoneId);
            return provider.Insert(dicManagerwillparttipsEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicManagerwillparttipsEntity dicManagerwillparttipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicManagerwillparttipsProvider(zoneId);
            return provider.Update(dicManagerwillparttipsEntity,trans);
        }
		
		#endregion	
		
		
	}
}

