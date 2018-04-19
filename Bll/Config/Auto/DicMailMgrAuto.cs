
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
    /// DicMail管理类
    /// </summary>
    public static partial class DicMailMgr
    {
        
		#region  GetById
		
        public static DicMailEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicMailProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicMailEntity> GetAll(string zoneId="")
        {
            var provider = new DicMailProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicMailEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicMailProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicMailProvider provider = new DicMailProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicMailEntity dicMailEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicMailProvider(zoneId);
            return provider.Insert(dicMailEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicMailEntity dicMailEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicMailProvider(zoneId);
            return provider.Update(dicMailEntity,trans);
        }
		
		#endregion	
		
		
	}
}

