
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
    /// DicBallsoul管理类
    /// </summary>
    public static partial class DicBallsoulMgr
    {
        
		#region  GetById
		
        public static DicBallsoulEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicBallsoulProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicBallsoulEntity> GetAll(string zoneId="")
        {
            var provider = new DicBallsoulProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicBallsoulEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicBallsoulProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicBallsoulProvider provider = new DicBallsoulProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicBallsoulEntity dicBallsoulEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicBallsoulProvider(zoneId);
            return provider.Insert(dicBallsoulEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicBallsoulEntity dicBallsoulEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicBallsoulProvider(zoneId);
            return provider.Update(dicBallsoulEntity,trans);
        }
		
		#endregion	
		
		
	}
}

