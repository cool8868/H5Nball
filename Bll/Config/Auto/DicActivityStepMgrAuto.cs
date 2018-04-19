
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
    /// DicActivitystep管理类
    /// </summary>
    public static partial class DicActivitystepMgr
    {
        
		#region  GetById
		
        public static DicActivitystepEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicActivitystepProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicActivitystepEntity> GetAll(string zoneId="")
        {
            var provider = new DicActivitystepProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicActivitystepEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicActivitystepProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicActivitystepProvider provider = new DicActivitystepProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicActivitystepEntity dicActivitystepEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicActivitystepProvider(zoneId);
            return provider.Insert(dicActivitystepEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicActivitystepEntity dicActivitystepEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicActivitystepProvider(zoneId);
            return provider.Update(dicActivitystepEntity,trans);
        }
		
		#endregion	
		
		
	}
}

