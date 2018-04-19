
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
    /// DicStarskilltips管理类
    /// </summary>
    public static partial class DicStarskilltipsMgr
    {
        
		#region  GetById
		
        public static DicStarskilltipsEntity GetById( System.Int32 skillId,string zoneId="")
        {
            var provider = new DicStarskilltipsProvider(zoneId);
            return provider.GetById( skillId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicStarskilltipsEntity> GetAll(string zoneId="")
        {
            var provider = new DicStarskilltipsProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
        
		#region Insert

        public static bool Insert(DicStarskilltipsEntity dicStarskilltipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicStarskilltipsProvider(zoneId);
            return provider.Insert(dicStarskilltipsEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicStarskilltipsEntity dicStarskilltipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicStarskilltipsProvider(zoneId);
            return provider.Update(dicStarskilltipsEntity,trans);
        }
		
		#endregion	
		
		
	}
}

