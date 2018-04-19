
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
    /// A8csdk管理类
    /// </summary>
    public static partial class A8csdkMgr
    {
        
		#region  GetById
		
        public static A8csdkEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new A8csdkProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<A8csdkEntity> GetAll(string zoneId="")
        {
            var provider = new A8csdkProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByGameOrderId
		
        public static List<A8csdkEntity> GetByGameOrderId( System.String gameOrderId,string zoneId="")
        {
            var provider = new A8csdkProvider(zoneId);
            return provider.GetByGameOrderId( gameOrderId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            A8csdkProvider provider = new A8csdkProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(A8csdkEntity a8csdkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new A8csdkProvider(zoneId);
            return provider.Insert(a8csdkEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(A8csdkEntity a8csdkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new A8csdkProvider(zoneId);
            return provider.Update(a8csdkEntity,trans);
        }
		
		#endregion	
		
		
	}
}
