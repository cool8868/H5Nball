
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
    /// LogError管理类
    /// </summary>
    public static partial class LogErrorMgr
    {
        
		#region  GetById
		
        public static LogErrorEntity GetById( System.Int32 idx)
        {
            var provider = new LogErrorProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LogErrorEntity> GetAll()
        {
            var provider = new LogErrorProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            LogErrorProvider provider = new LogErrorProvider();

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LogErrorEntity logErrorEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LogErrorProvider(zoneId);
            return provider.Insert(logErrorEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LogErrorEntity logErrorEntity,DbTransaction trans=null)
        {
            var provider = new LogErrorProvider();
            return provider.Update(logErrorEntity,trans);
        }
		
		#endregion	
		
		
	}
}

