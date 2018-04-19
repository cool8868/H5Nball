
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
    /// DicManagerwilltips管理类
    /// </summary>
    public static partial class DicManagerwilltipsMgr
    {
        
		#region  GetById
		
        public static DicManagerwilltipsEntity GetById( System.String skillCode,string zoneId="")
        {
            var provider = new DicManagerwilltipsProvider(zoneId);
            return provider.GetById( skillCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicManagerwilltipsEntity> GetAll(string zoneId="")
        {
            var provider = new DicManagerwilltipsProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
        
		#region Insert

        public static bool Insert(DicManagerwilltipsEntity dicManagerwilltipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicManagerwilltipsProvider(zoneId);
            return provider.Insert(dicManagerwilltipsEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicManagerwilltipsEntity dicManagerwilltipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicManagerwilltipsProvider(zoneId);
            return provider.Update(dicManagerwilltipsEntity,trans);
        }
		
		#endregion	
		
		
	}
}

