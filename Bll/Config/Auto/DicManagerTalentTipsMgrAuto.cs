
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
    /// DicManagertalenttips管理类
    /// </summary>
    public static partial class DicManagertalenttipsMgr
    {
        
		#region  GetById
		
        public static DicManagertalenttipsEntity GetById( System.String skillCode,string zoneId="")
        {
            var provider = new DicManagertalenttipsProvider(zoneId);
            return provider.GetById( skillCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicManagertalenttipsEntity> GetAll(string zoneId="")
        {
            var provider = new DicManagertalenttipsProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
        
		#region Insert

        public static bool Insert(DicManagertalenttipsEntity dicManagertalenttipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicManagertalenttipsProvider(zoneId);
            return provider.Insert(dicManagertalenttipsEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicManagertalenttipsEntity dicManagertalenttipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicManagertalenttipsProvider(zoneId);
            return provider.Update(dicManagertalenttipsEntity,trans);
        }
		
		#endregion	
		
		
	}
}

