
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
    /// GambleSeason管理类
    /// </summary>
    public static partial class GambleSeasonMgr
    {
        
		#region  GetCurrent
		
        public static GambleSeasonEntity GetCurrent(string zoneId="")
        {
            var provider = new GambleSeasonProvider(zoneId);
            return provider.GetCurrent();
        }
		
		#endregion		  
		
		#region  GetLastNotOpend
		
        public static GambleSeasonEntity GetLastNotOpend(string zoneId="")
        {
            var provider = new GambleSeasonProvider(zoneId);
            return provider.GetLastNotOpend();
        }
		
		#endregion		  
		
		#region  GetById
		
        public static GambleSeasonEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new GambleSeasonProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<GambleSeasonEntity> GetAll(string zoneId="")
        {
            var provider = new GambleSeasonProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  CreateOnce
		
        public static bool CreateOnce (DbTransaction trans=null,string zoneId="")
        {
            GambleSeasonProvider provider = new GambleSeasonProvider(zoneId);

            return provider.CreateOnce(trans);
            
        }
		
		#endregion
        
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            GambleSeasonProvider provider = new GambleSeasonProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(GambleSeasonEntity gambleSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleSeasonProvider(zoneId);
            return provider.Insert(gambleSeasonEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(GambleSeasonEntity gambleSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleSeasonProvider(zoneId);
            return provider.Update(gambleSeasonEntity,trans);
        }
		
		#endregion	
		
		
	}
}
