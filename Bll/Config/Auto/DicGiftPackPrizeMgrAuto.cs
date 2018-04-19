
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
    /// DicGiftpackprize管理类
    /// </summary>
    public static partial class DicGiftpackprizeMgr
    {
        
		#region  GetById
		
        public static DicGiftpackprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicGiftpackprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicGiftpackprizeEntity> GetAll(string zoneId="")
        {
            var provider = new DicGiftpackprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicGiftpackprizeProvider provider = new DicGiftpackprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicGiftpackprizeEntity dicGiftpackprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicGiftpackprizeProvider(zoneId);
            return provider.Insert(dicGiftpackprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicGiftpackprizeEntity dicGiftpackprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicGiftpackprizeProvider(zoneId);
            return provider.Update(dicGiftpackprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

