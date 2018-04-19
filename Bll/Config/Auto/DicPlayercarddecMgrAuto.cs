
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
    /// DicPlayercarddec管理类
    /// </summary>
    public static partial class DicPlayercarddecMgr
    {
        
		#region  GetById
		
        public static DicPlayercarddecEntity GetById( System.Int32 itemCode,string zoneId="")
        {
            var provider = new DicPlayercarddecProvider(zoneId);
            return provider.GetById( itemCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicPlayercarddecEntity> GetAll(string zoneId="")
        {
            var provider = new DicPlayercarddecProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 itemCode,DbTransaction trans=null,string zoneId="")
        {
            DicPlayercarddecProvider provider = new DicPlayercarddecProvider(zoneId);

            return provider.Delete( itemCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicPlayercarddecEntity dicPlayercarddecEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicPlayercarddecProvider(zoneId);
            return provider.Insert(dicPlayercarddecEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicPlayercarddecEntity dicPlayercarddecEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicPlayercarddecProvider(zoneId);
            return provider.Update(dicPlayercarddecEntity,trans);
        }
		
		#endregion	
		
		
	}
}

