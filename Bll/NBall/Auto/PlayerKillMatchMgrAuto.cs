
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
    /// PlayerkillMatch管理类
    /// </summary>
    public static partial class PlayerkillMatchMgr
    {
        
		#region  GetById
		
        public static PlayerkillMatchEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new PlayerkillMatchProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<PlayerkillMatchEntity> GetAll(string zoneId="")
        {
            var provider = new PlayerkillMatchProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            PlayerkillMatchProvider provider = new PlayerkillMatchProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(PlayerkillMatchEntity playerkillMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PlayerkillMatchProvider(zoneId);
            return provider.Insert(playerkillMatchEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(PlayerkillMatchEntity playerkillMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PlayerkillMatchProvider(zoneId);
            return provider.Update(playerkillMatchEntity,trans);
        }
		
		#endregion	
		
		
	}
}
