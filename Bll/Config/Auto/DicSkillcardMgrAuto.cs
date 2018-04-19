
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
    /// DicSkillcard管理类
    /// </summary>
    public static partial class DicSkillcardMgr
    {
        
		#region  GetById
		
        public static DicSkillcardEntity GetById( System.String skillCode,string zoneId="")
        {
            var provider = new DicSkillcardProvider(zoneId);
            return provider.GetById( skillCode);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicSkillcardEntity> GetAll(string zoneId="")
        {
            var provider = new DicSkillcardProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicSkillcardEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicSkillcardProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String skillCode,DbTransaction trans=null,string zoneId="")
        {
            DicSkillcardProvider provider = new DicSkillcardProvider(zoneId);

            return provider.Delete( skillCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicSkillcardEntity dicSkillcardEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillcardProvider(zoneId);
            return provider.Insert(dicSkillcardEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicSkillcardEntity dicSkillcardEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillcardProvider(zoneId);
            return provider.Update(dicSkillcardEntity,trans);
        }
		
		#endregion	
		
		
	}
}

