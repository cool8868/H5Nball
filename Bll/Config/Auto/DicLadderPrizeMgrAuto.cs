
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
    /// DicLadderprize管理类
    /// </summary>
    public static partial class DicLadderprizeMgr
    {
        
		#region  GetById
		
        public static DicLadderprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicLadderprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicLadderprizeEntity> GetAll(string zoneId="")
        {
            var provider = new DicLadderprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicLadderprizeEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicLadderprizeProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicLadderprizeProvider provider = new DicLadderprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicLadderprizeEntity dicLadderprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicLadderprizeProvider(zoneId);
            return provider.Insert(dicLadderprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicLadderprizeEntity dicLadderprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicLadderprizeProvider(zoneId);
            return provider.Update(dicLadderprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

