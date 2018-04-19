
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
    /// DicPlayer管理类
    /// </summary>
    public static partial class DicPlayerMgr
    {
        
		#region  GetById
		
        public static DicPlayerEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicPlayerProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicPlayerEntity> GetAll(string zoneId="")
        {
            var provider = new DicPlayerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicPlayerEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicPlayerProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicPlayerProvider provider = new DicPlayerProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicPlayerEntity dicPlayerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicPlayerProvider(zoneId);
            return provider.Insert(dicPlayerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicPlayerEntity dicPlayerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicPlayerProvider(zoneId);
            return provider.Update(dicPlayerEntity,trans);
        }
		
		#endregion	
		
		
	}
}

