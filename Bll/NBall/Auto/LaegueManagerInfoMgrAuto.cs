
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
    /// LaegueManagerinfo管理类
    /// </summary>
    public static partial class LaegueManagerinfoMgr
    {
        
		#region  GetById
		
        public static LaegueManagerinfoEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new LaegueManagerinfoProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LaegueManagerinfoEntity> GetAll(string zoneId="")
        {
            var provider = new LaegueManagerinfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            LaegueManagerinfoProvider provider = new LaegueManagerinfoProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
		#region  InsertManager
		
        public static bool InsertManager ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            LaegueManagerinfoProvider provider = new LaegueManagerinfoProvider(zoneId);

            return provider.InsertManager( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LaegueManagerinfoEntity laegueManagerinfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LaegueManagerinfoProvider(zoneId);
            return provider.Insert(laegueManagerinfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LaegueManagerinfoEntity laegueManagerinfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LaegueManagerinfoProvider(zoneId);
            return provider.Update(laegueManagerinfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}

