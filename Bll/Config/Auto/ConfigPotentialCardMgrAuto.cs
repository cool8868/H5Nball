
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
    /// ConfigPotentialcard管理类
    /// </summary>
    public static partial class ConfigPotentialcardMgr
    {
        
		#region  GetById
		
        public static ConfigPotentialcardEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigPotentialcardProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigPotentialcardEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigPotentialcardProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigPotentialcardProvider provider = new ConfigPotentialcardProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigPotentialcardEntity configPotentialcardEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPotentialcardProvider(zoneId);
            return provider.Insert(configPotentialcardEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigPotentialcardEntity configPotentialcardEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPotentialcardProvider(zoneId);
            return provider.Update(configPotentialcardEntity,trans);
        }
		
		#endregion	
		
		
	}
}
