
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
    /// LogInfo管理类
    /// </summary>
    public static partial class LogInfoMgr
    {
        
		#region  GetById
		
        public static LogInfoEntity GetById( System.Int32 idx)
        {
            var provider = new LogInfoProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LogInfoEntity> GetAll()
        {
            var provider = new LogInfoProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            LogInfoProvider provider = new LogInfoProvider();

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LogInfoEntity logInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LogInfoProvider(zoneId);
            return provider.Insert(logInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LogInfoEntity logInfoEntity,DbTransaction trans=null)
        {
            var provider = new LogInfoProvider();
            return provider.Update(logInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}

