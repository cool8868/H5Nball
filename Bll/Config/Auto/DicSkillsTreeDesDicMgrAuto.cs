
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
    /// DicSkillstreedesdic管理类
    /// </summary>
    public static partial class DicSkillstreedesdicMgr
    {
        
		#region  GetById
		
        public static DicSkillstreedesdicEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicSkillstreedesdicProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicSkillstreedesdicEntity> GetAll(string zoneId="")
        {
            var provider = new DicSkillstreedesdicProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicSkillstreedesdicProvider provider = new DicSkillstreedesdicProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicSkillstreedesdicEntity dicSkillstreedesdicEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillstreedesdicProvider(zoneId);
            return provider.Insert(dicSkillstreedesdicEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicSkillstreedesdicEntity dicSkillstreedesdicEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillstreedesdicProvider(zoneId);
            return provider.Update(dicSkillstreedesdicEntity,trans);
        }
		
		#endregion	
		
		
	}
}

