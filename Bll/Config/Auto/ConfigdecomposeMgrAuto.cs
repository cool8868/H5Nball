
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
    /// ConfigDecompose管理类
    /// </summary>
    public static partial class ConfigDecomposeMgr
    {
        
		#region  GetById
		
        public static ConfigDecomposeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigDecomposeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigDecomposeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigDecomposeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigDecomposeProvider provider = new ConfigDecomposeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigDecomposeEntity configDecomposeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigDecomposeProvider(zoneId);
            return provider.Insert(configDecomposeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigDecomposeEntity configDecomposeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigDecomposeProvider(zoneId);
            return provider.Update(configDecomposeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

