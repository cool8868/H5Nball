
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
    /// EuropeSeason管理类
    /// </summary>
    public static partial class EuropeSeasonMgr
    {
        
		#region  GetById
		
        public static EuropeSeasonEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new EuropeSeasonProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetSeason
		
        public static EuropeSeasonEntity GetSeason( System.DateTime dateTime,string zoneId="")
        {
            var provider = new EuropeSeasonProvider(zoneId);
            return provider.GetSeason( dateTime);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<EuropeSeasonEntity> GetAll(string zoneId="")
        {
            var provider = new EuropeSeasonProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            EuropeSeasonProvider provider = new EuropeSeasonProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(EuropeSeasonEntity europeSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EuropeSeasonProvider(zoneId);
            return provider.Insert(europeSeasonEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(EuropeSeasonEntity europeSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EuropeSeasonProvider(zoneId);
            return provider.Update(europeSeasonEntity,trans);
        }
		
		#endregion	
		
		
	}
}
