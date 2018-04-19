
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
    /// NbUserreg管理类
    /// </summary>
    public static partial class NbUserregMgr
    {
        
		#region  GetById
		
        public static NbUserregEntity GetById( System.String account,string zoneId="")
        {
            var provider = new NbUserregProvider(zoneId);
            return provider.GetById( account);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbUserregEntity> GetAll(string zoneId="")
        {
            var provider = new NbUserregProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String account,DbTransaction trans=null,string zoneId="")
        {
            NbUserregProvider provider = new NbUserregProvider(zoneId);

            return provider.Delete( account,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbUserregEntity nbUserregEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbUserregProvider(zoneId);
            return provider.Insert(nbUserregEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbUserregEntity nbUserregEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbUserregProvider(zoneId);
            return provider.Update(nbUserregEntity,trans);
        }
		
		#endregion	
		
		
	}
}
