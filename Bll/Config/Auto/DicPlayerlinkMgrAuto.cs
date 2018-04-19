
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
    /// DicPlayerlink管理类
    /// </summary>
    public static partial class DicPlayerlinkMgr
    {
        
		#region  GetById
		
        public static DicPlayerlinkEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicPlayerlinkProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicPlayerlinkEntity> GetAll(string zoneId="")
        {
            var provider = new DicPlayerlinkProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicPlayerlinkProvider provider = new DicPlayerlinkProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicPlayerlinkEntity dicPlayerlinkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicPlayerlinkProvider(zoneId);
            return provider.Insert(dicPlayerlinkEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicPlayerlinkEntity dicPlayerlinkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicPlayerlinkProvider(zoneId);
            return provider.Update(dicPlayerlinkEntity,trans);
        }
		
		#endregion	
		
		
	}
}
