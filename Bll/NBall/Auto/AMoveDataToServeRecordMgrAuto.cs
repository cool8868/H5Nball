
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
    /// AMovedatatoserverecord管理类
    /// </summary>
    public static partial class AMovedatatoserverecordMgr
    {
        
		#region  GetById
		
        public static AMovedatatoserverecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new AMovedatatoserverecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetDataByAccount
		
        public static List<AMovedatatoserverecordEntity> GetDataByAccount( System.String account,string zoneId="")
        {
            var provider = new AMovedatatoserverecordProvider(zoneId);
            return provider.GetDataByAccount( account);            
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<AMovedatatoserverecordEntity> GetAll(string zoneId="")
        {
            var provider = new AMovedatatoserverecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            AMovedatatoserverecordProvider provider = new AMovedatatoserverecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(AMovedatatoserverecordEntity aMovedatatoserverecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AMovedatatoserverecordProvider(zoneId);
            return provider.Insert(aMovedatatoserverecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(AMovedatatoserverecordEntity aMovedatatoserverecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AMovedatatoserverecordProvider(zoneId);
            return provider.Update(aMovedatatoserverecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

