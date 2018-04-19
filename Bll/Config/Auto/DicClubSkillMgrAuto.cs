
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
    /// DicClubskill管理类
    /// </summary>
    public static partial class DicClubskillMgr
    {
        
		#region  GetById
		
        public static DicClubskillEntity GetById( System.Int32 skillId,string zoneId="")
        {
            var provider = new DicClubskillProvider(zoneId);
            return provider.GetById( skillId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicClubskillEntity> GetAll(string zoneId="")
        {
            var provider = new DicClubskillProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 skillId,DbTransaction trans=null,string zoneId="")
        {
            DicClubskillProvider provider = new DicClubskillProvider(zoneId);

            return provider.Delete( skillId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicClubskillEntity dicClubskillEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicClubskillProvider(zoneId);
            return provider.Insert(dicClubskillEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicClubskillEntity dicClubskillEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicClubskillProvider(zoneId);
            return provider.Update(dicClubskillEntity,trans);
        }
		
		#endregion	
		
		
	}
}

