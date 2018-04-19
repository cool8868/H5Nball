
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
    /// A8csdkState管理类
    /// </summary>
    public static partial class A8csdkStateMgr
    {
        
		#region  GetById
		
        public static A8csdkStateEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new A8csdkStateProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<A8csdkStateEntity> GetAll(string zoneId="")
        {
            var provider = new A8csdkStateProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            A8csdkStateProvider provider = new A8csdkStateProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  UpdateByOrderId
		
        public static bool UpdateByOrderId ( System.String gameOrderId, System.String orderState,DbTransaction trans=null,string zoneId="")
        {
            A8csdkStateProvider provider = new A8csdkStateProvider(zoneId);

            return provider.UpdateByOrderId( gameOrderId, orderState,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(A8csdkStateEntity a8csdkStateEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new A8csdkStateProvider(zoneId);
            return provider.Insert(a8csdkStateEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(A8csdkStateEntity a8csdkStateEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new A8csdkStateProvider(zoneId);
            return provider.Update(a8csdkStateEntity,trans);
        }
		
		#endregion	
		
		
	}
}
