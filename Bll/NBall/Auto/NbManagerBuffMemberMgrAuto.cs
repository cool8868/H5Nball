
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
    /// NbManagerbuffmember管理类
    /// </summary>
    public static partial class NbManagerbuffmemberMgr
    {
        
		#region  GetById
		
        public static NbManagerbuffmemberEntity GetById( System.Int64 id,string zoneId="")
        {
            var provider = new NbManagerbuffmemberProvider(zoneId);
            return provider.GetById( id);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagerbuffmemberEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagerbuffmemberProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
        
		#region Insert

        public static bool Insert(NbManagerbuffmemberEntity nbManagerbuffmemberEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerbuffmemberProvider(zoneId);
            return provider.Insert(nbManagerbuffmemberEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagerbuffmemberEntity nbManagerbuffmemberEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerbuffmemberProvider(zoneId);
            return provider.Update(nbManagerbuffmemberEntity,trans);
        }
		
		#endregion	
		
		
	}
}

