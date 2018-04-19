
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
    /// GambleHostoptionrate管理类
    /// </summary>
    public static partial class GambleHostoptionrateMgr
    {
        
		#region  GetById
		
        public static GambleHostoptionrateEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new GambleHostoptionrateProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByHostId
		
        public static List<GambleHostoptionrateEntity> GetByHostId( System.Int32 hostId,string zoneId="")
        {
            var provider = new GambleHostoptionrateProvider(zoneId);
            return provider.GetByHostId( hostId);            
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<GambleHostoptionrateEntity> GetAll(string zoneId="")
        {
            var provider = new GambleHostoptionrateProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            GambleHostoptionrateProvider provider = new GambleHostoptionrateProvider(zoneId);

            return provider.Delete( idx, rowVersion,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(GambleHostoptionrateEntity gambleHostoptionrateEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleHostoptionrateProvider(zoneId);
            return provider.Insert(gambleHostoptionrateEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(GambleHostoptionrateEntity gambleHostoptionrateEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleHostoptionrateProvider(zoneId);
            return provider.Update(gambleHostoptionrateEntity,trans);
        }
		
		#endregion	
		
		
	}
}
