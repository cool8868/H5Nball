
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
    /// CrosscrowdManagerhistory管理类
    /// </summary>
    public static partial class CrosscrowdManagerhistoryMgr
    {
        
		#region  GetById
		
        public static CrosscrowdManagerhistoryEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new CrosscrowdManagerhistoryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrosscrowdManagerhistoryEntity> GetAll(string zoneId="")
        {
            var provider = new CrosscrowdManagerhistoryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            CrosscrowdManagerhistoryProvider provider = new CrosscrowdManagerhistoryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrosscrowdManagerhistoryEntity crosscrowdManagerhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdManagerhistoryProvider(zoneId);
            return provider.Insert(crosscrowdManagerhistoryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrosscrowdManagerhistoryEntity crosscrowdManagerhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdManagerhistoryProvider(zoneId);
            return provider.Update(crosscrowdManagerhistoryEntity,trans);
        }
		
		#endregion	
		
		
	}
}
