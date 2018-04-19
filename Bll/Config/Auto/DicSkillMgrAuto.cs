
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
    /// DicSkill管理类
    /// </summary>
    public static partial class DicSkillMgr
    {
        
		#region  GetById
		
        public static DicSkillEntity GetById( System.String skillCode, System.Int32 skillLevel,string zoneId="")
        {
            var provider = new DicSkillProvider(zoneId);
            return provider.GetById( skillCode, skillLevel);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicSkillEntity> GetAll(string zoneId="")
        {
            var provider = new DicSkillProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String skillCode, System.Int32 skillLevel,DbTransaction trans=null,string zoneId="")
        {
            DicSkillProvider provider = new DicSkillProvider(zoneId);

            return provider.Delete( skillCode, skillLevel,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicSkillEntity dicSkillEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillProvider(zoneId);
            return provider.Insert(dicSkillEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicSkillEntity dicSkillEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillProvider(zoneId);
            return provider.Update(dicSkillEntity,trans);
        }
		
		#endregion	
		
		
	}
}

