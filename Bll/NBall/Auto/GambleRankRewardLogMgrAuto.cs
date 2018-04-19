
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
    /// GambleRankrewardlog管理类
    /// </summary>
    public static partial class GambleRankrewardlogMgr
    {
        
		#region  GetLastestOne
		
        public static GambleRankrewardlogEntity GetLastestOne(string zoneId="")
        {
            var provider = new GambleRankrewardlogProvider(zoneId);
            return provider.GetLastestOne();
        }
		
		#endregion		  
		
		#region  GetById
		
        public static GambleRankrewardlogEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new GambleRankrewardlogProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<GambleRankrewardlogEntity> GetAll(string zoneId="")
        {
            var provider = new GambleRankrewardlogProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  AddNewOne
		
        public static bool AddNewOne (DbTransaction trans=null,string zoneId="")
        {
            GambleRankrewardlogProvider provider = new GambleRankrewardlogProvider(zoneId);

            return provider.AddNewOne(trans);
            
        }
		
		#endregion
        
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            GambleRankrewardlogProvider provider = new GambleRankrewardlogProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(GambleRankrewardlogEntity gambleRankrewardlogEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleRankrewardlogProvider(zoneId);
            return provider.Insert(gambleRankrewardlogEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(GambleRankrewardlogEntity gambleRankrewardlogEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleRankrewardlogProvider(zoneId);
            return provider.Update(gambleRankrewardlogEntity,trans);
        }
		
		#endregion	
		
		
	}
}
