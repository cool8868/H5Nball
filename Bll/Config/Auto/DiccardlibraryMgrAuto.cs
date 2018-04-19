
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
    /// DicCardlibrary管理类
    /// </summary>
    public static partial class DicCardlibraryMgr
    {
        
		#region  GetById
		
        public static DicCardlibraryEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicCardlibraryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicCardlibraryEntity> GetAll(string zoneId="")
        {
            var provider = new DicCardlibraryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicCardlibraryProvider provider = new DicCardlibraryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicCardlibraryEntity dicCardlibraryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicCardlibraryProvider(zoneId);
            return provider.Insert(dicCardlibraryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicCardlibraryEntity dicCardlibraryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicCardlibraryProvider(zoneId);
            return provider.Update(dicCardlibraryEntity,trans);
        }
		
		#endregion	
		
		
	}
}

