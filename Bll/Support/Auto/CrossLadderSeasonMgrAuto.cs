
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
    /// CrossladderSeason管理类
    /// </summary>
    public static partial class CrossladderSeasonMgr
    {
        
		#region  GetById
		
        public static CrossladderSeasonEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new CrossladderSeasonProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrossladderSeasonEntity> GetAll(string zoneId="")
        {
            var provider = new CrossladderSeasonProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            CrossladderSeasonProvider provider = new CrossladderSeasonProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrossladderSeasonEntity crossladderSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderSeasonProvider(zoneId);
            return provider.Insert(crossladderSeasonEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrossladderSeasonEntity crossladderSeasonEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderSeasonProvider(zoneId);
            return provider.Update(crossladderSeasonEntity,trans);
        }
		
		#endregion	
		
		
	}
}
