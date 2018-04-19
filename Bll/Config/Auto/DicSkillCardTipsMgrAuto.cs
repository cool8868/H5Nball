
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
    /// DicSkillcardtips管理类
    /// </summary>
    public static partial class DicSkillcardtipsMgr
    {
        
		#region  GetById
		
        public static DicSkillcardtipsEntity GetById( System.String skillCode,string zoneId="")
        {
            var provider = new DicSkillcardtipsProvider(zoneId);
            return provider.GetById( skillCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicSkillcardtipsEntity> GetAll(string zoneId="")
        {
            var provider = new DicSkillcardtipsProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
        
		#region Insert

        public static bool Insert(DicSkillcardtipsEntity dicSkillcardtipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillcardtipsProvider(zoneId);
            return provider.Insert(dicSkillcardtipsEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicSkillcardtipsEntity dicSkillcardtipsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillcardtipsProvider(zoneId);
            return provider.Update(dicSkillcardtipsEntity,trans);
        }
		
		#endregion	
		
		
	}
}

