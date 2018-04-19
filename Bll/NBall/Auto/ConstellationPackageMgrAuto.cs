
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
    /// ConstellationPackage管理类
    /// </summary>
    public static partial class ConstellationPackageMgr
    {
        
		#region  GetById
		
        public static ConstellationPackageEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new ConstellationPackageProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConstellationPackageEntity> GetAll(string zoneId="")
        {
            var provider = new ConstellationPackageProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            ConstellationPackageProvider provider = new ConstellationPackageProvider(zoneId);

            return provider.Delete( managerId, rowVersion,trans);
            
        }
		
		#endregion
        
		#region  Update
		
        public static bool Update ( System.Guid managerId, System.Byte[] itemString, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            ConstellationPackageProvider provider = new ConstellationPackageProvider(zoneId);

            return provider.Update( managerId, itemString, rowVersion,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConstellationPackageEntity constellationPackageEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConstellationPackageProvider(zoneId);
            return provider.Insert(constellationPackageEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConstellationPackageEntity constellationPackageEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConstellationPackageProvider(zoneId);
            return provider.Update(constellationPackageEntity,trans);
        }
		
		#endregion	
		
		
	}
}

