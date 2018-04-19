
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
    /// PlayerkillRevenge管理类
    /// </summary>
    public static partial class PlayerkillRevengeMgr
    {
        
		#region  GetById
		
        public static PlayerkillRevengeEntity GetById( System.Int64 idx,string zoneId="")
        {
            var provider = new PlayerkillRevengeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<PlayerkillRevengeEntity> GetAll(string zoneId="")
        {
            var provider = new PlayerkillRevengeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetRevenge
		
        public static List<PlayerkillRevengeEntity> GetRevenge( System.Guid managerId,string zoneId="")
        {
            var provider = new PlayerkillRevengeProvider(zoneId);
            return provider.GetRevenge( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int64 idx,DbTransaction trans=null,string zoneId="")
        {
            PlayerkillRevengeProvider provider = new PlayerkillRevengeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(PlayerkillRevengeEntity playerkillRevengeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PlayerkillRevengeProvider(zoneId);
            return provider.Insert(playerkillRevengeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(PlayerkillRevengeEntity playerkillRevengeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PlayerkillRevengeProvider(zoneId);
            return provider.Update(playerkillRevengeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

