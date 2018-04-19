
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
    /// DicNewplayerpack管理类
    /// </summary>
    public static partial class DicNewplayerpackMgr
    {
        
		#region  GetById
		
        public static DicNewplayerpackEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DicNewplayerpackProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DicNewplayerpackEntity> GetAll(string zoneId="")
        {
            var provider = new DicNewplayerpackProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<DicNewplayerpackEntity> GetAllForCache(string zoneId="")
        {
            var provider = new DicNewplayerpackProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            DicNewplayerpackProvider provider = new DicNewplayerpackProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DicNewplayerpackEntity dicNewplayerpackEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicNewplayerpackProvider(zoneId);
            return provider.Insert(dicNewplayerpackEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DicNewplayerpackEntity dicNewplayerpackEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DicNewplayerpackProvider(zoneId);
            return provider.Update(dicNewplayerpackEntity,trans);
        }
		
		#endregion	
		
		
	}
}

