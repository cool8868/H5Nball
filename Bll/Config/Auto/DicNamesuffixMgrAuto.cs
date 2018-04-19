
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
    /// DicNamesuffix管理类
    /// </summary>
    public static partial class DicNamesuffixMgr
    {
        
		#region  GetById
		
        public static DicNamesuffixEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicNamesuffixProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicNamesuffixEntity> GetAll(string zoneId="")
        {
            var provider = new DicNamesuffixProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicNamesuffixProvider provider = new DicNamesuffixProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicNamesuffixEntity dicNamesuffixEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicNamesuffixProvider(zoneId);
            return provider.Insert(dicNamesuffixEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicNamesuffixEntity dicNamesuffixEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicNamesuffixProvider(zoneId);
            return provider.Update(dicNamesuffixEntity,trans);
        }
		
		#endregion	
		
		
	}
}

