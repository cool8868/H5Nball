
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
    /// DicGuildskill管理类
    /// </summary>
    public static partial class DicGuildskillMgr
    {
        
		#region  GetById
		
        public static DicGuildskillEntity GetById( System.String skillCode,string zoneId="")
        {
            var provider = new DicGuildskillProvider(zoneId);
            return provider.GetById( skillCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicGuildskillEntity> GetAll(string zoneId="")
        {
            var provider = new DicGuildskillProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String skillCode,DbTransaction trans=null,string zoneId="")
        {
            DicGuildskillProvider provider = new DicGuildskillProvider(zoneId);

            return provider.Delete( skillCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicGuildskillEntity dicGuildskillEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicGuildskillProvider(zoneId);
            return provider.Insert(dicGuildskillEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicGuildskillEntity dicGuildskillEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicGuildskillProvider(zoneId);
            return provider.Update(dicGuildskillEntity,trans);
        }
		
		#endregion	
		
		
	}
}

