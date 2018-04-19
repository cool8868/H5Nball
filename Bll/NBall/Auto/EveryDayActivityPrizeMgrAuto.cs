
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
    /// Everydayactivityprize管理类
    /// </summary>
    public static partial class EverydayactivityprizeMgr
    {
        
		#region  GetById
		
        public static EverydayactivityprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new EverydayactivityprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<EverydayactivityprizeEntity> GetAll(string zoneId="")
        {
            var provider = new EverydayactivityprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetManagerInfo
		
        public static List<EverydayactivityprizeEntity> GetManagerInfo( System.Guid managerId,string zoneId="")
        {
            var provider = new EverydayactivityprizeProvider(zoneId);
            return provider.GetManagerInfo( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            EverydayactivityprizeProvider provider = new EverydayactivityprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            EverydayactivityprizeProvider provider = new EverydayactivityprizeProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(EverydayactivityprizeEntity everydayactivityprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EverydayactivityprizeProvider(zoneId);
            return provider.Insert(everydayactivityprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(EverydayactivityprizeEntity everydayactivityprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EverydayactivityprizeProvider(zoneId);
            return provider.Update(everydayactivityprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

