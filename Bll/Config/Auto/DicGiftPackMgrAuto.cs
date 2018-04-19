
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
    /// DicGiftpack管理类
    /// </summary>
    public static partial class DicGiftpackMgr
    {
        
		#region  GetById
		
        public static DicGiftpackEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicGiftpackProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicGiftpackEntity> GetAll(string zoneId="")
        {
            var provider = new DicGiftpackProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicGiftpackProvider provider = new DicGiftpackProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicGiftpackEntity dicGiftpackEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicGiftpackProvider(zoneId);
            return provider.Insert(dicGiftpackEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicGiftpackEntity dicGiftpackEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicGiftpackProvider(zoneId);
            return provider.Update(dicGiftpackEntity,trans);
        }
		
		#endregion	
		
		
	}
}

