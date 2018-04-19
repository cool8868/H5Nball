
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
    /// DicNameprefix管理类
    /// </summary>
    public static partial class DicNameprefixMgr
    {
        
		#region  GetById
		
        public static DicNameprefixEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicNameprefixProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicNameprefixEntity> GetAll(string zoneId="")
        {
            var provider = new DicNameprefixProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicNameprefixProvider provider = new DicNameprefixProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicNameprefixEntity dicNameprefixEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicNameprefixProvider(zoneId);
            return provider.Insert(dicNameprefixEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicNameprefixEntity dicNameprefixEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicNameprefixProvider(zoneId);
            return provider.Update(dicNameprefixEntity,trans);
        }
		
		#endregion	
		
		
	}
}

