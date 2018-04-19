
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
    /// NbManagervippackage管理类
    /// </summary>
    public static partial class NbManagervippackageMgr
    {
        
		#region  GetById
		
        public static NbManagervippackageEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new NbManagervippackageProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetRecord
		
        public static NbManagervippackageEntity GetRecord( System.Guid managerId, System.Int32 packageLevel,string zoneId="")
        {
            var provider = new NbManagervippackageProvider(zoneId);
            return provider.GetRecord( managerId, packageLevel);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagervippackageEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagervippackageProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByManagerId
		
        public static List<NbManagervippackageEntity> GetByManagerId( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagervippackageProvider(zoneId);
            return provider.GetByManagerId( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            NbManagervippackageProvider provider = new NbManagervippackageProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  InsertRecord
		
        public static bool InsertRecord ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            NbManagervippackageProvider provider = new NbManagervippackageProvider(zoneId);

            return provider.InsertRecord( managerId,trans);
            
        }
		
		#endregion
        
		#region  DayUpdate
		
        public static bool DayUpdate (DbTransaction trans=null,string zoneId="")
        {
            NbManagervippackageProvider provider = new NbManagervippackageProvider(zoneId);

            return provider.DayUpdate(trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagervippackageEntity nbManagervippackageEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagervippackageProvider(zoneId);
            return provider.Insert(nbManagervippackageEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagervippackageEntity nbManagervippackageEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagervippackageProvider(zoneId);
            return provider.Update(nbManagervippackageEntity,trans);
        }
		
		#endregion	
		
		
	}
}
