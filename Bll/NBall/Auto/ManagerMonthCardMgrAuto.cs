
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
    /// ManagerMonthcard管理类
    /// </summary>
    public static partial class ManagerMonthcardMgr
    {
        
		#region  GetById
		
        public static ManagerMonthcardEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new ManagerMonthcardProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ManagerMonthcardEntity> GetAll(string zoneId="")
        {
            var provider = new ManagerMonthcardProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetPrizeList
		
        public static List<ManagerMonthcardEntity> GetPrizeList( System.DateTime dayDate,string zoneId="")
        {
            var provider = new ManagerMonthcardProvider(zoneId);
            return provider.GetPrizeList( dayDate);            
        }
		
		#endregion		  
		
		#region  Insert
		
        public static bool Insert ( System.Int32 buyNumber, System.DateTime buyTime, System.DateTime dueToTime, System.DateTime prizeDate, System.DateTime updateTime, System.DateTime rowTime,ref  System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            ManagerMonthcardProvider provider = new ManagerMonthcardProvider(zoneId);

            return provider.Insert( buyNumber, buyTime, dueToTime, prizeDate, updateTime, rowTime,ref  managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ManagerMonthcardEntity managerMonthcardEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerMonthcardProvider(zoneId);
            return provider.Insert(managerMonthcardEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ManagerMonthcardEntity managerMonthcardEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerMonthcardProvider(zoneId);
            return provider.Update(managerMonthcardEntity,trans);
        }
		
		#endregion	
		
		
	}
}

