
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
    /// GambleDetail管理类
    /// </summary>
    public static partial class GambleDetailMgr
    {
        
		#region  GetById
		
        public static GambleDetailEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new GambleDetailProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByOptionId
		
        public static List<GambleDetailEntity> GetByOptionId( System.Int32 optionId,string zoneId="")
        {
            var provider = new GambleDetailProvider(zoneId);
            return provider.GetByOptionId( optionId);            
        }
		
		#endregion		  
		
		#region  GetByManagerId
		
        public static List<GambleDetailEntity> GetByManagerId( System.Guid managerId,string zoneId="")
        {
            var provider = new GambleDetailProvider(zoneId);
            return provider.GetByManagerId( managerId);            
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<GambleDetailEntity> GetAll(string zoneId="")
        {
            var provider = new GambleDetailProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            GambleDetailProvider provider = new GambleDetailProvider(zoneId);

            return provider.Delete( idx, rowVersion,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(GambleDetailEntity gambleDetailEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleDetailProvider(zoneId);
            return provider.Insert(gambleDetailEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(GambleDetailEntity gambleDetailEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleDetailProvider(zoneId);
            return provider.Update(gambleDetailEntity,trans);
        }
		
		#endregion	
		
		
	}
}
