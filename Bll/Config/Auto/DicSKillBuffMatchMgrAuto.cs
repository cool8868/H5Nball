
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
    /// DicSkillbuffmatch管理类
    /// </summary>
    public static partial class DicSkillbuffmatchMgr
    {
        
		#region  GetById
		
        public static DicSkillbuffmatchEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicSkillbuffmatchProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicSkillbuffmatchEntity> GetAll(string zoneId="")
        {
            var provider = new DicSkillbuffmatchProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicSkillbuffmatchEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicSkillbuffmatchProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicSkillbuffmatchProvider provider = new DicSkillbuffmatchProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicSkillbuffmatchEntity dicSkillbuffmatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillbuffmatchProvider(zoneId);
            return provider.Insert(dicSkillbuffmatchEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicSkillbuffmatchEntity dicSkillbuffmatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicSkillbuffmatchProvider(zoneId);
            return provider.Update(dicSkillbuffmatchEntity,trans);
        }
		
		#endregion	
		
		
	}
}

