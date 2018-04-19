
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
    /// Matchprocess管理类
    /// </summary>
    public static partial class MatchprocessMgr
    {
        
		#region  GetById
		
        public static MatchprocessEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new MatchprocessProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByMatchId
		
        public static MatchprocessEntity GetByMatchId( System.String dateChar, System.Int32 matchType, System.Guid matchId,string zoneId="")
        {
            var provider = new MatchprocessProvider(zoneId);
            return provider.GetByMatchId( dateChar, matchType, matchId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<MatchprocessEntity> GetAll(string zoneId="")
        {
            var provider = new MatchprocessProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            MatchprocessProvider provider = new MatchprocessProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Job_CrossCreateProcessTable
		
        public static bool Job_CrossCreateProcessTable (DbTransaction trans=null,string zoneId="")
        {
            MatchprocessProvider provider = new MatchprocessProvider(zoneId);

            return provider.Job_CrossCreateProcessTable(trans);
            
        }
		
		#endregion
        
		#region  Save
		
        public static bool Save ( System.Int32 matchType, System.Byte[] process, System.DateTime rowTime, System.String dateChar, System.Guid matchId,DbTransaction trans=null,string zoneId="")
        {
            MatchprocessProvider provider = new MatchprocessProvider(zoneId);

            return provider.Save( matchType, process, rowTime, dateChar, matchId,trans);
            
        }
		
		#endregion
        
		#region  Job_CreateProcessTable
		
        public static bool Job_CreateProcessTable (DbTransaction trans=null,string zoneId="")
        {
            MatchprocessProvider provider = new MatchprocessProvider(zoneId);

            return provider.Job_CreateProcessTable(trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(MatchprocessEntity matchprocessEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MatchprocessProvider(zoneId);
            return provider.Insert(matchprocessEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(MatchprocessEntity matchprocessEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MatchprocessProvider(zoneId);
            return provider.Update(matchprocessEntity,trans);
        }
		
		#endregion	
		
		
	}
}

