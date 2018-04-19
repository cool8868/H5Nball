
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
    /// GmLog管理类
    /// </summary>
    public static partial class GmLogMgr
    {
        
		#region  GetById
		
        public static GmLogEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new GmLogProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<GmLogEntity> GetAll(string zoneId="")
        {
            var provider = new GmLogProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            GmLogProvider provider = new GmLogProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(GmLogEntity gmLogEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GmLogProvider(zoneId);
            return provider.Insert(gmLogEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(GmLogEntity gmLogEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GmLogProvider(zoneId);
            return provider.Update(gmLogEntity,trans);
        }
		
		#endregion	
		
		
	}
}
