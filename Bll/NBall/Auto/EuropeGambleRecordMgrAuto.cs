
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
    /// EuropeGamblerecord管理类
    /// </summary>
    public static partial class EuropeGamblerecordMgr
    {
        
		#region  GetById
		
        public static EuropeGamblerecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new EuropeGamblerecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GambleRecord
		
        public static EuropeGamblerecordEntity GambleRecord( System.Guid managerId, System.Int32 matchId,string zoneId="")
        {
            var provider = new EuropeGamblerecordProvider(zoneId);
            return provider.GambleRecord( managerId, matchId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<EuropeGamblerecordEntity> GetAll(string zoneId="")
        {
            var provider = new EuropeGamblerecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GambleRecord
		
        public static List<EuropeGamblerecordEntity> GambleRecord( System.Guid managerId,string zoneId="")
        {
            var provider = new EuropeGamblerecordProvider(zoneId);
            return provider.GambleRecord( managerId);            
        }
		
		#endregion		  
		
		#region  GetNotPrize
		
        public static List<EuropeGamblerecordEntity> GetNotPrize( System.Int32 matchId,string zoneId="")
        {
            var provider = new EuropeGamblerecordProvider(zoneId);
            return provider.GetNotPrize( matchId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            EuropeGamblerecordProvider provider = new EuropeGamblerecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(EuropeGamblerecordEntity europeGamblerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EuropeGamblerecordProvider(zoneId);
            return provider.Insert(europeGamblerecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(EuropeGamblerecordEntity europeGamblerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EuropeGamblerecordProvider(zoneId);
            return provider.Update(europeGamblerecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

