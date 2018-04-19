
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
    /// DicStarskillleveltips管理类
    /// </summary>
    public static partial class DicStarskillleveltipsMgr
    {
        
		#region  GetById
		
        public static DicStarskillleveltipsEntity GetById( System.Int32 skillId,string zoneId="")
        {
            var provider = new DicStarskillleveltipsProvider(zoneId);
            return provider.GetById( skillId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicStarskillleveltipsEntity> GetAll(string zoneId="")
        {
            var provider = new DicStarskillleveltipsProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
        
		#region Insert

        public static bool Insert(DicStarskillleveltipsEntity dicStarskillleveltipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicStarskillleveltipsProvider(zoneId);
            return provider.Insert(dicStarskillleveltipsEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicStarskillleveltipsEntity dicStarskillleveltipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicStarskillleveltipsProvider(zoneId);
            return provider.Update(dicStarskillleveltipsEntity,trans);
        }
		
		#endregion	
		
		
	}
}

