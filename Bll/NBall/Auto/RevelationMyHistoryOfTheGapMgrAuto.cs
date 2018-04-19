
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
    /// RevelationMyhistoryofthegap管理类
    /// </summary>
    public static partial class RevelationMyhistoryofthegapMgr
    {
        
		#region  GetById
		
        public static RevelationMyhistoryofthegapEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new RevelationMyhistoryofthegapProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetMyMarkHistoryOfTheGap
		
        public static RevelationMyhistoryofthegapEntity GetMyMarkHistoryOfTheGap( System.Guid managerid, System.Int32 mark, System.Int32 littleLevels,string zoneId="")
        {
            var provider = new RevelationMyhistoryofthegapProvider(zoneId);
            return provider.GetMyMarkHistoryOfTheGap( managerid, mark, littleLevels);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RevelationMyhistoryofthegapEntity> GetAll(string zoneId="")
        {
            var provider = new RevelationMyhistoryofthegapProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetMyHistoryOfTheGap
		
        public static List<RevelationMyhistoryofthegapEntity> GetMyHistoryOfTheGap( System.Guid managerId, System.Int32 mark,string zoneId="")
        {
            var provider = new RevelationMyhistoryofthegapProvider(zoneId);
            return provider.GetMyHistoryOfTheGap( managerId, mark);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            RevelationMyhistoryofthegapProvider provider = new RevelationMyhistoryofthegapProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  C_RevelationEverDay
		
        public static bool C_RevelationEverDay ( System.Guid managerid,DbTransaction trans=null,string zoneId="")
        {
            RevelationMyhistoryofthegapProvider provider = new RevelationMyhistoryofthegapProvider(zoneId);

            return provider.C_RevelationEverDay( managerid,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RevelationMyhistoryofthegapEntity revelationMyhistoryofthegapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationMyhistoryofthegapProvider(zoneId);
            return provider.Insert(revelationMyhistoryofthegapEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RevelationMyhistoryofthegapEntity revelationMyhistoryofthegapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationMyhistoryofthegapProvider(zoneId);
            return provider.Update(revelationMyhistoryofthegapEntity,trans);
        }
		
		#endregion	
		
		
	}
}

