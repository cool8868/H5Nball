
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
    /// ConfigRevelationthedifficulty管理类
    /// </summary>
    public static partial class ConfigRevelationthedifficultyMgr
    {
        
		#region  GetById
		
        public static ConfigRevelationthedifficultyEntity GetById( System.Int32 mark, System.Int32 smallClearance,string zoneId="")
        {
            var provider = new ConfigRevelationthedifficultyProvider(zoneId);
            return provider.GetById( mark, smallClearance);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigRevelationthedifficultyEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigRevelationthedifficultyProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 mark, System.Int32 smallClearance,DbTransaction trans=null,string zoneId="")
        {
            ConfigRevelationthedifficultyProvider provider = new ConfigRevelationthedifficultyProvider(zoneId);

            return provider.Delete( mark, smallClearance,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigRevelationthedifficultyEntity configRevelationthedifficultyEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationthedifficultyProvider(zoneId);
            return provider.Insert(configRevelationthedifficultyEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigRevelationthedifficultyEntity configRevelationthedifficultyEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationthedifficultyProvider(zoneId);
            return provider.Update(configRevelationthedifficultyEntity,trans);
        }
		
		#endregion	
		
		
	}
}

