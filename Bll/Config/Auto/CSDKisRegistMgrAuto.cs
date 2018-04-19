
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
    /// Csdkisregist管理类
    /// </summary>
    public static partial class CsdkisregistMgr
    {
        
		#region  GetById
		
        public static CsdkisregistEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new CsdkisregistProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CsdkisregistEntity> GetAll(string zoneId="")
        {
            var provider = new CsdkisregistProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            CsdkisregistProvider provider = new CsdkisregistProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CsdkisregistEntity csdkisregistEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CsdkisregistProvider(zoneId);
            return provider.Insert(csdkisregistEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CsdkisregistEntity csdkisregistEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CsdkisregistProvider(zoneId);
            return provider.Update(csdkisregistEntity,trans);
        }
		
		#endregion	
		
		
	}
}
