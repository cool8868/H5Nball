
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
    /// LogSchedule管理类
    /// </summary>
    public static partial class LogScheduleMgr
    {
        
		#region  GetById
		
        public static LogScheduleEntity GetById( System.Int32 idx)
        {
            var provider = new LogScheduleProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LogScheduleEntity> GetAll()
        {
            var provider = new LogScheduleProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            LogScheduleProvider provider = new LogScheduleProvider();

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LogScheduleEntity logScheduleEntity,DbTransaction trans=null)
        {
            var provider = new LogScheduleProvider();
            return provider.Insert(logScheduleEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LogScheduleEntity logScheduleEntity,DbTransaction trans=null)
        {
            var provider = new LogScheduleProvider();
            return provider.Update(logScheduleEntity,trans);
        }
		
		#endregion	
		
		
	}
}

