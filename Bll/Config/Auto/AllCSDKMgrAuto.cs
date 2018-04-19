
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
    /// AllCsdk管理类
    /// </summary>
    public static partial class AllCsdkMgr
    {
        
		#region  GetById
		
        public static AllCsdkEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new AllCsdkProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<AllCsdkEntity> GetAll(string zoneId="")
        {
            var provider = new AllCsdkProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
        
		#region Insert

        public static bool Insert(AllCsdkEntity allCsdkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllCsdkProvider(zoneId);
            return provider.Insert(allCsdkEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(AllCsdkEntity allCsdkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllCsdkProvider(zoneId);
            return provider.Update(allCsdkEntity,trans);
        }
		
		#endregion	
		
		
	}
}
