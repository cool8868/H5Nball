
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
    /// RevelationHistoryofthegap管理类
    /// </summary>
    public static partial class RevelationHistoryofthegapMgr
    {
        
		#region  GetById
		
        public static RevelationHistoryofthegapEntity GetById( System.Int32 customsPass, System.Int32 schedule,string zoneId="")
        {
            var provider = new RevelationHistoryofthegapProvider(zoneId);
            return provider.GetById( customsPass, schedule);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RevelationHistoryofthegapEntity> GetAll(string zoneId="")
        {
            var provider = new RevelationHistoryofthegapProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  C_RevelationHistoryOfTheGapGetId
		
        public static List<RevelationHistoryofthegapEntity> C_RevelationHistoryOfTheGapGetId( System.Int32 mark,string zoneId="")
        {
            var provider = new RevelationHistoryofthegapProvider(zoneId);
            return provider.C_RevelationHistoryOfTheGapGetId( mark);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 customsPass, System.Int32 schedule,DbTransaction trans=null,string zoneId="")
        {
            RevelationHistoryofthegapProvider provider = new RevelationHistoryofthegapProvider(zoneId);

            return provider.Delete( customsPass, schedule,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RevelationHistoryofthegapEntity revelationHistoryofthegapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationHistoryofthegapProvider(zoneId);
            return provider.Insert(revelationHistoryofthegapEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RevelationHistoryofthegapEntity revelationHistoryofthegapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationHistoryofthegapProvider(zoneId);
            return provider.Update(revelationHistoryofthegapEntity,trans);
        }
		
		#endregion	
		
		
	}
}
