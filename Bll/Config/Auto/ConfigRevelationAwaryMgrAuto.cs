
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
    /// ConfigRevelationawary管理类
    /// </summary>
    public static partial class ConfigRevelationawaryMgr
    {
        
		#region  GetById
		
        public static ConfigRevelationawaryEntity GetById( System.Int32 itemCore,string zoneId="")
        {
            var provider = new ConfigRevelationawaryProvider(zoneId);
            return provider.GetById( itemCore);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigRevelationawaryEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigRevelationawaryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 itemCore,DbTransaction trans=null,string zoneId="")
        {
            ConfigRevelationawaryProvider provider = new ConfigRevelationawaryProvider(zoneId);

            return provider.Delete( itemCore,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigRevelationawaryEntity configRevelationawaryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationawaryProvider(zoneId);
            return provider.Insert(configRevelationawaryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigRevelationawaryEntity configRevelationawaryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationawaryProvider(zoneId);
            return provider.Update(configRevelationawaryEntity,trans);
        }
		
		#endregion	
		
		
	}
}

