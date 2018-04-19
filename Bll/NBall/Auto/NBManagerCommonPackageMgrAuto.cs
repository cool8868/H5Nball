
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
    /// NbManagercommonpackage管理类
    /// </summary>
    public static partial class NbManagercommonpackageMgr
    {
        
		#region  GetById
		
        public static NbManagercommonpackageEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new NbManagercommonpackageProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  Select
		
        public static NbManagercommonpackageEntity Select( System.Guid idx,string zoneId="")
        {
            var provider = new NbManagercommonpackageProvider(zoneId);
            return provider.Select( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagercommonpackageEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagercommonpackageProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            NbManagercommonpackageProvider provider = new NbManagercommonpackageProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagercommonpackageEntity nbManagercommonpackageEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagercommonpackageProvider(zoneId);
            return provider.Insert(nbManagercommonpackageEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagercommonpackageEntity nbManagercommonpackageEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagercommonpackageProvider(zoneId);
            return provider.Update(nbManagercommonpackageEntity,trans);
        }
		
		#endregion	
		
		
	}
}
