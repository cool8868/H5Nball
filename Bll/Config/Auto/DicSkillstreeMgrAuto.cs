
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
    /// DicSkillstree管理类
    /// </summary>
    public static partial class DicSkillstreeMgr
    {
        
		#region  GetById
		
        public static DicSkillstreeEntity GetById( System.String skillCode,string zoneId="")
        {
            var provider = new DicSkillstreeProvider(zoneId);
            return provider.GetById( skillCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicSkillstreeEntity> GetAll(string zoneId="")
        {
            var provider = new DicSkillstreeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String skillCode,DbTransaction trans=null,string zoneId="")
        {
            DicSkillstreeProvider provider = new DicSkillstreeProvider(zoneId);

            return provider.Delete( skillCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicSkillstreeEntity dicSkillstreeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillstreeProvider(zoneId);
            return provider.Insert(dicSkillstreeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicSkillstreeEntity dicSkillstreeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillstreeProvider(zoneId);
            return provider.Update(dicSkillstreeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

