
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
    /// GambleOption管理类
    /// </summary>
    public static partial class GambleOptionMgr
    {
        
		#region  GetById
		
        public static GambleOptionEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new GambleOptionProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByTitleId
		
        public static List<GambleOptionEntity> GetByTitleId( System.Guid titleId,string zoneId="")
        {
            var provider = new GambleOptionProvider(zoneId);
            return provider.GetByTitleId( titleId);            
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<GambleOptionEntity> GetAll(string zoneId="")
        {
            var provider = new GambleOptionProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            GambleOptionProvider provider = new GambleOptionProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(GambleOptionEntity gambleOptionEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleOptionProvider(zoneId);
            return provider.Insert(gambleOptionEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(GambleOptionEntity gambleOptionEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleOptionProvider(zoneId);
            return provider.Update(gambleOptionEntity,trans);
        }
		
		#endregion	
		
		
	}
}
