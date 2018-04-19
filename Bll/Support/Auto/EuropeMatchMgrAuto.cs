
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
    /// EuropeMatch管理类
    /// </summary>
    public static partial class EuropeMatchMgr
    {
        
		#region  GetById
		
        public static EuropeMatchEntity GetById( System.Int32 matchId,string zoneId="")
        {
            var provider = new EuropeMatchProvider(zoneId);
            return provider.GetById( matchId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<EuropeMatchEntity> GetAll(string zoneId="")
        {
            var provider = new EuropeMatchProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllMatvch
		
        public static List<EuropeMatchEntity> GetAllMatvch( System.DateTime startDate,string zoneId="")
        {
            var provider = new EuropeMatchProvider(zoneId);
            return provider.GetAllMatvch( startDate);            
        }
		
		#endregion		  
		
		#region  GetIsMatch
		
		public static Int32 GetIsMatch ( System.DateTime matchDate,string zoneId="")
        {
            var provider = new EuropeMatchProvider(zoneId);
            return provider.GetIsMatch( matchDate);
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 matchId,DbTransaction trans=null,string zoneId="")
        {
            EuropeMatchProvider provider = new EuropeMatchProvider(zoneId);

            return provider.Delete( matchId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(EuropeMatchEntity europeMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EuropeMatchProvider(zoneId);
            return provider.Insert(europeMatchEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(EuropeMatchEntity europeMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EuropeMatchProvider(zoneId);
            return provider.Update(europeMatchEntity,trans);
        }
		
		#endregion	
		
		
	}
}
