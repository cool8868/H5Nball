
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
    /// RevelationDraw管理类
    /// </summary>
    public static partial class RevelationDrawMgr
    {
        
		#region  GetById
		
        public static RevelationDrawEntity GetById( System.Guid drawId,string zoneId="")
        {
            var provider = new RevelationDrawProvider(zoneId);
            return provider.GetById( drawId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RevelationDrawEntity> GetAll(string zoneId="")
        {
            var provider = new RevelationDrawProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid drawId,DbTransaction trans=null,string zoneId="")
        {
            RevelationDrawProvider provider = new RevelationDrawProvider(zoneId);

            return provider.Delete( drawId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RevelationDrawEntity revelationDrawEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationDrawProvider(zoneId);
            return provider.Insert(revelationDrawEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RevelationDrawEntity revelationDrawEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationDrawProvider(zoneId);
            return provider.Update(revelationDrawEntity,trans);
        }
		
		#endregion	
		
		
	}
}
