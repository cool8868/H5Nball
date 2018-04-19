
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
    /// ConfigReveationcheckawary管理类
    /// </summary>
    public static partial class ConfigReveationcheckawaryMgr
    {
        
		#region  GetById
		
        public static ConfigReveationcheckawaryEntity GetById( System.Int32 mark, System.Int32 littleLevels,string zoneId="")
        {
            var provider = new ConfigReveationcheckawaryProvider(zoneId);
            return provider.GetById( mark, littleLevels);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigReveationcheckawaryEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigReveationcheckawaryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 mark, System.Int32 littleLevels,DbTransaction trans=null,string zoneId="")
        {
            ConfigReveationcheckawaryProvider provider = new ConfigReveationcheckawaryProvider(zoneId);

            return provider.Delete( mark, littleLevels,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigReveationcheckawaryEntity configReveationcheckawaryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigReveationcheckawaryProvider(zoneId);
            return provider.Insert(configReveationcheckawaryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigReveationcheckawaryEntity configReveationcheckawaryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigReveationcheckawaryProvider(zoneId);
            return provider.Update(configReveationcheckawaryEntity,trans);
        }
		
		#endregion	
		
		
	}
}

