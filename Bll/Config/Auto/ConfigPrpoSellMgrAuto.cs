
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
    /// ConfigPrposell管理类
    /// </summary>
    public static partial class ConfigPrposellMgr
    {
        
		#region  GetById
		
        public static ConfigPrposellEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigPrposellProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigPrposellEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigPrposellProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigPrposellProvider provider = new ConfigPrposellProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigPrposellEntity configPrposellEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPrposellProvider(zoneId);
            return provider.Insert(configPrposellEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigPrposellEntity configPrposellEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPrposellProvider(zoneId);
            return provider.Update(configPrposellEntity,trans);
        }
		
		#endregion	
		
		
	}
}

