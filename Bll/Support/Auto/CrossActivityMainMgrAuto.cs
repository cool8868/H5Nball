
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
    /// CrossactivityMain管理类
    /// </summary>
    public static partial class CrossactivityMainMgr
    {
        
		#region  GetById
		
        public static CrossactivityMainEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new CrossactivityMainProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetActivityInfo
		
        public static CrossactivityMainEntity GetActivityInfo( System.Int32 domainId,string zoneId="")
        {
            var provider = new CrossactivityMainProvider(zoneId);
            return provider.GetActivityInfo( domainId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrossactivityMainEntity> GetAll(string zoneId="")
        {
            var provider = new CrossactivityMainProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            CrossactivityMainProvider provider = new CrossactivityMainProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrossactivityMainEntity crossactivityMainEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossactivityMainProvider(zoneId);
            return provider.Insert(crossactivityMainEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrossactivityMainEntity crossactivityMainEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossactivityMainProvider(zoneId);
            return provider.Update(crossactivityMainEntity,trans);
        }
		
		#endregion	
		
		
	}
}
