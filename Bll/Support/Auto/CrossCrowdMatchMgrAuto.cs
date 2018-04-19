
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
    /// CrosscrowdMatch管理类
    /// </summary>
    public static partial class CrosscrowdMatchMgr
    {
        
		#region  GetById
		
        public static CrosscrowdMatchEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new CrosscrowdMatchProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrosscrowdMatchEntity> GetAll(string zoneId="")
        {
            var provider = new CrosscrowdMatchProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            CrosscrowdMatchProvider provider = new CrosscrowdMatchProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  SaveKillPrizeStatus
		
        public static bool SaveKillPrizeStatus ( System.Guid idx, System.Int32 status,DbTransaction trans=null,string zoneId="")
        {
            CrosscrowdMatchProvider provider = new CrosscrowdMatchProvider(zoneId);

            return provider.SaveKillPrizeStatus( idx, status,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrosscrowdMatchEntity crosscrowdMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdMatchProvider(zoneId);
            return provider.Insert(crosscrowdMatchEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrosscrowdMatchEntity crosscrowdMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdMatchProvider(zoneId);
            return provider.Update(crosscrowdMatchEntity,trans);
        }
		
		#endregion	
		
		
	}
}
