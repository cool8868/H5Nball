
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
    /// A8csdkStartgame管理类
    /// </summary>
    public static partial class A8csdkStartgameMgr
    {
        
		#region  GetById
		
        public static A8csdkStartgameEntity GetById( System.String openId,string zoneId="")
        {
            var provider = new A8csdkStartgameProvider(zoneId);
            return provider.GetById( openId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<A8csdkStartgameEntity> GetAll(string zoneId="")
        {
            var provider = new A8csdkStartgameProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String openId,DbTransaction trans=null,string zoneId="")
        {
            A8csdkStartgameProvider provider = new A8csdkStartgameProvider(zoneId);

            return provider.Delete( openId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(A8csdkStartgameEntity a8csdkStartgameEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new A8csdkStartgameProvider(zoneId);
            return provider.Insert(a8csdkStartgameEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(A8csdkStartgameEntity a8csdkStartgameEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new A8csdkStartgameProvider(zoneId);
            return provider.Update(a8csdkStartgameEntity,trans);
        }
		
		#endregion	
		
		
	}
}
