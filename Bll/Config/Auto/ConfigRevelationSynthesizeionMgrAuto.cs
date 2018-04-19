
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
    /// ConfigRevelationsynthesizeion管理类
    /// </summary>
    public static partial class ConfigRevelationsynthesizeionMgr
    {
        
		#region  GetById
		
        public static ConfigRevelationsynthesizeionEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigRevelationsynthesizeionProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigRevelationsynthesizeionEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigRevelationsynthesizeionProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigRevelationsynthesizeionProvider provider = new ConfigRevelationsynthesizeionProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigRevelationsynthesizeionEntity configRevelationsynthesizeionEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationsynthesizeionProvider(zoneId);
            return provider.Insert(configRevelationsynthesizeionEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigRevelationsynthesizeionEntity configRevelationsynthesizeionEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationsynthesizeionProvider(zoneId);
            return provider.Update(configRevelationsynthesizeionEntity,trans);
        }
		
		#endregion	
		
		
	}
}

