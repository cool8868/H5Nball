
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
    /// ConfigMallextra管理类
    /// </summary>
    public static partial class ConfigMallextraMgr
    {
        
		#region  GetById
		
        public static ConfigMallextraEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigMallextraProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigMallextraEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigMallextraProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigMallextraProvider provider = new ConfigMallextraProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigMallextraEntity configMallextraEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigMallextraProvider(zoneId);
            return provider.Insert(configMallextraEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigMallextraEntity configMallextraEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigMallextraProvider(zoneId);
            return provider.Update(configMallextraEntity,trans);
        }
		
		#endregion	
		
		
	}
}

